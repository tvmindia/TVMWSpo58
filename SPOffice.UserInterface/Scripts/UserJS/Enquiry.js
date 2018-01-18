﻿var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
//--Loading DOM--//
$(document).ready(function () {
    try {
        //Fileupload 
        $('#btnUpload').click(function () {
            //Pass the controller name
            var FileObject = new Object;
            if ($('#hdnFileDupID').val() != emptyGUID) {
                FileObject.ParentID = (($('#ID').val()) != emptyGUID ? ($('#ID').val()) : $('#hdnFileDupID').val());
            }
            else {
                FileObject.ParentID = ($('#ID').val() == emptyGUID) ? "" : $('#ID').val();
            }


            FileObject.ParentType = "Enquiry";
            FileObject.Controller = "FileUpload";
            UploadFile(FileObject);
        });       

   //-- Initialising TimePicker plugin in FollowUp Modal--//   
        $('input.timepicker').timepicker({
            timeFormat: 'hh:mm:p',
            interval: 10,
            minTime: '1',
            maxTime: '11:50pm',
            defaultTime: '11',
            startTime: '10:00am',
            dynamic: false,
            dropdown: true,
            scrollbar: true
        });


        $('[data-toggle="tooltip"]').tooltip();



//--Binding Enquiry Data table with all enquiry details--//
        DataTables.EnquiryTable = $('#EnquiryTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllEnquiry(),
             pageLength: 10,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "ID", "defaultContent": "<i>-</i>" },
               { "data": "EnquiryNo", "defaultContent": "<i>-</i>" },
               { "data": "EnquiryDate", "defaultContent": "<i>-</i>" ,"width":"10%"},
               { "data": "CompanyName", "defaultContent": "<i>-</i>" },
               { "data": "Subject", "defaultContent": "<i>-</i>" },
               { "data": "Mobile", "defaultContent": "<i>-</i>" },
               { "data": "LandLine", "defaultContent": "<i>-</i>" },
               { "data": "EnquirySource", "defaultContent": "<i>-</i>" },
               { "data": "IndustryName", "defaultContent": "<i>-</i>" },
               { "data": "EnqStatusDescription", "defaultContent": "<i>-</i>" },
               { "data": "LeadOwner", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#"  class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [
                 { "targets": [0], "visible": false, "searchable": false },
                 { className: "text-left", "targets": [1, 2, 3, 4, 5, 6, 8, 9, 10, 11] },
                 { className: "text-center", "targets": [ 2] }
             ]
         });

        //--For Editing Enquiry Table on row double click--//
        $('#EnquiryTable tbody').on('dblclick', 'td', function () { 
            Edit(this);
        });

        if ($('#BindValue').val() != '') {
            debugger;
            dashboardBind($('#BindValue').val())
        }

        
        
    } catch (x) {

        notyAlert('error', x.message);

    }

});
//function CheckInsert()
//{
//    var flag;
//    if ($('#hdnMessageID').val() == emptyGUID) {

//        flag == "INSERT"
//    }
//    else {
//        flag == "UPDATE"
//    }
//}



//--To Get List of Enquiries from server --// 
function GetAllEnquiry(filter) {
    try {
        debugger;
      
        var data = { 'filter': filter };
        var ds = {};
        ds = GetDataFromServer("Enquiry/GetAllEnquiry/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
            //-- For getting open,converted,notconverted count--//
            BindSummarBox(ds.Open, ds.Converted, ds.NotConverted);
           
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

//--Placing Open,Converted,Non Converted Counts on tiles--// 
function BindSummarBox(Open, Converted, NotConverted) {
    $("#statusOpen").text(Open);
    $("#statusConverted").text(Converted);
    $("#statusNotConverted").text(NotConverted);
    $("#openCountDescription").text(Open + ' Open Enquiry(s)');
    $("#convertedCountDescription").text(Converted + ' Converted Enquiry(s)');
    $("#notConvertedCountDescription").text(NotConverted + ' Not Converted Enquiry(s)');
}

//-- Saves Enquiry details to server on InsertUpdateEnquiry button trigger--//
function Save() {
    debugger;
    try {
        $("#btnInsertUpdateEnquiry").trigger('click');
    }
    catch (e) {
        notyAlert('error', e.message);

    }
}

//--Clearing Enquiry table and Rebinding with all enquiry details --//
function BindAllEnquiries() {
    debugger;
    try {
        DataTables.EnquiryTable.clear().rows.add(GetAllEnquiry()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

//Function onsucess ajax post event for enquiry form save
function SaveSuccess(data) {
    debugger;
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            var isInsert=0;
            if ($("#ID").val()===emptyGUID)
            {
                isInsert=1;              
            }
            FillEnquiryDetails(JsonResult.Records.ID);
            ChangeButtonPatchView('Enquiry', 'btnPatchAdd', 'Edit');
            BindAllEnquiries();
            if (isInsert === 1)
            { SendEnquiryMessage(); }
            notyAlert('success', JsonResult.Message);
            $('#hdnMessageID').val("");
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}

function FollowUp(flag, status){
    //--This function have 2 parameters ,
    //'flag' checks whether new followup is added or edit action is performed 
    //'status' checks whether the  element is first record  or not                                  
    debugger;
    //--To Reset,enable textbox and display FollowUp modal PopUp on Add Follow Up Button Click --//
    //--
    if ((flag == 1)) {            
               
                $('#ModelReset').trigger('click');
                $("#hdnFollowUpID").val(ID);
                $('#FollowUpDate').prop("disabled", false);
                $('#FollowUpTime').prop("disabled", false);
                $('#followUpObj_Subject').prop("disabled", false);
               // $('#ddlPriority').prop("disabled", false);
                $('#followUpObj_ContactName').prop("disabled", false);
                $('#followUpObj_ReminderType').prop("disabled", false);
                $('#followUpObj_GeneralNotes').prop("disabled", false);
                $('#followUpObj_RemindPriorTo').prop("disabled", false);
                $('#followUpObj_Status').prop("disabled", false);
                $('#followUpObj_Status').attr('onchange', 'EnableTextbox("new")');
                $('#followUpObj_Priority').prop("disabled", false);
                $("#btnFollowUps").modal('show');
                $('#followUpDeletebtn').hide();
    }
    else 
    {
        //--To disable textbox and display FollowUp modal PopUp on Edit Button Click --//
        var ID = flag;        
        debugger;
        $("#btnFollowUps").modal('show');
        $("#hdnFollowUpID").val(ID);
        $('#followUpObj_Status').attr('onchange', 'EnableTextbox("Edit")');        
        
        FillFollowUpDetails(ID);
        if (status==1)
        {
            debugger;
            $('#followUpObj_Status').prop("disabled", false);
            $('#followUpDeletebtn').show();
            $('#btnSave').show();
            $('#followUpResetbtn').show();
            EnableTextbox("Edit");         
        }
        else
        {
            $('#followUpObj_Status').prop("disabled", true);
            $('#followUpDeletebtn').hide();
            $('#followUpResetbtn').hide();
            $('#btnSave').hide();
        }       
    }   
}

function EnableTextbox(flag)
{
    debugger;   
    if (flag === "Edit")
    {
        if ($('#followUpObj_Status').val() == 'Open') {
            FollowUpOpen()
        }
        else if ($('#followUpObj_Status').val() == 'Closed') {
            FollowUpClosed()
        }
    }   
}

//--Fills the Edit FollowUp form  with details corresponding to FollowUp ID--//
function FillFollowUpDetails(ID) {
    debugger;

    var thisItem = GetFollowUpDetailsByFollowUpID(ID); //Binding Data
    debugger;
    //$("#hdnFileID").val(thisItem.ID);
    if (thisItem.Status == "Closed") {
        FollowUpClosed()
    }
    else {
        FollowUpOpen()
    }
    $('#FollowUpDate').val(thisItem.FollowUpDate);
    $('#FollowUpTime').val(thisItem.FollowUpTime);
    $("#followUpObj_Subject").val(thisItem.Subject);
    //$('#ddlPriority').val(thisItem.Priority);
    $('#followUpObj_ContactName').val(thisItem.ContactName);
    $('#followUpObj_ReminderType').val(thisItem.ReminderType);
    $('#followUpObj_Status').val(thisItem.Status);
    $('#followUpObj_RemindPriorTo').val(thisItem.RemindPriorTo);
    $('#followUpObj_GeneralNotes').val(thisItem.GeneralNotes);
    $('#followUpObj_Priority').val(thisItem.Priority);

}

function FollowUpClosed() {
    $('#FollowUpDate').prop("disabled", true);
    $('#FollowUpTime').prop("disabled", true);
    $('#followUpObj_Subject').prop("disabled", true);
    $('#ddlPriority').prop("disabled", true);
    $('#followUpObj_ContactName').prop("disabled", true);
    $('#followUpObj_ReminderType').prop("disabled", true);
    $('#followUpObj_GeneralNotes').prop("disabled", true);
    $('#followUpObj_RemindPriorTo').prop("disabled", true);
    $('#followUpObj_Priority').prop("disabled", true);
}
function FollowUpOpen() {
    $('#FollowUpDate').prop("disabled", false);
    $('#FollowUpTime').prop("disabled", false);
    $('#followUpObj_Subject').prop("disabled", false);
    $('#ddlPriority').prop("disabled", false);
    $('#followUpObj_ContactName').prop("disabled", false);
    $('#followUpObj_ReminderType').prop("disabled", false);
    $('#followUpObj_GeneralNotes').prop("disabled", false);
    $('#followUpObj_RemindPriorTo').prop("disabled", false);
    $('#followUpObj_Priority').prop("disabled", false);
    $('#followUpObj_Status').prop("disabled", false);
}

//--To Get FollowUp details from server corresponding to Enquiry ID--//
function GetFollowUpDetailsByFollowUpID(ID) {
    try {
        debugger;

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Enquiry/GetFollowUpDetailByFollowUpId/", data);
        debugger;
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


//--To reset FollowUp Modal on reset button click--//
function Reset() {
    debugger;
    var ID=$("#hdnFollowUpID").val()
    $('#ModelReset').trigger('click');
    if(ID)
    FollowUp(ID,1);
}

//--To Edit Enquiry Table with row details with particular enquiry ID--//
function Edit(currentObj) {

    debugger;    
    Resetform();
    ChangeButtonPatchView('Enquiry', 'btnPatchAdd', 'Edit');
    openNav();
    var rowData = DataTables.EnquiryTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        FillEnquiryDetails(rowData.ID);
       
    }
}

//--Fills the Edit form  with details corresponding to ID--//
function FillEnquiryDetails(ID) {
    debugger;

    var thisItem = GetEnquiryDetailsByID(ID); //Binding Data
    debugger;

        $("#ID").val(thisItem.ID);
        $("#EnquiryDate").val(thisItem.EnquiryDate);
        $("#ddlSalesPerson").val(thisItem.EnquiryOwnerID);
        $("#ddlTitle").val(thisItem.ContactTitle);
        $("#ContactName").val(thisItem.ContactName);
        $("#CompanyName").val(thisItem.CompanyName);
        $("#Subject").val(thisItem.Subject);
        $("#GeneralNotes").val(thisItem.GeneralNotes);
        $("#ddlIndustry").val(thisItem.IndustryCode);
        $("#ddlProgressStatus").val(thisItem.ProgressStatus);
        $("#ddlEnquirySource").val(thisItem.EnquirySource);
        $("#ddlEnquiryStatus").val(thisItem.EnquiryStatus);
        $("#Address").val(thisItem.Address);
        $("#WebSite").val(thisItem.Website);
        $("#Email").val(thisItem.Email);
        $("#Mobile").val(thisItem.Mobile);
        $("#LandLine").val(thisItem.LandLine);
        $("#Fax").val(thisItem.Fax);
        $("#lblEnquiryNo").text(thisItem.EnquiryNo);
        $("#hdnEnqID").val(thisItem.ID);
       // $("#hdnmessageID").val(thisItem.hdnMessageID);

    //--Checking Enquiry Status and displays the corresponding description --//
        if (thisItem.EnquiryStatus == "OE") {
            $("#lblEnquiryStatus").text('Open');
        }
        if (thisItem.EnquiryStatus == "CE") {
            $("#lblEnquiryStatus").text('Converted');
        }
        if (thisItem.EnquiryStatus == "NCE") {
            $("#lblEnquiryStatus").text('Not Converted');
        }
        FollowUpList(ID);   
        $('#divAddFollowUp,#flist').show();
        var Count = $('#hdnCountOpen').val()
        if(parseInt(Count)!==0)
        {
            $("#btnAddFollowup").attr({ "disabled": "disabled", "style": "cursor:not-allowed;","title":"Cannot Add New Follow Up(Open FollowUp(s) Existing..!)" });
        }
        else {
            $("#btnAddFollowup").removeAttr("disabled");
            $("#btnAddFollowup").removeAttr("style");
            $("#btnAddFollowup").attr({ "title": "Add New FollowUps" });     
        }
}

//--To Get Enquiry details from server corresponding to Enquiry ID--//
function GetEnquiryDetailsByID(ID) {
    try {
        debugger;

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Enquiry/GetEnquiryDetailsByID/", data);
        debugger;
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

//--To Add new Enquiry,clears the form--//
function Add() {
    Resetform();
    $("#lblEnquiryNo").text("Add New");
    $("#btnResetEnquiry").trigger('click');
    $("#lblEnquiryStatus").text('Open');   
    $("#flist").empty();
    $("#ID").val(emptyGUID);
    openNav();
}

function Resetform() {
    debugger;
    var validator = $("#EnquiryForm").validate();
    $('#EnquiryForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    $('#EnquiryForm')[0].reset();
}

//---drop down change enquiry status changes
function ChangeEnquiryStatus()
{
    debugger;
    
     if($("#ddlEnquiryStatus").val() == "OE") {
        $("#lblEnquiryStatus").text('Open');
    }
    if ($("#ddlEnquiryStatus").val() == "CE") {
        $("#lblEnquiryStatus").text('Converted');
    }
    if ($("#ddlEnquiryStatus").val()== "NCE") {
        $("#lblEnquiryStatus").text('Not Converted');
    }
}

//--To Get FollowUp list from server corresponding to an EnquiryID--//
function FollowUpList(ID) {
    debugger;
    var data = { EnquiryID: ID };
    var ds = {};
    ds = GetDataFromServer('Enquiry' + "/FollowUps/", data);
    if (ds == "Nochange") {
        return; 0
    }
    $("#flist").empty();
    $("#flist").html(ds);

}

//--Saves Follow Up to the server by triggering hidden button--//
function SaveFollowUp() {
    try {
        var time = hrsTo24hrormat();
        $("#hdnFollowUpTime").val(time);
        $("#btnFollowUpSave").trigger('click');
         GetRecentFollowUpCount();
    }
        catch(e){
            notyAlert('error',e.Message);   //--Shows error id save is failed--//
        }

}

//Function onsucess ajax post event for follow Up form save
function FollowUpSaveSuccess(data) {
    debugger;
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            debugger;
            FollowUpList($('#ID').val());
            $('#divAddFollowUp,#flist').show();          
            var Count = $('#hdnCountOpen').val()
            if (parseInt(Count) !== 0) {
                $("#btnAddFollowup").attr({ "disabled": "disabled", "style": "cursor:not-allowed;" });                
            }
            else {
                $("#btnAddFollowup").removeAttr("disabled");
                $("#btnAddFollowup").removeAttr("style");                
            }
            GetRecentFollowUpCount();
            $("#btnFollowUps").modal('hide');
            notyAlert('success', JsonResult.Message);
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}



//--Function to Convert AM,PM format to 24 hour time format-----//
function hrsTo24hrormat() {
    try {
        debugger;
        var h = 0;
        var addTime = 12;
        var time = $("#FollowUpTime").val();
        var hours = parseInt(time.split(":")[0]);
        var minutes = parseInt(time.split(":")[1]);
        var AMPM = time.split(":")[2];

        if (AMPM == "PM" && hours < 12) {
            hours = parseInt(hours) + parseInt(addTime);
        }
        if (AMPM == "AM" && hours == 12) {
            hours = parseInt(hours) - parseInt(addTime);
        }
        var h = hours;
        var sHours = hours.toString();
        var sMinutes = minutes.toString();
        if (h < 10) sHours = sHours;
        if (minutes < 10) sMinutes = sMinutes;
        return sHours + ":" + sMinutes;
    }
    catch (e) {
        noty({ type: 'error', text: e.message });
    }
}


//------------------------------------------------ Filter clicks------------------------------------------------------------//

function Gridfilter(filter) {
    debugger;
    $('#filter').show();

    $('#OEfilter').hide();
    $('#CEfilter').hide();
    $('#NCEfilter').hide();

    if (filter == 'OE') {
        $('#OEfilter').show();
    }
    else if (filter == 'CE') {
        $('#CEfilter').show();
    }
    else if (filter == 'NCE') {
        $('#NCEfilter').show();
    }
    var result = GetAllEnquiry(filter);
    if (result != null) {
            DataTables.EnquiryTable.clear().rows.add(result).draw(false);     
    }
}

    //--Function To Reset Enquiry Table--//
function FilterReset() {
    $('#filter').hide();
    var result = GetAllEnquiry();
    if (result != null) {
        DataTables.EnquiryTable.clear().rows.add(result).draw(false);
    }
}


//Bind values to dashboard
function dashboardBind(ID) {
    debugger;
    ChangeButtonPatchView('Enquiry', 'btnPatchAdd', 'Edit');
    $('#ID').val(ID);
    openNav();
    FillEnquiryDetails(ID);   
}



//to delete enquiry
function Delete()
{
    debugger;
    notyConfirm('Are you sure to delete?', 'EnquiryDelete()');
}

function EnquiryDelete()
{
    try {
        debugger;
        var id = $('#ID').val();
        if (id != '' && id != null) {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("Enquiry/DeleteEnquiry/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                notyAlert('success', ds.Record.Message);
                debugger;
                BindAllEnquiries();
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

// Delete FollowUp
function DeleteFollowUp()
{
    notyConfirm('Are you sure to delete?', 'FollowUpDelete(ID);');
}

function FollowUpDelete(ID)
{
    try {
        debugger;
        var ID = $('#hdnFollowUpID').val();
        if (ID != '' && ID != null) {
            var data = { "ID": ID };
            var ds = {};
            ds = GetDataFromServer("Enquiry/DeleteFollowUp/", data);
            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK")
            {
                debugger;               
                $("#btnFollowUps").modal('hide');
                notyAlert('success', ds.Record.Message);
                GetRecentFollowUpCount();
                var id = $('#ID').val();               
                FollowUpList(id);
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

function SendEnquiryMessage(DS)
{
    try {
        debugger;
        //var ID = $("#ID").val();
        var Mobile = $("#Mobile").val();       
        var eqno=$("#lblEnquiryNo").text();
        var data = { "mobileNumber": Mobile, "EQNumber": eqno };
        var ds = {};
        ds = GetDataFromServer("Enquiry/SendEnquiryMessage/", data);
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


