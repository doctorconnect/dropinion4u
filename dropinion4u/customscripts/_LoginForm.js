$(document).ready(function () {
    $('#spanerrorNext').hide();
    $('#passwordform').hide();
    $('#registerform').hide();
    $('#confirmemailform').hide();
    $('#newpasswordform').hide();
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
        $('#spanerrorNext1').html('');
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
            data: { email: $('#email').val().trim() },
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
                    $('#spanerrorNext1').html('0oo0... Email Missing');
                    $('#spanerrorNext1').show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorNext1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorNext1').show();
            }
        });
    }
    else {
        $('#spanerrorNext1').html('0oo0... valid email addreess is required before proceeding');
        $('#spanerrorNext1').show();
    }
}

function back() {
    $('#hmsg').html('We don\'t spam!');
    $('#passwordform').hide();
    $('#registerform').hide();
    $('#newpasswordform').hide();
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
        $('#spanerrorLogin1').html('');
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
                    $('#spanerrorLogin1').html('0oo0... Incorrect Password');
                    $('#spanerrorLogin1').show();
                }
                else {
                    $('#spanerrorLogin1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                    $('#spanerrorLogin1').show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorLogin1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorLogin1').show();
            }
        });
    }
    else {
        $('#spanerrorLogin1').html('0oo0... Password is required to proceed');
        $('#spanerrorLogin1').show();
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
        $('#spanerrorRegister1').html('');
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
                    $('#spanerrorRegister1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                    $('#spanerrorRegister1').show();
                }
                else {
                    $('#spanerrorRegister1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                    $('#spanerrorRegister1').show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorRegister1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorRegister1').show();
            }
        });
    }
    else {
        $('#spanerrorConfirm').html('0oo0... Either Password too short or not matching');
        $('#spanerrorConfirm').show();
    }
}

function confirmemail() {
    $('#passwordform').hide();
    $('#confirmemail').val($('#email').val());
    $('#confirmemailform').show();
    $('#hmsg').html('Verify Your Email address');
}
function validateconfirmemail() {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test($('#confirmemail').val().trim())) {
        $('#btnConfirmEmail').removeAttr("disabled");
        $('#spanerrorConfirmEmail').html('');
    }
    $('#confirmemail').focus();
}
$('#confirmemail').keyup(function () {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test($('#confirmemail').val().trim())) {
        $('#btnConfirmEmail').removeAttr("disabled");
        $('#spanerrorConfirmEmail').html('');
        $('#spanerrorConfirmEmail1').html('');
    }
    else {
        $('#spanerrorConfirmEmail').html('0oo0... valid email addreess is required before proceeding');
        $('#spanerrorConfirmEmail').show();
    }
});
function requestOTP() {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test($('#confirmemail').val().trim())) {
        $.ajax({
            url: "/ModalDialog/RequestOTP",
            type: "POST",
            dataType: "JSON",
            data: { email: $('#confirmemail').val().trim() },
            success: function (data) {
                if (data.Item1 == "OTPSent") {
                    $('#confirmemailform').hide();
                    $('#newpasswordform').show();
                    $('#hmsg').html('Create new password for <a><b>' + data.Item2 + '</b></a>');
                }
                else if (data.Item1 == "EmailNotRegistered") {
                    $('#confirmemailform').hide();
                    $('#registerform').show();
                    $('#hmsg').html('Account does not exist. Create account for <a><b>' + data.Item2 + '</b></a>');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorConfirmEmail1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorConfirmEmail1').show();
            }
        });
    }
    else {
        $('#spanerrorConfirmEmail1').html('0oo0... valid email addreess is required before proceeding');
        $('#spanerrorConfirmEmail1').show();
    }
}
function backtopwdfield() {
    $('#hmsg').html('Password for <a><b>' + $('#email').val() + '</b></a>');
    $('#confirmemailform').hide();
    $('#passwordform').show();
}

function focuscreatenewpwdfield() {
    $('#otp').focus();
}
$('#newpassword').keyup(function () {
    var val = $('#newpassword').val().trim().length;
    if (val > 3) {
        if ($('#confirmpassword').val() == $('#newpassword').val()) {
            $('#btnUpdatePassword').removeAttr("disabled");
        }
        $('#spanerrornewpwd').html('');
        $('#spanerrornewpwd1').html('');
    }
    else {
        $('#spanerrornewpwd').html('0oo0... Password too short');
        $('#spanerrornewpwd').show();
    }
});
$('#confirmnewpassword').keyup(function () {
    var val = $('#confirmnewpassword').val().trim().length;
    if (val > 3 && $('#confirmnewpassword').val() == $('#newpassword').val()) {
        $('#btnUpdatePassword').removeAttr("disabled");
        $('#spanerrorConfirmnewpwd').html('');
    }
    else {
        $('#spanerrorConfirmnewpwd').html('0oo0... Password should match');
        $('#spanerrorConfirmnewpwd').show();
    }
});
function UpdatePassword() {
    var val = $('#newpassword').val().trim().length;
    var otplen = $('#otp').val().trim().length;
    if (otplen == 6) {
        if (val > 3 && $('#confirmnewpassword').val() == $('#newpassword').val()) {
            $.ajax({
                url: "/ModalDialog/VerifyOTP",
                type: "POST",
                dataType: "JSON",
                data: { otp: $('#otp').val(), email: $('#confirmemail').val(), pass: $('#newpassword').val() },
                success: function (data) {
                    if (data.Item1 == "VerifyOTPSuccessful") {
                        window.location.href = "/home/Index"
                    }
                    else if (data.Item1 == "InvalidOTP") {
                        $('#spanerrornewpwd1').html('Your OTP is Incorrect');
                        $('#spanerrornewpwd1').show();
                    }
                    else {
                        $('#spanerrornewpwd1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                        $('#spanerrornewpwd1').show();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $('#spanerrornewpwd1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                    $('#spanerrornewpwd1').show();
                }
            });
        }
        else {
            $('#spanerrorConfirmnewpwd').html('0oo0... Either Password too short or not matching');
            $('#spanerrorConfirmnewpwd').show();
        }
    }
    else {
        $('#spanerrorConfirmnewpwd').html('0oo0... OTP should be of 6 digits');
        $('#spanerrorConfirmnewpwd').show();
    }
}

function logout() {
    debugger;
    $.ajax({
        url: "/ModalDialog/Logout",
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            if (data.Item1 == "LogoutSuccessful") {                
                window.location.href = "/home/Index"
                toastr.success('Yes! You have successfully Logout your Acount!', 'Thanks for serve us !!!')
            }
        }
    })
}