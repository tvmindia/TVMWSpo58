var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {   
        debugger;
        DataTables.CourierReportTable = $('#courierDetailTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching:false,
             paging:false,
             data: GetCourierDetails(),
             pageLength: 10,
             //language: {
             //    search: "_INPUT_",
             //    searchPlaceholder: "Search"
             //},
             columns: [              
               { "data": "Type", "defaultContent": "<i>-</i>" },
               { "data": "TransactionDate", "defaultContent": "<i>-</i>" },
               { "data": "Track", "defaultContent": "<i>-</i>" },
               { "data": "SourceName", "defaultContent": "<i>-</i>" },
               { "data": "DestName", "defaultContent": "<i>-</i>" },
               { "data": "courierAgency.Name", "defaultContent": "<i>-</i>" },
               { "data": "TrackingRefNo", "defaultContent": "<i>-</i>" },              
             ],
             columnDefs: [
                 { "targets": [0], "searchable": true },
               { className: "text-left", "targets": [0,1, 2, 3, 4, 5, 6] }
             ]
         });

        debugger;
        startdate = $("#todate").val();
        enddate = $("#fromdate").val();

    } catch (x) {

        notyAlert('error', x.message);

    }

});

function GetCourierDetails() {
    try {

        debugger;
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
        debugger;     
       
        var fromdate = $("#fromdate").val();
        var todate = $("#todate").val();
        var AgencyCode = $("#AgencyCode").val();
        var Type = $("#Type").val();
      
        if (DataTables.CourierReportTable != undefined && IsVaildDateFormat(fromdate) && IsVaildDateFormat(todate)) {
            DataTables.CourierReportTable.clear().rows.add(GetCourierDetails()).draw(true);
        }
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}
