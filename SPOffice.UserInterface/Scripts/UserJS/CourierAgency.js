var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {

        DataTables.CourierAgencyTable = $('#CourierAgencyTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllCourierAgencies(),
             pageLength: 10,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "Code", "defaultContent": "<i>-</i>" },
               { "data": "Name", "defaultContent": "<i>-</i>" },
               { "data": "Address", "defaultContent": "<i>-</i>" },
               { "data": "Website", "defaultContent": "<i>-</i>" },
               { "data": "Phone", "defaultContent": "<i>-</i>" },
               { "data": "Email", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [

               { className: "text-left", "targets": [0, 1,2,3,4,5] }
               ]
         });

        $('#CourierAgencyTable tbody').on('dblclick', 'td', function () {

            Edit(this);
        });



    } catch (x) {

        notyAlert('error', x.message);

    }

});

function GetAllCourierAgencies() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("CourierAgency/GetAllCourierAgencies/", data);
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
        $("#Operation").val('Insert');
    }

}

function goBack() {
    ClearFields();
    closeNav();
    BindAllCourierAgencies();
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
    notyConfirm('Are you sure to delete?', 'DeleteCourierAgency()', '', "Yes, delete it!");
}

function DeleteCourierAgency() {
    try {
        var id = $('#Code').val();
        if (id != '' && id != null) {
            var data = { "Code": id };
            var ds = {};
            ds = GetDataFromServer("CourierAgency/DeleteCourierAgency/", data);

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

    $("#Code").val("");
    $("#Name").val("");
    $("#Website").val('');
    $("#Phone").val('');
    $("#Fax").val('');
    $("#Email").val('');
    $("#Address").val('');
    $("#Code").prop("readonly", false);

    ResetForm();
    ChangeButtonPatchView("CourierAgency", "btnPatchAdd", "Add"); //ControllerName,id of the container div,Name of the action
}

//-----------------------------------------Reset Validation Messages--------------------------------------//
function ResetForm() {

    var validator = $("#CourierAgencyForm").validate();
    $('#CourierAgencyForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}

function BindAllCourierAgencies() {
    try {
        DataTables.CourierAgencyTable.clear().rows.add(GetAllCourierAgencies()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function SaveSuccess(data, status) {

    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            BindAllCourierAgencies();
            $("#Operation").val('Update');
            $("#Code").prop("readonly", true);
            notyAlert('success', JsonResult.Record.Message);
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
    if ($("#Code").val() == "") {
        ClearFields();
    }
    else {
        FillCourierAgencyDetails($("#Code").val());
    }
    ResetForm();
}

function GetCourierAgencyByID(Code) {
    try {

        var data = { "Code": Code };
        var ds = {};
        ds = GetDataFromServer("CourierAgency/GetCourierAgencyDetails/", data);
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

//---------------------------------------Fill Customer Details--------------------------------------------------//
function FillCourierAgencyDetails(Code) {

    ChangeButtonPatchView("CourierAgency", "btnPatchAdd", "Edit"); //ControllerName,id of the container div,Name of the action
    var thisItem = GetCourierAgencyByID(Code); //Binding Data
    //Hidden
    $("#Code").val(thisItem.Code);
    $("#Name").val(thisItem.Name);
    $("#Website").val(thisItem.Website);
    $("#Phone").val(thisItem.Phone);
    $("#Fax").val(thisItem.Fax);
    $("#Email").val(thisItem.Email);
    $("#Address").val(thisItem.Address);
   

}

//---------------------------------------Edit Bank--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click

    openNav("0");
    ResetForm();
    $("#Code").prop("readonly", true);
    $("#Operation").val('Update');
    var rowData = DataTables.CourierAgencyTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.Code != null)) {
        FillCourierAgencyDetails(rowData.Code);
    }
}