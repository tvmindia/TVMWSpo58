﻿
//--Function to move from Dashboard to Quotation index page with URL ---//
function OpenQuotationSummaryDashboard(filter) {
    var url = '../Quotation/Index/__id__';
    window.location.href = url.replace('__id__', filter);
    
}

//--Function to move from Dashboard to Customer PO index page with URL ---//
function OpenCustomerOrderDashboard(filter) {
    var url = '../CustomerOrder/Index/__id__';
    window.location.href = url.replace('__id__', filter);
}

//--Function to move from Dashboard to Supplier Order index page with URL ---//
function OpenSupplierOrderDashboard(filter) {
    var url = '../DynamicUI/UnderConstruction/__id__';
    window.location.href = url.replace('__id__', filter);
}

//--Function to move from Dashboard to Quotation edit page with URL  ---//
function OpenQuotationDashboardfilter(filter) {
   
    var url = '../Quotation/Index/__id__';
    window.location.href = url.replace('__id__', filter);
}

//--Function to move from Dashboard to CustomerPo edit page with URL  ---//
function OpenCustomerPODashboardfilter(filter) {
   
    var url = '../CustomerOrder/Index/__id__';
    window.location.href = url.replace('__id__', filter);
}

