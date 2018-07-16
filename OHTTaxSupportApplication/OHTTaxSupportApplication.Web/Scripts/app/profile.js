var urlGetProfile = "/api/Users/GetProfile";
var urlGetCompany = "/api/companies/getall";
var urlUpdateProfile = "/api/Users/update";
var urlUploadImage = "/api/Users/uploadAvatar";
getCompany();
function saveChangesClick() {
    var id = $('#lblID').text();
    var avatar = $('#avatar').prop('src');
    var username = $('#txtUserName').val();
    var password = $('#txtPassword').val();
    var confirmPassword = $('#txtConfirmPassword').val();
    var companyID = $('#ddlCompany').val();
    var fullName = $('#txtFullName').val();
    var location = $('#txtLocation').val();
    var age = $('#txtAge').val();
    var lastOnline = $('#lblLastOnline').text();
    var aboutMe = $('#txtAboutMe').val();
    if (username == null || username.trim().length <= 0) {
        ohtToastMessage("error", "Username can not be null.");
        $('#txtUserName').focus();
        return;
    }
    if (password == null || password.trim().length <= 0) {
        ohtToastMessage("error", "Password can not be null.");
        $('#txtPassword').focus();
        return;
    } else {
        if (password != confirmPassword) {
            ohtToastMessage("error", "Password not match.");
            $('#txtConfirmPassword').focus();
            return;
        }
    }
    if (companyID == null || companyID.trim().length <= 0) {
        ohtToastMessage("error", "Company can not be null.");
        $('#ddlCompany').focus();
        return;
    }
    if (fullName == null || fullName.trim().length <= 0) {
        ohtToastMessage("error", "Full Name can not be null.");
        $('#txtFullName').focus();
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
        LastOnline: lastOnline,
        IsActive: true

    };
    ohtCallServiceToUpdate(obj, urlUpdateProfile, urlUpdateProfileSuccess);
}
function urlUpdateProfileSuccess(data) {
    if (data.Code != 0) {
        ohtShowCustomError(data.Message);
    } else {
        ohtShowSuccess(data.Message);
        getProfile();
    }
}
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
        $('#ddlCompany').html(options);
        getProfile();
    }
}

function getProfile() {
    ohtCallServiceToGet(urlGetProfile, refreshDataSuccess);
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
            $('#avatar').attr('src', response.Image);
            $('#avatar').attr('alt', response.Fullname + "'s Avatar");
            $('#lblID').text(response.ID);
            $('#lblFullName').text(response.Fullname);
            $('#txtUserName').val(response.Username);
            $('#txtPassword').val(response.Password);
            $('#txtConfirmPassword').val(response.Password);
            $('#ddlCompany').val(response.CompanyID);
            $('#txtFullName').val(response.Fullname);
            $('#txtLocation').val(response.Address);
            $('#txtAge').val(response.Age);
            $('#lblLastOnline').text(response.LastOnline);
            $('#txtAboutMe').val(response.AboutMe);
        }
    }
}
function resetFileSelect() {
    $('#uploadEditorImage')[0].value = null;
    $('#avatar').attr('src','');
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