// 유효성 검사 : 아이디,비밀번호 찾기, 로그인, 회원가입
// 유효성 검사에 사용할 정규식
var mail = /^[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_.]?[0-9a-zA-Z])*.[a-zA-Z]{2,3}$/i; //이메일
var id = /^[a-zA-Z0-9]{3,16}$/; // 아이디 영문 숫자 3글자 이상
var pw = /^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]{8}$/; // 비밀번호
var _name = /^[가-힣]{2,4}$/;  //이름
var nickname = /^([a-zA-Z]{3,8}|[가-힣]{2,6})$/; //닉네임
var tel = /^(01[0|1|6|7|8|9])-?[0-9]{3,4}-?[0-9]{4}$/; // 연락처

var isvalid_mail = $("#emailcheck"); //이메일 인증여부
var isvalid_Id = $("#idcheck"); //아이디 중복검사 여부
var isvalid_tel = $("#phonecheck"); // sms인증여부
//------------------------------------------------------------------------------------
//사용자 입력값

//로그인 
var input_id = $("#input_id"); // 비밀번호 찾기, 회원가입이랑 공유
var input_pw = $("#input_pw");  // 회원가입이랑 공유
// 회원가입 (id,pw 공유)
var input_mail = $("#input_mail"); //아이디,비번 찾기랑 공유
var input_name = $("#input_name"); //아이디 찾기랑 공유
var input_NickName = $("#input_NickName");

//회원가입 시 비밀번호 체크
var input_pw2 = $("#input_pw2");

//------------------------------------------------------

// Script

// 로그인
$(function () {
    $("#Login").submit(function () {
        if (input_id.val() == "") {
            alert('아이디를 입력 하세요.');
            console.log(input_id.val());
            $("#input_id").focus();
            return false;
        }
        else if (input_pw.val() == "") {
            alert("비밀번호를 입력 하세요.");
            $("#input_pw").focus();
            return false;
        }
    })
})

    // 회원가입

    //양식입력 유효성 검사
        //아이디 검사
    $(function () {
        $("#Userid").on("keyup", function () {
            isvalid_Id.val("unable"); // ID변경 될 시 인증여부 값 초기화 (다시 인증해야 함)
            if (id.test($("#Userid").val()) == false) {
                $("#Go_idcheck").prop("disabled", true);
                $("#valid_userId").css("color", "red");
                $("#valid_userId").text("id는 영문숫자 3~16 글자");
            } else {
                $("#Go_idcheck").prop("disabled", false);
                $("#valid_userId").css("color", "#0C964A");
                $("#valid_userId").text("OK!");
            }
        });

    //비밀번호
    $("#Pw").on("keyup", function () {
        if (pw.test($("#Pw").val()) == false) {
            $("#valid_pw").css("color", "red");
            $("#valid_pw").text("비밀번호는 영문숫자 포함 8글자 이상");
        } else {
            $("#valid_pw").css("color", "#0C964A");
            $("#valid_pw").text("OK!");
        }
    });

    //비밀번호 확인
    $("#input_pw2").on("keyup", function () {
        if ($("#input_pw2").val() != $("#Pw").val()) {
            $("#valid_pw2").css("color", "red");
            $("#valid_pw2").text("비밀번호 불일치!!!");
        } else {
            $("#valid_pw2").css("color", "#0C964A");
            $("#valid_pw2").text("비밀번호 일치");
        }
    });

    //닉네임
    $("#NickName").on("keyup", function () {
        if (nickname.test($("#NickName").val()) == false) {
            $("#valid_NickName").css("color", "red");
            $("#valid_NickName").text("닉네임은 영어 3~8글자, 한글 2~6글자");
        } else {
            $("#valid_NickName").css("color", "#0C964A");
            $("#valid_NickName").text("사용가능한 닉네임 입니다.");
        }
    });

    //이름
    $("#Name").on("keyup", function () {
        if (_name.test($("#Name").val()) == false) {
            $("#valid_name").css("color", "red");
            $("#valid_name").text("올바른 이름형식이 아닙니다.");
        } else {
            $("#valid_name").css("color", "#0C964A");
            $("#valid_name").text("Ok!");
        }
    });

    //메일
    $("#Mail").on("keyup", function () {
        if (mail.test($("#Mail").val()) == false) {
            $("#valid_mail").css("color", "red");
            $("#valid_mail").text("올바른 이메일 형식이 아닙니다. ex)abc@aaaa.com");
        } else {
            $("#valid_mail").css("color", "#0C964A");
            $("#valid_mail").text("Ok!");
        }
    });

        //연락처
        //$("#PhoneNum").on("keyup", function () {
        //    if (tel.test($("#PhoneNum").val()) == false) {
        //        $("#valid_tel").css("color", "red");
        //        $("#valid_tel").text("휴대전화 번호를 올바르게 입력해 주세요.");
        //    } else {
        //        $("#valid_tel").css("color", "#0C964A");
        //        $("#valid_tel").text("Ok!");
        //    }
        //});
    });

    
    // 아이디 중복검사
$("#Go_idcheck").on("click", function () {
    // 변수 지정
    var input_id = $("#Userid").val();
    // URL 설정
    var url = "/User/Idcheck" + "?Userid=" + encodeURIComponent(input_id);

    // Fetch
    fetch(url)
        .then(function (response) {
            // 응답 실패
            if (!response.ok) {
                alert("잠시 후 다시 시도하세요");
                throw new Error("서버 요청에 실패했습니다. 다시 시도해주세요.");
            }
            return response.json();
        })
        .then(function (usable) { 
            if (usable == "unable") {
                isvalid_Id.val("unable");
                alert("이미 사용중인 아이디입니다.");
            }
            else if (usable == "able") {
                isvalid_Id.val("able");
                alert("사용 가능한 아이디입니다.");
            }
        });
});
//SMS 본인 인증 체크
//$("#").on("click", function () {

//})



// 회원가입 폼 검증
$(function () {
    $("#register").submit(function () {
        if ($.trim($("#Userid").val()) == "" || $("#Userid").val() == null) {
            alert('아이디를 입력 하세요.');
            $("#Userid").focus();
            return false;
        } else if (id.test($("#Userid").val()) == false) {
            alert("잘못된 형식의 아이디 입니다.");
            $("#Userid").focus();
            return false;
        }

        if ($("#Pw").val() == "" || $("#Pw").val() == null) {
            alert('비밀번호를 입력 하세요.');
            $("#Pw").focus();
            return false;
        } else if (pw.test($("#Pw").val()) == false) {
            alert("잘못된 형식의 비밀번호 입니다.");
            $("#Pw").focus();
            return false;
        }

        if ($("#Pw").val() != input_pw2.val()) {
            alert('비밀번호가 일치하지 않습니다.');
            $("#input_pw2").focus();
            return false;
        } else if ($.trim(input_pw2.val()) == "" || input_pw2.val() == null) {
            alert("비밀번호 확인을 꼭 해주세요!");
            $("#input_pw2").focus();
            return false;
        }

        if ($.trim($("#NickName").val()) == "" || $("#NickName").val() == null) {
            alert('사용하실 닉네임을 입력 하세요.');
            $("#NickName").focus();
            return false;
        } else if (nickname.test($("#NickName").val()) == false) {
            alert("잘못된 형식의 닉네임 입니다.");
            $("#NickName").focus();
            return false;
        }

        if ($.trim($("#Name").val()) == "" || $("#Name").val() == null) {
            alert('회원님의 이름을 입력 하세요.');
            $("#Name").focus();
            return false;
        } else if (_name.test($("#Name").val()) == false) {
            alert("이름을 올바르게 입력하세요!");
            $("#Name").focus();
            return false;
        }

        if ($.trim($("#Mail").val()) == "" || $("#Mail").val() == null) {
            alert('사용하실 이메일을 입력 하세요.');
            $("#Mail").focus();
            return false;
        } else if (mail.test($("#Mail").val()) == false) {
            alert("잘못된 형식의 이메일 입니다.");
            $("#Mail").focus();
            return false;
        }

        //if ($.trim($("#PhoneNum").val()) == "" || $("#PhoneNum").val() == null) {
        //    alert('회원님의 연락처를 입력해 주세요.');
        //    $("#PhoneNum").focus();
        //    return false;
        //} else if (tel.test($("#PhoneNum").val()) == false) {
        //    alert("잘못된 형식의 연락처 입니다.");
        //    $("#PhoneNum").focus();
        //    return false;
        //}

        

        //이메일인증, 아이디 중복검사 여부 체크

        //if ((isvalid_Id.val() == "able" && isvalid_tel.val() == "able") == false) {}
        if (isvalid_Id.val() == "unable")
        {
            alert("아이디 중복검사를 실시 해 주세요!");
            return false;
        }
        
    });
});

//ID중복 확인 버튼 클릭 시 수행 할 동작 (입력 id값 서버에 넘겨주기)

// POST(실패작)
//$("#Go_idcheck").on("click", function () {
//    var inputid = $("#Userid").val(); //사용자 입력 ID

    //    //URL 설정
    //    fetch("/User/Idcheck", {
    //        //메소드 설정
    //        method: "POST",
    //        //헤더 설정 (문서타입 : xml / Json)
    //        headers: {
    //            "Content-Type": "application/json"
    //        },
    //        cache: "no-cache",
    //        //body 설정 (전송할 데이터)
    //        body: JSON.stringify({ UserId: inputid })
    //    })
    //        .then(function (response) {
    //            if (!response.ok) { //실패 했을 때
    //                alert("서버 오류입니다. 잠시 후 다시 시도하세요!");
    //                throw new Error("서버 요청에 실패했습니다. 다시 시도해주세요.");
    //            }
    //            return response.json();
    //        })
    //        .then(function (useable) { // 요청 성공 시, 중복검사 결과
    //            if (useable == "unable") { //아이디가 중복일 경우
    //                alert("이미 사용중인 아이디 입니다.");
    //            }
    //            else if (useable == "able") { // 중복 아닐 경우
    //                isvalid_Id.val("1");
    //                alert("사용가능한 ID 입니다!");
    //            }
    //        })


//Get
$("#Go_idcheck").on("click", function () {
    // 변수 지정
    var input_id = $("#Userid").val();
    // URL 설정
    var url = "/Login/Idcheck" + "?Userid=" + encodeURIComponent(input_id);

    // Fetch
    fetch(url)
        .then(function (response) {
            // 응답 실패
            if (!response.ok) {
                alert("잠시 후 다시 시도하세요");
                throw new Error("서버 요청에 실패했습니다. 다시 시도해주세요.");
            }
            return response.json();
        })
        .then(function (usable) { // 변수명 수정: useable -> usable
            if (usable == "unable") {
                isvalid_Id.val("0");
                alert("이미 사용중인 아이디입니다.");
            }
            else if (usable == "able") {
                isvalid_Id.val("1");
                alert("사용 가능한 아이디입니다.");
            }
        });
});


//--------------------------Id PW 찾기 검증----------------------------------
//Id 찾기 검증
function validateForm() {
    var name = document.forms[0]["Name"].value;
    var email = document.forms[0]["Email"].value;

    if (name === "" && email === "") {
        alert("이름과 이메일을 입력하세요.");
        return false;
    }
    else if (name === "") {
        alert("이름을 입력하세요.");
        return false;
    }
    else if (email === "") {
        alert("이메일을 입력하세요.");
        return false;
    }
    else if (!validateEmailFormat(email)) {
        alert("올바른 이메일 형식이 아닙니다.");
        return false;
    }

}
function validatepassword() {
    var password = document.forms[1]["InputPassword"].value;
    var emailCodeSent = document.getElementById('emailCodeSent').value;

    if (emailCodeSent === "") {
        alert("이메일 인증번호를 먼저 발송하세요.");
        return false;
    }
    else if (password === "") {
        alert("인증번호를 입력하세요.");
        return false;
    }
}

//비밀번호 찾기 검증----------------------
function validateEmailForm() {
    var id = document.forms[0]["Id"].value;
    var email = document.forms[0]["Email"].value;

    if (id === "" && email === "") {
        alert("Id와 이메일을 입력하세요.");
        return false;
    }
    else if (id === "") {
        alert("Id를 입력하세요.");
        return false;
    }
    else if (email === "") {
        alert("이메일을 입력하세요.");
        return false;
    }
    else if (!validateEmailFormat(email)) {
        alert("올바른 이메일 형식이 아닙니다.");
        return false;
    }
}

function validateVerificationForm() {
    var verificationCode = document.forms[1]["InputPassword"].value;
    var emailCodeSent = document.getElementById('emailCodeSent').value;

    if (emailCodeSent === "") {
        alert("이메일 인증번호를 먼저 발송하세요.");
        return false;
    }
    else if (verificationCode === "") {
        alert("인증번호를 입력하세요.");
        return false;
    }
}
//이메일 형식 검증
function validateEmailFormat(email) {
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}







































    // 아이디 찾기


    // 비밀번호 찾기