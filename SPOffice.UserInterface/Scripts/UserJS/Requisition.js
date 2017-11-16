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
            { "data": "ReqDate", "defaultContent": "<i>-</i>" },
            { "data": "CompanyObj.ReqForCompany", "defaultContent": "<i>-</i>" },
            { "data":"ReqStatus","defaultContent":"<i>-</i>"},
            {
                "data": "ManagerApproved", render: function (data, type, row)
                {
                    debugger;
                    if (data)
                    {
                        return '<a class="circlebtn circlebtn-info" title="Approved"><i class="halflings-icon white ok"></i></a>';
                    }
                    else {
                        return '<a class="circlebtn circlebtn-info" title="Approved"><i class="fa fa-close"></i></a>';
                    }
                   
                }, "defaultContent": "<i>-</i>"
            },
            {
                "data": "FinalApproval", render: function (data, type, row) {
                    if (data) {
                        return '<a class="circlebtn circlebtn-info" style="background-color:green;" title="Approved"><i class="halflings-icon white ok"></i></a>';
                    }
                    else {
                        return '<a class="circlebtn circlebtn-info" style="background-color:green;" title="Approved"><i class="halflings-icon white ok"></i></a>';
                    }

                }, "defaultContent": "<i>-</i>"
            },
            { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
            ],
            columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
            { className: "text-left", "targets": [1, 2, 3, 4, 6] },
            { className: "text-center", "targets": [] },
            { className: "text-right", "targets": [5] }

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
        //Remove border color red while added data into the fields
        $('.Ivalidate').keypress(function (event) {
            if ($(this).val() !== "")
                $(this).css("border-color", "");
        });
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
});
function AddNew() {
    openNav();
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
function GetRequisitionDetail(ID) {
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
function AddItemsToTable()
{
    debugger;
    try {
        var ReqQty =$('#RequisitionDetailObj_RequestedQty');
        var Desc = $('#RequisitionDetailObj_Description');
        var ExtDesc=$('#RequisitionDetailObj_ExtendedDescription');
            var container = [
                { id: ExtDesc[0].id, name: ExtDesc[0].name, Value: ExtDesc[0].value },
                { id: Desc[0].id, name: Desc[0].name, Value: Desc[0].value },
                { id:ReqQty[0].id,name:ReqQty[0].name,Value:ReqQty[0].value}
            ];

            var j = 0;
            for (var i = 0; i < container.length; i++) {

                if (container[i].Value == "") {
                    j = 1;
                    var txtB = document.getElementById(container[i].id);
                    txtB.style.backgroundImage = "url('../img/invalid.png')";
                    txtB.style.borderColor = "#d87b7b";
                    txtB.style.backgroundPosition = "95% center";
                    txtB.style.backgroundRepeat = "no-repeat";

                }
            }
            if (j === 0)
            {
                if (DataTables.RequisitionDetailList.rows().data().length === 0) {
                    DataTables.RequisitionDetailList.clear().rows.add(GetRequisitionDetail(0)).draw(false);
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
                    var Itemtabledata = DataTables.RequisitionDetailList.rows().data();
                    var Item = new Object();
                    Item = Itemtabledata[0];
                    Item.ReqID = emptyGUID;
                    Item.ID = emptyGUID;
                    Item.RawMaterialObj.MaterialCode = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID option:selected").text() : "";
                    Item.RawMaterialObj.MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : emptyGUID;
                    Item.MaterialID = $("#RequisitionDetailObj_MaterialID").val() != "" ? $("#RequisitionDetailObj_MaterialID").val() : emptyGUID;
                    Item.AppxRate = $('#RequisitionDetailObj_AppxRate').val();
                    Item.Description = $('#RequisitionDetailObj_Description').val();
                    Item.ExtendedDescription = $('#RequisitionDetailObj_ExtendedDescription').val();
                    Item.CurrStock = $('#RequisitionDetailObj_CurrStock').val();
                    Item.RequestedQty = $('#RequisitionDetailObj_RequestedQty').val();
                    DataTables.RequisitionDetailList.row.add(Item).draw(false);
                }
                ClearItemFields();
            }        
    }
    catch(e)
    {
        console.log(e.message);
    }
}
function ClearItemFields()
{
    $("#RequisitionDetailObj_MaterialID ").val('');
    $('#RequisitionDetailObj_AppxRate').val('');
    $('#RequisitionDetailObj_Description').val('');
    $('#RequisitionDetailObj_ExtendedDescription').val('');
    $('#RequisitionDetailObj_CurrStock').val('');
    $('#RequisitionDetailObj_RequestedQty').val('');
}
function SaveRequisition()
{
    debugger;
    var Itemtabledata = DataTables.RequisitionDetailList.rows().data().toArray();
    $('#RequisitionDetailObj_RequisitionDetailObject').val(JSON.stringify(Itemtabledata));
    $('#btnSave').trigger('click');
}