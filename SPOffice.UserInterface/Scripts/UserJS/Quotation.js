var DataTables = {};
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


