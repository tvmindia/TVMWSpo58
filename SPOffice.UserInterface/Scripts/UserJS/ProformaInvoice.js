var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var _Products = [];
var _Units = [];
$(document).ready(function () {
    try {
        debugger;
        //$('#btnUpload').click(function () {
        //    //Pass the controller name
        //    var FileObject = new Object;
        //    if ($('#hdnFileDupID').val() != emptyGUID) {
        //        FileObject.ParentID = (($('#ID').val()) != emptyGUID ? ($('#ID').val()) : $('#hdnFileDupID').val());
        //    }
        //    else {
        //        FileObject.ParentID = ($('#ID').val() == emptyGUID) ? "" : $('#ID').val();
        //    }
           

        //    FileObject.ParentType = "Quotation";
        //    FileObject.Controller = "FileUpload";
        //    UploadFile(FileObject);
        //});
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
               { "data": "Subject", "defaultContent": "<i>-</i>" },
               //{ "data": "CustomerID", "defaultContent": "<i>-</i>" },
               { "data": "customer.CustomerName", "defaultContent": "<i>-</i>" },
               { "data": "ValidTillDate", "defaultContent": "<i>-</i>" },
               { "data": "Total","defaultContent": "<i>-</i>" },
               { "data": "Discount", "defaultContent": "<i>-</i>" },
               { "data": "TaxAmount", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                  { className: "text-right", "targets": [6,7,8] },
                   { className: "text-left", "targets": [2,3] },
             { className: "text-center", "targets": [1,4,5] }

             ]
         });

        $('#ProformaInvoiceTable tbody').on('dblclick', 'td', function () {
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
          //columns: EG_Columns(),
          //columnDefs: EG_Columns_Settings()
      });

       

    }
    catch (x) {
        notyAlert('error', x.message);
    }

   
});

//---------------Bind logics-------------------
function GetAllProformaInvoices() {
    try {
        debugger;
        var data = {};
        var ds = {};
        ds = GetDataFromServer("ProformaInvoice/GetAllProformaInvoices/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            // BindSummarBox(ds.Draft, ds.Delivered, ds.InProgress,ds.Closed);
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

//function QuotationNoOnChange(curobj)
//{
//    $("#lblQuotationNo").text($(curobj).val());
//}

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


//function BindAllQuotes() {
//    try {
//        DataTables.QuotationTable.clear().rows.add(GetAllQuotations()).draw(false);
//    }
//    catch (e) {
//        notyAlert('error', e.message);
//    }
//}



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
    //$("#ddlQuoteStage").val('DFT');
    //$("#lblQuoteStage").text('N/A');
    //$("#lblEmailSent").text('N/A');
    //$("#lblQuotationNo").text('New Quotation');
    clearUploadControl();
    EG_AddBlankRows(5)
  //  clearUploadControl();
}

function Reset() {
   
    $('#ProformaForm')[0].reset();
    $('#ID').val('');
}


//function GetCustomerDeails(curobj) {
//    var customerid = $(curobj).val();
//    if (customerid) {
//        var data = { "ID": customerid };
//        var ds = {};
//        ds = GetDataFromServer("CustomerOrder/GetCustomerDetailsByID/", data);
//        if (ds != '') {
//            ds = JSON.parse(ds);
//        }
//        if (ds.Result == "OK") {

//            $("#SentToAddress").val(ds.Record.BillingAddress);
//            $("#ContactPerson").val(ds.Record.ContactPerson);
//            $("#SentToEmails").val(ds.Record.ContactEmail);
//            return ds.Record;
//        }
//        if (ds.Result == "ERROR") {
//            return 0;
//        }
//    }

//}
