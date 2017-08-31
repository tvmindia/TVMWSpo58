var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var _Products = [];
var _Units = [];
$(document).ready(function () {
    try {
    
        $('#btnUpload').click(function () {
            //Pass the controller name
            var FileObject = new Object;
            if ($('#hdnFileDupID').val() != emptyGUID) {
                FileObject.ParentID = (($('#ID').val()) != "" ? ($('#ID').val()) : $('#hdnFileDupID').val());
            }
            else {
                FileObject.ParentID = $('#ID').val();
            }

            FileObject.ParentType = "CusInvoice";
            FileObject.Controller = "FileUpload";
            UploadFile(FileObject);
        });
        DataTables.QuotationTable = $('#QuotationTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllQuotations(),
             pageLength: 15,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "ID" },
               { "data": "QuotationDate", "defaultContent": "<i>-</i>" },
               { "data": "QuotationNo", "defaultContent": "<i>-</i>" },
               { "data": "QuoteSubject", "defaultContent": "<i>-</i>" },
               { "data": "customer.CustomerName", "defaultContent": "<i>-</i>" },
               { "data": "ContactPerson", "defaultContent": "<i>-</i>" },
               { "data": "company.Name","defaultContent": "<i>-</i>" },
               { "data": "quoteStage.Description", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                  { className: "text-right", "targets": [] },
                   { className: "text-left", "targets": [2,3,4,5,6,7] },
             { className: "text-center", "targets": [1,8] }

             ]
         });

        $('#QuotationTable tbody').on('dblclick', 'td', function () {
            Edit(this);
        });
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
       // showLoader();
       // List();

        //$('.Roundoff').on('change', function () {
        //    var CustomerInvoiceViewModel = new Object();
        //    CustomerInvoiceViewModel.GrossAmount = $('#txtGrossAmt').val();
        //    CustomerInvoiceViewModel.Discount = ((parseInt($('#txtDiscount').val())) > (parseInt($('#txtGrossAmt').val()))) ? "0.00" : $('#txtDiscount').val();
        //    CustomerInvoiceViewModel.NetTaxableAmount = (CustomerInvoiceViewModel.GrossAmount - CustomerInvoiceViewModel.Discount)
        //    CustomerInvoiceViewModel.TaxType = $('#ddlTaxType').val() != "" ? GetTaxRate($('#ddlTaxType').val()) : $('#txtTaxPercApp').val();
        //    CustomerInvoiceViewModel.TaxPercentage = CustomerInvoiceViewModel.TaxType
        //    CustomerInvoiceViewModel.TaxAmount = (CustomerInvoiceViewModel.NetTaxableAmount * CustomerInvoiceViewModel.TaxPercentage) / 100
        //    CustomerInvoiceViewModel.TotalInvoiceAmount = (CustomerInvoiceViewModel.NetTaxableAmount + CustomerInvoiceViewModel.TaxAmount)
        //    $('#txtNetTaxableAmt').val(CustomerInvoiceViewModel.NetTaxableAmount);
        //    $('#txtTaxPercApp').val(CustomerInvoiceViewModel.TaxPercentage);
        //    $('#txtTaxAmt').val(CustomerInvoiceViewModel.TaxAmount);
        //    $('#txtTotalInvAmt').val(CustomerInvoiceViewModel.TotalInvoiceAmount);
        //    if ((parseInt($('#txtDiscount').val())) > (parseInt($('#txtGrossAmt').val()))) {
        //        $('#txtDiscount').val("0.00");
        //    }

        //});
        //$('#txtTaxPercApp').on('keypress', function () {
        //    debugger;
        //    if ($('#ddlTaxType').val() != "")
        //        $('#ddlTaxType').val('')
        //});

    }
    catch (x) {
        notyAlert('error', x.message);
    }


    $('#MailPreviewModel').on('shown.bs.modal', function () {
        
        var QHID = $("#ID").val();
        if (QHID) {
            //Bind mail html into model
            GetMailPreview(QHID);


        }
    })
});


//-----------------------EDIT GRID DEFN-------------------------------------
var EG_totalDetailRows = 0;
var EG_GridData;//DATA SOURCE OBJ ARRAY
var EG_GridDataTable;//DATA TABLE ITSELF FOR REBIND PURPOSE
var EG_SlColumn = 'SlNo';
var EG_GridInputPerRow = 4;
var EG_MandatoryFields = 'ProductCode';


function EG_TableDefn() {
    var tempObj = new Object();
    tempObj.ID = "";
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
   
    //var vatp = (parseFloat($('#vatpercentage').val()) || 0);
    //if (vatp > 0) {
    //    vatamount = (total * vatp) / 100;
    //    $('#vatamount').val(roundoff(vatamount));
    //}
    //$('#grandtotal').val(roundoff(total + vatamount));
}

function GetTaxPercentage()
{
   
    try {
        var curObj=$("#TaxTypeCode").val();
        if (curObj)
        {
            $("#TaxPercApplied").val(0);
            var data = { "Code": curObj };
            var ds = {};
            ds = GetDataFromServer("Quotation/GetTaxRate/", data);
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
        else
        {
            $("#TaxPercApplied").val(0);
        }
        
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function Delete() {
    notyConfirm('Are you sure to delete?', 'DeleteItem(' + curobj + ')', '', "Yes, delete it!");
}

function CheckAmount() {
  
    if ($("#txtDiscount").val() == "")
        $("#txtDiscount").val(roundoff(0));
}

function DeleteItem(curobj) {

    try {
        var rowData = DataTables.ItemDetailTable.row($(curobj).parents('tr')).data();
        //Event Request Case
        if ((rowData != null) && (rowData.ID != null))
        {
            var data = { "ID": rowData.ID };
            var ds = {};
            ds = GetDataFromServer("Quotation/DeleteItemByID/", data);
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

//function DeleteSuccess(data, status) {
//    var JsonResult = JSON.parse(data)
//    switch (JsonResult.Result) {
//        case "OK":
//            AddNew();
//            List();
//            notyAlert('success', JsonResult.Message);
//            break;
//        case "Error":
//            notyAlert('error', JsonResult.Message);
//            break;
//        case "ERROR":
//            notyAlert('error', JsonResult.Message);
//            break;
//        default:
//            break;
//    }
//}

function SaveSuccess(data, status) {
   
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
           
            notyAlert('success', JsonResult.Message);
            ChangeButtonPatchView('Quotation', 'btnPatchAdd', 'Edit');
            if (JsonResult.Record.ID) {
                $("#ID").val(JsonResult.Record.ID);
            }
            $('#deleteId').val(JsonResult.Record.ID);
           
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}






function Edit(Obj) {
   
    $('#QuoteForm')[0].reset();
    var rowData = DataTables.QuotationTable.row($(Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    $('#deleteId').val(rowData.ID);
    BindQuationDetails(rowData.ID);
    GetTaxPercentage();
    ChangeButtonPatchView('Quotation', 'btnPatchAdd', 'Edit');
    openNav();
}
function PreviewMail()
{
    try
    {
       
        $("#MailPreviewModel").modal('show');
        
    }
    catch(e)
    {
        notyAlert('error', e.Message);
    }
 
}
function GetMailPreview(ID) {

    var data = { "ID": ID };
    var ds = {};
    ds = GetDataFromServer("Quotation/GetMailPreview/", data);
    if (ds == "Nochange") {
        return; 0
    }
    $("#mailmodelcontent").empty();
    $("#mailmodelcontent").html(ds);
}


function BindQuationDetails(ID)
{
    try
    {
        var jsresult = GetQuationDetailsByID(ID)
        if(jsresult)
        {
            //bind
            $("#txtQuotationNo").val(jsresult.QuotationNo);
            $("#QuotationDate").val(jsresult.QuotationDate);
            $("#ValidTillDate").val(jsresult.ValidTillDate);
            $("#ddlCustomer").val(jsresult.CustomerID);
            $("#SentToAddress").val(jsresult.SentToAddress);
            $("#ContactPerson").val(jsresult.ContactPerson);
            $("#ddlSalesPerson").val(jsresult.SalesPersonID);
            $("#ddlCompany").val(jsresult.company.Code);
            $("#ddlQuoteStage").val(jsresult.quoteStage.Code);
            $("#QuoteSubject").val(jsresult.QuoteSubject);
            $("#QuoteBodyHead").val(jsresult.QuoteBodyHead);

            $("#GrossAmount").val(jsresult.GrossAmount);
            $("#Discount").val(jsresult.Discount);
            $("#NetTaxableAmount").val(jsresult.NetTaxableAmount);
            $("#TaxTypeCode").val(jsresult.TaxTypeCode);
            $("#TaxPercApplied").val(jsresult.TaxPercApplied);
            $("#TaxAmount").val(jsresult.TaxAmount);
            $("#TotalAmount").val(jsresult.TotalAmount);
            $("#QuoteBodyFoot").val(jsresult.QuoteBodyFoot);
            $("#GeneralNotes").val(jsresult.GeneralNotes);
             
            $("#lblQuoteStage").text(jsresult.quoteStage.Description);
            $("#lblEmailSent").text(jsresult.EmailSentYN=="True"?'YES':'NO');
            
            
            EG_Rebind_WithData(GetAllQuoteItems(jsresult.ID), 1);

         

        }

    }
    catch(e)
    {
        notyAlert('error', e.Message);
    }
}

function GetQuationDetailsByID(ID) {
    try {

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Quotation/GetQuationDetailsByID/", data);
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
function AddNew() {
    ChangeButtonPatchView('Quotation', 'btnPatchAdd', 'Add');
    openNav();
    EG_ClearTable();
    Reset();
    $("#ddlQuoteStage").val('DFT');
    $("#lblQuoteStage").text('N/A');
    $("#lblEmailSent").text('N/A');
    EG_AddBlankRows(5)
  //  clearUploadControl();
}

function Reset() {
   
    $('#QuoteForm')[0].reset();
    $('#ID').val('');
}

//---------------Bind logics-------------------
function GetAllQuotations() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("Quotation/GetAllQuotations/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            BindSummarBox(ds.Draft, ds.Delivered, ds.InProgress,ds.Closed);
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

function BindSummarBox(Draft, Delivered, InProgress, Closed)
{
    $("#draftCount").text(Draft);
    $("#deliveredCount").text(Delivered);
    $("#inProgressCount").text(InProgress);
    $("#closedCount").text(Closed);
}

function GetAllQuoteItems(ID) {
    try {

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Quotation/GetQuateItemsByQuateHeadID/", data);
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

function GetAllProductCodes()
{
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("Quotation/GetAllProductCodes/", data);
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

function GetAllUnitCodes() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("Quotation/GetAllUnitCodes/", data);
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


