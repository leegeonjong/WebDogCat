$(function () {
    $("#delete").click(function () {
        let answer = confirm("삭제하시겠습니까?");
        if (answer) {
            $("form[name='frmpet']").submit();
        }
    });

});