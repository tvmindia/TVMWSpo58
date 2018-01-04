//CreatedDate: 22-Nov-2017 Wednesday
//Created By : Gibin Jacob Job
//FileName: SupplierOrder.js
//Description: Client side coding for Supplier PO
//******************************************************************************
//******************************************************************************

//Global Declarations
var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var EditSPOdetailID;
var reqDetail = [];
var RequisitionDetailViewModel = new Object();
var reqDetailLink = [];
var RequisitionDetailLink = new Object();
var SupplierOrderViewModel = new Object();

$(document).ready(function () {

    //----------------------------Table 1 :Supplier Purchase Order Table List---------------------//
    try {
        DataTables.PurchaseOrderTable = $('#tblSupplierPurchaseOrder').DataTable(
        {
            dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
            order: [],
            searching: true,
            paging: true,
            data:GetAllSupplierPurchaseOrders(),
            pageLength: 15,
            language: {
                search: "_INPUT_",
                searchPlaceholder: "Search"
            },
            columns: [
              { "data": "ID" },
              { "data": "PONo", "defaultContent": "<i>-</i>" },
              { "data": "PODate", "defaultContent": "<i>-</i>" },
              { "data": "SuppliersObj.CompanyName", "defaultContent": "<i>-</i>" },
              { "data": "company.Name", "defaultContent": "<i>-</i>" },
              { "data": "TotalAmount", "defaultContent": "<i>-</i>" },
              { "data": "POStatus", "defaultContent": "<i>-</i>" },
            {
                "data": "IsFinalApproved", render: function (data, type, row) {
                    if (data) {
                        return "Approved ✔ <br/>📅 " + (row.FinalApprovedDateString !== null ? row.FinalApprovedDateString : "-");
                    }
                    else {
                        return 'Pending';
                    }

                }, "defaultContent": "<i>-</i>"
            },
              { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                 { className: "text-right", "targets": [] },
                  { className: "text-left", "targets": [1, 2, 3, 4, 5] },
            { className: "text-center", "targets": [] }

            ]
        });

        $('#tblSupplierPurchaseOrder tbody').on('dblclick', 'td', function () {
            Edit(this);
        });

    } catch (x) {

        notyAlert('error', x.message);

    }
  
    //----------------------------Table2 : Supplier Purchase Order Detail Table ----------------//
    try {
        DataTables.PurchaseOrderDetailTable = $('#tblPurchaseOrderDetail').DataTable(
        {
            dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
            order: [],
            searching: false,
            paging: true,
            data:null, 
            pageLength: 15,
            language: {
                search: "_INPUT_",
                searchPlaceholder: "Search"
            },
            columns: [
              { "data": "ID" },
              { "data": "MaterialID", "defaultContent": "<i>-</i>" },
              { "data": "MaterialCode", "defaultContent": "<i>-</i>" },
              { "data": "MaterialDesc", "defaultContent": "<i>-</i>" },
              { "data": "UnitCode", "defaultContent": "<i>-</i>" },
              { "data": "Qty", "defaultContent": "<i>-</i>" },
              { "data": "Rate", "defaultContent": "<i>-</i>" },
              { "data": "Amount", "defaultContent": "<i>-</i>"},
              {
                  "data": "Particulars", "defaultContent": "<i>-</i>", 'render': function (data, type, row) {
                      if (data != null)
                          return 'Requisitions are: ' + data
                      else
                          return '-'
                  }
              },
            { "data": null, "orderable": false, "width": "5%", "defaultContent": '<a href="#" class="ItemEditlink" onclick="EditPurchaseOrderDetailTable(this)"><i class="fa fa-pencil-square-o" aria-hidden="true"></i></a><span> | </span><a href="#" class="ItemEditlink" onclick="DeletePurchaseOrderDetailTable(this)"><i class="fa fa-trash-o" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0,1], "visible": false, "searchable": false },
                 { className: "text-right", "targets": [5,6,7] },
                  { className: "text-left", "targets": [2,3,4] },
            { className: "text-center", "targets": [8,9] }

            ]
        });

        $('#tblPurchaseOrderDetail tbody').on('dblclick', 'td', function () {
            EditPurchaseOrderDetailTable(this);
        });

    } catch (x) {

        notyAlert('error', x.message);

    }
  
    //------------------------Modal Popups Add SPO Details-------------------------------------//

    //------------------------Table3 tblRequisitionList
    try
    {
        debugger;
        DataTables.RequisitionListTable = $('#tblRequisitionList').DataTable({
            dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
            order: [],
            searching: true,
            paging: true,
            pageLength: 10,
            data: null,
            columns: [
                 { "data": "ID", "defaultContent": "<i>-</i>" },
                 { "data": null, "defaultContent": "", "width": "5%" },
                 { "data": "ReqNo", "defaultContent": "<i>-</i>" },
                 { "data": "Title", "defaultContent": "<i>-</i>" },
                 { "data": "ReqDateFormatted", "defaultContent": "<i>-</i>" },
                 { "data": "FinalApprovalDateFormatted", "defaultContent": "<i>-</i>" },
                 { "data": "ReqStatus", "defaultContent": "<i>-</i>" },
                 { "data": "CommonObj.CreatedBy", "defaultContent": "<i>-</i>" }
            ],
            columnDefs: [{ orderable: false, className: 'select-checkbox', "targets": 1 }
                , { className: "text-left", "targets": [2,3,7,6] }
                , { className: "text-center", "targets": [1,4,5] }
                , { "targets": [0], "visible": false, "searchable": false }
                , { "targets": [2, 3, 4, 5, 6,7], "bSortable": false }],
            select: { style: 'multi', selector: 'td:first-child' }
        });
    } catch (x) {
        notyAlert('error', x.message);
    }
    //Table4 tblRequisitionDetails
    try {
        debugger;
        DataTables.RequisitionDetailsTable = $('#tblRequisitionDetails').DataTable({
            dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
            order: [],
            searching: true,
            paging: true,
            pageLength: 7,
            data: null,
            columns: [
                 { "data": "ID", "defaultContent": "<i>-</i>" },
                 { "data": "ReqID", "defaultContent": "<i>-</i>" },
                 { "data": "MaterialID", "defaultContent": "<i>-</i>" },
                 { "data": null, "defaultContent": "", "width": "5%" },
                 { "data": "ReqNo", "defaultContent": "<i>-</i>","width": "10%" },
                 { "data": "RawMaterialObj.MaterialCode", "defaultContent": "<i>-</i>" },
                 { "data": "ExtendedDescription", "defaultContent": "<i>-</i>", 'render': function (data, type, row) {
                         if (row.ExtendedDescription)
                             Desc = data;
                         else
                             Desc = row.Description;
                         return '<input class="form-control description" name="Markup" value="' + Desc + '" type="text" onchange="textBoxValueChanged(this,1);">';
                     }
                 },
                 {
                     "data": "AppxRate", "defaultContent": "<i>-</i>", "width": "10%", 'render': function (data, type, row) {
                         return '<input class="form-control text-right " name="Markup" value="' + row.AppxRate + '" type="text" onclick="SelectAllValue(this);" onkeypress = "return isNumber(event)", onchange="textBoxValueChanged(this,2);">';
                     }
                 },
                 { "data": "RequestedQty", "defaultContent": "<i>-</i>", "width": "10%" },
                 { "data": "OrderedQty", "defaultContent": "<i>-</i>", "width": "10%" },
                 {
                     "data": "POQty", "defaultContent": "<i>-</i>", "width": "10px", 'render': function (data, type, row) {
                         return '<input class="form-control text-right " name="Markup" type="text"  value="' + data + '"  onclick="SelectAllValue(this);" onkeypress = "return isNumber(event)", onchange="textBoxValueChanged(this,3);">';
                     }
                 },
                 { "data": "UnitCode", "defaultContent": "<i>-</i>" }

            ],
            columnDefs: [{ orderable: false, className: 'select-checkbox', "targets": 3 }
                , { className: "text-left", "targets": [5, 6] }
                , { className: "text-right", "targets": [7, 8,9,10] }
                , { className: "text-center", "targets": [1, 4] }
                , { "targets": [0,1,2,11], "visible": false, "searchable": false }
                , { "targets": [2, 3, 4, 5, 6,7,8,9,10], "bSortable": false }],

            select: { style: 'multi', selector: 'td:first-child' }
        });     
    } catch (x) {
        notyAlert('error', x.message);
    }

    //------------------------Table5 tblRequisitionDetailsEdit

    try {
        debugger;
        DataTables.EditRequisitionDetailsTable = $('#tblRequisitionDetailsEdit').DataTable({
            dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
            order: [],
            searching: true,
            paging: true,
            pageLength: 7,
            data: null,
            columns: [
                 { "data": "ID", "defaultContent": "<i>-</i>" },
                 { "data": "ReqID", "defaultContent": "<i>-</i>" },
                 { "data": "MaterialID", "defaultContent": "<i>-</i>" },
                // { "data": null, "defaultContent": "", "width": "5%" },
                 { "data": "ReqNo", "defaultContent": "<i>-</i>", "width": "10%" },
                 { "data": "RawMaterialObj.MaterialCode", "defaultContent": "<i>-</i>" },
                 {
                     "data": "ExtendedDescription", "defaultContent": "<i>-</i>", 'render': function (data, type, row) {
                         if (row.ExtendedDescription)
                             Desc = data;
                         else
                             Desc = row.Description;
                         return '<input class="form-control description" name="Markup" value="' + Desc + '" type="text" onchange="EdittextBoxValue(this,1);">';
                     }
                 },
                 {
                     "data": "AppxRate", "defaultContent": "<i>-</i>", "width": "10%", 'render': function (data, type, row) {
                         return '<input class="form-control text-right " name="Markup" value="' + row.AppxRate + '" type="text" onclick="SelectAllValue(this);" onkeypress = "return isNumber(event)", onchange="EdittextBoxValue(this,2);">';
                     }
                 },
                 { "data": "RequestedQty", "defaultContent": "<i>-</i>", "width": "10%" },
                 { "data": "OrderedQty", "defaultContent": "<i>-</i>", "width": "10%" },
                 {
                     "data": "POQty", "defaultContent": "<i>-</i>", "width": "10px", 'render': function (data, type, row) {
                         return '<input class="form-control text-right " name="Markup" type="text"  value="' + data + '"  onclick="SelectAllValue(this);" onkeypress = "return isNumber(event)", onchange="EdittextBoxValue(this,3);">';
                     }
                 },
                 { "data": "UnitCode", "defaultContent": "<i>-</i>" }

            ],
            columnDefs: [//{ orderable: false, className: 'select-checkbox', "targets": 3 },
                { className: "text-left", "targets": [4, 5] }
                , { className: "text-right", "targets": [6,7, 8, 9] }
                , { className: "text-center", "targets": [1, 3] }
                , { "targets": [0, 1, 2, 10], "visible": false, "searchable": false }
                , { "targets": [2, 3, 4, 5, 6, 7, 8, 9, 10], "bSortable": false }],

            select: { style: 'multi', selector: 'td:first-child' }
        });
    } catch (x) {
        notyAlert('error', x.message);
    }

});
//---------------------------------Data Table Bindings------------------------------------------//
function GetAllSupplierPurchaseOrders(filter)
{
    try {
        var data = { "filter": filter };
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/GetAllSupplierPurchaseOrders/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            BindSummaryBox(ds.Open, ds.InProgress, ds.Closed);
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.message);
        }
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }

}

function BindSummaryBox(Open, InProgress, Closed) {

    $("#openCount").text(Open);
    $("#inProgressCount").text(InProgress);
    $("#closedCount").text(Closed);
    $("#openCountDescription").text(Open + ' Supplier Purchase Order(s) are Opened');
    $("#progressCountDescription").text(InProgress + ' In Progress Supplier Purchase Order(s)');
    $("#closedCountDescription").text(Closed + ' Closed Supplier Purchase Order(s)');
}

function BindAllPurchaseOrders() {
    try {
        DataTables.PurchaseOrderTable.clear().rows.add(GetAllSupplierPurchaseOrders()).draw(false);
}
    catch (e) {
//this will show the error msg in the browser console(F12) 
console.log(e.message);
}
}


//----PurchaseOrderDetailTable------//
function PurchaseOrderDetailBindTable() {
    try {
        DataTables.PurchaseOrderDetailTable.clear().rows.add(GetPurchaseOrderDetailTable()).draw(false);
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}
function GetPurchaseOrderDetailTable() {
    try {
        var id = $('#ID').val();
        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/GetPurchaseOrderDetailTable/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            $('#GrossTotal').val(roundoff(ds.GrossAmount));
            return ds.Record;

        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);
        }
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.Message);
    }
}

//---------------------------------------------------------------------------//

//--------------------------------Edit Clicks-------------------------------------------//
function Edit(Obj) {
    debugger;
    $('#SupplierPOForm')[0].reset();
    var rowData = DataTables.PurchaseOrderTable.row($(Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    BindPurchaseOrder(rowData.ID)
    $('#MailHeaderID').val(rowData.ID);
    //ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Edit');
    RemovevalidationMsg();
    openNav();

}

function BindPurchaseOrder(ID) {
    try {
        debugger;
        var jsresult = GetPurchaseOrderDetailsByID(ID)
        if (jsresult) {
            $("#ddlSupplier").val(jsresult.SupplierID);
            $("#ddlCompany").val(jsresult.POFromCompCode);
            $("#PONo").val(jsresult.PONo);
            $("#PODate").val(jsresult.PODate);
            $("#POIssuedDate").val(jsresult.POIssuedDate);
           
            $("#ShipToAddress").val(jsresult.ShipToAddress);
            $("#SupplierMailingAddress").val(jsresult.SupplierMailingAddress);
            $("#ddlOrderStatus").val(jsresult.POStatus);
            OrderStatusChange();

            $("#BodyFooter").val(jsresult.BodyFooter);
            $("#BodyHeader").val(jsresult.BodyHeader);
            $("#GeneralNotes").val(jsresult.GeneralNotes);
            if (jsresult.EmailSentYN=="True") {
                $("#lblEmailSent").text('Yes');
            }
            else {
                $("#lblEmailSent").text('No');
            }
            $("#SentToEmails").val(jsresult.SuppliersObj.ContactEmail);
            $("#TaxTypeCode").val(jsresult.TaxTypeCode);
            $("#TaxPercApplied").val(jsresult.TaxPercApplied);
            $("#Discount").val(roundoff(jsresult.Discount));
            $("#TaxAmount").val(roundoff(jsresult.TaxAmount));
            $("#TotalAmount").val(roundoff(jsresult.TotalAmount));
            
            PurchaseOrderDetailBindTable() //------binding Details table
            if ((jsresult.IsFinalApproved) && (jsresult.IsApprover))
            {
                EnableSupplierPoForm();
            }
            else if ((jsresult.IsFinalApproved) && !(jsresult.IsApprover))
            {
                DisableSupplierPoForm();
            }
            else if (!(jsresult.IsFinalApproved))
            {
                EnableSupplierPoForm();
            }
            //Attachment below functions calls go to custom.js
            clearUploadControl();
            PaintImages(ID);
        }
    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}
function EnableSupplierPoForm()
{
    $("#ddlSupplier").prop('disabled', false);
    $("#ddlCompany").prop('disabled', false);
    $("#PONo").prop('disabled', false);
    $("#PODate").prop('disabled', false);
    $("#POIssuedDate").prop('disabled', false);

    $("#ShipToAddress").prop('disabled', false);
    $("#SupplierMailingAddress").prop('disabled', false);
    $("#ddlOrderStatus").prop('disabled', false);

    $("#BodyFooter").prop('disabled', false);
    $("#BodyHeader").prop('disabled', false);
    $("#GeneralNotes").prop('disabled', false);

    $("#TaxTypeCode").prop('disabled', false);
    $("#TaxPercApplied").prop('disabled', false);
    $("#Discount").prop('disabled', false);
    $("#TaxAmount").prop('disabled', false);
    $("#TotalAmount").prop('disabled', false);
    $('#btnAddRequisitionItems').attr('disabled', false);
    $('#btnAddRequisitionItems').attr('onclick', 'AddPurchaseOrderDetail()');
    ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Edit');
    $('.ItemEditlink').show();
    $('.attachbutton').attr('disabled', false);
    $('.attachbutton').attr('onclick',"$('#FileUpload1').click();")
    $('input[type="button"]').prop('disabled', false);
}
function DisableSupplierPoForm()
{
    $("#ddlSupplier").prop('disabled', true);
    $("#ddlCompany").prop('disabled', true);
    $("#PONo").prop('disabled', true);
    $("#PODate").prop('disabled', true);
    $("#POIssuedDate").prop('disabled', true);

    $("#ShipToAddress").prop('disabled', true);
    $("#SupplierMailingAddress").prop('disabled', true);
    $("#ddlOrderStatus").prop('disabled', true);

    $("#BodyFooter").prop('disabled', true);
    $("#BodyHeader").prop('disabled', true);
    $("#GeneralNotes").prop('disabled', 'disabled');

    $("#TaxTypeCode").prop('disabled', true);
    $("#TaxPercApplied").prop('disabled', true);
    $("#Discount").prop('disabled', true);
    $("#TaxAmount").prop('disabled', true);
    $("#TotalAmount").prop('disabled', true);
    $('#btnAddRequisitionItems').attr('disabled', true);
    $('#btnAddRequisitionItems').removeAttr('onclick');
    ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'EditDisable');
    $('.ItemEditlink').hide();
    $('.attachbutton').attr('disabled', true);
    $('.attachbutton').removeAttr('onclick');
    $('input[type="button"]').prop('disabled', true);
}
function GetPurchaseOrderDetailsByID(ID) {
    try {

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/GetPurchaseOrderByID/", data);
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
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}


//--------------------------------Button Patch Clicks-------------------------------------------//
function AddNew() {
    ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Add');
    ResetForm();
    RemovevalidationMsg()
    openNav();
}


function Save() {
    debugger; 
    //validation main form 
    var $form = $('#SupplierPOForm');
    if($form.valid())
    {
        SupplierOrderViewModel.ID = $('#ID').val();
        SupplierOrderViewModel.hdnFileID = $('#hdnFileID').val();
        SupplierOrderViewModel.PONo = $('#PONo').val();
        SupplierOrderViewModel.PODate = $('#PODate').val();
        SupplierOrderViewModel.POIssuedDate = $('#POIssuedDate').val();
        SupplierOrderViewModel.SupplierID = $('#ddlSupplier').val();
        SupplierOrderViewModel.POFromCompCode = $('#ddlCompany').val();
        SupplierOrderViewModel.SupplierMailingAddress = $('#SupplierMailingAddress').val();
        SupplierOrderViewModel.ShipToAddress = $('#ShipToAddress').val();
        SupplierOrderViewModel.BodyHeader = $('#BodyHeader').val();
        SupplierOrderViewModel.BodyFooter = $('#BodyFooter').val();
        SupplierOrderViewModel.GrossTotal = $('#GrossTotal').val();
        SupplierOrderViewModel.Discount = $('#Discount').val();
        SupplierOrderViewModel.TaxPercApplied = $('#TaxPercApplied').val();
        SupplierOrderViewModel.TaxAmount = $('#TaxAmount').val();
        SupplierOrderViewModel.TaxTypeCode = $('#TaxTypeCode').val();
        SupplierOrderViewModel.TotalAmount = $('#TotalAmount').val();
        SupplierOrderViewModel.GeneralNotes = $('#GeneralNotes').val();
        // SupplierOrderViewModel.EmailSentYN = $('#EmailSentYN').val();
        SupplierOrderViewModel.POStatus = $('#ddlOrderStatus').val();
       
        SupplierOrderViewModel.reqDetailObj = reqDetail;
        SupplierOrderViewModel.reqDetailLinkObj = reqDetailLink;

        var data = "{'SPOViewModel':" + JSON.stringify(SupplierOrderViewModel) + "}";

        PostDataToServer("SupplierOrder/InsertUpdatePurchaseOrder/", data, function (JsonResult) {
            debugger;
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Record.Message);
                    ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Edit');
                    if (JsonResult.Record.ID) {
                        $("#ID").val(JsonResult.Record.ID);
                        BindPurchaseOrder($("#ID").val());
                    } else
                    {
                        Reset();
                    }
                    reqDetail = [];
                    reqDetailLink = [];
                    BindAllPurchaseOrders();
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
        })
    }
}

function UpdateDetailLinkSave() {
    debugger;
    //validation main form 
    var $form = $('#SupplierPOForm');
    if ($form.valid()) {
        SupplierOrderViewModel.ID = $('#ID').val(); 
        SupplierOrderViewModel.reqDetailObj = reqDetail;
        SupplierOrderViewModel.reqDetailLinkObj = reqDetailLink;

        var data = "{'SPOViewModel':" + JSON.stringify(SupplierOrderViewModel) + "}";

        PostDataToServer("SupplierOrder/UpdatePurchaseOrderDetailLink/", data, function (JsonResult) {

            debugger;
            switch (JsonResult.Result) {
                case "OK":
                    notyAlert('success', JsonResult.Record.Message);
                    ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Edit');
                    BindPurchaseOrder($("#ID").val());
                    BindAllPurchaseOrders();
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
        })
    } 
}

function Reset()
{
    if ($("#ID").val())
        BindPurchaseOrder($("#ID").val());
    else
        DataTables.PurchaseOrderDetailTable.clear().draw(false);
}

function DeleteSupplierPO() {
    var ID = $("#ID").val();
    if (ID) {
        notyConfirm('Are you sure to delete?', 'DeleteItem("' + ID + '");', '', "Yes, delete it!");
    }  
}
function DeleteItem(ID) {
    try {
        //Event Request Case
        if (ID) {
            var data = { "ID": ID };
            var ds = {};
            ds = GetDataFromServer("SupplierOrder/DeletePurchaseOrder/", data);
            debugger;
            if (ds != '') {
                ds = JSON.parse(ds);
            }
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
                //return ds.Record;
        }
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}

//-----------------------------------------------------------//
function ResetForm() {
    debugger;
    $('#ID').val('');
    var validator = $("#SupplierPOForm").validate();
    $('#SupplierPOForm').find('.field-validation-error span').each(function () {
            validator.settings.success($(this));
    });
    $('#SupplierPOForm')[0].reset();

    DataTables.PurchaseOrderDetailTable.clear().draw(false);
}
//-----------------------------------------------------------//
function RemovevalidationMsg() {
    var validator = $("#SupplierPOForm").validate();
    $('#SupplierPOForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
}

function OrderStatusChange()
{
    if ($("#ddlOrderStatus").val()!="")
        $("#lblStatus").text($("#ddlOrderStatus option:selected").text());
    else
        $("#lblStatus").text('N/A');
}

function SupplierOnChange(curobj) {
    debugger;
    var supplierID = $(curobj).val();
    if (supplierID) {
        var data = { "ID": supplierID };
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/GetSupplierDetailsByID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            $("#SupplierMailingAddress").val(ds.Record.BillingAddress);
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            return 0;
        }
    }
    else {
        $("#SupplierMailingAddress").val('');
    }
}
function CompanyOnChange(curobj) {
    debugger;
    var companyCode = $(curobj).val();
    if (companyCode) {
        var data = { "Code": companyCode };
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/GetCompanyDetailsByCode/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            $("#ShipToAddress").val(ds.Record.BillingAddress);
            return ds.Record;
        }
        if (ds.Result == "ERROR") {
            return 0;
        }
    }
    else {
        $("#ShipToAddress").val('');
    }
}


//------------------------------------------------ Filter clicks-----------------------------------------------//
function GridFilter(status) {
    $('#hdnfilterDescriptionDiv').show();

    $('#OPNfilter').hide();
    $('#CSDfilter').hide();
    $('#PGSfilter').hide();

    if (status == 'OPN') {
        $('#OPNfilter').show();
    }
    else if (status == 'CSD') {
        $('#CSDfilter').show();
    }
    else if (status == 'PGS') {
        $('#PGSfilter').show();
    }
    var result = GetAllSupplierPurchaseOrders(status);
    if (result != null) {
        DataTables.PurchaseOrderTable.clear().rows.add(result).draw(false);
    }
}
//--Function To Reset Purchase Order Table--//
function FilterReset() {
    $('#hdnfilterDescriptionDiv').hide();
    var result = GetAllSupplierPurchaseOrders();
    if (result != null) {
        DataTables.PurchaseOrderTable.clear().rows.add(result).draw(false);
    }
}

//----------------Calculations---------------------------------//
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
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
    AmountSummary();
}
function AmountSummary() {
    var total = 0.00;
    var GAmount = roundoff($('#GrossTotal').val());
    if (GAmount) {
        var discount = parseFloat($('#Discount').val()) || 0;
        var nettaxableamount = GAmount - discount;
        $('#NetTaxableAmount').val(roundoff(nettaxableamount));
        var applicabletax = $("#TaxPercApplied").val();
        $('#TaxAmount').val(roundoff((nettaxableamount * applicabletax) / 100));
        var totamt = roundoff(nettaxableamount + (nettaxableamount * applicabletax) / 100);
        $("#TotalAmount").val(totamt);
    }
    else {
        $('#GrossTotal').val(roundoff(total));
    }
}

//----------------Modals---------------------------------//

//----------ADD Requisition------------//
function AddPurchaseOrderDetail() {
    debugger;
    //Reset();
    //reqDetail = [];
    //reqDetailLink = [];
    var $form = $('#SupplierPOForm');
    if ($form.valid()) {
        $('#RequisitionDetailsModal').modal('show');
        ViewRequisitionList(1);
        DataTables.RequisitionDetailsTable.clear().draw(false);
        BindAllRequisitions();
    }
    else 
    {
        notyAlert('warning', "Please Fill Required Fields,To Add Items ");
    }
}

function BindAllRequisitions() {
    try {
        DataTables.RequisitionListTable.clear().rows.add(GetAllRequisitionHeaderForSupplierPO()).draw(false);
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}
function GetAllRequisitionHeaderForSupplierPO() {
    try {
        var data = {};
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/GetAllRequisitionHeaderForSupplierPO/", data);
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
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }

}

function ViewRequisitionDetails(value) {
    debugger;
    $('#tabDetail').attr('data-toggle', 'tab');
    if (value)
        $('#tabDetail').trigger('click');
    else {
        //selecting Checked IDs for  bind the detail Table
        var IDs = GetSelectedRowIDs();
        if (IDs) {
            BindGetRequisitionDetailsTable(IDs);
            DataTables.RequisitionDetailsTable.rows().select();
            $('#btnForward').hide();
            $('#btnBackward').show();
            $('#btnAddSPODetails').show();
        }
        else {
            $('#tabDetail').attr('data-toggle', '');
            DataTables.RequisitionDetailsTable.clear().draw(false);
            notyAlert('warning', "Please Select Requisition");
        }
    }  
}
function ViewRequisitionList(value) {
    $('#tabDetail').attr('data-toggle', 'tab');
    $('#btnForward').show();
    $('#btnBackward').hide();
    $('#btnAddSPODetails').hide();
    if(value)
        $('#tabList').trigger('click');
}
function GetSelectedRowIDs() {
    var SelectedRows = DataTables.RequisitionListTable.rows(".selected").data();
    if ((SelectedRows) && (SelectedRows.length > 0)) {
        var arrIDs="";
        for (var r = 0; r < SelectedRows.length; r++) {
            if (r == 0)
                arrIDs = SelectedRows[r].ID;
            else
                arrIDs = arrIDs + ',' + SelectedRows[r].ID;
        }
        return arrIDs;
    }
}
function BindGetRequisitionDetailsTable(IDs) { 
    try {
        DataTables.RequisitionDetailsTable.clear().rows.add(GetRequisitionDetailsByIDs(IDs)).draw(false);
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}
function GetRequisitionDetailsByIDs(IDs) {
    try {
        debugger;
        var SPOID=$('#ID').val();
        var data = { "IDs": IDs, "SPOID": SPOID };

        var ds = {};
        ds = GetDataFromServer("SupplierOrder/GetRequisitionDetailsByIDs/", data);
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
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}

function textBoxValueChanged(thisObj,textBoxCode) {
    debugger;
    var IDs = selectedRowIDs();//identify the selected rows 
    var allData = DataTables.RequisitionDetailsTable.rows().data();
    var table = DataTables.RequisitionDetailsTable;
    var rowtable = table.row($(thisObj).parents('tr')).data();
    for (var i = 0; i < allData.length; i++) {
        if (allData[i].ID == rowtable.ID) {
            if (textBoxCode == 1)//textBoxCode is the code to know, which textbox changed is triggered
                allData[i].ExtendedDescription = thisObj.value;
            if (textBoxCode == 2)
                allData[i].AppxRate = parseFloat(thisObj.value);
            if (textBoxCode == 3)
                allData[i].POQty = parseFloat(thisObj.value);
        }
    }
    DataTables.RequisitionDetailsTable.clear().rows.add(allData).draw(false);
    selectCheckbox(IDs); //Selecting the checked rows with their ids taken 
}

function AddSPODetails()
{
    debugger;
    //Merging  the rows with same MaterialID
    var allData = DataTables.RequisitionDetailsTable.rows(".selected").data();
    var mergedRows = []; //to store rows after merging
    var currentMaterial, QuantitySum;
    AddRequsitionDetailLink(allData)// adding values to reqDetailLink array function call

    for (var r = 0; r < allData.length; r++) {
        var Particulars="";
        Particulars = allData[r].ReqNo;
        currentMaterial = allData[r].MaterialID
        for (var j = r + 1; j < allData.length; j++) {
            if(allData[j].MaterialID==currentMaterial)
            {
                Particulars = Particulars + "," + allData[j].ReqNo;
                allData[r].POQty = parseFloat(allData[r].POQty) + parseFloat(allData[j].POQty);
                allData.splice(j, 1);//removing duplicate after adding value 
                j = j - 1;// for avoiding skipping row while checking
            }
        }
        allData[r].Particulars =  Particulars
        mergedRows.push(allData[r])// adding rows to merge array
    }

    var res=AddRequsitionDetail(mergedRows)// adding to reqDetail array function call

    if (res) {
        debugger;
        CalculateGrossAmount();//Calculating GrossAmount after adding new rows 
        $('#RequisitionDetailsModal').modal('hide');
        Save();
    }
}

function AddRequsitionDetail(mergedRows)
{
    if ((mergedRows) && (mergedRows.length > 0)) {
        for (var r = 0; r < mergedRows.length; r++) {
            RequisitionDetailViewModel = new Object();
            RequisitionDetailViewModel.MaterialID = mergedRows[r].MaterialID;
            RequisitionDetailViewModel.ID = emptyGUID; //newly added items has emptyguid
            RequisitionDetailViewModel.ReqDetailId = mergedRows[r].ID;
            RequisitionDetailViewModel.ReqID = mergedRows[r].ReqID;
            RequisitionDetailViewModel.MaterialCode = mergedRows[r].RawMaterialObj.MaterialCode;
            RequisitionDetailViewModel.MaterialDesc = mergedRows[r].ExtendedDescription == null ? mergedRows[r].Description : mergedRows[r].ExtendedDescription;
            RequisitionDetailViewModel.Qty = mergedRows[r].POQty;
            RequisitionDetailViewModel.Rate = mergedRows[r].AppxRate;
            RequisitionDetailViewModel.UnitCode = mergedRows[r].UnitCode;
            RequisitionDetailViewModel.Particulars = mergedRows[r].Particulars;
            RequisitionDetailViewModel.Amount = parseFloat(mergedRows[r].AppxRate) * parseFloat(mergedRows[r].POQty);
            //Particulars after adding same material(item)
            reqDetail.push(RequisitionDetailViewModel);
        }
        return true;
    }
    else {
        notyAlert('warning', "Please Select Requisition");
        return false;
    }
}

function mergedRowsWithExistingData() {
    debugger;
    var updatedData = [];
    var allDataExists = DataTables.PurchaseOrderDetailTable.rows().data();
    if (allDataExists.length > 0) {
        DataTables.PurchaseOrderDetailTable.rows.add(reqDetail).draw(false); //binding Detail table 
        for (var j = 0; j < allDataExists.length; j++) {
            for (var r = 0; r < reqDetail.length; r++) {
                if (allDataExists[j].MaterialID == reqDetail[r].MaterialID) {

                    allDataExists[j].Qty = parseFloat(allDataExists[j].Qty) + parseFloat(reqDetail[r].Qty); //old Qty + new Qty
                    allDataExists[j].Amount = parseFloat(reqDetail[r].Rate) * parseFloat(allDataExists[j].Qty); //new Rate * changed Qty
                    allDataExists[j].MaterialDesc = reqDetail[r].MaterialDesc; //New Material Description
                    allDataExists[j].Particulars = allDataExists[j].Particulars + ',' + reqDetail[r].Particulars; //Old Particulars + New 
                    reqDetail.splice(r, 1);//removing duplicate after adding value 
                    r = r - 1;// for avoiding skipping row while checking
                    updatedData.push(allDataExists[j]);
                }
            }
        }
        DataTables.PurchaseOrderDetailTable.clear().rows.add(allDataExists).draw(false);// binding table with Existing data changed
    }
    debugger;
    DataTables.PurchaseOrderDetailTable.rows.add(reqDetail).draw(false); //binding Detail table with new values added(not existing)
    //reqDetail = DataTables.PurchaseOrderDetailTable.rows().data();
    var temp = reqDetail.concat(updatedData);
    reqDetail = temp;
}

function AddRequsitionDetailLink(data) {
    for (var r = 0; r < data.length; r++) {
        RequisitionDetailLink = new Object();
        RequisitionDetailLink.MaterialID = data[r].MaterialID;
        RequisitionDetailLink.ID = emptyGUID; //[PODetailID]
        RequisitionDetailLink.ReqDetailID = data[r].ID;//[ReqDetailID]
        RequisitionDetailLink.ReqID = data[r].ReqID;
        RequisitionDetailLink.Qty = data[r].POQty;
        reqDetailLink.push(RequisitionDetailLink);
    }
}
function selectedRowIDs() {
    var allData = DataTables.RequisitionDetailsTable.rows(".selected").data();
    var arrIDs = "";
    for (var r = 0; r < allData.length; r++) {
        if (r == 0)
            arrIDs = allData[r].ID;
        else
            arrIDs = arrIDs + ',' + allData[r].ID;
    }
    return arrIDs; 
}
function selectCheckbox(IDs) {
    var allData = DataTables.RequisitionDetailsTable.rows().data()
    for (var i = 0; i < allData.length; i++) {
        if (IDs.includes(allData[i].ID)) {
            DataTables.RequisitionDetailsTable.rows(i).select();
        }
        else {
            DataTables.RequisitionDetailsTable.rows(i).deselect();
        }
    }
}
function CalculateGrossAmount()
{
    debugger;
    var allData = DataTables.PurchaseOrderDetailTable.rows().data();
    var GrossAmount=0;
    for (var i = 0; i < allData.length; i++)
    {
        GrossAmount = GrossAmount + parseFloat(allData[i].Amount)
    }
    $('#GrossTotal').val(GrossAmount);
    AmountSummary();
}


//----------Edit Requisition------------//
function EditPurchaseOrderDetailTable(curObj) {
    debugger;
    var rowData = DataTables.PurchaseOrderDetailTable.row($(curObj).parents('tr')).data();
    EditPurchaseOrderDetailByID(rowData.ID)
    EditSPOdetailID = rowData.ID// to set SPODetailID
    $('#EditRequisitionDetailsModal').modal('show');
}

function EditPurchaseOrderDetailByID(ID) {
    try {
        DataTables.EditRequisitionDetailsTable.clear().rows.add(EditPurchaseOrderDetail(ID)).draw(false);
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}

function EditPurchaseOrderDetail(ID) {
    try {
        var data = {ID};
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/EditPurchaseOrderDetail/", data);
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
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}

function EditSPODetails()
{
    debugger;
    var allData = DataTables.EditRequisitionDetailsTable.rows().data();

    var mergedRows = []; //to store rows after merging
   
    EditRequsitionDetailLink(allData)// adding to object function call

    for (var r = 0; r < allData.length; r++) {
        for (var j = r + 1; j < allData.length; j++) {
                allData[r].POQty = parseFloat(allData[r].POQty) + parseFloat(allData[j].POQty);
                allData.splice(j, 1);//removing duplicate after adding value 
                j = j - 1;// for avoiding skipping row while checking
        }
        mergedRows.push(allData[r])// adding rows to merge array
    }
    debugger;
    if ((mergedRows) && (mergedRows.length > 0)) {
        for (var r = 0; r < mergedRows.length; r++) {
            RequisitionDetailViewModel = new Object();
            RequisitionDetailViewModel.MaterialID = mergedRows[r].MaterialID;
            RequisitionDetailViewModel.ID = EditSPOdetailID;
            RequisitionDetailViewModel.ReqDetailId = mergedRows[r].ID;
            RequisitionDetailViewModel.ReqID = mergedRows[r].ReqID;
            RequisitionDetailViewModel.MaterialCode = mergedRows[r].RawMaterialObj.MaterialCode;
            RequisitionDetailViewModel.MaterialDesc = mergedRows[r].ExtendedDescription == null ? mergedRows[r].Description : mergedRows[r].ExtendedDescription;
            RequisitionDetailViewModel.Qty = mergedRows[r].POQty;
            RequisitionDetailViewModel.Rate = mergedRows[r].AppxRate;
            RequisitionDetailViewModel.UnitCode = mergedRows[r].UnitCode;
            RequisitionDetailViewModel.Particulars = mergedRows[r].Particulars;
            RequisitionDetailViewModel.Amount = parseFloat(mergedRows[r].AppxRate) * parseFloat(mergedRows[r].POQty);
            //Particulars after adding same material(item)
            reqDetail.push(RequisitionDetailViewModel);
        }
        debugger;
        UpdateDetailLinkSave();
        $('#EditRequisitionDetailsModal').modal('hide');
    }
}

function EditRequsitionDetailLink(data) {
    debugger;
    for (var r = 0; r < data.length; r++) {
        RequisitionDetailLink = new Object();
        RequisitionDetailLink.MaterialID = data[r].MaterialID;
        RequisitionDetailLink.ID = data[r].LinkID;//LinkId
        RequisitionDetailLink.ReqDetailID = data[r].ReqDetailID;//[ReqDetailID]
        RequisitionDetailLink.ReqID = data[r].ReqID;
        RequisitionDetailLink.Qty = data[r].POQty;
        reqDetailLink.push(RequisitionDetailLink);
    }
}

//function mergedEditedRowsWithExistingData() {
//    debugger;
//    var updatedData = [];
//    var allDataExists = DataTables.PurchaseOrderDetailTable.rows().data();
//    if (allDataExists.length > 0) {
//        for (var j = 0; j < allDataExists.length; j++) {
//            for (var r = 0; r < reqDetail.length; r++) {
//                if (allDataExists[j].MaterialID == reqDetail[r].MaterialID) {
//                    allDataExists[j].Qty = parseFloat(reqDetail[r].Qty);//new Qty
//                    allDataExists[j].Amount = parseFloat(reqDetail[r].Rate) * parseFloat(allDataExists[j].Qty); //new Rate * changed Qty
//                    allDataExists[j].MaterialDesc = reqDetail[r].MaterialDesc; //New Material Description
//                    allDataExists[j].Particulars = allDataExists[j].Particulars 
//                    reqDetail.splice(r, 1);//removing duplicate after adding value 
//                    r = r - 1;// for avoiding skipping row while checking
//                    updatedData.push(allDataExists[j]);
//                }
//            }
//        }
//        DataTables.PurchaseOrderDetailTable.clear().rows.add(allDataExists).draw(false);// binding table with Existing data changed
//    }
//    debugger;
//    var temp = DataTables.PurchaseOrderDetailTable.rows().data();
//    reqDetail = temp; 
//}


function EdittextBoxValue(thisObj, textBoxCode) {
    debugger;
    var IDs = selectedRowIDs();//identify the selected rows 
    var allData = DataTables.EditRequisitionDetailsTable.rows().data();
    var table = DataTables.EditRequisitionDetailsTable;
    var rowtable = table.row($(thisObj).parents('tr')).data();
    for (var i = 0; i < allData.length; i++) {
        if (allData[i].ID == rowtable.ID) {
            if (textBoxCode == 1)//textBoxCode is the code to know, which textbox changed is triggered
                allData[i].ExtendedDescription = thisObj.value;
            if (textBoxCode == 2)
                allData[i].AppxRate = parseFloat(thisObj.value);
            if (textBoxCode == 3)
                allData[i].POQty = parseFloat(thisObj.value);
        }
    }
    DataTables.EditRequisitionDetailsTable.clear().rows.add(allData).draw(false);
}


//----------Delete Purchase Order Detail Table------------//

function DeletePurchaseOrderDetailTable(curObj) {
    debugger; 
    var rowData = DataTables.PurchaseOrderDetailTable.row($(curObj).parents('tr')).data();
    var ID = rowData.ID;
    if (ID == emptyGUID)
    {
        DataTables.PurchaseOrderDetailTable.row($(curObj).parents('tr')).remove().draw();
    }
    else if (ID) {
    notyConfirm('Are you sure to delete?', 'DeletePurchaseOrderDetail("' + ID + '");', '', "Yes, delete it!");
    }
}
function DeletePurchaseOrderDetail(ID) {
    try { 
        if (ID) {
            var data = { "ID": ID };
            var ds = {};
            ds = GetDataFromServer("SupplierOrder/DeletePurchaseOrderDetail/", data);
            debugger;
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            switch (ds.Result) {
                case "OK":
                    notyAlert('success', ds.Message);
                    BindAllPurchaseOrders();
                    Reset();
                    break;
                case "ERROR":
                    notyAlert('error', ds.Message);
                    break;
                default:
                    break;
            }
            //return ds.Record;
        }
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}
function ApproveSupplierPO() {
    try {
        var data = { "ID": $('#ID').val() };
        var ds = {};
        ds = GetDataFromServer("SupplierOrder/ApproveSupplierOrder/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            notyAlert('success', ds.Record.Message);
            BindPurchaseOrder($('#ID').val());
            BindAllPurchaseOrders();
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);
        }
    }
    catch (e) {
        console.log(e.message);
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
    ds = GetDataFromServer("SupplierOrder/GetMailPreview/", data);
    if (ds == "Nochange") {
        return; 0
    }
    debugger;
    $("#mailmodelcontent").empty();
    $("#mailmodelcontent").html(ds);
    $("#mailBodyText").val(ds);

}

function SendMailClick() {
    debugger;
    $('#btnFormSendMail').trigger('click');
}

function ValidateEmail() {
    debugger;
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
            if(JsonResult.Record.Status=="1") {
                $("#lblEmailSent").text('Yes');
            }
            else{
                $("#lblEmailSent").text('No');
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

