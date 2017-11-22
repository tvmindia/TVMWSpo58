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


            FileObject.ParentType = "Courier";
            FileObject.Controller = "FileUpload";
            UploadFile(FileObject);
        });




        DataTables.CourierTable = $('#CourierTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllCouriers(),
             pageLength: 10,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "ID", "defaultContent": "<i>-</i>" },
               { "data": "Type", "defaultContent": "<i>-</i>" },
               { "data": "TransactionDate", "defaultContent": "<i>-</i>" },
               { "data": "Track", "defaultContent": "<i>-</i>" },
               { "data": "SourceName", "defaultContent": "<i>-</i>" },
               { "data": "DestName", "defaultContent": "<i>-</i>" },
               { "data": "courierAgency.Name", "defaultContent": "<i>-</i>" },
                { "data": "TrackingRefNo", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [
                 { "targets": [0], "visible": false, "searchable": false },
               { className: "text-left", "targets": [1, 2, 3, 4, 5,6,7] }
             ]
         });

        $('#CourierTable tbody').on('dblclick', 'td', function () {

            Edit(this);
        });

        if ($('#BindValue').val() != '') {
            CourierBind($('#BindValue').val())
        }

    } catch (x) {

        notyAlert('error', x.message);

    }

});

function GetAllCouriers() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("Courier/GetAllCouriers/", data);
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



function openNav(id) {
    var left = $(".main-sidebar").width();
    var total = $(document).width();

    $('.main').fadeOut();
    document.getElementById("myNav").style.left = "3%";
    $('#main').fadeOut();

    if ($("body").hasClass("sidebar-collapse")) {

    }
    else {
        $(".sidebar-toggle").trigger("click");
    }
    if (id != "0") {
        ClearFields();
        $('#lblCourierRefNo').text("New Courier");
    }

}

function goBack() {
    ClearFields();
    closeNav();
    BindAllCouriers();
}

function TrackingRefnoOnChange(curobj)
{
    $('#lblCourierRefNo').text($(curobj).val());
}

function Save() {

    try {
        //    $('#EmployeeType').val("EMP");
        $("#btnInsertUpdateCourierAgency").trigger('click');
    }
    catch (e) {
        notyAlert('error', e.message);

    }
}

function Delete() {
    notyConfirm('Are you sure to delete?', 'DeleteCourier()', '', "Yes, delete it!");
}

function DeleteCourier() {
    try {
        var id = $('#ID').val();
        if (id != '' && id != null) {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("Courier/DeleteCourier/", data);

            if (ds != '') {
                ds = JSON.parse(ds);
            }
            if (ds.Result == "OK") {
                notyAlert('success', ds.Message.Message);
                goBack();
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

function ClearFields() {

    $("#ID").val("");
    $("#ddlType").val("");
    $("#TransactionDate").val("");
    $("#SourceName").val("");
    $("#SourceAddress").val("");
    $("#DestName").val("");
    $("#DestAddress").val("");
    $("#DistributedTo").val("");

    $("#DistributionDate").val("");
    $("#ddlAgency").val("");
    $("#TrackingRefNo").val("");
    $("#GeneralNotes").val("");
    $("#TrackingURL").val("");
    ResetForm();
    ChangeButtonPatchView("Courier", "btnPatchAdd", "Add"); //ControllerName,id of the container div,Name of the action
    clearUploadControl();
}

//-----------------------------------------Reset Validation Messages--------------------------------------//
function ResetForm() {

    var validator = $("#CourierForm").validate();
    $('#CourierForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}

function BindAllCouriers() {
    try {
        DataTables.CourierTable.clear().rows.add(GetAllCouriers()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function SaveSuccess(data, status) {

    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            notyAlert('success', JsonResult.Record.Message);
            if (JsonResult.Record.ID) {
                $("#ID").val(JsonResult.Record.ID);
                FillCourierDetails(JsonResult.Record.ID);
            }
            ChangeButtonPatchView("Courier", "btnPatchAdd", "Edit");
            BindAllCouriers(); 
            break;
        case "ERROR":
            notyAlert('error', JsonResult.Message);
            break;
        default:
            notyAlert('error', JsonResult.Message);
            break;
    }
}

function Reset() {
    if ($("#ID").val() == "") {
        ClearFields();
    }
    else {
        FillCourierDetails($("#ID").val());
    }
    ResetForm();
}

function GetCourierByID(ID) {
    try {

        var data = { "ID": ID };
        var ds = {};
        ds = GetDataFromServer("Courier/GetCourierDetails/", data);
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

//---------------------------------------Fill Courier Details--------------------------------------------------//
function FillCourierDetails(ID) {

    ChangeButtonPatchView("Courier", "btnPatchAdd", "Edit"); //ControllerName,id of the container div,Name of the action
    var thisItem = GetCourierByID(ID); //Binding Data
    //Hidden
    $("#ID").val(thisItem.ID);
    $("#ddlType").val(thisItem.Type);
    $("#TransactionDate").val(thisItem.TransactionDate);
    $("#SourceName").val(thisItem.SourceName);
    $("#SourceAddress").val(thisItem.SourceAddress);
    $("#DestName").val(thisItem.DestName);
    $("#DestAddress").val(thisItem.DestAddress);
    $("#DistributedTo").val(thisItem.DistributedTo);

    $("#DistributionDate").val(thisItem.DistributionDate);
    $("#ddlAgency").val(thisItem.AgencyCode);
    $("#TrackingRefNo").val(thisItem.TrackingRefNo);
    $("#GeneralNotes").val(thisItem.GeneralNotes);
    $("#TrackingURL").val(thisItem.TrackingURL);
    $("#lblCourierRefNo").text(thisItem.TrackingRefNo);
   
    clearUploadControl();
    PaintImages(ID);

}

//---------------------------------------Edit Bank--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    openNav("0");
    ResetForm();
    var rowData = DataTables.CourierTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        FillCourierDetails(rowData.ID);
    }
}

//Call when Auto bind from Report 
function CourierBind(ID) {
    $('#ID').val(ID);
    openNav();
    FillCourierDetails(ID);
}