
document.querySelector('form').addEventListener('submit', function (event) {
    //오류메시지 초기화
    resetErrorMessages();
    // 이름 필드 유효성 검사
    var nameInput = document.querySelector('input[name="Name"]');
    if (nameInput.value.trim() === '') {
        event.preventDefault(); // 폼 제출 막기
        showErrorMessage(nameInput, '이름을 입력하세요.');
    }

    // 종류 필드 유효성 검사
    var speciesInput = document.querySelector('input[name="Species"]');
    if (speciesInput.value.trim() === '') {
        event.preventDefault(); // 폼 제출 막기
        showErrorMessage(speciesInput, '종류를 입력하세요.');
    }

    // 나이 필드 유효성 검사
    var oldInput = document.querySelector('input[name="Old"]');
    if (oldInput.value.trim() === '') {
        event.preventDefault(); // 폼 제출 막기
        showErrorMessage(oldInput, '나이를 입력하세요.');
    }
    else if (isNaN(oldInput.value.trim())) {
        event.preventDefault(); // 폼 제출 막기
        showErrorMessage(oldInput, '숫자를 입력하세요.');
    }

    // 무게 필드 유효성 검사
    var weightInput = document.querySelector('input[name="Weight"]');
    if (weightInput.value.trim() === '') {
        event.preventDefault(); // 폼 제출 막기
        showErrorMessage(weightInput, '무게를 입력하세요.');
    }
    else if (isNaN(weightInput.value.trim())) {
        event.preventDefault(); // 폼 제출 막기
        showErrorMessage(weightInput, '숫자를 입력하세요.');
    }
});

// 오류 메시지 표시
function showErrorMessage(input, message) {
    var errorContainer = input.parentElement.querySelector('.text-danger');
    errorContainer.textContent = message;
}

// 오류 메시지 초기화
function resetErrorMessages() {
    var errorMessages = document.querySelectorAll('.text-danger');
    errorMessages.forEach(function (errorMessage) {
        errorMessage.textContent = '';
    });
}