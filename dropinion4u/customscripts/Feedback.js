function PostFeedback() {
    var txtFeedback = $('.Feedback').val();
    swal({
        title: "Are you sure?",
        text: "Once modified, you will not be able to recover your profile!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: "/Support/Feedback?Feedback=" + txtFeedback,
                    type: "POST",
                    dataType: "JSON",
                    success: function (response) {
                        swal("Poof! Your profile has been modified!", {
                            icon: "success",
                        }).then(function () {                            
                            window.location.href = "/Home/Index"
                        });

                    },
                    failure: function (response) {
                        alert(response.responseText);
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });

            }
            else {
                swal("Your profil is safe!");
            }
        });
}