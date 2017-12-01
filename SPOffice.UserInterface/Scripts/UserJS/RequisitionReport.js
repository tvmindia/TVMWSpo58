//CreatedDate: 28-Nov-2017 Thursday
//LastModified: 28-nov-2017 Thursday
//FileName: RequisitionReport.js
//Description: Client side coding for RequisitionReport
//******************************************************************************
//******************************************************************************
//Global Declarations
var DataTables = {};
var startdate = '';
var enddate = '';
var RequisitionStatus = "";
var RequisitionByStatus = "";
var ApprovalStatusList = "";
var ManagerApproved = false;
var FinalApproved = false;
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {
        debugger;
        DataTables.RequisitionReportTable = $('#requisitionDetailTable').DataTable(
         {
             dom: '<"pull-right"Bf>rt<"bottom"ip><"clear">',
             buttons: [{
                 extend: 'excel',
                 exportOptions:
                              {
                                  columns: [1,2,3,4,5,6,7]
                              }
             }],
             order: [],
             fixedHeader: {
                 header: true
             },
             searching: false,
             paging: true,
             data: null,
             pageLength: 50,

             columns: [
                 { "data": "RequisitionObj.ID", "defaultContent": "<i>-</i>" },
               { "data": "RequisitionObj.ReqNo", "defaultContent": "<i>-</i>" },
               { "data": "RequisitionObj.Title", "defaultContent": "<i>-</i>" },
                { "data": "RequisitionObj.ReqDateFormatted", "defaultContent": "<i>-</i>" },
                 { "data": "RequisitionObj.CompanyObj.Name", "defaultContent": "<i>-</i>" },
                  { "data": "RequisitionObj.ReqStatus", "defaultContent": "<i>-</i>" },
                    {
                        "data": "RequisitionObj.ManagerApproved", render: function (data, type, row) {
                            if (data) {
                                return "Approved ✔ <br/> 📅 " + (row.RequisitionObj.ManagerApprovalDateFormatted !== null ? row.RequisitionObj.ManagerApprovalDateFormatted : "-");
                            }
                            else {
                                if (row.RequisitionObj.FinalApproval) {
                                    return '-'
                                }
                                else {
                                    return 'Pending';
                                }

                            }

                        }, "defaultContent": "<i>-</i>"
                    },

                    {
                        "data": "RequisitionObj.FinalApproval", render: function (data, type, row) {
                            if (data) {
                                return "Approved ✔ <br/>📅 " + (row.RequisitionObj.FinalApprovalDateFormatted !== null ? row.RequisitionObj.FinalApprovalDateFormatted : "-");
                            }
                            else {
                                return 'Pending';
                            }

                        }, "defaultContent": "<i>-</i>"
                    },


               
             ],
             columnDefs: [
                 { "targets": [0], "searchable": false, "visible": false },
               { className: "text-left", "targets": [1,2,4] },
               { className: "text-center", "targets": [3,5,6,7] },            
            
             ]

         });
        $('.advance-filter').on('change', function () {
            FilterContent();
        });

        $(".buttons-excel").hide();
        startdate = $("#todate").val();
        enddate = $("#fromdate").val();
        FilterContent();

    } catch (x) {

        notyAlert('error', x.message);

    }

});



//--Advanced search--//
function FilterContent() {
    debugger;
    var FromDate = $('#fromdate');
    var ToDate = $('#todate');
    var ReqStatus = $('#ddlReqStatus');
    var RequisitionBy = $('#ddlRequisitionBy');
    var ApprovalStatus = $('#ddlApprovalStatus');  
    var Search = $('#Search');
    var ReptAdvanceSearch = new Object();
    ReptAdvanceSearch.FromDate = FromDate[0].value !== "" ? FromDate[0].value : null;
    ReptAdvanceSearch.ToDate = ToDate[0].value !== "" ? ToDate[0].value : null;
    ReptAdvanceSearch.ReqStatus = ReqStatus[0].value !== "" ? ReqStatus[0].value : (RequisitionStatus !== "" ? RequisitionStatus : null);
    ReptAdvanceSearch.RequisitionBy = RequisitionBy[0].value !== "" ? $("#ddlRequisitionBy option:selected").text() :"";
    ReptAdvanceSearch.ApprovalStatus = ApprovalStatus[0].value !== "" ? ApprovalStatus[0].value : (ApprovalStatusList !== "" ? ApprovalStatusList : null);
    ReptAdvanceSearch.Search = Search[0].value !== "" ? Search[0].value : null;
    ReptAdvanceSearch.ManagerApproved = ManagerApproved;
    ReptAdvanceSearch.FinalApproved = FinalApproved;
    DataTables.RequisitionReportTable.clear().rows.add(GetRequisitionDetails(ReptAdvanceSearch)).draw(false);
    ManagerApproved = false;
    FinalApproved = false;
}





//--function to get requisition details from server corresponding to from date ,to date,Req status and search --//
function GetRequisitionDetails(AdvanceSearchObject) {
    try {
        debugger;
        if (AdvanceSearchObject === 0) {
            var data = {};
        }
        else {
            var data = { "AdvanceSearchObject": JSON.stringify(AdvanceSearchObject) };
        }
        var ds = {};
        ds = GetDataFromServer("Report/GetRequisitionDetails/", data);
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
        notyAlert('error', e.message);
    }
}



//--Function for print button to print--//
function PrintReport() {
    try {

        $(".buttons-excel").trigger('click');
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
    }
}

//--function on back button click--//
function Back() {
    window.location = appAddress + "Report/Index/";
}

//Function on Reset button click--//
function Reset() {
    $("#todate").val(startdate);
    $("#fromdate").val(enddate);
    $("#ddlReqStatus").val('');
    $("#ddlApprovalStatus").val('');
    $('#ddlRequisitionBy').val('ALL');
    $('#Search').val('');
    FilterContent();
}

