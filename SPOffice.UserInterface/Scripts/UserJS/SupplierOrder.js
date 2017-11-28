﻿//CreatedDate: 22-Nov-2017 Wednesday
//LastModified: 22-nov-2017 Wednesday
//FileName: SupplierOrder.js
//Description: Client side coding for Supplier PO
//******************************************************************************
//******************************************************************************

//Global Declarations
var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
  
    debugger;
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
              { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                 { className: "text-right", "targets": [] },
                  { className: "text-left", "targets": [1, 2, 3, 4, 5] },
            { className: "text-center", "targets": [] }

            ]
        });

        $('#tblPurchaseOrderDetail tbody').on('dblclick', 'td', function () {
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
              { "data": "Code", "defaultContent": "<i>-</i>" },
              { "data": "MaterialDesc", "defaultContent": "<i>-</i>" },
              { "data": "UnitCode", "defaultContent": "<i>-</i>" },
              { "data": "Qty", "defaultContent": "<i>-</i>" },
              { "data": "Rate", "defaultContent": "<i>-</i>" },
              { "data": "Amount", "defaultContent": "<i>-</i>"},
              { "data": "Particulars", "defaultContent": "<i>-</i>" },
              { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="EditDetail(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                 { className: "text-right", "targets": [4,5,6] },
                  { className: "text-left", "targets": [1, 2, 3] },
            { className: "text-center", "targets": [7,8] }

            ]
        });

        $('#tblPurchaseOrderDetail tbody').on('dblclick', 'td', function () {
            EditDetail(this);
        });

    } catch (x) {

        notyAlert('error', x.message);

    }
  

//------------------------Table3
    //------------------------Modal Popup Add SPO Details-------------------------------------//
    //try
    //{
    //    debugger;
    //    DataTables.RequisitionDetailsTable = $('#tblRequisitionDetails').DataTable({
    //        dom: '<"pull-left"f>rt<"bottom"ip><"clear">',
    //        order: [],
    //        searching: false,
    //        paging: false,
    //        data: null,
    //        columns: [
    //             { "data": "ID", "defaultContent": "<i>-</i>" },
    //             { "data": null, "defaultContent": "", "width": "5%" },
    //             { "data": "RequisitionNo", "defaultContent": "<i>-</i>"},
    //             { "data": "Title", "defaultContent": "<i>-</i>" },
    //               { "data": "Date", "defaultContent": "<i>-</i>" },
    //             { "data": "Status", "defaultContent": "<i>-</i>" },
    //            { "data": "CreatedBy", "defaultContent": "<i>-</i>" }                 
    //        ],
    //        columnDefs: [{ orderable: false, className: 'select-checkbox', "targets": 1 }
    //            , { className: "text-right", "targets": [4, 5, 6] }
    //            , { "targets": [0], "visible": false, "searchable": false }
    //            , { "targets": [2, 3, 4, 5, 6], "bSortable": false }],

    //        select: { style: 'multi', selector: 'td:first-child' }
    //    });


    //    //Table3 tblRequisitionList

    //} catch (x) {

    //    notyAlert('error', x.message);

    //}

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
    debugger;
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
        debugger;
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
            notyAlert('error', ds.message);
        }
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
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
    ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Edit');
    RemovevalidationMsg();
    openNav();

}

function BindPurchaseOrder(ID) {
    try {
        var jsresult = GetPurchaseOrderDetailsByID(ID)
        debugger;
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
            debugger;
            $("#TaxTypeCode").val(jsresult.TaxTypeCode);
            $("#TaxPercApplied").val(jsresult.TaxPercApplied);
            $("#Discount").val(roundoff(jsresult.Discount));
           // $("#NetTaxableAmount").val(roundoff(jsresult.NetTaxableAmount));
            $("#TaxAmount").val(roundoff(jsresult.TaxAmount));
            $("#TotalAmount").val(roundoff(jsresult.TotalAmount));
            
            PurchaseOrderDetailBindTable() //------binding Details table

            //clearUploadControl();
            //PaintImages(ID);
        }
    }
    catch (e) {
        notyAlert('error', e.Message);
    }
}

function GetPurchaseOrderDetailsByID(ID) {
    try {
        debugger;

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
    $('#btnSave').trigger('click');
}

function Reset()
{
    BindPurchaseOrder($("#ID").val());
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
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}

//-----------------------------------------------------------//
function ResetForm() {
    $('#ID').val('');
    $('#SupplierPOForm')[0].reset();
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

function SaveSuccess(data, status) {

    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            notyAlert('success', JsonResult.Message);
            ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Edit');
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

//------------------------------------------------ Filter clicks-----------------------------------------------//

function GridFilter(status) {
    debugger;
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
    debugger;
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
function AddPurchaseOrderDetail() {

    $('#RequisitionDetailsModal').modal('show');
    //Modal close
    //$('#EditRequisitionDetailsModal').modal('hide');
}



