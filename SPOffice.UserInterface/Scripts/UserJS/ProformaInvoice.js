var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var _Products = [];
var _Units = [];
$(document).ready(function () {
    try {
        debugger;
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
               { "data": "Total", "defaultContent": "<i>-</i>" },
               { "data": "Discount", "defaultContent": "<i>-</i>" },
               { "data": "TaxAmount", "defaultContent": "<i>-</i>" },
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
          columns: EG_Columns(),
          columnDefs: EG_Columns_Settings()
      });

        GetAllProductCodes();
        GetAllUnitCodes();
        EG_ComboSource('UnitCodes', _Units, 'Code', 'Description');
        EG_ComboSource('Products', _Products, 'Code', 'Description');
     
        EG_GridDataTable = DataTables.ItemDetailTable;

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

//-----------------------EDIT GRID DEFN-------------------------------------
var EG_totalDetailRows = 0;
var EG_GridData;//DATA SOURCE OBJ ARRAY
var EG_GridDataTable;//DATA TABLE ITSELF FOR REBIND PURPOSE
var EG_SlColumn = 'SlNo';
var EG_GridInputPerRow = 4;
var EG_MandatoryFields = 'ProductCode,ProductDescription,UnitCode,Quantity,Rate';


function EG_TableDefn() {
    var tempObj = new Object();
    tempObj.ID = "";
    tempObj.InvoiceID = " ";
    tempObj.SlNo = 0;
    tempObj.ProductCode = "";
    tempObj.ProductDescription = "";
    tempObj.UnitCode = "";
    tempObj.Quantity = "";
    tempObj.Rate = "";
    tempObj.Amount = "";
    tempObj.ProductID = "";
    return tempObj
}


function EG_Columns() {
      var obj = [
                { "data": "ID", "defaultContent": "<i></i>" },
              
                { "data": "SlNo", "defaultContent": "<i></i>" },
                { "data": "ProductCode", render: function (data, type, row) { return (EG_createCombo(data, 'S', row, 'ProductCode', 'Products', 'FillDescription')); } },
                { "data": "ProductDescription", render: function (data, type, row) { return (EG_createTextBox(data, 'S', row, 'ProductDescription', '')); }, "defaultContent": "<i></i>" },
                 { "data": "UnitCode", render: function (data, type, row) { return (EG_createCombo(data, 'S', row, 'UnitCode', 'UnitCodes','')); }, "defaultContent": "<i></i>" },
                { "data": "Quantity", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Quantity', 'CalculateGridAmount')); }, "defaultContent": "<i></i>" },
               
                { "data": "Rate", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Rate', 'CalculateGridAmount')); }, "defaultContent": "<i></i>" },
                { "data": "Amount", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Amount', 'CalculateGridAmount')); }, "defaultContent": "<i></i>" },
                { "data": null, "orderable": false, "defaultContent": '<a href="#" class="DeleteLink"  onclick="Delete(this)" ><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a>' },
                { "data": "ProductID", render: function (data, type, row) { return (EG_createTextBox(data, 'S', row, 'ProductID', '')); }, "defaultContent": "<i></i>" }
                ]

      return obj;

}

function EG_Columns_Settings() {

    var obj = [
        { "targets": [0,9], "visible": false, "searchable": false },
            { "width": "5%", "targets": 1 },
        { "width": "15%", "targets": 2 },
         { "width": "20%", "targets": 3 },
           { "width": "8%", "targets": 4 },
        { "width": "8%", "targets": 5 },
         { "width": "8%", "targets": 6 },
          { "width": "10%", "targets": 7 },
           { "width": "12%", "targets": 8 },
            //{ "width": "10%", "targets": 11 },
        //{ className: "text-right", "targets": [8] },
        //{ className: "text-left disabled", "targets": [5] },
        //{ className: "text-center", "targets": [3, 4, 6, 12] },
        //{ className: "text-center disabled", "targets": [7] },
        //{ className: "text-right disabled", "targets": [9, 10, 11] },
        //{ "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11] }

    ]

    return obj;

}

function FillDescription(row) {
    
    for (i = 0; i < _Products.length; i++) {
        if (_Products[i].Code == EG_GridData[row - 1]['ProductCode']) {
            EG_GridData[row - 1]['ProductDescription'] = _Products[i].Description;
            EG_GridData[row - 1]['ProductID'] = _Products[i].ID;
            EG_GridData[row - 1]['Rate'] = _Products[i].Rate;

            //Description
            EG_Rebind();
            break;
        }
    }

}


function CalculateGridAmount(row) {
    var qty = 0.00;
    var rate = 0.00;
    var EGqty = '';
    var EGrate = '';
    EGqty = EG_GridData[row - 1]["Quantity"];
    EGrate = EG_GridData[row - 1]['Rate'];
    qty = parseFloat(EGqty) || 0;
    rate = parseFloat(EGrate) || 0;
    EG_GridData[row - 1]['Rate'] = roundoff(rate);
    EG_GridData[row - 1]['Amount'] = roundoff(qty * rate);
    EG_Rebind();

    var total = 0.00;
    for (i = 0; i < EG_GridData.length; i++) {
        total = total + (parseFloat(EG_GridData[i]['Amount']) || 0);
    }
    $('#GrossAmount').val(roundoff(total));
    AmountSummary();

}

function InvoiceNoOnChange(curobj)
{
    $("#lblInvoiceNo").text($(curobj).val());
}

function AmountSummary() {
    var total = 0.00;
    for (i = 0; i < EG_GridData.length; i++) {
        total = total + (parseFloat(EG_GridData[i]['Amount']) || 0);
    }
    $('#GrossAmount').val(roundoff(total));

   
   
    var discount = parseFloat($('#Discount').val()) || 0;
    var nettaxableamount = total - discount;
    $('#NetTaxableAmount').val(roundoff(nettaxableamount));

    var applicabletax=$("#TaxPercApplied").val();
    $('#TaxAmount').val((roundoff(nettaxableamount) * applicabletax)/100);
    var totamt = nettaxableamount + (roundoff(nettaxableamount) * applicabletax) / 100;
    $("#TotalAmount").val(totamt);  
    
}

function GetTaxPercentage() {

    try {
        var curObj = $("#TaxTypeCode").val();
        if (curObj) {
            $("#TaxPercApplied").val(0);
            var data = { "Code": curObj };
            var ds = {};
            ds = GetDataFromServer("ProformaInvoice/GetTaxRate/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {

                $("#TaxPercApplied").val(ds.Records);
                AmountSummary();
                return ds.Records;
            }
            if (ds.Result == "ERROR") {
                return 0;
            }
        }
        else {
            $("#TaxPercApplied").val(0);
        }

    }
    catch (e) {
        notyAlert('error', e.message);
    }
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
    var rowData = DataTables.ItemDetailTable.row($(curobj).parents('tr')).data();
    if (rowData.ID) {
        notyConfirm('Are you sure to delete?', 'DeleteItem("' + rowData.ID + '");', '', "Yes, delete it!");
    }


}

function CheckAmount() {

    if ($("#txtDiscount").val() == "")
        $("#txtDiscount").val(roundoff(0));
}

function DeleteItem(ID) {

    try {

        //Event Request Case
        if (ID) {
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
                        EG_Rebind_WithData(GetAllQuoteItems($("#ID").val()), 1);
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
    }
    catch (e) {

        notyAlert('error', e.message);
    }



}
function saveInvoices() {
    debugger;
    var validation = EG_Validate();
    if (validation == "") {

        var result = JSON.stringify(EG_GridData);
        $("#DetailJSON").val(result);
        $('#btnSave').trigger('click');
    }
    else {
        notyAlert('error', validation);
    }

    }


function SaveSuccess(data, status) {
   
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
           
            notyAlert('success', JsonResult.Message);
            ChangeButtonPatchView('ProformaInvoice', 'btnPatchAdd', 'Edit');
            if (JsonResult.Record.ID) {
                $("#ID").val(JsonResult.Record.ID);
                $('#deleteId').val(JsonResult.Record.ID);
            }
            BindAllQuotes();
            EG_Rebind_WithData(GetAllQuoteItems($("#ID").val()), 1);
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}

function AddNew() {
    ChangeButtonPatchView('ProformaInvoice', 'btnPatchAdd', 'Add');
    openNav();
    EG_ClearTable();
    Reset();   
    $("#lblEmailSent").text('N/A');
    $("#lblInvoiceNo").text('New ProformaInvoice');
    clearUploadControl();
    EG_AddBlankRows(5)
    clearUploadControl();
}

function Reset() {
   
    $('#ProformaForm')[0].reset();
    $('#ID').val('');
}

function Edit(Obj) {
    debugger;
   $('#ProformaForm')[0].reset();
    var rowData = DataTables.ProformaInvoiceTable.row($(Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    $('#deleteId').val(rowData.ID);
    $('#QuoteMAilHeaderID').val(rowData.ID);

    BindProformaInvoiceDetails(rowData.ID);
    GetTaxPercentage();
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
            $("#ddlCust").val(jsresult.CustomerID);
            $("#SentToAddress").val(jsresult.SentToAddress);
            $("#ContactPerson").val(jsresult.ContactPerson);            
            $("#Subject").val(jsresult.Subject);
            $("#BodyHead").val(jsresult.BodyHead);
            $("#ddlCompany").val(jsresult.company.Code);
            
            $("#GrossAmount").val(jsresult.GrossAmount);
            $("#Discount").val(jsresult.Discount);
            $("#NetTaxableAmount").val(jsresult.NetTaxableAmount);
            $("#TaxTypeCode").val(jsresult.TaxTypeCode);
            $("#TaxPercApplied").val(jsresult.TaxPercApplied);
            $("#TaxAmount").val(jsresult.TaxAmount);
            $("#TotalAmount").val(jsresult.TotalAmount);
            $("#BodyFoot").val(jsresult.BodyFoot);       

            //$("#lblQuoteStage").text(jsresult.quoteStage.Description);
             $("#lblEmailSent").text(jsresult.EmailSentYN == "True" ? 'YES' : 'NO');
           // $('#SentToEmails').val(jsresult.SentToEmails);
            $("#lblInvoiceNo").text(jsresult.InvoiceNo);
            debugger;
            EG_Rebind_WithData(GetAllQuoteItems(jsresult.ID),1);
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




//------------------------------------------------ Filter clicks------------------------------------------------------------//

function Gridfilter(filter) {
    debugger;
    $('#filter').show();

    $('#Draftfilter').hide();
    $('#Deliveredfilter').hide();
    $('#Progressfilter').hide();
    $('#Closedfilter').hide();

    if (filter == 'DFT') {
        $('#Draftfilter').show();
    }
    else if (filter == 'DVD') {
        $('#Deliveredfilter').show();
    }
    else if (filter == 'NGT') {
        $('#Progressfilter').show();
    }
    else if (filter == "CLT,CWN") {
        $('#Closedfilter').show();
    }
    var result = GetAllProformaInvoices(filter);
    if (result != null) {
        DataTables.ProformaInvoiceTable.clear().rows.add(result).draw(false);
    }
}

//--Function To Reset ProformaInvoice Table--//
function FilterReset() {
    $('#filter').hide();
    var result = GetAllProformaInvoices();
    if (result != null) {
        DataTables.ProformaInvoiceTable.clear().rows.add(result).draw(false);
    }
}
