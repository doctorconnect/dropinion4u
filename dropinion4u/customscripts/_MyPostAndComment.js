function showhide(id) {
    var e = document.getElementById('P_' + id);
    e.style.display = (e.style.display == 'block') ? 'none' : 'block';
}


function LoadPostLike(Id) {
    debugger;
    $.ajax
        ({
            url: "PostLikes?key=" + Id.id,
            contentType: "application/html; charset=utf-8",
            type: "POST",
            datatype: "html",
            success: function (t) {
                $("#dvHomePostsAndCommentBind").html(t)
            },
            error: function () {
                $("#dvHomePostsAndCommentBind").html("PostsAndComment Not Found");
                window.location.href = "/Home/Error";
            }
        });
}

function DeletePost(key) {
    //var Identifier = "POST";
    $.confirm({
        title: '',
        type: 'red',
        content: '<p style="font-style:italic;">Are you sure you want to Delete post ?</p>',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Home/DeletePost?keyId=" + key + "&Identifier=POST",
                    method: 'post',
                    datatype: "html",
                    success: function (t) {
                        $("#dvHomePostsAndCommentBind").html(t)
                    },
                    error: function () {
                        $("#dvHomePostsAndCommentBind").html("PostsAndComment Not Found");
                        window.location.href = "/Home/Error";
                    }
                });
            },
            cancel: function () {
            }
        }
    });
}

function FlagPost(key, userId) {
    $.confirm({
        title: '',
        type: 'red',
        content: '<p style="font-style:italic;">Are you sure, you found this post inappropriate? Once Flagged, it will go to Admin for Review.</p>',
        buttons: {
            confirm: function () {
                $.ajax({
                    url: "/Home/FlagPost?key=" + key + "&Identifier=POST&Id=" + userId + "",
                    method: 'post',
                    datatype: "html",
                    success: function (t) {
                        $("#dvHomePostsAndCommentBind").html(t)
                    },
                    error: function () {
                        $("#dvHomePostsAndCommentBind").html("PostsAndComment Not Found");
                        window.location.href = "/Home/Error";
                    }
                });
            },
            cancel: function () {
            }
        }
    });
}

function PostComment(Pid) {
    var controlName = Pid.id;
    var Comment = $("#txt_" + controlName + "").text();
    if (Comment != null) {
        $.ajax({
            type: "POST",
            url: "/Home/SaveComment?Identifier=POST&txtcomment=" + Comment + "&PostId=" + Pid.id,
            contentType: "application/html; charset=utf-8",
            datatype: "html",
            success: function (t) {
                $("#dvHomePostsAndCommentBind").html(t)
            },
            error: function () {
                $("#dvHomePostsAndCommentBind").html("PostsAndComment Not Found");
                window.location.href = "/Home/Error";
            }
        });
    }
}