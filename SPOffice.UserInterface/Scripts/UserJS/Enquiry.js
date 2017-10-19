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



        DataTables.EnquiryItemDetailsTable = $('#EnquiryItemDetailsTable').DataTable();
        DataTables.CourierTable = $('#EnquiryTable').DataTable(
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
               { "data": "EnquiryDate", "defaultContent": "<i>-</i>" },
               { "data": "CompanyName", "defaultContent": "<i>-</i>" },
               { "data": "Mobile", "defaultContent": "<i>-</i>" },
                  { "data": "LandLine", "defaultContent": "<i>-</i>" },
                  { "data": "EnquirySource", "defaultContent": "<i>-</i>" },
               
               { "data": "IndustryName", "defaultContent": "<i>-</i>" },
                { "data": "EnquiryStatus", "defaultContent": "<i>-</i>" },
                { "data": "LeadOwner", "defaultContent": "<i>-</i>" },
                //  { "data": null, "defaultContent": "<i>-</i>" },
               { "data": null, "orderable": false, "defaultContent": '<a href="#" title="Edit OtherIncome" class="actionLink"  onclick="Edit(this)" ><i class="glyphicon glyphicon-share-alt" aria-hidden="true"></i></a>' }
             ],
             columnDefs: [
                 { "targets": [0], "visible": false, "searchable": false },
               { className: "text-left", "targets": [1, 2, 3, 4, 5, 6,8,9,10] }
             ]
         });

        $('#EnquiryTable tbody').on('dblclick', 'td', function () {

            Edit(this);
        });



    } catch (x) {

        notyAlert('error', x.message);

    }

});


function Edit(id) { }

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
            BindAllEnquiries();
            notyAlert('Success', JsonResult.Message);
            if (JsonResult.Records.ID) {
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
