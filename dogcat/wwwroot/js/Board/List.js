$(function () {
    $("[name='pageRows']").change(function () {
        const frm = $("[name='frmPageRows']");
        frm.attr("method", "POST");
        frm.attr("action", "PageRows");
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