//인증번호 발송
$(function () {
    $("#SendMail").click(function () {
        var mail = $("#input_mail");
        //if ($.trim(mail) == 0 || mail == null) {
        //    alert("이메일을 입력하세요!");
        //}
        //ajax
        $.ajax({
            url: "/Login/Send",
            type: "POST",
            cache: false,
            data: { input_mail: mail.val() },
            success: function (data, status) {
                alert("★인정번호가 발송됐습니다 메일을 확인해 주세요!");
            },
            error: function () {
                alert("발송에 실패했습니다. 잠시 후 다시 시도해 주세요.");
            }
        })
    })
})





//비밀번호 발송
$(function () {
    $("#SendPw").click(function () {

        var mail = $("#input_mail");
        $.ajax({
            url: "/Login/Send2",
            type: "POST",
            cache: false,
            data: { input_mail: mail.val() },
            success: function (data, status) {
                console.log("발송 성공");
                alert("비밀번호가 발송됐습니다.");
            },
            error: function () {
                console.log("발송 실패");
                alert("잠시 후 다시 시도해 주세요.");
            }
        })
    })
})







