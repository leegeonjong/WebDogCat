$(function () {
    $("#drop").click(function () {
        let answer = confirm("탈퇴하시겠습니까?");
        if (answer) {
            $("form[name='userform']").submit();
        }
    });

});