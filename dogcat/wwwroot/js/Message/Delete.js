var del_list = []; // 삭제 목록

$(function () {
    // 체크박스 선택 시 동작
    $(".message_select").change(function () {
        var msg_id = $(this).val(); // 체크박스 value값 가져오기 (메시지 pk)

        if ($(this).is(":checked")) { // 체크박스 체크 시
            del_list.push(msg_id); // 목록에 추가
        }
        else { // 체크 해제 시
            var target = del_list.indexOf(msg_id);
            if (target >= 0) {
                del_list.splice(target, 1); // 1개만 splice(삭제)
            }
        }
    });
});

// 삭제 버튼 클릭 시 동작
$(function () {
    $("#delete_message").click(function () {
        let answer = confirm("정말로 삭제하시겠습니까?");
        if (answer) {
            if (del_list.length > 0) {
                $.post("/Message/Delete", { ids: del_list }, function () {
                    alert("삭제되었습니다");
                    location.reload(); // 삭제 후 페이지 새로고침
                }).done(function () {
                    location.reload(); // 모든 삭제 요청 완료 후 페이지 새로고침
                });
            }
        }
    });
});
