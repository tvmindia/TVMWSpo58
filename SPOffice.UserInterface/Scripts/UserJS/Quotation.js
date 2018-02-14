var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var _Products = [];
var _Units = [];
var _QuoteProductDetail = [];
var _QuoteProductList = [];
var footer = "Terms and Conditions" + "\n"

"Payment terms - 50% advance payment before production and balance 50% before delivery." + "\n"
"Please accept the same and confirm." + "\n"
"Awaiting your valuable order." + "\n" + "\n"


"Regards," + "\n"
"Manager";

$(document).ready(function () {
    try {
        debugger;
        //For implementating select2
        $("#ddlCustomer").select2({
        });
        $("#ddlProductSearch").select2({ dropdownParent:$("#AddQuotationItemModal") });
        $('#btnUpload').click(function () {
            //Pass the controller name
            var FileObject = new Object;
            if ($('#hdnFileDupID').val() != emptyGUID) {
                FileObject.ParentID = (($('#ID').val()) != emptyGUID ? ($('#ID').val()) : $('#hdnFileDupID').val());
            }
            else {
                FileObject.ParentID = ($('#ID').val() == emptyGUID) ? "" : $('#ID').val();
            }
           

            FileObject.ParentType = "Quotation";
            FileObject.Controller = "FileUpload";
            UploadFile(FileObject);
        });
        DataTables.QuotationTable = $('#QuotationTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllQuotations(),
             pageLength: 15,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "ID" },
               { "data": "QuotationDate", "defaultContent": "<i>-</i>" },
               { "data": "QuotationNo", "defaultContent": "<i>-</i>" },
               { "data": "QuoteSubject", "defaultContent": "<i>-</i>" },
               { "data": "customer.CompanyName", "defaultContent": "<i>-</i>" },
               { "data": "ContactPerson", "defaultContent": "<i>-</i>" },
               { "data": "company.Name","defaultContent": "<i>-</i>" },
               { "data": "quoteStage.Description", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },
                  { className: "text-right", "targets": [] },
                   { className: "text-left", "targets": [2,3,4,5,6,7] },
             { className: "text-center", "targets": [1,8] }

             ]
         });

        $('#QuotationTable tbody').on('dblclick', 'td', function () {
            Edit(this);
        });
        $('input[type="text"].Roundoff').on('focus', function () {
            $(this).select();
        });

        DataTables.ItemDetailsTable = $('#ItemDetailsTable').DataTable(
            {
                dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
                order: [],
                searching: false,
                paging: true,
                data: null,
                language: {
                    search: "_INPUT_",
                    searchPlaceholder: "Search"
                },
                columns: [
                { "data": "ID", "defaultContent": "<i></i>" },
                { "data": "ProductID", "defaultContent": "<i></i>" },
                { "data": "ProductCode", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
                { "data": "OldProductCode", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
                { "data": "ProductDescription", render: function (data, type, row) { return data }, "defaultContent": "<i></i>" },
                {"data" : "Quantity",render:function(data,type,row){return data},"defaultContent":"<i></i>"},
                { "data": "Rate", render: function (data, type, row) { return roundoff(data) }, "defaultContent": "<i></i>" },
                { "data": null, "orderable": false, "defaultContent": '<a href="#" class="DeleteLink"  onclick="Delete(this)" ><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a> | <a href="#" class="actionLink"  onclick="ProductEdit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' },
                ],
                columnDefs: [{ "targets": [0, 1], "visible": false, "searchable": false },
                    { "targets": [2, 3, 5], "width": "15%" },
                    { "targets": [6], "width": "8%" },
                     { className: "text-right", "targets": [5,6] },
                      { className: "text-left", "targets": [2,3,4] },
                { className: "text-center", "targets": [7] }
                ]
            }); 

        //--checking hidden field filter value--//
        if ($('#filter').val() != '') {
            dashboardBind($('#filter').val())
        } 
        if ($('#BindValue').val() != '') {
            debugger;
            quotationBind($('#BindValue').val())
        }
        //if ($('#filter').val() != '') {
        //    dashboardBind($('#filter').val())
        //}
        $('#btnDownload').hide();
    }
    catch (x) {
        notyAlert('error', x.message);
    } 
}); 


function BindAllQuotes() {
    try {
        DataTables.QuotationTable.clear().rows.add(GetAllQuotations()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function Delete(curobj) {
    debugger;
    var rowData = DataTables.ItemDetailsTable.row($(curobj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
            notyConfirm('Are you sure to delete?', 'DeleteItem("' + rowData.ID + '")');
    }
    else {
        DataTables.ItemDetailsTable.row($(curobj).parents('tr')).remove().draw(false);
        notyAlert('success', 'Deleted Successfully');
    }
}


    function DeleteItem(ID) {


        try {
            debugger;
           //Event Request Case 
                var data = { "ID": ID };
                var ds = {};
                ds = GetDataFromServer("Quotation/DeleteItemByID/", data);
                if (ds != '') {
                    ds = JSON.parse(ds);
                }
                if (ds.Result == "OK") {
                    switch (ds.Result) {
                        case "OK":
                            notyAlert('success', ds.Record.Message);
                            BindQuationDetails($("#ID").val());
                            break;
                        case "ERROR":
                            notyAlert('error', ds.Message);
                            break;
                        default:
                            break;
                    }
                    return ds.Record;
                }

            
        }
        catch (e) {
            notyAlert('error', e.message);
        }
    }

    function saveInvoices() {
        debugger;
        $("#DetailJSON").val('');
        _QuoteProductList = [];
        AddQuoteProductList();
        if (_QuoteProductList.length > 0) {
            var result = JSON.stringify(_QuoteProductList);
            $("#DetailJSON").val(result);
            $('#btnSave').trigger('click');
        }
        else {
            notyAlert('warning', 'Please Add Product Details!');
        }
    }



    function QuotationSaveSuccess(data, status) {
        debugger;
        var JsonResult = JSON.parse(data)
        switch (JsonResult.Result) {
            case "OK":

                notyAlert('success', JsonResult.Record.Message);
                ChangeButtonPatchView('Quotation', 'btnPatchAdd', 'Edit');
                if (JsonResult.Record.ID) {
                    $("#ID").val(JsonResult.Record.ID);
                    $('#deleteId').val(JsonResult.Record.ID);
                }
                BindAllQuotes();
                BindQuationDetails($("#ID").val());
                break;
            case "ERROR":
                notyAlert('error', JsonResult.Message);
                break;
            default:
                notyAlert('error', JsonResult.Message);
                break;
        }
    }

    //To Reset Quotation Form
    function Resetform() {
        debugger;
        var validator = $("#QuoteForm").validate();
        $('#QuoteForm').find('.field-validation-error span').each(function () {
            validator.settings.success($(this));
        });
        $('#QuoteForm')[0].reset();
        DataTables.ItemDetailsTable.clear().draw(false);
    }





    function Edit(Obj) {
        debugger;
        $('#QuoteForm')[0].reset();
        var rowData = DataTables.QuotationTable.row($(Obj).parents('tr')).data();
        $('#ID').val(rowData.ID);
        $('#deleteId').val(rowData.ID);
        $('#QuoteMAilHeaderID').val(rowData.ID);

        BindQuationDetails(rowData.ID);
        ChangeButtonPatchView('Quotation', 'btnPatchAdd', 'Edit');
        openNav();
    }
    function PreviewMail()
    {
        try
        {            
            var QHID = $("#ID").val();
            if (QHID) {
                //Bind mail html into model
                GetMailPreview(QHID);
                $("#MailPreviewModel").modal('show');
            }
        }
        catch(e)
        {
            notyAlert('error', e.Message);
        }
    }
    function GetMailPreview(ID) {
     
        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Quotation/GetMailPreview/", data);
        if (ds == "Nochange") {
            return; 0
        }
        $("#mailmodelcontent").empty();
        $("#mailmodelcontent").html(ds);
        $("#MailBody").val(ds);

    }


    function BindQuationDetails(ID)
    {
        try
        {
            var jsresult = GetQuationDetailsByID(ID)
            if(jsresult)
            {
                //bind
                $("#txtQuotationNo").val(jsresult.QuotationNo);
                $("#QuotationDate").val(jsresult.QuotationDate);
                $("#ValidTillDate").val(jsresult.ValidTillDate);
                if (jsresult.CustomerID != null) {
                    $("#ISRegularCustomer").val('REG')
                    CustomerTypeChange();
                    $("#ddlCustomer").select2();
                    $("#ddlCustomer").val(jsresult.CustomerID).trigger('change');
                }
                else {
                    $("#ISRegularCustomer").val('NEW')
                    $("#NewCustomer").val(jsresult.customer.CompanyName)
                    CustomerTypeChange()
                }
                $("#SentToAddress").val(jsresult.SentToAddress);
                $("#ContactPerson").val(jsresult.ContactPerson);
                $("#ddlSalesPerson").val(jsresult.SalesPersonID);
                $("#ddlCompany").val(jsresult.company.Code);
                $("#ddlQuoteStage").val(jsresult.quoteStage.Code);
                $("#QuoteSubject").val(jsresult.QuoteSubject);
                $("#QuoteBodyHead").val(jsresult.QuoteBodyHead);

                $("#GrossAmount").val(jsresult.GrossAmount);
                $("#Discount").val(jsresult.Discount);
                $("#NetTaxableAmount").val(jsresult.NetTaxableAmount);
                $("#TaxTypeCode").val(jsresult.TaxTypeCode);
                $("#TaxPercApplied").val(jsresult.TaxPercApplied);
                $("#TaxAmount").val(jsresult.TaxAmount);
                $("#TotalAmount").val(jsresult.TotalAmount);
                $("#QuoteBodyFoot").val(jsresult.QuoteBodyFoot);
                $("#GeneralNotes").val(jsresult.GeneralNotes);

                $("#lblQuoteStage").text(jsresult.quoteStage.Description);
                $("#lblEmailSent").text(jsresult.EmailSentYN == "True" ? 'YES' : 'NO');
                if (jsresult.SentToEmails != null)
                    $('#SentToEmails').val(jsresult.SentToEmails);
                else
                $('#SentToEmails').val(jsresult.customer.ContactEmail);
                $("#lblQuotationNo").text(jsresult.QuotationNo);
                //bind details here
                GetQuoteItemsList(ID)
                clearUploadControl();
                PaintImages(ID);
            }

        }
        catch(e)
        {
            notyAlert('error', e.Message);
        }
    }

    function GetQuationDetailsByID(ID) {
        try {

            var data = { "ID": ID };
            var ds = {};
            ds = GetDataFromServer("Quotation/GetQuationDetailsByID/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                return ds.Record;
            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.Message);

            }
        }
        catch (e) {
            notyAlert('error', e.message);
        }
    }
    function AddNew() {
        ChangeButtonPatchView('Quotation', 'btnPatchAdd', 'Add');
        Resetform();
        openNav(); 
        $('#ID').val('');
        $("#DetailJSON").val('');
        //Reset();  
        CustomerTypeChange();
        $("#ddlCustomer").select2();
        $("#ddlCustomer").val('').trigger('change');
        $("#ddlQuoteStage").val('DFT');
        $("#lblQuoteStage").text('Draft');
        $("#lblEmailSent").text('No');
        $("#lblQuotationNo").text('New Quotation');
        $("#txtQuotationNo").focus();
        clearUploadControl();  
        $("#QuoteBodyFoot").val(footer);
    }

    function Reset() {
        BindQuationDetails($('#ID').val());
        $("#QuoteBodyFoot").val(footer);
    }

    //---------------Bind logics-------------------
    function GetAllQuotations(filter) {
        try {
            debugger;
            var data = { "filter": filter };
            var ds = {};
            ds = GetDataFromServer("Quotation/GetAllQuotations/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                BindSummarBox(ds.Draft, ds.Converted, ds.Negotiation, ds.Lost);
                return ds.Records;
            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.message);
            }
        }
        catch (e) {
            //this will show the error msg in the browser console(F12) 
            console.log(e.message);

            //notyAlert('error', e.message);
        }
    }

    //--function to place Counts on Tiles--//
    function BindSummarBox(Draft, Converted, Negotiation, Lost) {
        debugger;
        $("#draftCount").text(Draft);
        $("#negotiationCount").text(Negotiation);
        $("#convertedCount").text(Converted);
        $("#lostCount").text(Lost);
        //--To place discription--//
        $("#draftCountDescription").text(Draft + 'Draft Quotation(s)');
        $("#negotiationCountDescription").text(Negotiation + ' Negotiation Quotation(s)');
        $("#convertedCountDescription").text(Converted + ' Converted Quotation(s)');
        $("#lostCountDescription").text(Lost + ' Lost Quotation(s)');

    }

    function GetQuoteItemsList(ID) {
        DataTables.ItemDetailsTable.clear().rows.add(GetAllQuoteItems(ID)).draw(false);
    }

    function GetAllQuoteItems(ID) {
        try {
      
            var data = { "ID": ID };
            var ds = {};
            ds = GetDataFromServer("Quotation/GetQuateItemsByQuateHeadID/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                return ds.Records;
            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.message);
            }
        }
        catch (e) {
            //this will show the error msg in the browser console(F12) 
            console.log(e.message);

        }
    }

    function GetAllProductCodes()
    {
        try {

            var data = {};
            var ds = {};
            ds = GetDataFromServer("Quotation/GetAllProductCodes/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                _Products = ds.Records;
                return ds.Records;
            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.message);
            }
        }
        catch (e) {
            //this will show the error msg in the browser console(F12) 
            console.log(e.message);
        }
    }

    function GetAllUnitCodes() {
        try {

            var data = {};
            var ds = {};
            ds = GetDataFromServer("Quotation/GetAllUnitCodes/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                _Units = ds.Records;
                return ds.Records;
            }
            if (ds.Result == "ERROR") {
                notyAlert('error', ds.message);
            }
        }
        catch (e) {
            //this will show the error msg in the browser console(F12) 
            console.log(e.message);
        }
    }

    function ValidateEmail()
    {
        var ste = $('#SentToEmails').val();
        if (ste)
        {
            var atpos = ste.indexOf("@");
            var dotpos = ste.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= ste.length)
            {
                notyAlert('error', 'Invalid Email');
                return false;
            }
                //not valid

            else
            {
                $("#MailPreviewModel").modal('hide');
                showLoader();
                return true;
            }

        }

        else
            notyAlert('error', 'Enter email address');
        return false;
    }

    function MailSuccess(data, status) {
        hideLoader();
        var JsonResult = JSON.parse(data)
        switch (JsonResult.Result) {
            case "OK":

                notyAlert('success', JsonResult.Message);
                switch (JsonResult.MailResult)
                {
                    case 1:
                        $("#lblEmailSent").text('YES');
                        break;
                    case 0:
                        $("#lblEmailSent").text('NO');
                        break;
                }

                break;
            case "ERROR":
                notyAlert('error', JsonResult.Message);
                break;
            default:
                notyAlert('error', JsonResult.Message);
                break;
        }
    }


    function SendMailClick()
    {
      var QMH = $('#ID').val();
      $('#QuoteMAilHeaderID').val(QMH);   
      $('#btnFormSendMail').trigger('click');         
    }

    function GetCustomerDeails(curobj) {
        var customerid = $(curobj).val();
        if (customerid) {
            var data = { "ID": customerid };
            var ds = {};
            ds = GetDataFromServer("CustomerOrder/GetCustomerDetailsByID/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {

                $("#SentToAddress").val(ds.Record.BillingAddress);
                $("#ContactPerson").val(ds.Record.ContactPerson);
                $("#SentToEmails").val(ds.Record.ContactEmail);
                return ds.Record;
            }
            if (ds.Result == "ERROR") {
                return 0;
            }
        }

    }

    //Delete Quotation
    function DeleteClick() {
        debugger;
        notyConfirm('Are you sure to delete?', 'DeleteQuotation()');
    }

    function DeleteQuotation() {
        try {
            debugger;
            var id = $('#ID').val();
            if (id != '' && id != null) {
                var data = { "ID": id };
                var ds = {};
                ds = GetDataFromServer("Quotation/DeleteQuotation/", data);
                if (ds != '') {
                    ds = JSON.parse(ds);
                }
                if (ds.Result == "OK") {
                    notyAlert('success', ds.Record.Message);
                    debugger;
                    BindAllQuotes();
                    closeNav();
                }
                if (ds.Result == "ERROR") {
                    notyAlert('error', ds.Message);
                    return 0;
                }
                return 1;
            }
        }
        catch (e) {
            notyAlert('error', e.message);
            return 0;
        }
    }

    // change quotestatus of label on dropdown selection
    function ChangeQuoteStatus()
    {
        debugger;
        if ($("#ddlQuoteStage").val() == "DFT") {
            $("#lblQuoteStage").text('Draft');
        }
        //if ($("#ddlQuoteStage").val() == "CFD") {
        //    $("#lblQuoteStage").text('Confirmed');
        //}
        if ($("#ddlQuoteStage").val() == "LST") {
            $("#lblQuoteStage").text('Lost');
        }

        //if ($("#ddlQuoteStage").val() == "CWN") {
        //    $("#lblQuoteStage").text('Closed Won');
        //}
        //if ($("#ddlQuoteStage").val() == "DVD") {
        //    $("#lblQuoteStage").text('Delivered');
        //}
        if ($("#ddlQuoteStage").val() == "NGT") {
            $("#lblQuoteStage").text('Negotiation');
        }

        if ($("#ddlQuoteStage").val() == "CVD") {
            $("#lblQuoteStage").text('Converted');
        }
    }

    //------------------------------------------------ Filter clicks------------------------------------------------------------//

    function Gridfilter(filter) {
        debugger;
        $('#hdnfilterDescriptionDiv').show();

        $('#Draftfilter').hide();
        //$('#Deliveredfilter').hide();
        $('#Negotiationfilter').hide();
        $('#Convertedfilter').hide();
        $('#Lostfilter').hide();


        if (filter == 'DFT') {
            $('#Draftfilter').show();
        }
        //else if (filter == 'DVD') {
        //    $('#Deliveredfilter').show();
        //}
        else if (filter == 'NGT') {
            $('#Negotiationfilter').show();
        }
        else if (filter == "CVD") {
            $('#Convertedfilter').show();
        }
        else if (filter == "LST") {
            $('#Lostfilter').show();
        }
        var result = GetAllQuotations(filter);
        if (result != null) {
            DataTables.QuotationTable.clear().rows.add(result).draw(false);
        }
    }

    //--Function To Reset Quotation Table--//
    function FilterReset() {
        $('#hdnfilterDescriptionDiv').hide();
        var result = GetAllQuotations();
        if (result != null) {
            DataTables.QuotationTable.clear().rows.add(result).draw(false);
        }
    }

    //--function To Filter Quatation Table by call from dashboard ----//
    function dashboardBind(filterValue) {
        if (filterValue == 'Quotation') {
            GetAllQuotations()

        }
        else {
            if (filterValue == 'Draft') {
                filter = 'DFT';
            }
            else if (filterValue == 'Negotiation') {
                filter = 'NGT';
            }
            else if (filterValue == 'Converted') {
                filter = 'CVD';
            }
            else if (filterValue == 'Lost') {
                filter = 'LST';
            }
            Gridfilter(filter)

        }
    }


    //setting value to hidden field in quotation
    function quotationBind(ID) {
        debugger;
        $('#ID').val(ID);
        openNav();
        BindQuationDetails(ID);
    }

//-------------------------------------------------------------------------------------------------//
    function AddQuotationList()
    {
        debugger;
        PopupClearFields();
        $('#quoteItemListObj_Quantity').val("1");
        $("#ddlProductSearch").prop('disabled', false);
        $('#AddQuotationItemModal').modal('show');
    }
    
    function PopupClearFields()
    {
        _QuoteProductDetail = [];
        $("#ddlProductSearch").select2({ dropdownParent: $("#AddQuotationItemModal") });
        $("#ddlProductSearch").val('').trigger('change');
        $('#quoteItemListObj_ProductCode').val('');
        $('#quoteItemListObj_OldProductCode').val('');
        $('#quoteItemListObj_ProductName').val('');
        $('#quoteItemListObj_ProductDescription').val('');
        $('#quoteItemListObj_Quantity').val('1');
        $('#quoteItemListObj_Rate').val('');
    }

    function ProductSearchOnchange(curObj) {
        debugger;
        if (curObj.value != "") {
            _QuoteProductDetail = [];
            $('#quoteItemListObj_Quantity').val('1');
            var ds = GetProductByID(curObj.value);
            QuoteProduct = new Object();
            QuoteProduct.ID = null;
            QuoteProduct.ProductID = ds.ID
            QuoteProduct.OldProductCode = ds.OldCode;
            QuoteProduct.ProductCode = ds.Code
            QuoteProduct.ProductDescription = ds.Description;
            QuoteProduct.ProductName = ds.Name;         
            QuoteProduct.Rate = ds.Rate;
            _QuoteProductDetail.push(QuoteProduct);
            $('#quoteItemListObj_ProductCode').val(_QuoteProductDetail[0].ProductCode);
            $('#quoteItemListObj_OldProductCode').val(_QuoteProductDetail[0].OldProductCode);
            $('#quoteItemListObj_ProductName').val(_QuoteProductDetail[0].ProductName);
            $('#quoteItemListObj_ProductDescription').val(_QuoteProductDetail[0].ProductDescription);          
            $('#quoteItemListObj_Rate').val(roundoff(_QuoteProductDetail[0].Rate));
        }
    }

    function GetProductByID(id) {
        debugger;
        try {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("Product/GetProductDetails/", data);
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
    
    function AddQuotationItem()
    {
        debugger;
        if ($("#ddlProductSearch").val() != "")
        {
            if (_QuoteProductDetail != null)
            {
                //check product existing or not if soo update the new
                var allData = DataTables.ItemDetailsTable.rows().data(); 
                if (allData.length>0)
                {
                    var checkPoint=0;
                    for (var i = 0; i < allData.length; i++)
                    {
                        if (allData[i].ProductID == _QuoteProductDetail[0].ProductID)
                        {
                            allData[i].ProductDescription = $('#quoteItemListObj_ProductDescription').val();
                            allData[i].Quantity = $('#quoteItemListObj_Quantity').val();
                            allData[i].Rate = $('#quoteItemListObj_Rate').val();
                            checkPoint = 1;
                            break;
                        }
                    }

                    if (!checkPoint) {
                        _QuoteProductDetail[0].ProductDescription = $('#quoteItemListObj_ProductDescription').val();
                        _QuoteProductDetail[0].Quantity = $('#quoteItemListObj_Quantity').val();
                        _QuoteProductDetail[0].Rate = $('#quoteItemListObj_Rate').val();
                        DataTables.ItemDetailsTable.rows.add(_QuoteProductDetail).draw(false);
                    }
                    else
                    {
                        DataTables.ItemDetailsTable.clear().rows.add(allData).draw(false);
                    }
                  
                }              
                else
                {
                    _QuoteProductDetail[0].ProductDescription = $('#quoteItemListObj_ProductDescription').val();
                    _QuoteProductDetail[0].Quantity = $('#quoteItemListObj_Quantity').val();
                    _QuoteProductDetail[0].Rate = $('#quoteItemListObj_Rate').val();
                    DataTables.ItemDetailsTable.rows.add(_QuoteProductDetail).draw(false);
                }
            }
            $('#AddQuotationItemModal').modal('hide');
        }
        else
        {
            notyAlert('warning', "Product is Empty");
        } 
    }

    function AddQuoteProductList() {
        debugger;
        var data = DataTables.ItemDetailsTable.rows().data();
        for (var r = 0; r < data.length; r++) {
            QuoteProduct = new Object();
            QuoteProduct.ID = data[r].ID;
            QuoteProduct.ProductID = data[r].ProductID;
            QuoteProduct.ProductCode = data[r].ProductCode;
            QuoteProduct.ProductDescription = data[r].ProductDescription;
            QuoteProduct.ProductName = data[r].ProductName;
            QuoteProduct.Quantity = data[r].Quantity;
            QuoteProduct.Rate = data[r].Rate; 
            _QuoteProductList.push(QuoteProduct);
        }
    }

    function ProductEdit(curObj) {
        debugger;
        $('#AddQuotationItemModal').modal('show');      
        $("#ddlProductSearch").prop('disabled', true);
        PopupClearFields();
        var rowData = DataTables.ItemDetailsTable.row($(curObj).parents('tr')).data();
        if ((rowData != null) && (rowData.ProductID != null))
        {
            $("#ddlProductSearch").select2({ dropdownParent: $("#AddQuotationItemModal") });
            $("#ddlProductSearch").val(rowData.ProductID).trigger('change');
            $('#quoteItemListObj_ProductDescription').val(rowData.ProductDescription);
            $('#quoteItemListObj_Quantity').val(rowData.Quantity);

            $('#quoteItemListObj_Rate').val(roundoff(rowData.Rate));
           
        }
    }

    function CustomerTypeChange() {
        debugger;
        if ($("#ISRegularCustomer").val() == "REG") {
            
            $("#divCustomerID").show();
            $("#divCustomerName").hide();
        }
        else {
            $("#divCustomerID").hide();
            $("#divCustomerName").show();
        }
    }
   

    //To trigger download button
    function DownloadMailPreview() {
        debugger;
        $('#btnDownload').trigger('click');
    }

    //To download file in PDF
    function GetHtmlData() {
        debugger;       
        var bodyContent = $('#mailmodelcontent').html();
        ///var bodyContent = $('#customtbl').html();
        var headerContent = $('#headercontainer').html();
        $("#hdnContent").val(bodyContent);
       // $('#hdnHeadContent').val("<h1></h1>");
        $('#hdnHeadContent').val(headerContent);
    }




