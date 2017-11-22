var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
var _Products = [];
var _Units = [];
var footer="Terms and conditions :"+"that's not a reason to use Multiply,"+
    "that's a reason to understand integer overflow, using the correct type for the operation,"+
    "and typecasting/promotion. The correct thing to do if you find someone resorting to calling"+
    "Multiply in such a case is to sit them down and have a discussion about numeric representations"+
    "and choosing (casting to) the right type. Strictly speaking, the way this question is worded,"+
    "this isn't really an answer either -- at least to me since 100 * 200 is much more readable,"+
    "though I'd write it 100m * 200m to be consistent with the types.";
$(document).ready(function () {
    try {
        debugger;

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
               { "data": "customer.CustomerName", "defaultContent": "<i>-</i>" },
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


        DataTables.ItemDetailTable = $('#ItemDetailsTable').DataTable(
      {
          dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
          ordering:false,
          searching: false,
          paging: false,
          data: null,
          autoWidth: false,
          columns: EG_Columns(),
          columnDefs: EG_Columns_Settings()
      });

        //--checking hidden field filter value--//
        if ($('#filter').val() != '') {
            dashboardBind($('#filter').val())
        }
        GetAllProductCodes();
        GetAllUnitCodes();
        EG_ComboSource('UnitCodes', _Units, 'Code', 'Description');
        EG_ComboSource('Products', _Products, 'Code', 'Description');
     
        EG_GridDataTable = DataTables.ItemDetailTable;
       // showLoader();
       // List();

        //$('.Roundoff').on('change', function () {
        //    var CustomerInvoiceViewModel = new Object();
        //    CustomerInvoiceViewModel.GrossAmount = $('#txtGrossAmt').val();
        //    CustomerInvoiceViewModel.Discount = ((parseInt($('#txtDiscount').val())) > (parseInt($('#txtGrossAmt').val()))) ? "0.00" : $('#txtDiscount').val();
        //    CustomerInvoiceViewModel.NetTaxableAmount = (CustomerInvoiceViewModel.GrossAmount - CustomerInvoiceViewModel.Discount)
        //    CustomerInvoiceViewModel.TaxType = $('#ddlTaxType').val() != "" ? GetTaxRate($('#ddlTaxType').val()) : $('#txtTaxPercApp').val();
        //    CustomerInvoiceViewModel.TaxPercentage = CustomerInvoiceViewModel.TaxType
        //    CustomerInvoiceViewModel.TaxAmount = (CustomerInvoiceViewModel.NetTaxableAmount * CustomerInvoiceViewModel.TaxPercentage) / 100
        //    CustomerInvoiceViewModel.TotalInvoiceAmount = (CustomerInvoiceViewModel.NetTaxableAmount + CustomerInvoiceViewModel.TaxAmount)
        //    $('#txtNetTaxableAmt').val(CustomerInvoiceViewModel.NetTaxableAmount);
        //    $('#txtTaxPercApp').val(CustomerInvoiceViewModel.TaxPercentage);
        //    $('#txtTaxAmt').val(CustomerInvoiceViewModel.TaxAmount);
        //    $('#txtTotalInvAmt').val(CustomerInvoiceViewModel.TotalInvoiceAmount);
        //    if ((parseInt($('#txtDiscount').val())) > (parseInt($('#txtGrossAmt').val()))) {
        //        $('#txtDiscount').val("0.00");
        //    }

        //});
        //$('#txtTaxPercApp').on('keypress', function () {
        //    debugger;
        //    if ($('#ddlTaxType').val() != "")
        //        $('#ddlTaxType').val('')
        //});
        if ($('#BindValue').val() != '') {
            debugger;
            quotationBind($('#BindValue').val())
        }
       
    }
    catch (x) {
        notyAlert('error', x.message);
    }


    //$('#MailPreviewModel').on('shown.bs.modal', function () {
        
       
    //})
});


//-----------------------EDIT GRID DEFN-------------------------------------
var EG_totalDetailRows = 0;
var EG_GridData;//DATA SOURCE OBJ ARRAY
var EG_GridDataTable;//DATA TABLE ITSELF FOR REBIND PURPOSE
var EG_SlColumn = 'SlNo';
var EG_GridInputPerRow = 4;
var EG_MandatoryFields = 'ProductCode,ProductDescription,Rate';


function EG_TableDefn() {
    var tempObj = new Object();
    tempObj.ID = "";
    tempObj.SlNo = 0;
    tempObj.ProductCode = "";
    tempObj.ProductDescription = "";
    tempObj.UnitCode = "";
    tempObj.Quantity = "";
    tempObj.Rate = "";
    tempObj.Amount = "";
    tempObj.ProductID = "";
    return tempObj
}


function EG_Columns() {
    debugger;
      var obj = [
                { "data": "ID", "defaultContent": "<i></i>" },
                { "data": "SlNo", "defaultContent": "<i></i>" },
                { "data": "ProductCode", render: function (data, type, row) { return (EG_createCombo(data, 'S', row, 'ProductCode', 'Products', 'FillDescription')); } },
                { "data": "ProductDescription", render: function (data, type, row) { return (EG_createTextBox(data, 'S', row, 'ProductDescription', '')); }, "defaultContent": "<i></i>" },
               //{ "data": "UnitCode", render: function (data, type, row) { return (EG_createCombo(data, 'S', row, 'UnitCode', 'UnitCodes','')); }, "defaultContent": "<i></i>" },
               //{ "data": "Quantity", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Quantity', 'CalculateGridAmount')); }, "defaultContent": "<i></i>" },
               
                { "data": "Rate", render: function (data, type, row) { return (EG_createTextBox(data, 'N', row, 'Rate', 'CalculateGridAmount')); }, "defaultContent": "<i></i>" },
               //{ "data": "Amount",   "defaultContent": "<i></i>" },
                { "data": null, "orderable": false, "defaultContent": '<a href="#" class="DeleteLink"  onclick="Delete(this)" ><i class="glyphicon glyphicon-trash" aria-hidden="true"></i></a>' },
                { "data": "ProductID",   "defaultContent": "<i></i>" }
                ]

      return obj;

}

function EG_Columns_Settings() {
 
    var obj = [
            { "targets": [0,6], "visible": false, "searchable": false },
            { "width": "5%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            //{ "width": "8%", "targets": 4 },
           // { "width": "8%", "targets": 5 },
            { "width": "8%", "targets": 4 },
            //{ "width": "10%", "targets": 7 },
            {className:"text-center", "width": "3%", "targets": 5 },
            //{ "width": "10%", "targets": 11 },
        //{ className: "text-right", "targets": [8] },
        //{ className: "text-left disabled", "targets": [5] },
        //{ className: "text-center", "targets": [3, 4, 6, 12] },
        //{ className: "text-center disabled", "targets": [7] },
        //{ className: "text-right disabled", "targets": [9, 10, 11] },
        //{ "orderable": false, "targets": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11] }

    ]

    return obj;

}

function FillDescription(row) {

    for (i = 0; i < _Products.length; i++) {
        if (_Products[i].Code == EG_GridData[row - 1]['ProductCode']) {
            EG_GridData[row - 1]['ProductDescription'] = _Products[i].Description;
            EG_GridData[row - 1]['ProductID'] = _Products[i].ID;
            EG_GridData[row - 1]['Rate'] = _Products[i].Rate;

            //Description
            EG_Rebind();
            break;
        }
    }

}


function CalculateGridAmount(row) {
    var qty = 0.00;
    var rate = 0.00;
    var EGqty = '';
    var EGrate = '';
    EGqty = EG_GridData[row - 1]["Quantity"];
    EGrate = EG_GridData[row - 1]['Rate'];
    qty = parseFloat(EGqty) || 0;
    rate = parseFloat(EGrate) || 0;
    EG_GridData[row - 1]['Rate'] = roundoff(rate);
    EG_GridData[row - 1]['Amount'] = roundoff(qty * rate);
    EG_Rebind();

    var total = 0.00;
    for (i = 0; i < EG_GridData.length; i++) {
        total = total + (parseFloat(EG_GridData[i]['Amount']) || 0);
    }
    $('#GrossAmount').val(roundoff(total));
    AmountSummary();

}

function QuotationNoOnChange(curobj)
{
    $("#lblQuotationNo").text($(curobj).val());
}

function AmountSummary() {
    var total = 0.00;
    for (i = 0; i < EG_GridData.length; i++) {
        total = total + (parseFloat(EG_GridData[i]['Amount']) || 0);
    }
    $('#GrossAmount').val(roundoff(total));

   
   
    var discount = parseFloat($('#Discount').val()) || 0;
    var nettaxableamount = total - discount;
    $('#NetTaxableAmount').val(roundoff(nettaxableamount));

    var applicabletax=$("#TaxPercApplied").val();
    $('#TaxAmount').val((roundoff(nettaxableamount) * applicabletax)/100);
    var totamt = nettaxableamount + (roundoff(nettaxableamount) * applicabletax) / 100;
    $("#TotalAmount").val(totamt);
   
    //var vatp = (parseFloat($('#vatpercentage').val()) || 0);
    //if (vatp > 0) {
    //    vatamount = (total * vatp) / 100;
    //    $('#vatamount').val(roundoff(vatamount));
    //}
    //$('#grandtotal').val(roundoff(total + vatamount));
}


function BindAllQuotes() {
    try {
        DataTables.QuotationTable.clear().rows.add(GetAllQuotations()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function GetTaxPercentage()
{
   
    try {
        var curObj=$("#TaxTypeCode").val();
        if (curObj)
        {
            $("#TaxPercApplied").val(0);
            var data = { "Code": curObj };
            var ds = {};
            ds = GetDataFromServer("Quotation/GetTaxRate/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
               
                $("#TaxPercApplied").val(ds.Records);
                AmountSummary();
             return ds.Records;
            }
            if (ds.Result == "ERROR") {
                return 0;
            }
        }
        else
        {
            $("#TaxPercApplied").val(0);
        }
        
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function Delete(curobj) {
    var rowData = DataTables.ItemDetailTable.row($(curobj).parents('tr')).data();
    if (rowData.ID)
    {
        notyConfirm('Are you sure to delete?', 'DeleteItem("' + rowData.ID + '");', '', "Yes, delete it!");
    }
   
    
}

function CheckAmount() {
  
    if ($("#txtDiscount").val() == "")
        $("#txtDiscount").val(roundoff(0));
}

function DeleteItem(ID) {
    
    try {
       
        //Event Request Case
        if (ID)
        {
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
                                EG_Rebind_WithData(GetAllQuoteItems($("#ID").val()), 1);
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
    }
    catch (e) {

        notyAlert('error', e.message);
    }


   
}

function saveInvoices() {
    debugger;
    $("#DetailJSON").val('');//
    var validation = EG_Validate();
    if (validation == "") {

        var result = JSON.stringify(EG_GridData);
        $("#DetailJSON").val(result);
        $('#btnSave').trigger('click');
    }
    else {
        notyAlert('error', validation);
    }

   
   
}



function SaveSuccess(data, status) {
   
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
            EG_Rebind_WithData(GetAllQuoteItems($("#ID").val()), 1);
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Record.Message);
            break;
        default:
            notyAlert('error', JsonResult.Record.Message);
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
}





function Edit(Obj) {
    debugger;
    $('#QuoteForm')[0].reset();
    var rowData = DataTables.QuotationTable.row($(Obj).parents('tr')).data();
    $('#ID').val(rowData.ID);
    $('#deleteId').val(rowData.ID);
    $('#QuoteMAilHeaderID').val(rowData.ID);
    
    BindQuationDetails(rowData.ID);
    GetTaxPercentage();
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
            $("#ddlCustomer").val(jsresult.CustomerID);
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
            $("#lblEmailSent").text(jsresult.EmailSentYN=="True"?'YES':'NO');
            
            $('#SentToEmails').val(jsresult.SentToEmails);
            $("#lblQuotationNo").text(jsresult.QuotationNo);
            debugger;
            EG_Rebind_WithData(GetAllQuoteItems(jsresult.ID), 1);
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
    EG_ClearTable();   
    $('#ID').val('');
    $("#DetailJSON").val('');
    //Reset();  
    $("#ddlQuoteStage").val('DFT');
    $("#lblQuoteStage").text('Draft');
   $("#lblEmailSent").text('No');
    $("#lblQuotationNo").text('New Quotation');
    clearUploadControl();
    EG_AddBlankRows(5)
    //  clearUploadControl();
    $("#QuoteBodyFoot").val(footer);
}

function Reset() {      
    BindQuationDetails($('#ID').val());    
    $("#QuoteBodyFoot").val(footer);    
}

//---------------Bind logics-------------------
function GetAllQuotations(filter) {
    try {

        var data = {"filter":filter};
        var ds = {};
        ds = GetDataFromServer("Quotation/GetAllQuotations/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            BindSummarBox(ds.Draft, ds.Delivered, ds.InProgress,ds.Closed,ds.OnHold);
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
function BindSummarBox(Draft, Delivered, InProgress, Closed, OnHold)
{
    $("#draftCount").text(Draft);
    $("#deliveredCount").text(Delivered);
    $("#inProgressCount").text(InProgress);
    $("#closedCount").text(Closed);
    $("#onHoldCount").text(OnHold);
    //--To place discription--//
    $("#draftCountDescription").text(Draft + ' Quotation(s) are Draft');
    $("#deliveredCountDescription").text(Delivered + ' Delivered Quotation (s)');
    $("#inprogressCountDescription").text(InProgress + ' In Progress Quotation(s)');
    $("#closedCountDescription").text(Closed + ' Closed Quotation(s)');
    $("#onHoldCountDescription").text(OnHold + ' On Hold Quotation(s)');

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
        if (atpos<1 || dotpos<atpos+2 || dotpos+2>=ste.length) 
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

function MailSuccess(data, status)
{
    hideLoader();
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":

            notyAlert('success', JsonResult.Message);
            switch(JsonResult.MailResult)
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


function SendMailClick() {
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
    if ($("#ddlQuoteStage").val() == "CFD") {
        $("#lblQuoteStage").text('Confirmed');
    }
    if ($("#ddlQuoteStage").val() == "CLT") {
        $("#lblQuoteStage").text('Closed Lost');
    }

    if ($("#ddlQuoteStage").val() == "CWN") {
        $("#lblQuoteStage").text('Closed Won');
    }
    if ($("#ddlQuoteStage").val() == "DVD") {
        $("#lblQuoteStage").text('Delivered');
    }
    if ($("#ddlQuoteStage").val() == "NGT") {
        $("#lblQuoteStage").text('Negotiation');
    }

    if ($("#ddlQuoteStage").val() == "OHD") {
        $("#lblQuoteStage").text('On Hold');
        }
    }






//------------------------------------------------ Filter clicks------------------------------------------------------------//

function Gridfilter(filter) {
    debugger;
    $('#hdnfilterDescriptionDiv').show();

    $('#Draftfilter').hide();
    $('#Deliveredfilter').hide();
    $('#Progressfilter').hide();
    $('#Closedfilter').hide();
    $('#OnHoldfilter').hide();


    if (filter == 'DFT') {
        $('#Draftfilter').show();
    }
    else if (filter == 'DVD') {
        $('#Deliveredfilter').show();
    }
    else if (filter == 'NGT,CFD') {
        $('#Progressfilter').show();
    }
    else if (filter == "CLT,CWN") {
        $('#Closedfilter').show();
    }
    else if (filter == "OHD") {
        $('#OnHoldfilter').show();
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
    else if (filterValue == 'InProgress') {
        filter = 'NGT,CFD';
    }
    else if (filterValue == 'Closed') {
        filter = 'CLT,CWN';
    }
    else if (filterValue == 'OnHold') {
        filter = 'OHD';
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