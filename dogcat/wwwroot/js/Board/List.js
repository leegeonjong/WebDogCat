$(function () {
    $("[name='pageRows']").change(function () {
        alert($(this).val());  // 확인용
        const frm = $("[name='frmPageRows']");
        frm.attr("method", "POST");
        frm.attr("action", "PageRows");
        frm.submit();
    });
});

