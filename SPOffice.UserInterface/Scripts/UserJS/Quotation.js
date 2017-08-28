﻿var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
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
          dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
          order: [],
          searching: false,
          paging: false,
          data: null,
          autoWidth: false,
          columns: EG_Columns(),
          columnDefs: EG_Columns_Settings()
      });
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

function EG_Columns() {
    var obj = [
                { "data": "SCCode", "defaultContent": "<i></i>" },
                { "data": "ID", "defaultContent": "<i>0</i>" },
                 { "data": "MaterialID", "defaultContent": "<i></i>" },
                { "data": "SlNo", "defaultContent": "<i></i>" },
                { "data": "Material", render: function (data, type, row) { return (EG_createCombo(data, 'S', row, 'Material', 'Materials', 'FillUOM')); } },
                { "data": "Description", "defaultContent": "<i></i>" },
                { "data": "Quantity", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Quantity', 'CalculateAmount')); }, "defaultContent": "<i></i>" },
                { "data": "UOM", "defaultContent": "<i></i>" },
                { "data": "Rate", render: function (data, type, row) { return (EG_createTextBox(data, 'F', row, 'Rate', 'CalculateAmount')); }, "defaultContent": "<i></i>" },
                { "data": "BasicAmount", render: function (data, type, row) { return roundoff(data, 1); }, "defaultContent": "<i></i>" },
                { "data": "TradeDiscount", render: function (data, type, row) { return roundoff(data, 1); }, "defaultContent": "<i></i>" },
                { "data": "NetAmount", render: function (data, type, row) { return roundoff(data); }, "defaultContent": "<i></i>" },
                { "data": null, "orderable": false, "defaultContent": '<a href="#" class="DeleteLink"  onclick="DeleteItem(this)" ><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a>' }

    ]

    return obj

}

function EG_Columns_Settings() {

    var obj = [
        { "targets": [0], "visible": false, "searchable": false }, { "targets": [1], "visible": false, "searchable": false }, { "targets": [2], "visible": false, "searchable": false },
            { "width": "5%", "targets": 3 },
        { "width": "15%", "targets": 4 },
         { "width": "20%", "targets": 5 },
           { "width": "8%", "targets": 6 },
        { "width": "8%", "targets": 7 },
         { "width": "8%", "targets": 8 },
          { "width": "10%", "targets": 9 },
           { "width": "12%", "targets": 10 },
            { "width": "10%", "targets": 11 },
        { className: "text-right", "targets": [8] },
          { className: "text-left disabled", "targets": [5] },
        { className: "text-center", "targets": [3, 4, 6, 12] },
          { className: "text-center disabled", "targets": [7] },
        { className: "text-right disabled", "targets": [9, 10, 11] },
        { "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11] }

    ]

    return obj;

}


function DeleteInvoices() {
    notyConfirm('Are you sure to delete?', 'Delete()', '', "Yes, delete it!");
}

function CheckAmount() {
    debugger;
    if ($("#txtDiscount").val() == "")
        $("#txtDiscount").val(roundoff(0));
}

function Delete() {
    $('#btnFormDelete').trigger('click');
}

function saveInvoices() {
    debugger;
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
            var res;
            if ($('#ID').val() == "") {
                res = Advanceadjustment(); //calling advance adjustment popup if inserting
            }
            if (!res) {
                notyAlert('success', JsonResult.Message);
            }
            $('#ID').val(JsonResult.Records.ID);
            $('#deleteId').val(JsonResult.Records.ID);
            PaintInvoiceDetails()
            List();
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
   
    $('#CustomerInvoiceForm')[0].reset();
    var rowData = DataTables.CustInvTable.row($(Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    $('#deleteId').val(rowData.ID);
    PaintInvoiceDetails();
    openNav();
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
function PaintInvoiceDetails() {
    debugger;
    ChangeButtonPatchView('Quotation', 'btnPatchAdd', 'Edit');
    var InvoiceID = $('#ID').val();
    var CustomerInvoicesViewModel = GetCustomerInvoiceDetails(InvoiceID);
    $('#lblInvoiceNo').text(CustomerInvoicesViewModel.InvoiceNo);
    $('#txtInvNo').val(CustomerInvoicesViewModel.InvoiceNo);
    $('#txtInvDate').val(CustomerInvoicesViewModel.InvoiceDateFormatted);
    $('#ddlCompany').val(CustomerInvoicesViewModel.companiesObj.Code);
    $('#ddlCustomer').val(CustomerInvoicesViewModel.customerObj.ID);
    $('#hdfCustomerID').val(CustomerInvoicesViewModel.customerObj.ID);
    $('#ddlCustomer').prop('disabled', true);
    //------------------------------------------------
    $('#ddlInvoiceType').prop('disabled', true);

    debugger;
    $('#ddlInvoiceType').val(CustomerInvoicesViewModel.InvoiceType);

    BindInvocieReferenceDropDown(CustomerInvoicesViewModel.customerObj.ID);//dropdownbinding
    if ($('#ddlInvoiceType').val() == 'PB')
        $('#ddlRefInvoice').val(CustomerInvoicesViewModel.RefInvoice);
    else
        $('#ddlRefInvoice').val(-1);
    InvoicesTypeChange();

    //------------------------------------------------
    $('#txtBillingAddress').val(CustomerInvoicesViewModel.BillingAddress);
    $('#ddlPaymentTerm').val(CustomerInvoicesViewModel.paymentTermsObj.Code);
    $('#txtPayDueDate').val(CustomerInvoicesViewModel.PaymentDueDateFormatted);
    $('#txtGrossAmt').val(CustomerInvoicesViewModel.GrossAmount);
    $('#txtDiscount').val(CustomerInvoicesViewModel.Discount);
    $('#txtNetTaxableAmt').val(CustomerInvoicesViewModel.GrossAmount - CustomerInvoicesViewModel.Discount);
    $('#ddlTaxType').val(CustomerInvoicesViewModel.TaxTypeObj.Code);
    $('#txtTaxPercApp').val(CustomerInvoicesViewModel.TaxPercApplied);
    $('#txtTaxAmt').val(CustomerInvoicesViewModel.TaxAmount);
    $('#txtTotalInvAmt').val(CustomerInvoicesViewModel.TotalInvoiceAmount);
    $('#txtNotes').val(CustomerInvoicesViewModel.Notes);
    $('#ID').val(CustomerInvoicesViewModel.ID);
    $('#lblinvoicedAmt').text(CustomerInvoicesViewModel.TotalInvoiceAmountstring);
    $('#lblpaidAmt').text(CustomerInvoicesViewModel.PaidAmountstring);
    $('#lblbalalnceAmt').text(CustomerInvoicesViewModel.BalanceDuestring);
    clearUploadControl();
    PaintImages(InvoiceID);

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
//onchange function for the customer dropdown to fill:- due date and Address 
function FillCustomerDefault(this_Obj) {
    try {
        debugger;
        var ID = this_Obj.value;
        var CustomerViewModel = GetCustomerDetails(ID);
        $('#txtBillingAddress').val(CustomerViewModel.BillingAddress);
        $('#ddlPaymentTerm').val(CustomerViewModel.PaymentTermCode);
        $('#ddlPaymentTerm').trigger('change');
        //if ($('#ddlInvoiceType').val() == "PB") {
        //Bind only if invoice type is PB 
        BindInvocieReferenceDropDown(ID);
        // }

    }
    catch (e) {

    }
}
function GetCustomerDetails(ID) {
    try {
        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("CustomerInvoices/GetCustomerDetails/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            alert(ds.Message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetDueDate(this_Obj) {
    try {
        debugger;
        var Code = this_Obj.value;
        var PaymentTermViewModel = GetPaymentTermDetails(Code);
        $('#txtPayDueDate').val(PaymentTermViewModel);
    }
    catch (e) {

    }
}
function GetPaymentTermDetails(Code) {
    debugger;
    try {
        var data = { "Code": Code, "InvDate": $('#txtInvDate').val() };
        var ds = {};
        ds = GetDataFromServer("CustomerInvoices/GetDueDate/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            alert(ds.Message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
function GetCustomerInvoiceDetails() {
    try {
        var InvoiceID = $('#ID').val();
        var data = { "ID": InvoiceID };
        var ds = {};
        ds = GetDataFromServer("CustomerInvoices/GetCustomerInvoiceDetails/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            alert(ds.Message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
//-----------------------------------------------------------------------------------------------------------------//

function InvoicesTypeChange() {
    debugger;
    if ($('#ddlInvoiceType').val() == "PB") {
        $('#ddlRefInvoice').prop('disabled', false);
        $('#txtInvNo').prop('disabled', true);
        $('#txtInvNo').val('');
    }
    else if ($('#ddlInvoiceType').val() == "WB") {
        $('#ddlRefInvoice').prop('disabled', true);
        $('#txtInvNo').prop('disabled', true);
        $('#txtInvNo').val('');
        $('#ddlRefInvoice').val(-1);
    }
    else {
        $('#ddlRefInvoice').prop('disabled', true);
        $('#ddlRefInvoice').val(-1);
        $('#txtInvNo').prop('disabled', false);
    }


}

function BindInvocieReferenceDropDown(ID) {
    debugger;
    try {
        var item = GetAllCustomerInvociesByID(ID);
        if (item) {
            $('#ddlRefInvoice').empty();
            $('#ddlRefInvoice').append(new Option('-- Select Invoice --', -1));
            for (var i = 0; i < item.length; i++) {
                var opt = new Option(item[i].InvoiceNo + '-' + item[i].companiesObj.Name, item[i].ID);
                $('#ddlRefInvoice').append(opt);
            }
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function GetAllCustomerInvociesByID(CustomerID) {
    try {
        debugger;

        var data = { "CustomerID": CustomerID };
        var ds = {};
        ds = GetDataFromServer("CustomerInvoices/GetAllCustomerInvociesByCustomerID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            alert(ds.Message);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
