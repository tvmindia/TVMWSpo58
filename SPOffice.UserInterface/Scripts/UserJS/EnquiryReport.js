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
        $("#ProductList").select2({
            multiple: true,
            placeholder: "Select a Product..",
        });
        DataTables.EnquiryReportTable = $('#enquiryReportTable').DataTable(
         {

             dom: '<"pull-right"Bf>rt<"bottom"ip><"clear">',
             buttons: [{
                 extend: 'excel',
                 exportOptions:
                              {
                                  columns: [ 1, 2, 3, 6,9,11,13,14,16,17]
                              }
             }],
             order: [],
             fixedHeader: {
                 header: true
             },
             searching: false,
             paging: true,
             data: GetEnquiryReportDetails(),
             pageLength: 50,
             //language: {
             //    search: "_INPUT_",
             //    searchPlaceholder: "Search"
             //},
             columns: [
                { "data": "ID", "defaultContent": "<i>-</i>", "width": "1%" },
               { "data": "EnquiryNo", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "EnquiryDate", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Subject", "defaultContent": "<i>-</i>", "width": "9%" },
               { "data": "ContactTitle", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "ContactName", render: function (data, type, row) { return row.ContactTitle + ' ' + row.ContactName }, "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "CompanyName", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Address", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Website", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Email", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "LandLine", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Mobile", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Fax", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "EnquirySource", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Industry", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "GeneralNotes", "defaultContent": "<i>-</i>", "width": "10%" },
               { "data": "EnquiryStatus", "defaultContent": "<i>-</i>", "width": "5%" },
               { "data": "Product" ,
               render: function (data, type, row)
                  
               {
                   
                   return '<span style="font-size : 10px">' + (data == null ? '-' : data )+ '</span>'
               },
               "defaultContent": "<i>-</i>", "width": "10%" },

             ],
             
             columnDefs: [{  "searchable": false ,"targets": [0,4,5,7,8,10,12,15], "visible": false,},
                  { className: "text-left", "targets": [1, 2,3,4,6,13,14,16,17] },
                  { className: "text-right", "targets": [7,8,10,12,15] },
                  { className: "text-center", "targets": [0, 5, 9, 11] }],
         
            
         });
        //hiding inbuilt export buttonand placing start date and end date
        $(".buttons-excel").hide();
        startdate = $("#todate").val();
        enddate = $("#fromdate").val();

    } catch (x) {
        //this will show the error msg in the browser console(F12) 
        console.log(e.message);

    }
});

//--function to get Enquiry list from server corresponding to from date ,to date,status and search--//
function GetEnquiryReportDetails() {
    try {
        debugger;

         var fromdate = $("#fromdate").val();
         var todate = $("#todate").val();
         var statusList = $("#statusList").val();
         var search = $("#Search").val();
         //var Product = $("#ProductList");
         var Product = ($("#ProductList").val() != "" ? $("#ProductList").val().toString() : null);
    
    
         if (IsVaildDateFormat(fromdate) && IsVaildDateFormat(todate) && statusList) {
             var data = { "FromDate": fromdate, "ToDate": todate , "EnquiryStatus": statusList, "search": search, "Product":Product};
       
        var ds = {};
        ds = GetDataFromServer("Report/GetEnquiryDetails/", data);
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

//--Function on date change--//
function OnChangeCall() {
    RefreshEnquiryReportDetailsTable();

}

//--function to refresh EnquiryReport Table---//
function RefreshEnquiryReportDetailsTable() {
    try {
        var fromdate = $("#fromdate").val();
        var todate = $("#todate").val();
        var statusList = $("#statusList").val();

        if (DataTables.EnquiryReportTable != undefined && IsVaildDateFormat(fromdate) && IsVaildDateFormat(todate) ) {
            DataTables.EnquiryReportTable.clear().rows.add(GetEnquiryReportDetails()).draw(true);
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
    $("#statusList").val('ALL').trigger('change');
    $("#Search").val('').trigger('change');
    $("#ProductList").val('').trigger('change');
    RefreshEnquiryReportDetailsTable()
}