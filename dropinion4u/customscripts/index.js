

function LoadPostsAndComment() {
    $.ajax
        ({
            url: "/Home/LoadPostsAndCommentPartialView",
            contentType: "application/html; charset=utf-8",
            type: "POST",
            datatype: "html",
            success: function (t) {
                $("#dvHomePostsAndCommentBind").html(t)
            },
            error: function () {
                $("#dvHomePostsAndCommentBind").html("Post Not Found");
                window.location.href = "/Home/Error";
            }
        })
}


$(function () {
    $(window).resize(function () {
        if (window.innerWidth >= 992) {
            $('#divProfile').fadeIn('slow');
            $('#divFeeds').fadeIn('slow');
            $('#divRight').fadeIn('slow');
        }
        else {
            $('#divProfile').fadeOut('fast');
            $('#divRight').fadeOut('fast');
            $('#divFeeds').fadeIn('slow');
        }
    }).resize();
});
function showProfile() {
    //$('#divRight').addClass('hidden-xs');
    //$('#divFeeds').addClass('hidden-xs');
    //$('#divProfile').removeClass('hidden-xs');

    $('#divRight').fadeOut('fast');
    $('#divFeeds').fadeOut('fast');
    $('#divProfile').fadeIn('slow');
}
function showFeeds() {
    //$('#divRight').addClass('hidden-xs');
    //$('#divProfile').addClass('hidden-xs');
    //$('#divFeeds').removeClass('hidden-xs');

    $('#divRight').fadeOut('fast');
    $('#divProfile').fadeOut('fast');
    $('#divFeeds').fadeIn('slow');
}
function showRight() {
    //$('#divFeeds').addClass('hidden-xs');
    //$('#divProfile').addClass('hidden-xs');
    //$('#divRight').removeClass('hidden-xs');

    $('#divFeeds').fadeOut('fast');
    $('#divProfile').fadeOut('fast');
    $('#divRight').fadeIn('slow');

    //$('#divFeeds').fadeOut('fast', function () {
    //    $('#divFeeds').addClass('hidden-xs');
    //});
    //$('#divProfile').fadeOut('fast', function () {
    //    $('#divProfile').addClass('hidden-xs');
    //});
    //$('#divRight').fadeIn('slow', function () {
    //    $('#divRight').removeClass('hidden-xs');
    //});
}