$("button[data-dismiss=modal]").click(function () {
    $(".modal.in").removeClass("in").addClass("fade").fadeOut();
    $(".modal-backdrop").remove();
});

function editUserDetails() {
    $.ajax({
        url: "/ModalDialog/EditUserDetails",
        type: "POST",
        dataType: "JSON",
        data: { name: $('#txtName').val() },
        success: function (data) {
            if (data.Item1 == "EditUserSuccess") {
                alert("user updated successfully")
            }
            else {
                $('#spanerrorEditPrfl1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
                $('#spanerrorEditPrfl1').show();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#spanerrorEditPrfl1').html('0oo0... something is wrong at our side. we will fix that soon. Try some other day');
            $('#spanerrorEditPrfl1').show();
        }
    });
}