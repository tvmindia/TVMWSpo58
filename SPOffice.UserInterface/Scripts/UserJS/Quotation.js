﻿var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var _Materials = [];
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
          order: [],
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
        EG_ComboSource('Materials', _Materials, 'Code', 'Description');
     
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
});


//-----------------------EDIT GRID DEFN-------------------------------------
var EG_totalDetailRows = 0;
var EG_GridData;//DATA SOURCE OBJ ARRAY
var EG_GridDataTable;//DATA TABLE ITSELF FOR REBIND PURPOSE
var EG_SlColumn = 'SlNo';
var EG_GridInputPerRow = 4;
var EG_MandatoryFields = 'ID';


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
    return tempObj
}


function EG_Columns() {
      var obj = [
                { "data": "ID", "defaultContent": "<i>0</i>" },
                { "data": "SlNo", "defaultContent": "<i></i>" },
                { "data": "ProductCode", render: function (data, type, row) { return (EG_createCombo(data, 'S', row, 'ProductCode', 'Materials', 'FillDescription')); } },
                { "data": "ProductDescription", render: function (data, type, row) { return (EG_createTextBox(data, 'S', row, 'ProductDescription', '')); }, "defaultContent": "<i></i>" },
                 { "data": "UnitCode", render: function (data, type, row) { return (EG_createCombo(data, 'S', row, 'UnitCode', 'UnitCodes','')); }, "defaultContent": "<i></i>" },
                { "data": "Quantity", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Quantity', 'CalculateAmount')); }, "defaultContent": "<i></i>" },
               
                { "data": "Rate", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Rate', '')); }, "defaultContent": "<i></i>" },
                { "data": "Amount", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Amount', '')); }, "defaultContent": "<i></i>" },
                { "data": null, "orderable": false, "defaultContent": '<a href="#" class="DeleteLink"  onclick="DeleteItem(this)" ><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a>' }
                ]

      return obj;

}

function EG_Columns_Settings() {

    var obj = [
        { "targets": [0], "visible": false, "searchable": false },
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

    for (i = 0; i < _Materials.length; i++) {
        if (_Materials[i].Code == EG_GridData[row - 1]['ProductCode']) {
            EG_GridData[row - 1]['ProductDescription'] = _Materials[i].Description;
            //Description
            EG_Rebind();
            break;
        }
    }

}


function DeleteInvoices() {
    notyConfirm('Are you sure to delete?', 'Delete()', '', "Yes, delete it!");
}

function CheckAmount() {
  
    if ($("#txtDiscount").val() == "")
        $("#txtDiscount").val(roundoff(0));
}

function Delete() {
    $('#btnFormDelete').trigger('click');
}

function saveInvoices() {
   
    $('#btnSave').trigger('click');
}

function DeleteSuccess(data, status) {
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            AddNew();
            List();
            notyAlert('success', JsonResult.Message);
            break;
        case "Error":
            notyAlert('error', JsonResult.Message);
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            break;
    }
}

function SaveSuccess(data, status) {
   
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
           
            notyAlert('success', JsonResult.Message);
            $('#ID').val(JsonResult.Records.ID);
            $('#deleteId').val(JsonResult.Records.ID);
            //PaintInvoiceDetails()
           // List();
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
    openNav();
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
            debugger;
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
  //  clearUploadControl();
}

function Reset() {
    debugger;
    PaintInvoiceDetails();
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
            _Materials = ds.Records;
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


