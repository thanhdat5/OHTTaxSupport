var urlGetAll = "/api/departments/getall";
var urlGetById = "/api/departments/getbyid/";
var urlDelete = "/api/departments/delete?id=";
var urlCreate = "/api/departments/create";
var urlUpdate = "/api/departments/update";
var urlGetCompany = "/api/companies/getall";
getCompany();
function getCompany() {
    ohtCallServiceToGet(urlGetCompany, getCompanySuccess);
}
function getCompanySuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        var options = '';
        for (var i = 0; i < response.length; i++) {
            options += '<option value="' + response[i].ID + '">' + response[i].CompanyName + '</option>';
        }
        $('#oht-Department-Company').html(options);
    }
}

function refreshForm() {
    $('#oht-Department-Name').val(null);
    $('#oht-Department-Company').val(null);
    $('#oht-Department-Addess').val(null);
}

function refreshData() {
    $('#oht-result-data').html('<tr class="oht-loading"><td colspan="4">Loading...</td></tr>');
    ohtCallServiceToGet(urlGetAll, refreshDataSuccess);
}
function refreshDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#oht-result-data').html('<tr class="oht-loading"><td colspan="4">No record found.</td></tr>');
        } else {
            var innerHtml = '';
            $.each(response, function (index, element) {
                innerHtml +=
                    '<tr>' +
                    '<td>' + element.ID + '</td>' +
                    '<td>' + element.DepartmentName + '</td>' +
                    '<td>' + element.CompanyName + '</td>' +
                    '<td>' +
                    '<button class="btn btn-primary btn-xs" id="edit-1" onclick="editItem(' + element.ID + ')">' +
                    '<span class="fa fa-edit"></span>' +
                    '</button>&nbsp;' +
                    '<button class="btn btn-danger btn-xs" id="delete-1" onclick="deleteItem(' + element.ID + ')">' +
                    '<span class="fa fa-trash"></span>' +
                    '</button>' +
                    '</td>' +
                    '</tr>';
            });
            $('#oht-result-data').html(innerHtml);
        }
    }
}

function editItem(id) {
    ohtCallServiceToGet(urlGetById + id, loadItemSuccess);
}
function loadItemSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null) {
            ohtShowCustomError('Item can not be found.');
        } else {
            // Set value
            $('#oht-Department-ID').val(response.ID);
            $('#oht-Department-Name').val(response.DepartmentName);
            $('#oht-Department-Company').val(response.CompanyID);
            $('#oht-Department-Address').val(response.Address);

            $('.oht-new-mode').hide();
            $('.oht-edit-mode').show();
            $('.oht-key').attr("disabled", "disabled");

            $('#oht-panel-form').toggleClass("widget-color-orange");
        }
    }
}

function deleteItem(id) {
    if (confirm("Are you want to delete this item?")) {
        // Delete item
        ohtCallServiceToDelete(urlDelete + id, deleteItemSuccess);

    }
    return false;
}
function deleteItemSuccess(data) {
    $('*').css('cursor', 'default');
    cancel();
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        ohtShowSuccess(data.Message);
        refreshData();
    }
}

function saveItem() {
    var id = $('#oht-Department-ID').val();
    var value = $('#oht-Department-Name').val();
    var company = $('#oht-Department-Company').val();
    var address = $('#oht-Department-Address').val();
    if (value == null || value.trim().length <= 0) {
        ohtToastMessage("error", "Department Name can not be null.");
        $('#oht-Department-Name').focus();
        return;
    }
    if (company == null || company.trim().length <= 0) {
        ohtToastMessage("error", "Company can not be null.");
        $('#oht-Department-Company').focus();
        return;
    }
    var obj = {
        ID: id,
        DepartmentName: value,
        CompanyID: company,
        Address: address,
        IsActive: true
    };
    if (id == null || id.trim().length <= 0) {
        obj.ID = 0;
        ohtCallServiceToAdd(obj, urlCreate, saveItemSuccess);
    } else {
        ohtCallServiceToUpdate(obj, urlUpdate, saveItemSuccess);
    }
}
function saveItemSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var id = $('#oht-Department-ID').val();
        if (id == null || id.trim().length <= 0) {
            $('#oht-Department-Name').val(null);
            $('#oht-Department-Company').val(null);
            $('#oht-Department-Address').val(null);
        }
        ohtShowSuccess(data.Message);
        refreshData();
    }
}

function cancel() {
    $('#oht-Department-ID').val(null);
    refreshForm();
    $('.oht-edit-mode').hide();
    $('.oht-new-mode').show();
    $('#oht-panel-form').toggleClass("widget-color-orange");
} 