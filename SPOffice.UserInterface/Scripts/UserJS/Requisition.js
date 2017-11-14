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
            
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
});
function AddNew() {
    openNav();
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