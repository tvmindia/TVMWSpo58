
//*****************************************************************************
//*****************************************************************************
//Author: Jais
//CreatedDate: 21-Nov-2017 Tuesday
//LastModified: 21-nov-2017 Tuesday
//FileName: EnquiryReport.js
//Description: Client side coding for EnquiryReport
//******************************************************************************
//******************************************************************************
//Global Declarations
var DataTables = {};
var startdate = '';
var enddate = '';
//This will fire on page loads
$(document).ready(function () {
    debugger;

    try {

        DataTables.QuotationReportTable = $('#quotationReportTable').DataTable(
         {

             dom: '<"pull-right"Bf>rt<"bottom"ip><"clear">',
             buttons: [{
                 extend: 'excel',
                 exportOptions:
                              {
                                  columns: [1, 2, 3, 6, 9, 11, 13, 14, 16]
                              }
             }],
             order: [],
             searching: false,
             paging: true,
             data: GetQuotationReportDetails(),
             pageLength: 50,
             //language: {
             //    search: "_INPUT_",
             //    searchPlaceholder: "Search"
             //},
             columns: [
                { "data": "ID" },
               { "data": "QuotationDate", "defaultContent": "<i>-</i>" },
               { "data": "QuotationNo", "defaultContent": "<i>-</i>" },
               { "data": "QuoteSubject", "defaultContent": "<i>-</i>" },
               { "data": "CompanyName", "defaultContent": "<i>-</i>" },
               { "data": "ContactPerson", "defaultContent": "<i>-</i>" },
               { "data": "QuoteFromCompName", "defaultContent": "<i>-</i>" },
               { "data": "QuoteStage", "defaultContent": "<i>-</i>" }

             ],

             columnDefs: [{ "searchable": false, "targets": [0], "visible": false, },
                  { className: "text-left", "targets": [1, 2, 3, 4, 6] },
                  { className: "text-right", "targets": [7] },
                  { className: "text-center", "targets": [0, 5] }],

         });
        //---hiding inbuilt export buttonand placing start date and end date
        //$(".buttons-excel").hide();
        startdate = $("#todate").val();
        enddate = $("#fromdate").val();

    } catch (x) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);

    }
});

//--function to get Enquiry list from server corresponding to from date ,to date,status and search--//
function GetQuotationReportDetails() {
    try {
        debugger;

        var fromdate = $("#fromdate").val();
        var todate = $("#todate").val();
        //var statusList = $("#statusList").val();
        //var search = $("#Search").val();
        //if (IsVaildDateFormat(fromdate) && IsVaildDateFormat(todate) && statusList) {
        var data = { "FromDate": fromdate, "ToDate": todate};//, "EnquiryStatus": statusList, "search": search };
            //var data={};
            var ds = {};
            ds = GetDataFromServer("Report/GetQuotationDetails/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }

            if (ds.Result == "OK") {
                return ds.Records;
            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.Message);
            }
        //}
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

//--Function on date change--//
function OnChangeCall() {
    RefreshQuotationReportTable();

}

//--function to refresh EnquiryReport Table---//
function RefreshQuotationReportTable() {
    try {
        var fromdate = $("#fromdate").val();
        var todate = $("#todate").val();
        //var statusList = $("#statusList").val();

        if (DataTables.QuotationReportTable != undefined && IsVaildDateFormat(fromdate) && IsVaildDateFormat(todate)) {
            DataTables.QuotationReportTable.clear().rows.add(GetQuotationReportDetails()).draw(true);
        }
    }
    catch (e) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);
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
    debugger;
    $("#todate").val(startdate);
    $("#fromdate").val(enddate);
    //$("#statusList").val('ALL').trigger('change');
   // $("#Search").val('').trigger('change');
    RefreshQuotationReportTable()
}
