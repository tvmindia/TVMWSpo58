
//CreatedDate: 23-Nov-2017 Thursday
//LastModified: 23-nov-2017 Thursday
//FileName: CustomerPOReport.js
//Description: Client side coding for CustomerPoReport
//******************************************************************************
//******************************************************************************
//Global Declarations
var DataTables = {};
var startdate = '';
var enddate = '';
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {   
        DataTables.CustomerPOReportTable = $('#customerDetailTable').DataTable(
         {
             dom: '<"pull-right"Bf>rt<"bottom"ip><"clear">',
             buttons: [{
                 extend: 'excel',
                 exportOptions:
                              {
                                  columns: [1,2,3,4,5,6]
                              }
             }],
             order: [],
             fixedHeader: {
                 header: true
             },
             searching:false,
             paging:true,
             data:null ,
             pageLength:50,
             
             columns: [
                 { "data": "CustomerPOObj.ID", "defaultContent": "<i>-</i>" },
               { "data": "CustomerPOObj.PONo", "defaultContent": "<i>-</i>" },
               { "data": "CustomerPOObj.PODate", "defaultContent": "<i>-</i>" },
               { "data": "CustomerPOObj.customer.CompanyName", "defaultContent": "<i>-</i>" },
               { "data": "CustomerPOObj.company.Name", "defaultContent": "<i>-</i>" },
               { "data": "CustomerPOObj.POStatus", "defaultContent": "<i>-</i>" },
               //{ "data": "CustomerPOObj.GrossAmount", "defaultContent": "<i>-</i>" },
               {
                   "data": "CustomerPOObj.Amount", render: function (data, type, row) {
                       return roundoff(data, 1);
                   }, "defaultContent": "<i>-</i>"
               }
             ],
             columnDefs: [
                 { "targets": [0], "searchable": false, "visible": false },
               { className: "text-left", "targets": [0, 1,3,4,5] },
               { className: "text-center", "targets": [2] } ,
             { className: "text-right", "targets": [6] },
             { "width": "10%", "targets": [6] }
             
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
    var POStatus = $('#ddlPOStatus');
    var Customer = $('#ddlCustomer');
    var Company = $('#ddlCompany');
    var Search = $('#Search');
    var ReptAdvanceSearch = new Object();
    ReptAdvanceSearch.FromDate = FromDate[0].value !== "" ? FromDate[0].value : null;
    ReptAdvanceSearch.ToDate = ToDate[0].value !== "" ? ToDate[0].value : null;
    ReptAdvanceSearch.POStatus = POStatus[0].value !== "" ? POStatus[0].value : (POStatus !== "" ? POStatus : null);
    ReptAdvanceSearch.Customer = Customer[0].value !== "" ? Customer[0].value : (Customer !== "" ? Customer : null);
    ReptAdvanceSearch.Company = Company[0].value !== "" ? Company[0].value : (Company !== "" ? Company : null);
    ReptAdvanceSearch.Search = Search[0].value !== "" ? Search[0].value : null;
    DataTables.CustomerPOReportTable.clear().rows.add(GetCustomerPODetails(ReptAdvanceSearch)).draw(false);
}





//--function to get CustomerPO details from server corresponding to from date ,to date,from company,POstatus and search --//
function GetCustomerPODetails(AdvanceSearchObject) {
    try {
        debugger;
        if (AdvanceSearchObject === 0) {
            var data = {};
        }
        else {
            var data = { "AdvanceSearchObject": JSON.stringify(AdvanceSearchObject) };
        }
        var ds = {};
        ds = GetDataFromServer("Report/GetCustomerPODetails/", data);
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
    $("#ddlCompany").val('ALL');
    $("#ddlPOStatus").val('ALL');
    $("#ddlCustomer").val('ALL');
    $('#Search').val('');
    FilterContent();
}

