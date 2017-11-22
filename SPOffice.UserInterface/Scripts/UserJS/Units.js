var DataTables = {};
$(document).ready(function () {
    try {
        debugger;
        DataTables.UnitsTable = $('#UnitsTable').DataTable(
         {
             dom: '<"pull-right"f>rt<"bottom"ip><"clear">',
             order: [],
             searching: true,
             paging: true,
             data: GetAllUnits(),
             pageLength: 10,
             language: {
                 search: "_INPUT_",
                 searchPlaceholder: "Search"
             },
             columns: [
               { "data": "UnitsCode", "defaultContent": "<i>-</i>" },
               { "data": "Description", "defaultContent": "<i>-</i>" },
               { "data": "commonObj.CreatedDateString", "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [{ "targets": [], "visible": false, "searchable": false },

               { className: "text-left", "targets": [0,1] },
             { className: "text-center", "targets": [3,2] }


             ]
         });

        $('#UnitsTable tbody').on('dblclick', 'td', function () {

            Edit(this);
        });



    } catch (x) {

        notyAlert('error', x.message);

    }

});

function GetAllUnits() {
    try {

        var data = {};
        var ds = {};
        ds = GetDataFromServer("Units/GetAllUnits/", data);
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
    BindAllUnits();
}

function Save() {

    try {
        //    $('#EmployeeType').val("EMP");
        $("#btnInsertUpdateUnits").trigger('click');
    }
    catch (e) {
        notyAlert('error', e.message);

    }
}

function Delete() {
    notyConfirm('Are you sure to delete?', 'DeleteUnits()', '', "Yes, delete it!");
}

function DeleteUnits() {
    try {
        var code = $('#hdnCode').val();
        if (code != '' && code != null) {
            var data = { "code": code };
            var ds = {};
            ds = GetDataFromServer("Units/DeleteUnits/", data);

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

    $("#hdnCode").val("");
    $("#UnitsCode").val("");
    $("#Description").val('');
    

    ResetForm();
    ChangeButtonPatchView("Units", "btnPatchAdd", "Add"); //ControllerName,id of the container div,Name of the action
}

//-----------------------------------------Reset Validation Messages--------------------------------------//
function ResetForm() {

    var validator = $("#UnitsForm").validate();
    $('#UnitsForm').find('.field-validation-error span').each(function () {
        validator.settings.success($(this));
    });
    validator.resetForm();
}

function BindAllUnits() {
    try {
        DataTables.UnitsTable.clear().rows.add(GetAllUnits()).draw(false);
    }
    catch (e) {
        notyAlert('error', e.message);
    }
}

function SaveSuccess(data, status) {

    var JsonResult = JSON.parse(data)
    switch (JsonResult.Result) {
        case "OK":
            debugger;
            BindAllUnits();
            notyAlert('success', JsonResult.Record.Message);
            if (JsonResult.Record.Code) {
                $("#hdnCode").val(JsonResult.Record.Code);
                FillUnitsDetails(JsonResult.Record.Code);
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
    if ($("#hdnCode").val() == "") {
        ClearFields();
    }
    else {
        FillUnitsDetails($("#hdnCode").val());
    }
    ResetForm();
}

function GetUnitsByCode(code) {
    try {

        var data = { "code": code };
        var ds = {};
        ds = GetDataFromServer("Units/GetUnitsDetails/", data);
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
function FillUnitsDetails(code) {

    ChangeButtonPatchView("Units", "btnPatchAdd", "Edit"); //ControllerName,id of the container div,Name of the action
    var thisItem = GetUnitsByCode(code); //Binding Data
    //Hidden
    $("#hdnCode").val(thisItem.UnitsCode);
    $("#UnitsCode").val(thisItem.UnitsCode);
    $("#Description").val(thisItem.Description);
}

//---------------------------------------Edit Bank--------------------------------------------------//
function Edit(currentObj) {
    //Tab Change on edit click
    openNav("0");
    ResetForm();
    var rowData = DataTables.UnitsTable.row($(currentObj).parents('tr')).data();
    if ((rowData != null) && (rowData.UnitsCode != null)) {
        FillUnitsDetails(rowData.UnitsCode);
    }
}