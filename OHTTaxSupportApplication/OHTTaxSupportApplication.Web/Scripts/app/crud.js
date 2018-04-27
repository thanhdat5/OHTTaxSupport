function ohtCallServiceToGet(url, ajaxSuccess) {
    $('*').css('cursor', 'wait');
    $.ajax({
        method: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: url,
        success: ajaxSuccess,
        error: function (error) {
            ohtShowError(error);
        }
    });
}
function ohtCallServiceToAdd(data, url, ajaxSuccess) {
    $('*').css('cursor', 'wait');
    $.ajax({
        method: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(data),
        url: url,
        success: ajaxSuccess,
        error: function (error) {
            ohtShowError(error);
        }
    });
}
function ohtCallServiceToUpdate(data, url, ajaxSuccess) {
    $('*').css('cursor', 'wait');
    $.ajax({
        method: "PUT",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(data),
        url: url,
        success: ajaxSuccess,
        error: function (error) {
            ohtShowError(error);
        }
    });
}
function ohtCallServiceToDelete(url, ajaxSuccess) {
    $('*').css('cursor', 'wait');
    $.ajax({
        method: "DELETE",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: url,
        success: ajaxSuccess,
        error: function (error) {
            ohtShowError(error);
        }
    });
}

function ohtToastMessage(type, message) {
    $.toast({
        text: message,
        icon: type,
        showHideTransition: 'fade',
        allowToastClose: true,
        hideAfter: 2000,
        stack: 5,
        position: 'top-right',
        loader: false,
    });
}
function ohtShowError(error) {
    $('*').css('cursor', 'default');
    ohtToastMessage("error", "[Internal server error] " + error.statusText);
}
function ohtShowCustomError(errorMessage) {
    $('*').css('cursor', 'default');
    ohtToastMessage("error", errorMessage);
}
function ohtShowSuccess(successMessage) {
    $('*').css('cursor', 'default');
    ohtToastMessage("success", successMessage);
}

