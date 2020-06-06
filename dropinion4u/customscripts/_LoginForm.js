$(document).ready(function () {
    $('#spanerrorNext').hide();
    $('#passwordform').hide();
    $('#registerform').hide();
});



function validateemail() {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test($('#email').val().trim())) {
        $('#btnNext').removeAttr("disabled");
        $('#spanerrorNext').html('');
    }
    $('#email').focus();
}


$('#email').keyup(function () {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test($('#email').val().trim())) {
        $('#btnNext').removeAttr("disabled");
        $('#spanerrorNext').html('');
    }
    else {
        $('#spanerrorNext').html('0oo0... valid email addreess is required before proceeding');
        $('#spanerrorNext').show();
    }
});

function submitEmail() {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test($('#email').val().trim())) {
        $.ajax({
            url: "/ModalDialog/CheckEmail",
            type: "POST",
            dataType: "JSON",
            data: { email: $('#email').val() },
            success: function (data) {
                if (data.Item1 == "Login") {
                    $('#emailform').hide();
                    $('#passwordform').show();
                    $('#hmsg').html('Password for <a><b>' + data.Item2 + '</b></a>');
                }
                else if (data.Item1 == "Register") {
                    $('#emailform').hide();
                    $('#registerform').show();
                    $('#hmsg').html('Create account for <a><b>' + data.Item2 + '</b></a>');
                }
                else if (data.Item1 == "LoginFailed") {
                    $('#spanerrorNext').html('0oo0... Email Missing');
                    $('#spanerrorNext').show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorNext').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorNext').show();
            }
        });
    }
    else {
        $('#spanerrorNext').html('0oo0... valid email addreess is required before proceeding');
        $('#spanerrorNext').show();
    }
}

function back() {
    $('#hmsg').html('We don\'t spam!');
    $('#passwordform').hide();
    $('#registerform').hide();
    $('#emailform').show();
}

function validatepwdfield() {
    var val = $('#password').val().trim().length;
    if (val > 0) {
        $('#btnLogin').removeAttr("disabled");
        $('#spanerrorLogin').html('');
    }
    $('#password').focus();
}

$('#password').keyup(function () {
    var val = $('#password').val().trim().length;
    if (val > 0) {
        $('#btnLogin').removeAttr("disabled");
        $('#spanerrorLogin').html('');
    }
    else {
        $('#spanerrorLogin').html('0oo0... Password is required to proceed');
        $('#spanerrorLogin').show();
    }
});
function login() {
    var val = $('#password').val().trim().length;
    if (val > 0) {
        $.ajax({
            url: "/ModalDialog/Login",
            type: "POST",
            dataType: "JSON",
            data: { pass: $('#password').val() },
            success: function (data) {
                if (data.Item1 == "LoginSuccessful") {
                    window.location.href = "/home/Index"
                }
                else if (data.Item1 == "LoginFailed") {
                    $('#spanerrorLogin').html('0oo0... Incorrect Password');
                    $('#spanerrorLogin').show();
                }
                else {
                    $('#spanerrorLogin').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                    $('#spanerrorLogin').show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorLogin').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorLogin').show();
            }
        });
    }
    else {
        $('#spanerrorLogin').html('0oo0... Password is required to proceed');
        $('#spanerrorLogin').show();
    }
}

function focuscreatepwdfield() {
    $('#registrationpassword').focus();
}

$('#registrationpassword').keyup(function () {
    var val = $('#registrationpassword').val().trim().length;
    if (val > 3) {
        if ($('#confirmpassword').val() == $('#registrationpassword').val()) {
            $('#btnRegister').removeAttr("disabled");
        }
        $('#spanerrorRegister').html('');
    }
    else {
        $('#spanerrorRegister').html('0oo0... Password too short');
        $('#spanerrorRegister').show();
    }
});
$('#confirmpassword').keyup(function () {
    var val = $('#confirmpassword').val().trim().length;
    if (val > 3 && $('#confirmpassword').val() == $('#registrationpassword').val()) {
        $('#btnRegister').removeAttr("disabled");
        $('#spanerrorConfirm').html('');
    }
    else {
        $('#spanerrorConfirm').html('0oo0... Password should match');
        $('#spanerrorConfirm').show();
    }
});

function register() {
    var val = $('#registrationpassword').val().trim().length;
    if (val > 3 && $('#confirmpassword').val() == $('#registrationpassword').val()) {
        $.ajax({
            url: "/ModalDialog/Register",
            type: "POST",
            dataType: "JSON",
            data: { pass: $('#registrationpassword').val() },
            success: function (data) {
                if (data.Item1 == "RegistrationSuccessful") {
                    window.location.href = "/home/Index"
                }
                else if (data.Item1 == "RegistrationFailed") {
                    $('#spanerrorRegister').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                    $('#spanerrorRegister').show();
                }
                else {
                    $('#spanerrorRegister').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                    $('#spanerrorRegister').show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorRegister').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorRegister').show();
            }
        });
    }
    else {
        $('#spanerrorConfirm').html('0oo0... Either Password too short or not matching');
        $('#spanerrorConfirm').show();
    }
}