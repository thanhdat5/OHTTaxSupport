var urlLogin = "/api/Users/CheckLogin"; 

function loginClick() {
    $('#oht-login-error').text("");
    var username = $('#oht-Login-Username').val();
    var password = $('#oht-Login-Password').val();
    if (username == null || username.trim().length <= 0) {
        $('#oht-login-error').text("Username can not be null.");  
        $('#oht-Login-Username').focus();
        return;
    }
    if (password == null || password.trim().length <= 0) {
        $('#oht-login-error').text("Password can not be null.");
        $('#oht-Login-Password').focus();
        return;
    }
    var obj = {
        username: username,
        password: password
    };
    ohtCallServiceToAdd(obj, urlLogin, loginCheckSuccess);
}
function loginCheckSuccess(data) {
    if (data.Code != 0) {
        $('#oht-login-error').text(data.Message);  
    } else {
        window.location = "/";
    }
    $('*').css('cursor', 'default');
}
