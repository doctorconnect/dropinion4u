function PostFeedback() {
    var txtFeedback = $('.Feedback').val();
    swal({
        title: "Are you sure?",
        text: "Once You Submit feedback, you will not be able to update your feedback!",
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
                        swal("Poof! Your feedback has been Submited!", {
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
                swal("Your feedback is safe!");
            }
        });
}