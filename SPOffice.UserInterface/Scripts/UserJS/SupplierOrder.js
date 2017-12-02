//CreatedDate: 22-Nov-2017 Wednesday
//Created By : Gibin Jacob Job
//FileName: SupplierOrder.js
//Description: Client side coding for Supplier PO
//******************************************************************************
//******************************************************************************

//Global Declarations
var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
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
              { "data": "Particulars", "defaultContent": "<i>-</i>" },
              { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="EditDetail(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0,1], "visible": false, "searchable": false },
                 { className: "text-right", "targets": [5,6,7] },
                  { className: "text-left", "targets": [2,3,4] },
            { className: "text-center", "targets": [8,9] }

            ]
        });

        $('#tblPurchaseOrderDetail tbody').on('dblclick', 'td', function () {
            EditDetail(this);
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
                 {
                     "data": "POQty", "defaultContent": "<i>-</i>", "width": "10px", 'render': function (data, type, row) {
                         return '<input class="form-control text-right " name="Markup" type="text"  value="' + data + '"  onclick="SelectAllValue(this);" onkeypress = "return isNumber(event)", onchange="textBoxValueChanged(this,3);">';
                     }
                 }
            ],
            columnDefs: [{ orderable: false, className: 'select-checkbox', "targets": 3 }
                , { className: "text-left", "targets": [5, 6] }
                , { className: "text-right", "targets": [7, 8] }
                , { className: "text-center", "targets": [1, 4] }
                , { "targets": [0,1,2], "visible": false, "searchable": false }
                , { "targets": [2, 3, 4, 5, 6,7,8], "bSortable": false }],

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
    ChangeButtonPatchView('SupplierOrder', 'btnPatchAdd', 'Edit');
    RemovevalidationMsg();
    openNav();

}

function BindPurchaseOrder(ID) {
    try {
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

            $("#TaxTypeCode").val(jsresult.TaxTypeCode);
            $("#TaxPercApplied").val(jsresult.TaxPercApplied);
            $("#Discount").val(roundoff(jsresult.Discount));
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
    $('#RequisitionDetailsModal').modal('show');
    ViewRequisitionList(1);
    DataTables.RequisitionDetailsTable.clear().draw(false);
    BindAllRequisitions();
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
    //selecting Checked IDs for  bind the detail Table
    var IDs = GetSelectedRowIDs();
        if (IDs) {
            BindGetRequisitionDetailsTable(IDs);
            DataTables.RequisitionDetailsTable.rows().select();
            if (value)
            $('#tabDetail').trigger('click');
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
        var data = {IDs};
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
            if (textBoxCode = 1)//textBoxCode is the code to know, which textbox changed is triggered
                allData[i].ExtendedDescription = thisObj.value;
            if (textBoxCode = 2)
                allData[i].AppxRate = parseFloat(thisObj.value);
            if (textBoxCode = 3)
                allData[i].POQty = parseFloat(thisObj.value);
        }
    }
    DataTables.RequisitionDetailsTable.clear().rows.add(allData).draw(false);
    selectCheckbox(IDs); //Selecting the checked rows with their ids taken 
}
//function POQtyChanged(thisObj) {
//    debugger;
//    var IDs= selectedRowIDs();
//    var allData = DataTables.RequisitionDetailsTable.rows().data();
//    var table = DataTables.RequisitionDetailsTable;
//    var rowtable = table.row($(thisObj).parents('tr')).data();
//    for (var i = 0; i < allData.length; i++) {
//        if (allData[i].ID == rowtable.ID) {
//            allData[i].POQty = parseFloat(thisObj.value);
//        }
//    } 
//    DataTables.RequisitionDetailsTable.clear().rows.add(allData).draw(false);
//    selectCheckbox(IDs);
//}
//function AppxRateChanged(thisObj) {
//    debugger;
//    var IDs = selectedRowIDs();
//    var allData = DataTables.RequisitionDetailsTable.rows().data();
//    var table = DataTables.RequisitionDetailsTable;
//    var rowtable = table.row($(thisObj).parents('tr')).data();
//    for (var i = 0; i < allData.length; i++) {
//        if (allData[i].ID == rowtable.ID) {
//            allData[i].AppxRate = parseFloat(thisObj.value);
//        }
//    }
//    DataTables.RequisitionDetailsTable.clear().rows.add(allData).draw(false);
//    selectCheckbox(IDs);
//}
//function ExtendedDescriptionChanged(thisObj) {
//    debugger;
//    var IDs = selectedRowIDs();
//    var allData = DataTables.RequisitionDetailsTable.rows().data();
//    var table = DataTables.RequisitionDetailsTable;
//    var rowtable = table.row($(thisObj).parents('tr')).data();
//    for (var i = 0; i < allData.length; i++) {
//        if (allData[i].ID == rowtable.ID) {
//            allData[i].ExtendedDescription = thisObj.value;
//        }
//    }
//    DataTables.RequisitionDetailsTable.clear().rows.add(allData).draw(false);
//    selectCheckbox(IDs);
//}

function AddSPODetails()
{
    debugger;
    //Merging  the rows with same MaterialID
    var allData = DataTables.RequisitionDetailsTable.rows(".selected").data();
    var mergedRows = []; //to store rows after merging
    var currentMaterial,QuantitySum;
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
            }
        }
        allData[r].Particulars = "Requisitions are: " + Particulars
        mergedRows.push(allData[r])// adding ros to merge array
    }
    // adding values to object array to bind detail table
    if ((mergedRows) && (mergedRows.length > 0)) {
        var ar = [];
        for (var r = 0; r < mergedRows.length; r++) {
            var RequisitionDetailViewModel = new Object();
            RequisitionDetailViewModel.MaterialID = mergedRows[r].MaterialID;
            RequisitionDetailViewModel.ID = mergedRows[r].ID;//ReqDetailId ?
            RequisitionDetailViewModel.ReqID = mergedRows[r].ReqID;
            RequisitionDetailViewModel.MaterialCode = mergedRows[r].RawMaterialObj.MaterialCode;
            RequisitionDetailViewModel.MaterialDesc = mergedRows[r].ExtendedDescription;
            RequisitionDetailViewModel.Qty = mergedRows[r].POQty;
            RequisitionDetailViewModel.Rate = mergedRows[r].AppxRate;
            //RequisitionDetailViewModel.UnitCode = mergedRows[r].UnitCode;
            RequisitionDetailViewModel.Particulars = mergedRows[r].Particulars;            
            RequisitionDetailViewModel.Amount = parseFloat(mergedRows[r].AppxRate) * parseFloat(mergedRows[r].POQty);
            //Particulars after adding same material(item)
            ar.push(RequisitionDetailViewModel);
        }
        DataTables.PurchaseOrderDetailTable.rows.add(ar).draw(false); //binding Detail table 
        CalculateGrossAmount();//Calculating GrossAmount after adding new rows 
        $('#RequisitionDetailsModal').modal('hide');
    }
    else 
    {
        notyAlert('warning', "Please Select Requisition");

    }
    
}
function selectedRowIDs() {
    var allData = DataTables.RequisitionDetailsTable.rows(".selected").data();
    var IDs = allData.ID.toString();
    return IDs;
}
function selectCheckbox(IDs) {
    var allData = DataTables.RequisitionDetailsTable.rows().data()
    for (var i = 0; i < allData.length; i++) {
        if (IDs.contains(allData[i].ID)) {
            DataTables.RequisitionDetailsTable.rows(i).deselect();
        }
        else {
            DataTables.RequisitionDetailsTable.rows(i).select();
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
function EditDetail(curObj) {
    var rowData = DataTables.PurchaseOrderDetailTable.row($(curObj).parents('tr')).data();
    $('#EditRequisitionDetailsModal').modal('show');
}

