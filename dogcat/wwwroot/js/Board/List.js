$(function () {
    $("[name='pageRows'], [name='category']").change(function () {
        const selectedPageRows = $("[name='pageRows']").val(); // 선택한 pageRows 옵션의 값 가져오기
        const selectedCategory = $("[name='category']").val(); // 선택한 category 옵션의 값 가져오기

        const frm = $("[name='frmCombined']");
        frm.attr("method", "GET");
        frm.attr("action", "/Board/List");
        frm.find("select[name='pageRows']").val(selectedPageRows); // pageRows 값을 설정
        frm.find("select[name='category']").val(selectedCategory); // category 값을 설정
        frm.submit();
    });
});

$(function () {
    $("[name='pageRows']").change(function () {
        const selectedOption = $(this).val();  // 선택한 옵션의 값 가져오기

        const frm = $("[name='frmPageRows']");
        frm.attr("method", "POST");
        frm.attr("action", "PageRows");
        frm.find("select[name='pageRows']").val(selectedOption);  // pagerows 값을 설정
        frm.submit();

        const frm1 = $("[name='frmCategory']");
        frm1.attr("method", "GET");
        frm1.attr("action", "/Board/List");
        frm1.find("select[name='pageRows']").val(selectedOption);  // pagerows 값을 설정
        frm1.submit();
    });
});

$(function () {
    $("[name='category']").change(function () {
        const frm = $("[name='frmCategory']");
        frm.attr("method", "GET"); 
        frm.attr("action", "/Board/List");
        frm.submit();
    });
});

function sendButtonValue(buttonName) {
    // 버튼 이름을 이용하여 원하는 동작 수행
    if (buttonName === 'Write') {
        window.location.href = '/Board/IsUser?buttonName=Write'; // IsUser 액션 호출하면서 버튼 이름 전달

    } else if (buttonName === 'Detail') {
        // 상세보기 버튼인 경우
        // 필요한 처리를 수행
        window.location.href = '/Board/IsUser?buttonName=Detail&writeId='; // IsUser 액션 호출하면서 버튼 이름과 writeId 전달
    }
}
