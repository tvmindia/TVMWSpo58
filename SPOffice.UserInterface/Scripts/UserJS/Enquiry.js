var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {
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



        //DataTables.EnquiryItemDetailsTable = $('#EnquiryItemDetailsTable').DataTable();
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
               { "data": "EnquiryStatus", "defaultContent": "<i>-</i>" },
               { "data": "LeadOwner", "defaultContent": "<i>-</i>" },
                //  { "data": null, "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#"  class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [
                 { "targets": [0], "visible": false, "searchable": false },
                 { className: "text-left", "targets": [1, 2, 3, 4, 5, 6, 8, 9, 10, 11] },
                 { className: "text-center", "targets": [ 2] }
             ]
         });

        $('#EnquiryTable tbody').on('dblclick', 'td', function () {
       
            Edit(this);
        });



    } catch (x) {

        notyAlert('error', x.message);

    }

});



function GetAllEnquiry() {
    debugger;
    try {
       
        var data = {};
        var ds = {};
        ds = GetDataFromServer("Enquiry/GetAllEnquiry/", data);
        if (ds != '') {
            ds = JSON.parse(ds);
        }
        if (ds.Result == "OK") {
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

function BindSummarBox(Open, Converted, NotConverted) {
    $("#statusOpen").text(Open);
    $("#statusConverted").text(Converted);
    $("#statusNotConverted").text(NotConverted);
}

function Save() {
    debugger;
    try {
        $("#btnInsertUpdateEnquiry").trigger('click');
    }
    catch (e) {
        notyAlert('error', e.message);

    }
}


function BindAllEnquiries() {
    debugger;
    try {
        DataTables.EnquiryTable.clear().rows.add(GetAllEnquiry()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function SaveSuccess(data) {
    debugger;
    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            debugger;
            FillEnquiryDetails(JsonResult.Records.ID)
            BindAllEnquiries()
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





function FollowUp() {
    debugger;
    $("#btnFollowUps").modal('show');
}


function Edit(currentObj) {

    debugger;
    openNav();
    var rowData = DataTables.EnquiryTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        FillEnquiryDetails(rowData.ID);
       
    }
}


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
        if (thisItem.EnquiryStatus == "OE") {
            $("#lblEnquiryStatus").text('Open');
        }
        if (thisItem.EnquiryStatus == "CE") {
            $("#lblEnquiryStatus").text('Converted');
        }
        if (thisItem.EnquiryStatus == "NCE") {
            $("#lblEnquiryStatus").text('Not Converted');
        }
        //$("#lblEnquiryStatus").text(thisItem.EnquiryStatus);

     ReloadFollowUpList(ID);
     $("#flist").show();
     $("#btnAddFollowUp").show();

      
}

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


function Add() {
    debugger;
    $("#lblEnquiryNo").text("Add New");
    $("#btnResetEnquiry").trigger('click');
    $("#lblEnquiryStatus").text('');
    openNav();
    
}

function ReloadFollowUpList(ID) {
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

