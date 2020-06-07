$(document).ready(function () {
    $('#spanerrorVerify').hide();
});
function validatecode() {
    var val = $('#txtverification').val().trim().length;
    if (val == 5) { 
        $('#btnVerifyCode').removeAttr("disabled");
        $('#spanerrorVerify').html('');
    }
    $('#txtverification').focus();
}


$('#txtverification').keyup(function () {
    var val = $('#txtverification').val().trim().length;
    if (val == 5) { 
        $('#btnVerifyCode').removeAttr("disabled");
        $('#spanerrorVerify').html('');
    }
    else {
        $('#spanerrorVerify').html('0oo0... 5 digit code is required before proceeding');
        $('#spanerrorVerify').show();
    }
});

function verifyCode() {
    var val = $('#txtverification').val().trim().length;
    if (val == 5) { 
        $.ajax({
            url: "/ModalDialog/VerifyCode",
            type: "POST",
            dataType: "JSON",
            data: { email: $('#txtverification').val() },
            success: function (data) {
                if (data.Item1 == "Verified") {
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $('#spanerrorVerify').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorVerify').show();
            }
        });
    }
    else {
        $('#spanerrorVerify').html('0oo0... 5 digit code is required before proceeding');
        $('#spanerrorVerify').show();
    }
}

function resendCode() {
}
