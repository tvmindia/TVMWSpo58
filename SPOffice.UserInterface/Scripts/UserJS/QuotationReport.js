
//*****************************************************************************
//*****************************************************************************
//Author: Jais
//CreatedDate: 21-Nov-2017 Tuesday
//LastModified: 21-nov-2017 Tuesday
//FileName: QuotationReport.js
//Description: Client side coding for QuotationReport
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
                                  columns: [1, 2, 3,4,5, 6, 7]
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
                  { className: "text-left", "targets": [ 2, 3, 4, 5,6,7] },
                  { className: "text-right", "targets": [] },
                  { className: "text-center", "targets": [0,1] }],

         });
        $('.advance-filter').on('change', function () {
            FilterContent();
        });
        //---hiding inbuilt export buttonand placing start date and end date
        $(".buttons-excel").hide();
        startdate = $("#todate").val();
        enddate = $("#fromdate").val();
        FilterContent();

    } catch (x) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);

    }
});

//--Advanced search--//
function FilterContent() {
    debugger;
    var FromDate = $('#fromdate');
    var ToDate = $('#todate');
    var FromCompany = $('#ddlFromCompany');
    var QuoteStage = $('#ddlQuoteStage');
    var Search = $('#Search');
    var ReptAdvanceSearch = new Object();
    ReptAdvanceSearch.FromDate = FromDate[0].value !== "" ? FromDate[0].value : null;
    ReptAdvanceSearch.ToDate = ToDate[0].value !== "" ? ToDate[0].value : null;
    ReptAdvanceSearch.FromCompany = FromCompany[0].value !== "" ? FromCompany[0].value : (FromCompany !== "" ? FromCompany : null);
    ReptAdvanceSearch.QuoteStage = QuoteStage[0].value !== "" ? QuoteStage[0].value : (QuoteStage !== "" ? QuoteStage : null);
    ReptAdvanceSearch.Search = Search[0].value !== "" ? Search[0].value : null;
    DataTables.QuotationReportTable.clear().rows.add(GetQuotationReportDetails(ReptAdvanceSearch)).draw(false);
}

//--function to get Quotation details from server corresponding to from date ,to date,from company and search--//
function GetQuotationReportDetails(AdvanceSearchObject) {
    try {
        debugger;
            if (AdvanceSearchObject === 0)
            {                
                var data = {};
            }
            else
            {
                var data = { "AdvanceSearchObject": JSON.stringify(AdvanceSearchObject) };
            } 
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
    debugger;
    $("#todate").val(startdate);
    $("#fromdate").val(enddate);
    $("#ddlFromCompany").val('ALL');
    $("#ddlQuoteStage").val('ALL');
    $('#Search').val('');
    FilterContent();
}
