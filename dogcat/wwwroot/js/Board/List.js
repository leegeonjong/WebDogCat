$(function () {
    $("[name='pageRows']").change(function () {
        const frm = $("[name='frmPageRows']");
        frm.attr("method", "POST");
        frm.attr("action", "pageRows");
        frm.attr("action", "category");
        frm.submit();

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
