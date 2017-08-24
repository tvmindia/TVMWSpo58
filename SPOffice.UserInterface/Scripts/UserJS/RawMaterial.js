var DataTables = {};
var emptyGUID = '00000000-0000-0000-0000-000000000000'
$(document).ready(function () {
    try {

        DataTables.RawMaterialTable = $('#rawMaterialTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllRawMaterials(),
             pageLength: 10,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "ID", "defaultContent": "<i>-</i>" },
               { "data": "MaterialCode", "defaultContent": "<i>-</i>" },
               { "data": "Description", "defaultContent": "<i>-</i>" },
               { "data": "commonObj.CreatedDateString", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [0], "visible": false, "searchable": false },

               { className: "text-left", "targets": [1, 2] },
             { className: "text-center", "targets": [3,4] }
            

             ]
         });

        $('#rawMaterialTable tbody').on('dblclick', 'td', function () {

            Edit(this);
        });



    } catch (x) {

        notyAlert('error', x.message);

    }

});

function GetAllRawMaterials() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("RawMaterial/GetAllRawMaterials/", data);
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

    }

}

function goBack() {
    ClearFields();
    closeNav();
    BindAllRawMaterials();
}

function Save() {

    try {
        //    $('#EmployeeType').val("EMP");
        $("#btnInsertUpdateRawMaterial").trigger('click');
    }
    catch (e) {
        notyAlert('error', e.message);

    }
}

function Delete() {
    notyConfirm('Are you sure to delete?', 'DeleteRawMaterial()', '', "Yes, delete it!");
}

function DeleteRawMaterial() {
    try {
        var id = $('#ID').val();
        if (id != '' && id != null) {
            var data = { "ID": id };
            var ds = {};
            ds = GetDataFromServer("RawMaterial/DeleteRawMaterial/", data);

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

    $("#MaterialCode").val("");
    $("#ID").val('');
    $("#Description").val('');
  

    ResetForm();
    ChangeButtonPatchView("RawMaterial", "btnPatchAdd", "Add"); //ControllerName,id of the container div,Name of the action
}

//-----------------------------------------Reset Validation Messages--------------------------------------//
function ResetForm() {

    var validator = $("#RawMaterialForm").validate();
    $('#RawMaterialForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}

function BindAllRawMaterials() {
    try {
        DataTables.RawMaterialTable.clear().rows.add(GetAllRawMaterials()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function SaveSuccess(data, status) {

    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            BindAllRawMaterials();
            notyAlert('success', JsonResult.Record.Message);
            if (JsonResult.Record.ID) {
                $("#ID").val(JsonResult.Record.ID);
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

function Reset() {
    if ($("#ID").val() == "") {
        ClearFields();
    }
    else {
        FillRawMaterialDetails($("#ID").val());
    }
    ResetForm();
}

function GetRawMaterialByID(id) {
    try {

        var data = { "ID": id };
        var ds = {};
        ds = GetDataFromServer("RawMaterial/GetRawMaterialDetails/", data);
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
function FillRawMaterialDetails(id) {

    ChangeButtonPatchView("RawMaterial", "btnPatchAdd", "Edit"); //ControllerName,id of the container div,Name of the action
    var thisItem = GetRawMaterialByID(id); //Binding Data
    //Hidden
    $("#ID").val(thisItem.ID);
    $("#MaterialCode").val(thisItem.MaterialCode);
    $("#Description").val(thisItem.Description);
}

//---------------------------------------Edit Bank--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    openNav("0");
    ResetForm();
    var rowData = DataTables.RawMaterialTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.ID != null)) {
        FillRawMaterialDetails(rowData.ID);
    }
}