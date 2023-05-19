function confirmDelete() {
    let answer = confirm("삭제하시겠습니까?");
    if (answer) {
        var writeId = parseInt($("form[name='frmDelete'] input[name='WriteId']").val());

        var hiddenInput = $("<input>").attr("type", "hidden").attr("name", "id").val(writeId);

        $("form[name='frmDelete']").append(hiddenInput);

        $("form[name='frmDelete']").submit();
    }
}

function confirmCommentDelete(commentId) {
    let answer = confirm("댓글을 삭제하시겠습니까?");
    if (answer) {
        var hiddenInput = $("<input>").attr("type", "hidden").attr("name", "id").val(commentId);

        $("form[name='CommentDelete']").append(hiddenInput);

        $("form[name='CommentDelete']").submit();
    }
}

$(function () {
    // 글 삭제 버튼
    $("#btnDel").click(function () {
        confirmDelete();
    });

    // 댓글 삭제 버튼
    $("[id^='btnDeleteComment']").click(function () {
        var commentId = $(this).attr("data-comment-id");
        confirmCommentDelete(commentId);
    });
});







