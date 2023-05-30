$(function () {
    $("#userban").click(function () {
        let answer = confirm("밴하시겠습니까?");
        if (answer) {
            $("form[name='frmban']").submit();
        }
    });
    $("#noban").click(function () {
        let answer = confirm("밴을 해제하시겠습니까?");
        if (answer) {
            $("form[name='frmban']").submit();
        }
    })
});

