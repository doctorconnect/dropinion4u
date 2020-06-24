
//$(function () {
//    $("#btnPost").click(function () {
//        var msg1 = $('#txtPost').text();
//        var msg = msg1.replace('&', '!!').replace('/', '!!!');
//        if (msg != null) {
//            $.ajax({
//                type: "POST",
//                url: "/Home/SavePost?Message=" + msg,
//                contentType: "application/html; charset=utf-8",
//                datatype: "html",
//                success: function (t) {
//                    $("#dvHomePostsAndCommentBind").html(t)
//                },
//                error: function () {
//                    $("#dvHomePostsAndCommentBind").html("PostsAndComment  Not Found");
//                    window.location.href = "/Home/Error";
//                }
//            });
//        }
//    });
//});

function Post() {
    var msg1 = $('#txtPost').text();
    var msg = msg1.replace('&', '!!').replace('/', '!!!');
    if (msg != null) {
        $.ajax({
            type: "POST",
            url: "/Home/SavePost?Message=" + msg,
            contentType: "application/html; charset=utf-8",
            datatype: "html",
            success: function (t) {
                $("#dvHomePostsAndCommentBind").html(t)
            },
            error: function () {
                $("#dvHomePostsAndCommentBind").html("PostsAndComment  Not Found");
                window.location.href = "/Home/Error";
            }
        });
    }
}

$('#txtPost').keyup(function () {
    var maxLength = $(this).text().length;
    if (maxLength > 1000) {
        alert('You cannot enter more than 1000 chars');
        return false;
    }
});