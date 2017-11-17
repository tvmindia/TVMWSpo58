//*****************************************************************************
//*****************************************************************************
//Author: Thomson Varkey
//CreatedDate: 14-Nov-2017 Tuesday
//LastModified: 14-nov-2017 Tuesday
//FileName: Requisition.js
//Description: Client side coding for Requisition
//******************************************************************************
//******************************************************************************
//Global Declarations
var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000';
var Rowindex = -1;
//This will fire on page loads
$(document).ready(function () {
    try {
        DataTables.RequisitionList = $('#tblRequisitionList').DataTable({
            dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
            order: [],
            searching: true,
            paging: true,
            data: GetUserRequisitionList(),
            pageLength: 10,
            language: {
                search: "_INPUT_",
                searchPlaceholder: "Search"
            },
            columns: [
            { "data": "ID", "defaultContent": "<i>-</i>" },
            { "data": "ReqNo", "defaultContent": "<i>-</i>" },
            { "data": "Title", "defaultContent": "<i>-</i>" },
            { "data": "ReqDateFormatted", "defaultContent": "<i>-</i>" },
            { "data": "CompanyObj.Name", "defaultContent": "<i>-</i>" },
            { "data":"ReqStatus","defaultContent":"<i>-</i>"},
            {
                "data": "ManagerApproved", render: function (data, type, row)
                {
                    debugger;
                    if (data)
                    {
                        return "Approved ✔ <br/> 📅 " + row.ManagerApprovalDateFormatted;
                    }
                    else {
                        if (row.FinalApproval)
                        {
                            return '-'
                        }
                        else
                        {
                            return 'Pending';
                        }
                        
                    }
                   
                }, "defaultContent": "<i>-</i>"
            },
            {
                "data": "FinalApproval", render: function (data, type, row) {
                    if (data) {
                        return "Approved ✔ <br/>📅 " + row.FinalApprovalDateFormatted;
                    }
                    else {
                        return 'Pending';
                    }

                }, "defaultContent": "<i>-</i>"
            },
            { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
            { className: "text-left", "targets": [1, 2, 3, 4, 6] },
            { className: "text-center", "targets": [5] }

            ]
        });           
        DataTables.RequisitionDetailList = $('#tblMaterialList').DataTable({
            dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
            order: [],
            searching: false,
            paging: true,
            data: null,
            pageLength: 10,
            language: {
                search: "_INPUT_",
                searchPlaceholder: "Search"
            },
            columns: [
            { "data": "ID", "defaultContent": "<i>-</i>" },
            //{ "data": "No", "defaultContent": "<i>-</i>" },
            { "data": "RawMaterialObj.MaterialCode", "defaultContent": "<i>-</i>" },
            { "data": "Description", "defaultContent": "<i>-</i>" },
            { "data": "ExtendedDescription","defaultContent":"<i>-</i>"},
            { "data": "CurrStock", "defaultContent": "<i>-</i>" },
            { "data": "RequestedQty", "defaultContent": "<i>-</i>" },
            { "data":"AppxRate","defaultContent":"<i>-</i>"},
            { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit Item" class="actionLink"  onclick="EditIemsFromGrid(this)" ><i class="glyphicon glyphicon-edit" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0], "visible": false, "searchable": false }//,
            //{ className: "text-left", "targets": [1, 2, 3, 4, 6] },
            //{ className: "text-center", "targets": [] },
            //{ className: "text-right", "targets": [5] }

            ]
        });
        $('#tblRequisitionList tbody').on('dblclick', 'td', function () {
            Edit(this);
        });
        //Remove border color red while added data into the fields
        $('.Ivalidate').keypress(function (event) {
            if ($(this).val() !== "")
                $(this).css("border-color", "");
        });
        $('#ReqStatus').on('change',function(){
            $('#lblReqStatus').text($(this).val());
        });
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
});
function ClearFormFields()
{
    $('#btnReset').trigger('click');
    $('#ID').val(emptyGUID);
    $('#RequisitionDetailObj_RequisitionDetailObject').val('');
    $('#lblApprovalStatus').text('Pending');
    $('#lblReqStatus').text('Open');
    DataTables.RequisitionDetailList.clear().draw();
}
function AddNew() {
    ClearFormFields();
    $('.Ivalidate').css("border-color", "");
    Rowindex = -1;
    openNav();
}
function Edit(this_Obj)
{
    ClearFormFields();
    $('.Ivalidate').css("border-color", "");
    Rowindex = -1;
    var rowData = DataTables.RequisitionList.row($(this_Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    BindRequisitionDetail();
    openNav();
}
function EditIemsFromGrid(this_Obj)
{
    debugger;
    Rowindex = DataTables.RequisitionDetailList.row($(this_Obj).parents('tr')).index();
    var rowData = DataTables.RequisitionDetailList.row($(this_Obj).parents('tr')).data();
    $("#RequisitionDetailObj_MaterialID ").val(rowData.MaterialID!==emptyGUID?rowData.MaterialID:"");
    $('#RequisitionDetailObj_AppxRate').val(rowData.AppxRate);
    $('#RequisitionDetailObj_Description').val(rowData.Description);
    if (rowData.MaterialID !== emptyGUID)
    {
        $('#RequisitionDetailObj_Description').prop("disabled", true);
    }
    $('#RequisitionDetailObj_ExtendedDescription').val(rowData.ExtendedDescription);
    $('#RequisitionDetailObj_CurrStock').val(rowData.CurrStock);
    $('#RequisitionDetailObj_RequestedQty').val(rowData.RequestedQty);
    $('#anchorDeleteItem').attr('onclick', 'DeleteItem("' + rowData.ID+ '")')
    $('.ItemAdd').hide();
    $('.ItemEdit').show();
}
function DeleteItem(ID)
{
    debugger;
    if(ID===emptyGUID)
    {
        var Itemtabledata = DataTables.RequisitionDetailList.rows().data();
        Itemtabledata.splice(Rowindex, 1);
        DataTables.RequisitionDetailList.clear().rows.add(Itemtabledata).draw(false);
        ClearItemFields();
    }
    else
    {
        notyConfirm('Are you sure to delete?', 'DeleteRequisitionDetail("' + ID + '")');
        
    }
}
function DeleteRequisitionDetail(ID)
{
    debugger;
    $('.cancel').trigger('click');
    //var id = $('#ID').val();
    if (ID != '' && ID != null) {
        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Requisition/DeleteRequisitionDetailByID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            // notyAlert('success', ds.Record.Message);
            var Itemtabledata = DataTables.RequisitionDetailList.rows().data();
            Itemtabledata.splice(Rowindex, 1);
            DataTables.RequisitionDetailList.clear().rows.add(Itemtabledata).draw(false);
            ClearItemFields();
        }
        if (ds.Result == "ERROR") {
           // notyAlert('error', ds.Message);
            return 0;
        }
        return 1;
    }
}
function BindRequisitionDetail()
{
    debugger;
    try{
        var RequisitionViewModel = GetRequisitionDetailByID()
        $('#ReqNo').val(RequisitionViewModel.ReqNo);
        $('#Title').val(RequisitionViewModel.Title);
        $('#ReqDateFormatted').val(RequisitionViewModel.ReqDateFormatted);
        $('#ReqStatus').val(RequisitionViewModel.ReqStatus);
        $('#ReqForCompany').val(RequisitionViewModel.ReqForCompany);
        $('#lblReqStatus').text(RequisitionViewModel.ReqStatus);
        $('#lblApprovalStatus').text((RequisitionViewModel.FinalApproval) ? "Final ✔ " : ((RequisitionViewModel.ManagerApproved) ? "Manager ✔ " : "Pending"))
        DataTables.RequisitionDetailList.clear().rows.add(GetRequisitionDetailList(RequisitionViewModel.ID)).draw(false);
    }
    catch(e)
    {
        console.log(e.message);
    }
}
function GetItemDetails()
{
    debugger;
    try{
        var curObj = $("#RequisitionDetailObj_MaterialID").val();
        if (curObj) {
            var data = { "MaterialID": curObj };
            var ds = {};
            ds = GetDataFromServer("Requisition/GetItemDetail/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {

                $("#RequisitionDetailObj_AppxRate").val(ds.Records.ApproximateRate);
                $('#RequisitionDetailObj_Description').val(ds.Records.Description)
                $('#RequisitionDetailObj_Description').prop("disabled", true);
                //AmountSummary();
                return ds.Records;
            }
            if (ds.Result == "ERROR") {
                return 0;
            }
        }
    }
    catch(e)
    {
        console.log(e.message);
    }
}
function GetUserRequisitionList()
{
    debugger;
    try{
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Requisition/GetUserRequisitionList/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);

        }
    }
    catch(e)
    {
        console.log(e.message);
    }
}
function GetRequisitionDetailList(ID) {
    debugger;
    try {
        var data = {"ID":ID};
        var ds = {};
        ds = GetDataFromServer("Requisition/GetRequisitionDetail/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            notyAlert('error', ds.Message);

        }
    }
    catch (e) {
        console.log(e.message);
    }
}
function GetRequisitionDetailByID()
{
    debugger;
    try{
        var data = { "ID": $('#ID').val() };
        var ds = {};
        ds = GetDataFromServer("Requisition/GetRequisitionDetailByID/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            return ds.Records;
        }
        if (ds.Result == "ERROR") {
            return 0;
        }
    }
    catch(e)
    {
        console.log(e.message);
    }
}
function AddItemsToTable()
{
    try {
        var ReqQty =$('#RequisitionDetailObj_RequestedQty');
        var Desc = $('#RequisitionDetailObj_Description');
        //var ExtDesc=$('#RequisitionDetailObj_ExtendedDescription');
            var container = [
                //{ id: ExtDesc[0].id, name: ExtDesc[0].name, Value: ExtDesc[0].value },
                { id: Desc[0].id, name: Desc[0].name, Value: Desc[0].value },
                { id:ReqQty[0].id,name:ReqQty[0].name,Value:ReqQty[0].value}
            ];

            var j = 0;
            for (var i = 0; i < container.length; i++) {

                if (container[i].Value == "") {
                    j = 1;
                    var txtB = document.getElementById(container[i].id);
                    //txtB.style.backgroundImage = "url('../img/invalid.png')";
                    txtB.style.borderColor = "#d87b7b";
                    //txtB.style.backgroundPosition = "95% center";
                    //txtB.style.backgroundRepeat = "no-repeat";

                }
            }
            if (j === 0)
            {
                if (DataTables.RequisitionDetailList.rows().data().length === 0) {
                    DataTables.RequisitionDetailList.clear().rows.add(GetRequisitionDetailList(0)).draw(false);
                    var Itemtabledata = DataTables.RequisitionDetailList.rows().data();
                    Itemtabledata[0].RawMaterialObj.MaterialCode = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID option:selected").text() : "";
                    Itemtabledata[0].RawMaterialObj.MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : emptyGUID;
                    Itemtabledata[0].MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : emptyGUID;
                    Itemtabledata[0].AppxRate = $('#RequisitionDetailObj_AppxRate').val();
                    Itemtabledata[0].Description = $('#RequisitionDetailObj_Description').val();
                    Itemtabledata[0].ExtendedDescription = $('#RequisitionDetailObj_ExtendedDescription').val();
                    Itemtabledata[0].CurrStock = $('#RequisitionDetailObj_CurrStock').val();
                    Itemtabledata[0].RequestedQty = $('#RequisitionDetailObj_RequestedQty').val();
                    DataTables.RequisitionDetailList.clear().rows.add(Itemtabledata).draw(false);
                }
                else {
                    //var Itemtabledata = DataTables.RequisitionDetailList.rows().data();
                    var Item = new Object();
                    var RawMaterialObj = new Object;
                    Item.ID = emptyGUID;
                    Item.ReqID = emptyGUID;                    
                    RawMaterialObj.MaterialCode = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID option:selected").text() : "";
                    RawMaterialObj.MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : emptyGUID;
                    Item.RawMaterialObj = RawMaterialObj;                    
                    Item.MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : emptyGUID;
                    Item.AppxRate = $('#RequisitionDetailObj_AppxRate').val();
                    Item.Description = $('#RequisitionDetailObj_Description').val();
                    Item.ExtendedDescription = $('#RequisitionDetailObj_ExtendedDescription').val();
                    Item.CurrStock = $('#RequisitionDetailObj_CurrStock').val();
                    Item.RequestedQty = $('#RequisitionDetailObj_RequestedQty').val();
                    DataTables.RequisitionDetailList.row.add(Item).draw(true);
                }
                ClearItemFields();
            }        
    }
    catch(e)
    {
        console.log(e.message);
    }
}
function UpdateItemsToTable()
{
    var Itemtabledata = DataTables.RequisitionDetailList.rows().data();
    Itemtabledata[Rowindex].RawMaterialObj.MaterialCode = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID option:selected").text() : Itemtabledata[Rowindex].RawMaterialObj.MaterialCode;
    Itemtabledata[Rowindex].RawMaterialObj.MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : Itemtabledata[Rowindex].RawMaterialObj.MaterialID;
    Itemtabledata[Rowindex].MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : Itemtabledata[Rowindex].RawMaterialObj.MaterialID;
    Itemtabledata[Rowindex].AppxRate = $('#RequisitionDetailObj_AppxRate').val();
    Itemtabledata[Rowindex].Description = $('#RequisitionDetailObj_Description').val();
    Itemtabledata[Rowindex].ExtendedDescription = $('#RequisitionDetailObj_ExtendedDescription').val();
    Itemtabledata[Rowindex].CurrStock = $('#RequisitionDetailObj_CurrStock').val();
    Itemtabledata[Rowindex].RequestedQty = $('#RequisitionDetailObj_RequestedQty').val();
    DataTables.RequisitionDetailList.clear().rows.add(Itemtabledata).draw(false);
    ClearItemFields();
}
function ClearItemFields()
{
    $("#RequisitionDetailObj_MaterialID ").val('');
    $('#RequisitionDetailObj_AppxRate').val('');
    $('#RequisitionDetailObj_Description').val('');
    $('#RequisitionDetailObj_ExtendedDescription').val('');
    $('#RequisitionDetailObj_CurrStock').val('');
    $('#RequisitionDetailObj_RequestedQty').val('');
    Rowindex = -1;
    $('.ItemAdd').show();
    $('.ItemEdit').hide();
    $('#RequisitionDetailObj_Description').prop("disabled", false);
}
function SaveRequisition()
{
    var Itemtabledata = DataTables.RequisitionDetailList.rows().data().toArray();
    $('#RequisitionDetailObj_RequisitionDetailObject').val(JSON.stringify(Itemtabledata));
    $('#btnSave').trigger('click');
}
function SaveSuccessRequisition(data, status) {
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            notyAlert('success', JsonResult.Record.Message);
            DataTables.RequisitionList.clear().rows.add(GetUserRequisitionList()).draw(false);
            if (JsonResult.Record.ID) {
                $("#ID").val(JsonResult.Record.ID);
                $("#ReqNo").val(JsonResult.Record.ReqNo);
            }
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.Message);
            break;
        default:
            notyAlert('error', JsonResult.Record.Message);
            break;
    }
}