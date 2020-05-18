window.history.forward();
function noBack() { window.history.forward(); }

$(function () {
    $("form").submit(function () {
        var value2 = $("[id^='RdoCmpSize']").text();
        var value = $('input[name="RdoCmpSize"]:checked').text();
        var value1 = $('input[name="RdoCmpSize"]:checked').val();
    });
});

$(document).ready(function () {
    $('input:radio[name="RdoCategory"]').click(function () {
        var val = $('input[name="RdoCategory"]:checked').val();
        if (val === 'RESELLER')
            $("#DvEndUserType").hide();
        else
            $("#DvEndUserType").show();
    });
});

$(document).ready(function () {
    $('input:radio[name="RdoProdCtg"]').click(function () {
        var val = $('input[name="RdoProdCtg"]:checked').val();
        if (val === 'Hardware') {
            $("#dvddlprdhd").show();
            $("#showothermessage1").show();

            $("#dvddlprdsf").hide();
            $("#showothermessage2").hide();
        }
        else {
            $("#dvddlprdsf").show();
            $("#showothermessage2").show();

            $("#dvddlprdhd").hide();
            $("#showothermessage1").hide();
        }
    });
});

 


$(function () {
    $('#ddlindustry').change(function () {
        var value = $('#ddlindustry option:selected').text();
        $('#DdlHdnindustryText').val(value);
     });
});
 
 $(function () {  
    $("[id^='RdoDatabase']").change(function () {
        var value = $(this).next('label').text();
        $('#RdoHdnDatabaseText').val(value);
    });
});

$(function () {
    $("[id^='RdoBusinessType']").change(function () {
        var value = $(this).next('label').text();
        $('#RdoHidBusinessTypeTxt').val(value);
    });
});

$(function () {
    $("[id^='RdoTotCmpLoc']").change(function () {
        var value = $(this).next('label').text();
        $('#RdoHidTotCmpLoc').val(value);
    });
});

$(function () {
    $("[id^='RdoCmpEmpSize']").change(function () {
        var value = $(this).next('label').text();
        $('#RdoHidCmpEmpSize').val(value);
    });
});

$(function () {
    $('#ddlwarranty').change(function () {
        var value = $('#ddlwarranty option:selected').text();
        if (value === 'NO') {
             $("#DvInAmc").show();
        }
        else
            $("#DvInAmc").hide();
    });
});

 
 
function pnvalid( pinexpr) { 
    var user_id=document.getElementById("email"); 
    var filter="^([a-z A-Z 0-9 _\.\-])+\@(([a-z A-Z 0-9\-])+\.)+([a-z A-z 0-9 {3,3})+$"; 
    if(!filter.test(user_id.vlaue))  { 
        alert("Email is in format"); 
        user_id.focus(); 
        return false; 
    } 
}

$(function () {
    $('#BtnPincode').click(function () {
        var comments = $('#TxtPincode').val();
        var txt = $('#TxtPincode');
        if (txt.val() != null && txt.val() != '') {
            $.ajax({
                url: "/Home/CheckPincode", type: "POST", data: { 'VPincode': comments }, cache: false,
                success: function (data) { 
                    if (data) {
                        var msg = $('#LblMessage').text("PinCode OK..."); $('#BtnPincode').hide(); $('#TxtPincode').attr('readonly', true);
                        $("#submit").prop('disabled', false);
                        $('#HdnPin').val(comments);
                        alert("PinCode OK...");
                    } else { alert("PinCode Not Found..."); $("#submit").prop('disabled', true); }
                },
                error: function (xhr, ajaxOptions, thrownError) { alert("Check Failed..."); }
            });
        } else { alert('Enter PinCode For Validation') }
    });
});

//-------------------------------
