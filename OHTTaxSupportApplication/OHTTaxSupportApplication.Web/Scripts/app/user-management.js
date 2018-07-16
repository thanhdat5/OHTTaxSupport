var urlGetAll = "/api/users/getall";
var urlGetCompany = "/api/companies/getall";
var urlUploadImage = "/api/Users/uploadAvatar";
var urlGetById = "/api/users/getbyid/";
var urlDelete = "/api/users/delete?id=";
var urlCreate = "/api/users/create";
var urlUpdate = "/api/users/update";
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
        $('#oht-User-CompanyID').html(options);
    }
}


function refreshForm() {
    $('.oht-new-mode').show();
    $('.oht-edit-mode').hide();
    $('#oht-panel-form').removeClass("widget-color-orange");
    $('#uploadEditorImage')[0].value = null;
    $('#avatar').attr('src', '');
    $('#oht-User-Username').val(null);
    $('#oht-User-Password').val(null);
    $('#oht-User-ConfirmPassword').val(null);
    $('#oht-User-Fullname').val(null);
    $('#oht-User-CompanyID').val(null);
    $('#oht-User-Age').val(null);
    $('#oht-User-Address').val(null);
    $('#oht-User-AboutMe').val(null);
    $('#oht-User-IsActive').val(null);
    $('#oht-User-IsActive').parent().hide();
}

function refreshData() {
    refreshForm();
    $('#oht-result-data').html('<tr class="oht-loading"><td colspan="3">Loading...</td></tr>');
    ohtCallServiceToGet(urlGetAll, refreshDataSuccess);
}
function refreshDataSuccess(data) {
    $('*').css('cursor', 'default');
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        var response = data.Result;
        if (response == null || response.length <= 0) {
            $('#oht-result-data').html('<tr class="oht-loading"><td colspan="3">No record found.</td></tr>');
        } else {
            var innerHtml = '';
            $.each(response, function (index, element) {
                innerHtml +=
                    '<tr>' +
                    '<td>' + element.ID + '</td>' +
                    '<td>' + element.Username + '</td>' +
                    '<td>' + element.Fullname + '</td>' +
                    '<td>' + element.Company + '</td>' +
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
            $('#oht-User-ID').text(response.ID);
            $('#avatar').attr('src', response.Image);
            $('#oht-User-Username').val(response.Username);
            $('#oht-User-Password').val(response.Password);
            $('#oht-User-ConfirmPassword').val(response.Password);
            $('#oht-User-Fullname').val(response.Fullname);
            $('#oht-User-CompanyID').val(response.CompanyID);
            $('#oht-User-Age').val(response.Age);
            $('#oht-User-Address').val(response.Address);
            $('#oht-User-AboutMe').val(response.AboutMe);
            var status = response.IsActive ? 1 : 0;
            $('#oht-User-IsActive').val(status);
            $('#oht-User-IsActive').parent().show();

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
    var id = $('#oht-User-ID').text();
    var avatar = $('#avatar').prop('src');
    var username = $('#oht-User-Username').val();
    var password = $('#oht-User-Password').val();
    var confirmPassword = $('#oht-User-ConfirmPassword').val();
    var companyID = $('#oht-User-CompanyID').val();
    var fullName = $('#oht-User-Fullname').val();
    var location = $('#oht-User-Address').val();
    var age = $('#oht-User-Age').val();
    var lastOnline = $('#oht-User-LastOnline').text();
    var aboutMe = $('#oht-User-AboutMe').val();
    var status = $('#oht-User-IsActive').val();
    if (username == null || username.trim().length <= 0) {
        ohtToastMessage("error", "Username can not be null.");
        $('#oht-User-UserName').focus();
        return;
    }
    if (password == null || password.trim().length <= 0) {
        ohtToastMessage("error", "Password can not be null.");
        $('#oht-User-Password').focus();
        return;
    } else {
        if (password != confirmPassword) {
            ohtToastMessage("error", "Password not match.");
            $('#oht-User-ConfirmPassword').focus();
            return;
        }
    }
    if (companyID == null || companyID.trim().length <= 0) {
        ohtToastMessage("error", "Company can not be null.");
        $('#oht-User-CompanyID').focus();
        return;
    }
    if (fullName == null || fullName.trim().length <= 0) {
        ohtToastMessage("error", "Full Name can not be null.");
        $('#oht-User-FullName').focus();
        return;
    }
    var obj = {
        ID: id,
        Username: username,
        Password: password,
        Fullname: fullName,
        CompanyID: companyID,
        Image: avatar,
        Age: age,
        Address: location,
        AboutMe: aboutMe,
        LastOnline: new Date(),
        IsActive: parseInt(status)

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
        var id = $('#oht-User-ID').val();
        if (id == null || id.trim().length <= 0) {
            refreshForm();
        }
        ohtShowSuccess(data.Message);
        refreshData();
    }
}

function cancel() {
    $('#oht-Category-ID').val(null);
    refreshForm();
    $('.oht-edit-mode').hide();
    $('.oht-new-mode').show();
    $('#oht-panel-form').toggleClass("widget-color-orange");
}


function resetFileSelect() {
    $('#uploadEditorImage')[0].value = null;
}
$("#uploadEditorImage").change(function () {
    var data = new FormData();
    var files = $("#uploadEditorImage").get(0).files;
    if (files.length > 0) {
        data.append("HelpSectionImages", files[0]);
    }
    $.ajax({
        url: urlUploadImage,
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            if (response.Code != 0) {
                ohtShowCustomError(response.Message);
            } else {
                $('#avatar').attr('src', response.Result);
                ohtShowSuccess(response.Message);
            }
        },
        error: function (er) {
            alert(er);
        }

    });
});