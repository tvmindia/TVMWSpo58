
//CreatedDate: 21-Nov-2017 Tuesday
//LastModified: 21-nov-2017 Tuesday
//FileName: CourierReport.js
//Description: Client side coding for CourierReport
//******************************************************************************
//******************************************************************************
//Global Declarations
var DataTables = {};
var startdate = '';
var enddate = '';
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {   
        DataTables.CourierReportTable = $('#courierDetailTable').DataTable(
         {
             dom: '<"pull-right"Bf>rt<"bottom"ip><"clear">',
             buttons: [{
                 extend: 'excel',
                 exportOptions:
                              {
                                  columns: [1, 2, 3,5,6,7,8]
                              }
             }],
             order: [],
             searching:false,
             paging:true,
             data: GetCourierDetails(),
             pageLength:50,
             //language: {
             //    search: "_INPUT_",
             //    searchPlaceholder: "Search"
             //},
             columns: [
                 {"data":"ID","defaultContent":"<i>-</i>"},
               { "data": "TrackingRefNo", "defaultContent": "<i>-</i>" },
               { "data": "Type","defaultContent": "<i>-</i>" },
               { "data": "TransactionDate", "defaultContent": "<i>-</i>" },
               { "data": "Track", "defaultContent": "<i>-</i>" },
               { "data": "SourceName", "defaultContent": "<i>-</i>" },
               { "data": "DestName", "defaultContent": "<i>-</i>" },
               { "data": "courierAgency.Name", "defaultContent": "<i>-</i>" },
               {
                    "data": "DistributionDate", render: function (data, type, row) {
                        if (row.Type == 'Outbound' && data == null)
                        { return 'N/A'; }
                        else if (row.Type == 'Outbound' && data != '' || row.Type=='Inbound')
                            return data;
                        else
                            return '-';

                       
                    }, "defaultContent": "<i>-</i>"
                },
                             
             ],
             columnDefs: [
                 { "targets": [0], "searchable":false, "visible":false },
               { className: "text-left", "targets": [0,1, 2, 3, 4, 5, 6,7] }
             ]
         });

        $(".buttons-excel").hide();
        startdate = $("#todate").val();
        enddate = $("#fromdate").val();

    } catch (x) {

        notyAlert('error', x.message);

    }

});

function GetCourierDetails() {
    try {

        var fromdate = $("#fromdate").val();
        var todate = $("#todate").val();
        var AgencyCode = $("#AgencyCode").val();
        var search = $("#Search").val();
        var Type = $("#Type").val();

        if (IsVaildDateFormat(fromdate) && IsVaildDateFormat(todate)) {
            var data = { "FromDate": fromdate, "ToDate": todate,"AgencyCode": AgencyCode,"search":search,"Type":Type };
            var ds = {};
            ds = GetDataFromServer("Report/GetCourierDetails/", data);
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
    }
    catch (e) {
        notyAlert('error', e.message);
    }
 }
 


function RefreshCourierDetailTable() {
    try {
       
        var fromdate = $("#fromdate").val();
        var todate = $("#todate").val();
        var AgencyCode = $("#AgencyCode").val();
        var search = $("#Search").val();
        var Type = $("#Type").val();
      
        if (DataTables.CourierReportTable != undefined && IsVaildDateFormat(fromdate) && IsVaildDateFormat(todate)) {
            DataTables.CourierReportTable.clear().rows.add(GetCourierDetails()).draw(true);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}


//back to report page
function Back() {
    window.location = appAddress + "Report/Index/";
}


//To print report
function PrintReport() {
    try {
        $(".buttons-excel").trigger('click');
    }
    catch (e) {
        console.log(e.message);
    }
}

//To reset courier report


function Reset() {
    $("#todate").val(startdate);
    $("#fromdate").val(enddate);
    $("#AgencyCode").val('ALL').trigger('change');
    $("#Search").val('').trigger('change');
    $("#Type").val('ALL').trigger('change');;
    RefreshCourierDetailTable();
}



function ChangeOnsearch()
{
    RefreshCourierDetailTable();
}