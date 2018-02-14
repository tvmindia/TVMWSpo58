var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var _Products = [];
var _Units = [];
var _ProformaProductDetail = [];
var _ProformaProductList = [];
$(document).ready(function () {
    try {
        //For implementating select2
        $("#ddlCustomer").select2({
        });
        $("#ddlProductSearch").select2({ dropdownParent: $("#AddProformaItemModal") });
        $('#btnUpload').click(function () {
            //Pass the controller name
            var FileObject = new Object;
            if ($('#hdnFileDupID').val() != emptyGUID) {
                FileObject.ParentID = (($('#ID').val()) != emptyGUID ? ($('#ID').val()) : $('#hdnFileDupID').val());
            }
            else {
                FileObject.ParentID = ($('#ID').val() == emptyGUID) ? "" : $('#ID').val();
            }
           

            FileObject.ParentType = "ProformaInvoice";
            FileObject.Controller = "FileUpload";
            UploadFile(FileObject);
        });
        DataTables.ProformaInvoiceTable = $('#ProformaInvoiceTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllProformaInvoices(),
             pageLength: 15,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "ID" },
               { "data": "InvoiceDate", "defaultContent": "<i>-</i>" },
               { "data": "InvoiceNo", "defaultContent": "<i>-</i>" },     
               { "data": "customer.CustomerName", "defaultContent": "<i>-</i>" },
               { "data": "Subject", "defaultContent": "<i>-</i>" },
               { "data": "ValidTillDate", "defaultContent": "<i>-</i>" },
               { "data": "Total", render: function (data, type, row) { return roundoff(data) } ,"defaultContent": "<i>-</i>" },
               { "data": "Discount",render: function (data, type, row) { return roundoff(data) } ,"defaultContent": "<i>-</i>" },
               { "data": "TaxAmount", render: function (data, type, row) { return roundoff(data) }, "defaultContent": "<i>-</i>" },
               {
                   "data": "TotalAmount", render: function (data, type, row) { 
                       return roundoff((row.Total - row.Discount) + row.TaxAmount);
                   }, "defaultContent": "<i>-</i>"
               },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                   { className: "text-right", "targets": [6,7,8] },
                   { className: "text-left", "targets": [2,4] },
                   { className: "text-center", "targets": [1,3,5] }

             ]
         });

        $('#ProformaInvoiceTable tbody').on('dblclick', 'td', function () {
            Edit(this);
        });
        //This will select the content of the textbox programatically
        $('input[type="text"].Roundoff').on('focus', function () {
            $(this).select();
        });


       
        DataTables.ItemDetailTable = $('#ItemDetailsTable').DataTable(
      {
          dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
          ordering:false,
          searching: false,
          paging: false,
          data: null,
          autoWidth: false,
          columns: [
          { "data": "ID", "defaultContent": "<i></i>" },
          { "data": "ProductID", "defaultContent": "<i></i>" },
          { "data": "ProductCode", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
          { "data": "OldProductCode", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
          { "data": "ProductDescription", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
          { "data": "UnitCode", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
          { "data": "Quantity", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
          { "data": "Rate", render: function (data, type, row) { return roundoff(data) }, "defaultContent": "<i></i>" },
          { "data": "Amount", render: function (data, type, row) { return roundoff(data) }, "defaultContent": "<i></i>" },
          { "data": null, "orderable": false, "defaultContent": '<a href="#" class="DeleteLink"  onclick="Delete(this)" ><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a> | <a href="#" class="actionLink"  onclick="ProductEdit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' },
          ],
          columnDefs: [{ "targets": [0, 1], "visible": false, searchable: false },
              { "targets": [2, 3, 5], "width": "15%" },
              { className:"text-center","targets": [9], "width": "8%" },
              { className: "text-right", "targets": [7, 8] },
              {className:"text-left","targets":[2,3,4,5,6]}
          ]
      });

        GetAllProductCodes();
        GetAllUnitCodes();
        $('#btnSendDownload').hide();

    }
    catch (x) {
        //this will show the error msg in the browser console(F12) 
        console.log(x.message);
    }
    
});


//---------------Bind logics-------------------
function GetAllProformaInvoices(filter) {
    try {
        debugger;
        var data = { "filter": filter };
        var ds = {};
        ds = GetDataFromServer("ProformaInvoice/GetAllProformaInvoices/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}


function InvoiceNoOnChange(curobj)
{
    $("#lblInvoiceNo").text($(curobj).val());
}


function BindAllQuotes() {
   try {
        DataTables.ProformaInvoiceTable.clear().rows.add(GetAllProformaInvoices()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}


function Delete(curobj) {
    debugger;
    var rowData = DataTables.ItemDetailTable.row($(curobj).parents('tr')).data();
    if ((rowData != null)&&(rowData.ID != null)) {
        notyConfirm('Are you sure to delete?', 'DeleteItem("' + rowData.ID + '")');
        AmountSummary();
    }
    else
    {
        DataTables.ItemDetailTable.row($(curobj).parents('tr')).remove().draw(false);
        notyAlert('success', 'Deleted Successfully');
        AmountSummary();
    }
}

//Delete ProformaInvoice
function DeleteClick()
{
    debugger;
    notyConfirm('Are you sure to delete?', 'DeleteProformaInvoice()');
}


function DeleteProformaInvoice()
{
    try {
        debugger;
        var id = $('#ID').val();
        if (id != '' && id != null) {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("ProformaInvoice/DeleteProformaInvoice/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                notyAlert('success', ds.Record.Message);
                debugger;
                BindAllQuotes();
                closeNav();
            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.Message);
                return 0;
            }
            return 1;
        }
    }
    catch (e) {
        notyAlert('error', e.message);
        return 0;
    }
}


function CheckAmount() {
    debugger;
    if ($("#txtDiscount").val() == "")
        $("#txtDiscount").val(roundoff(0));
}

function DeleteItem(ID) {

    try {
        debugger;

        //Event Request Case
            var data = { "ID": ID };
            var ds = {};
            ds = GetDataFromServer("ProformaInvoice/DeleteItemByID/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                switch (ds.Result) {
                    case "OK":
                        notyAlert('success', ds.Message);                       
                        GetAllQuoteItems($("#ID").val());
                        break;
                    case "ERROR":
                        notyAlert('error', ds.Message);
                        break;
                    default:
                        break;
                }
                return ds.Record;
            }
    }
    catch (e) {

        notyAlert('error', e.message);
    }
    
}

function saveInvoices() {
    debugger;
    //if (validation == "") {
        $("#DetailJSON").val('');
        _ProformaProductList = [];
        AddProformaProductList();
        if (_ProformaProductList.length > 0)
        {
            var result = JSON.stringify(_ProformaProductList);
            $("#DetailJSON").val(result);
            $('#btnSave').trigger('click');
        }
   // }
    else {
        notyAlert('warnig', 'Please Add Product Details!');
    }

    }


function ProformaSaveSuccess(data, status) {
    debugger;
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK": 
            notyAlert('success', JsonResult.Record.Message);
            ChangeButtonPatchView('ProformaInvoice', 'btnPatchAdd', 'Edit');
            if (JsonResult.Record.ID) {
                $("#ID").val(JsonResult.Record.ID);
               // $('#deleteId').val(JsonResult.Record.ID);
            }
            BindAllQuotes();
            GetAllQuoteItems($("#ID").val());
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.Message);
            break;
        default:
            notyAlert('error', JsonResult.Record.Message);
            break;
    }
}



function DeleteSuccess(data, status) {
var JsonResult = JSON.parse(data)
switch (JsonResult.DeleteInvoice) {
         case "OK":
notyAlert('success', JsonResult.Message);
break;
         case "Error":
            notyAlert('error', JsonResult.Message);
            break;
         case "ERROR":
            notyAlert('error', JsonResult.Message);//            break;
       default:
            break;
    }
}


function AddNew() {
    debugger;
    ChangeButtonPatchView('ProformaInvoice', 'btnPatchAdd', 'Add');
    Resetform();
    openNav();
    // Reset();  
    $("#ddlCustomer").select2();
    $("#ddlCustomer").val('').trigger('change');
    $('#ID').val(emptyGUID);
    $('#ProformaForm')[0].reset();
    DataTables.ItemDetailTable.clear().draw(false);
    $("#lblEmailSent").text('NO');
    $("#lblInvoiceNo").text('New ProformaInvoice');
    $("#CGST").val('9');
    $("#SGST").val('9');
}

function Reset() {
    debugger;
    BindProformaInvoiceDetails($('#ID').val());
    //GetTaxPercentage();
}

//To Reset ProformaInvoice Form
function Resetform() {
    var validator = $("#ProformaForm").validate();
    $('#ProformaForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    $('#ProformaForm')[0].reset();
    DataTables.ItemDetailTable.clear().draw(false);
}

function Edit(Obj) {
    debugger;
    $('#ProformaForm')[0].reset();
    var rowData = DataTables.ProformaInvoiceTable.row($(Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    //$('#deleteId').val(rowData.ID);
    $('#QuoteMAilHeaderID').val(rowData.ID);

    BindProformaInvoiceDetails(rowData.ID);
    AmountSummary();
    ChangeButtonPatchView('ProformaInvoice', 'btnPatchAdd', 'Edit');
    openNav();
}



function BindProformaInvoiceDetails(ID) {
    try {
        debugger;
        var jsresult = GetProformaInvoiceDetailsByID(ID)
        if (jsresult) {
            //bind
            $("#txtInvoiceNo").val(jsresult.InvoiceNo);
            $("#txtInvoiceDate").val(jsresult.InvoiceDate);
            $("#ValidTillDate").val(jsresult.ValidTillDate);
            $("#ddlCustomer").select2();
            $("#ddlCustomer").val(jsresult.CustomerID).trigger('change');
            $("#SentToAddress").val(jsresult.SentToAddress);
            $("#ContactPerson").val(jsresult.ContactPerson);            
            $("#Subject").val(jsresult.Subject);
            $("#BodyHead").val(jsresult.BodyHead);
            $("#ddlCompany").val(jsresult.company.Code);
            
            $("#GrossAmount").val(jsresult.GrossAmount);
            $("#Discount").val(roundoff(jsresult.Discount));
            $("#NetTaxableAmount").val(jsresult.NetTaxableAmount);
            
            $("#CGST").val(jsresult.CGST);
            $("#SGST").val(jsresult.SGST);
            $("#IGST").val(jsresult.IGST);

            $("#TotalAmount").val(jsresult.TotalAmount);
            $("#BodyFoot").val(jsresult.BodyFoot);       

           // $("#lblQuoteStage").text(jsresult.quoteStage.Description);
            $("#lblEmailSent").text(jsresult.EmailSentYN == "True" ? 'YES' : 'NO');
            $('#SentToEmails').val(jsresult.SentToEmails);
            $("#lblInvoiceNo").text(jsresult.InvoiceNo);
            debugger;
            GetQuoteItemsList(ID);
            AmountSummary();
            clearUploadControl();
            PaintImages(ID);
        }

    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}

function GetProformaInvoiceDetailsByID(ID) {
    try {
        debugger;
        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("ProformaInvoice/GetProformaInvoiceDetailsByID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);

        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function GetQuoteItemsList(ID) {
    DataTables.ItemDetailTable.clear().rows.add(GetAllQuoteItems(ID)).draw(false);
}

//To bind values to detail table
function GetAllQuoteItems(ID) {
    debugger;
    try {

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("ProformaInvoice/GetQuateItemsByQuateHeadID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
//To fill customer in dropdownlist
function GetCustomerDetails(curobj) {
    var customerid = $(curobj).val();
    if (customerid) {
        var data = { "ID": customerid };
        var ds = {};
        ds = GetDataFromServer("CustomerOrder/GetCustomerDetailsByID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {

            $("#SentToAddress").val(ds.Record.BillingAddress);
            $("#ContactPerson").val(ds.Record.ContactPerson);
            if( $("#SentToEmails").val()=="")
            $("#SentToEmails").val(ds.Record.ContactEmail);
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            return 0;
        }
    }

}

//To get all products in detail table
function GetAllProductCodes() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("ProformaInvoice/GetAllProductCodes/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            _Products = ds.Records;
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

//To get UnitCodes in detail table
function GetAllUnitCodes() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("ProformaInvoice/GetAllUnitCodes/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            _Units = ds.Records;
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function ValidateEmail() {
    var ste = $('#SentToEmails').val();
    if (ste) {
        var atpos = ste.indexOf("@");
        var dotpos = ste.lastIndexOf(".");
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= ste.length) {
            notyAlert('error', 'Invalid Email');
            return false;
        }
            //not valid

        else {
            $("#MailPreviewModel").modal('hide');
            showLoader();
            return true;
        }

    }

    else
        notyAlert('error', 'Enter email address');
    return false;
}

function MailSuccess(data, status) {
    debugger;
    hideLoader();
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":

            notyAlert('success', JsonResult.Message);
            switch (JsonResult.MailResult) {
                case 1:
                    $("#lblEmailSent").text('YES');
                    break;
                case 0:
                    $("#lblEmailSent").text('NO');
                    break;
            }
            Reset();
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}


function SendMailClick() {
    $('#btnFormSendMail').trigger('click');
}
function GetCustomerDeails(curobj) {
    var customerid = $(curobj).val();
    if (customerid) {
        var data = { "ID": customerid };
        var ds = {};
        ds = GetDataFromServer("CustomerOrder/GetCustomerDetailsByID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {

            $("#SentToAddress").val(ds.Record.BillingAddress);
            $("#ContactPerson").val(ds.Record.ContactPerson);
            $("#SentToEmails").val(ds.Record.ContactEmail);
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            return 0;
        }
    }

}

function PreviewMail() {
    try {
        debugger;

        var QHID = $("#ID").val();
        if (QHID) {
            //Bind mail html into model
            GetMailPreview(QHID);

            $("#MailPreviewModel").modal('show');
        }


    }
    catch (e) {
        notyAlert('error', e.Message);
    }

}
function GetMailPreview(ID) {
    debugger;
    var data = { "ID": ID };
    var ds = {};
    ds = GetDataFromServer("ProformaInvoice/GetMailPreview/", data);
    if (ds == "Nochange") {
        return; 0
    }
    debugger;
    $("#mailmodelcontent").empty();
    $("#mailmodelcontent").html(ds);
    $("#MailBody").val(ds);

}

//To trigger PDF download button
function DownloadPDF()
{
    debugger;
    $('#btnSendDownload').trigger('click');
}

//To download file in PDF
function GetHtmlData() {
    debugger;
    var bodyContent = $('#mailmodelcontent').html();
    var headerContent = $('#hdnHeadContent').html();
    $('#hdnContent').val(bodyContent);
    $('#hdnHeadContent').val(headerContent);
    
}

//-------------------------------------------------------------------------------------------------//
function AddProformaList() {
    debugger;
    PopupClearFields();
    $("#ddlProductSearch").prop('disabled', false);
    $('#AddProformaItemModal').modal('show');
}

function PopupClearFields() {
    _ProformaProductDetail = [];
    $("#ddlProductSearch").select2({ dropdownParent: $("#AddProformaItemModal") });
    $("#ddlProductSearch").val('').trigger('change');
    $('#proformaItemListObj_ProductCode').val('');
    $('#proformaItemListObj_OldProductCode').val('');
    $('#proformaItemListObj_ProductDescription').val('');
    $('#proformaItemListObj_UnitCode').val('');
    $('#proformaItemListObj_Quantity').val('');
    $('#proformaItemListObj_Rate').val('');
    $('#proformaItemListObj_Amount').val('');
}

// calculate amount W.R.T to qty
function CalculateAmount(amt)
{
    debugger;
    var qty = $('#proformaItemListObj_Quantity').val();
    var rate = $('#proformaItemListObj_Rate').val();
    var amount = roundoff(parseFloat(qty) * parseFloat(rate));
    $('#proformaItemListObj_Amount').val(amount);
}

function AmountSummary()
{
    debugger;
    var total = 0.00;
    var table = DataTables.ItemDetailTable.rows().data();
    for (i = 0; i < table.length; i++)
    {
        total = total + (parseFloat(table[i].Amount));
    }
    $('#GrossAmount').val(total);

    var discount = parseFloat($('#Discount').val());
    var nettaxableamount = total - discount;
    $('#NetTaxableAmount').val(roundoff(nettaxableamount));

    var cgst = parseFloat($('#CGST').val());
    var sgst = parseFloat($('#SGST').val());
    var igst = parseFloat($('#IGST').val());
    var taxtotal = cgst + sgst + igst;
    $('#TaxAmount').val(roundoff((nettaxableamount * taxtotal) / 100));
    var totalamt = nettaxableamount + (roundoff(nettaxableamount) * taxtotal) / 100;
    $('#TotalAmount').val(roundoff(totalamt));
}



function ProductSearchOnchange(curObj) {
    debugger;
    if (curObj.value != "") {
        _ProformaProductDetail = [];
        var ds = GetProductByID(curObj.value);
        ProformaProduct = new Object();
        ProformaProduct.ID = null;
        ProformaProduct.ProductID = ds.ID       
        ProformaProduct.ProductCode = ds.Code
        ProformaProduct.OldProductCode = ds.OldCode;
        ProformaProduct.ProductDescription = ds.Description;        
        ProformaProduct.UnitCode = ds.UnitCode;
        ProformaProduct.Quantity = ds.Quantity;
        ProformaProduct.Rate = ds.Rate;
      //  ProformaProduct.Amount = ds.Amount || 0;
        _ProformaProductDetail.push(ProformaProduct);
        $('#proformaItemListObj_ProductCode').val(_ProformaProductDetail[0].ProductCode);
        $('#proformaItemListObj_OldProductCode').val(_ProformaProductDetail[0].OldProductCode);
        $('#proformaItemListObj_ProductDescription').val(_ProformaProductDetail[0].ProductDescription);
        $('#proformaItemListObj_UnitCode').val(_ProformaProductDetail[0].UnitCode);
        $('#proformaItemListObj_Quantity').val(_ProformaProductDetail[0].Quantity);
        $('#proformaItemListObj_Rate').val(roundoff(_ProformaProductDetail[0].Rate));
       // $('#proformaItemListObj_Amount').val(roundoff(_ProformaProductDetail[0].Amount));
        
    }
}

function GetProductByID(id) {
    try {
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("Product/GetProductDetails/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}


function AddProformaItem() {
    debugger;
    if ($("#ddlProductSearch").val() != "")
    {
        if (_ProformaProductDetail != null)
        {
            //check product existing or not if soo update the new
            var allData = DataTables.ItemDetailTable.rows().data();
            if (allData.length > 0)
            {
                var checkPoint = 0;
                for (var i = 0; i < allData.length; i++) {
                    if (allData[i].ProductID == _ProformaProductDetail[0].ProductID) 
                    {                       
                        allData[i].ProductDescription = $('#proformaItemListObj_ProductDescription').val();
                        allData[i].UnitCode = $('#proformaItemLisrObj_UnitCode').val();
                        allData[i].Quantity = $('#proformaItemListObj_Quantity').val();                       
                        allData[i].Rate = $('#proformaItemListObj_Rate').val();
                        allData[i].Amount = $('#proformaItemListObj_Amount').val();
                        checkPoint = 1;
                       
                        break;
                    }
                }

                if (!checkPoint)
                {                  
                    _ProformaProductDetail[0].ProductDescription = $('#proformaItemListObj_ProductDescription').val();
                    _ProformaProductDetail[0].UnitCode = $('#proformaItemListObj_UnitCode').val();
                    _ProformaProductDetail[0].Quantity = $('#proformaItemListObj_Quantity').val();
                    _ProformaProductDetail[0].Rate = $('#proformaItemListObj_Rate').val();
                    _ProformaProductDetail[0].Amount = $('#proformaItemListObj_Amount').val();
                    DataTables.ItemDetailTable.rows.add(_ProformaProductDetail).draw(false);
                }
                else
                {
                    DataTables.ItemDetailTable.clear().rows.add(allData).draw(false);
                    
                }

            }
            else
            {
                _ProformaProductDetail[0].ProductDescription = $('#proformaItemListObj_ProductDescription').val();
                _ProformaProductDetail[0].UnitCode = $('#proformaItemListObj_UnitCode').val();
                _ProformaProductDetail[0].Quantity = $('#proformaItemListObj_Quantity').val();
                _ProformaProductDetail[0].Rate = $('#proformaItemListObj_Rate').val();
                _ProformaProductDetail[0].Amount = $('#proformaItemListObj_Amount').val();
                DataTables.ItemDetailTable.rows.add(_ProformaProductDetail).draw(false);
            }
        }        
        $('#AddProformaItemModal').modal('hide');
        //function call
        AmountSummary();
    }
    else {
        notyAlert('warning', "Product is Empty");
    }
   
}


function AddProformaProductList() {
    debugger;
    var data = DataTables.ItemDetailTable.rows().data();
    for (var r = 0; r < data.length; r++) {
        ProformaProduct = new Object();
        ProformaProduct.ID = data[r].ID;
        ProformaProduct.ProductID = data[r].ProductID;
        ProformaProduct.ProductCode = data[r].ProductCode;
        ProformaProduct.ProductDescription = data[r].ProductDescription;
        ProformaProduct.UnitCode = data[r].UnitCode;
        ProformaProduct.Quantity = data[r].Quantity;
        ProformaProduct.Rate = data[r].Rate;
        ProformaProduct.Amount = data[r].Amount;
        _ProformaProductList.push(ProformaProduct);
    }
}

function ProductEdit(curObj) {
    debugger;
    $('#AddProformaItemModal').modal('show');
    $("#ddlProductSearch").prop('disabled', true);
    PopupClearFields();
    var rowData = DataTables.ItemDetailTable.row($(curObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ProductID != null)) {
        $("#ddlProductSearch").select2({ dropdownParent: $("#AddProformaItemModal") });
        $("#ddlProductSearch").val(rowData.ProductID).trigger('change');
        $('#proformaItemListObj_ProductDescription').val(rowData.ProductDescription);
        $('#proformaItemListObj_UnitCode').val(rowData.UnitCode);
        $('#proformaItemListObj_Quantity').val(rowData.Quantity);
        $('#proformaItemListObj_Rate').val(roundoff(rowData.Rate));
        $('#proformaItemListObj_Amount').val(roundoff(rowData.Amount));

    }
}

