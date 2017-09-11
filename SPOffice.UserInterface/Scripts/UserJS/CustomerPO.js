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
                FileObject.ParentID = (($('#ID').val()) != emptyGUID ? ($('#ID').val()) : $('#hdnFileDupID').val());
            }
            else {
                FileObject.ParentID = ($('#ID').val() == emptyGUID) ? "" : $('#ID').val();
            }


            FileObject.ParentType = "Quotation";
            FileObject.Controller = "FileUpload";
            UploadFile(FileObject);
        });
        DataTables.PurchaseOrderTable = $('#PurchaseOrderTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllPurchaseOrders(),
             pageLength: 15,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "ID" },
               { "data": "PONo", "defaultContent": "<i>-</i>" },
               { "data": "PODate", "defaultContent": "<i>-</i>" },
               { "data": "customer.CompanyName", "defaultContent": "<i>-</i>" },
               { "data": "company.Name", "defaultContent": "<i>-</i>" },
               { "data": "purchaseOrderStatus.Description", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                  { className: "text-right", "targets": [] },
                   { className: "text-left", "targets": [1, 2,3,4,5] },
             { className: "text-center", "targets": [] }

             ]
         });

        $('#PurchaseOrderTable tbody').on('dblclick', 'td', function () {
            Edit(this);
        });
        $('input[type="text"].Roundoff').on('focus', function () {
            $(this).select();
        });


        

        

    }
    catch (x) {
        notyAlert('error', x.message);
    }


    //$('#MailPreviewModel').on('shown.bs.modal', function () {


    //})
});

function AmountSummary() {
    var total = 0.00;
    var GAmount = roundoff($('#GrossAmount').val());
    if (GAmount)
    {
        //$('#GrossAmount').val(roundoff(GAmount));
        var discount = parseFloat($('#Discount').val()) || 0;
        var nettaxableamount = GAmount - discount;
        $('#NetTaxableAmount').val(roundoff(nettaxableamount));
        var applicabletax = $("#TaxPercApplied").val();
        $('#TaxAmount').val(roundoff((nettaxableamount * applicabletax) / 100));
        var totamt = roundoff(nettaxableamount + (nettaxableamount * applicabletax) / 100);
        $("#TotalAmount").val(totamt);
    }
    else
    {
        $('#GrossAmount').val(roundoff(total));
    }
  
}


function BindAllPurchaseOrders() {
    try {
        DataTables.PurchaseOrderTable.clear().rows.add(GetAllPurchaseOrders()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function GetTaxPercentage() {

    try {
        var curObj = $("#TaxTypeCode").val();
        if (curObj) {
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
        else {
            $("#TaxPercApplied").val(0);
        }
       
    }
    catch (e) {
        notyAlert('error', e.message);
    }
    AmountSummary();
}


function GetCustomerDeails(curobj)
{
    var customerid = $(curobj).val();
    if(customerid)
    {
        var data = { "ID": customerid };
        var ds = {};
        ds = GetDataFromServer("CustomerOrder/GetCustomerDetailsByID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {

            $("#BillingAddress").val(ds.Record.BillingAddress);
            $("#ShippingAddress").val(ds.Record.ShippingAddress);
      
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            return 0;
        }
    }
 
}

function DeleteCustomerPO(curobj) {
    var rowData = $("#ID").val();
    if (rowData) {
        notyConfirm('Are you sure to delete?', 'DeleteItem("' + rowData + '");', '', "Yes, delete it!");
    }


}



function DeleteItem(ID) {

    try {

        //Event Request Case
        if (ID) {
            var data = { "ID": ID };
            var ds = {};
            ds = GetDataFromServer("CustomerOrder/DeletePurchaseOrder/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                switch (ds.Result) {
                    case "OK":
                        notyAlert('success', ds.Message);
                        BindAllPurchaseOrders();
                        closeNav();
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

function Save()
 {
   $('#btnSave').trigger('click');
 }





function SaveSuccess(data, status) {

    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":

            notyAlert('success', JsonResult.Message);
            ChangeButtonPatchView('CustomerOrder', 'btnPatchAdd', 'Edit');
            if (JsonResult.Record.ID) {
                $("#ID").val(JsonResult.Record.ID);
             
            }
            BindAllPurchaseOrders();
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

    $('#PurchaseOrderForm')[0].reset();
    var rowData = DataTables.PurchaseOrderTable.row($(Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    //$('#deleteId').val(rowData.ID);
   

    BindPurchaseOrderDetails(rowData.ID);
    //GetTaxPercentage();
    ChangeButtonPatchView('CustomerOrder', 'btnPatchAdd', 'Edit');
    openNav();
}




function BindPurchaseOrderDetails(ID) {
    try {
        var jsresult = GetPurchaseOrderDetailsByID(ID)
        if (jsresult) {
            //bind
            $("#ddlCustomer").val(jsresult.CustomerID);
            $("#ddlCompany").val(jsresult.POToCompCode);
            $("#PONo").val(jsresult.PONo);
            $("#PODate").val(jsresult.PODate);
            $("#BillingAddress").val(jsresult.customer.BillingAddress);
            $("#ShippingAddress").val(jsresult.customer.ShippingAddress);
            $("#GrossAmount").val(jsresult.GrossAmount);
            $("#Discount").val(jsresult.Discount);
            $("#ddlOrderStatus").val(jsresult.purchaseOrderStatus.Code);
            $("#TaxTypeCode").val(jsresult.TaxTypeCode);
          
            $("#GrossAmount").val(roundoff(jsresult.GrossAmount));
            $("#Discount").val(roundoff(jsresult.Discount));
            $("#NetTaxableAmount").val(roundoff(jsresult.NetTaxableAmount));
            $("#TaxPercApplied").val(jsresult.TaxPercApplied);
            $("#TaxAmount").val(roundoff(jsresult.TaxAmount));
            $("#TotalAmount").val(roundoff(jsresult.TotalAmount));
           
            $("#GeneralNotes").val(jsresult.GeneralNotes);

            $("#lblQuoteStage").text(jsresult.purchaseOrderStatus.Description);
           
            $("#POTitle").val(jsresult.POTitle);
            $("#POContent").val(jsresult.POContent);
            $('#lblCustomerPONo').text(jsresult.PONo);
         
            clearUploadControl();
            PaintImages(ID);


        }

    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}

function OrderStatusChange(curobj)
{
    var status = $(curobj).val();
    if (status)
    {

   
        switch(status)
        {
            case 'CSD':
                $("#lblQuoteStage").text('Closed');
                break;
            case 'OPN':
                $("#lblQuoteStage").text('Open');
                break;
            case 'PGS':
                $("#lblQuoteStage").text('In Progress');
                break;

        }
    }
    else
    {
        $("#lblQuoteStage").text('N/A');
    }
       
}

function GetPurchaseOrderDetailsByID(ID) {
    try {

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("CustomerOrder/GetPurchaseOrderByID/", data);
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
    ChangeButtonPatchView('CustomerOrder', 'btnPatchAdd', 'Add');
    openNav();
   
    ResetForm();
    $('#lblCustomerPONo').text("New Purchase Order");
    $("#lblQuoteStage").text('N/A');
    
    clearUploadControl();
   
  
}

function Reset() {

   
   
    if ($("#ID").val() == "") {
        ClearFields();
    }
    else {
        BindPurchaseOrderDetails($("#ID").val());
    }
    ResetForm();
}
function ClearFields() {

    ResetForm();
    ChangeButtonPatchView("CustomerOrder", "btnPatchAdd", "Add"); //ControllerName,id of the container div,Name of the action
}

function ResetForm() {
    $('#ID').val('');
    $('#PurchaseOrderForm')[0].reset();
}
function OrderNumberOnChange(curobj)
{
    $('#lblCustomerPONo').text($(curobj).val());
}
//---------------Bind logics-------------------
function GetAllPurchaseOrders() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("CustomerOrder/GetAllPurchaseOrders/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            BindSummarBox(ds.Open, ds.InProgress, ds.Closed);
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

function BindSummarBox(Open, InProgress, Closed) {
  
    $("#openCount").text(Open);
    $("#inProgressCount").text(InProgress);
    $("#closedCount").text(Closed);
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









