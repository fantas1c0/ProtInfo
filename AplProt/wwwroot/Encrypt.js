document.addEventListener('DOMContentLoaded', () => {
    document.getElementById("btnEncrypt").addEventListener('click', () => send("encrypt"));
    document.getElementById("btnDecrypt").addEventListener('click', () => send("decrypt"));
    document.getElementById("btnGenKey").addEventListener('click', generateKey);
});

function clearFeedback() {
    document.getElementById("key_feedback").innerHTML = "";
    document.getElementById("msg_feedback").innerHTML = "";

    const keyInput = document.getElementById("key_code");
    const msgInput = document.getElementById("text_message");
    keyInput.classList.remove("is-invalid", "border-warning");
    msgInput.classList.remove("is-invalid", "border-warning");
}

/**
 * Failed  -> is-invalid + invalid-feedback (красная рамка + красный текст)
 * Warning -> border-warning + text-warning под полем
 */
function applyFieldStatus(inputEl, feedbackEl, status, message) {
    if (status === "Failed") {
        inputEl.classList.add("is-invalid");
        feedbackEl.innerHTML = `<div class="invalid-feedback">${message || "Некорректное значение"}</div>`;
    } else if (status === "Warning") {
        inputEl.classList.add("border-warning");
        feedbackEl.innerHTML = `<div class="text-warning small mt-1">${message || "Предупреждение"}</div>`;
    }
}

async function generateKey() {
    const labId = document.getElementById("type").value;
    const textMessage = document.getElementById("text_message").value;

    if (!labId) {
        document.getElementById("result").innerHTML =
            `<div class="alert alert-warning mb-0">Сначала выберите тип шифрования</div>`;
        return;
    }

    const length = textMessage.length > 0
        ? Math.max(1, Math.floor(textMessage.length / 5))
        : 10;

    try {
        const response = await fetch("/api/random_key", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({ id_Labs: parseInt(labId, 10), length })
        });

        const data = await response.json();

        if (!response.ok) {
            document.getElementById("result").innerHTML =
                `<div class="alert alert-danger mb-0">Ошибка: ${data.error || "неизвестная"}</div>`;
            return;
        }

        document.getElementById("key_code").value = data.key;
        clearFeedback();
    } catch (e) {
        document.getElementById("result").innerHTML =
            `<div class="alert alert-danger mb-0">Ошибка сети: ${e.message}</div>`;
    }
}

async function send(mode) {
    const labId = document.getElementById("type").value;
    const keyCode = document.getElementById("key_code").value;
    const textMessage = document.getElementById("text_message").value;

    const resultDiv = document.getElementById("result");
    resultDiv.innerHTML = `<span class="text-secondary">Обработка...</span>`;
    clearFeedback();

    try {
        const response = await fetch("/api/result_encrypt", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                id_Labs: parseInt(labId, 10),
                keyCode: keyCode,
                textMessage: textMessage
            })
        });

        const data = await response.json();

        if (!response.ok) {
            resultDiv.innerHTML =
                `<div class="alert alert-danger mb-0">Ошибка: ${data.error || "неизвестная"}</div>`;
            return;
        }

        applyFieldStatus(
            document.getElementById("key_code"),
            document.getElementById("key_feedback"),
            data.keyStatus,
            data.keyMessage
        );

        applyFieldStatus(
            document.getElementById("text_message"),
            document.getElementById("msg_feedback"),
            data.textStatus,
            data.textMessage
        );

        const label = mode === "decrypt" ? "Расшифровка" : "Шифр";
        resultDiv.innerHTML =
            `<div><span class="text-info fw-bold">${label}:</span> ${data.encrypted || "—"}</div>`;

    } catch (e) {
        resultDiv.innerHTML =
            `<div class="alert alert-danger mb-0">Ошибка сети: ${e.message}</div>`;
    }
}
/*
document.getElementById("btnEncrypt").addEventListener('click', send);
async function send() {
    const labId = document.getElementById("type").value;
    const keyCode = document.querySelector('input[name="key_code"]').value;
    const textMessage = document.querySelector('input[name="text_message"]').value;

    сброс предыдущего результата
    document.getElementById("result").innerText = "";
    try {
        const response = await fetch("/api/result_encrypt", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                id_Labs: parseInt(labId, 10),
                keyCode: keyCode,
                textMessage: textMessage
            })
        });

        const data = await response.json();

        if (!response.ok) {
            document.getElementById("result").innerText = "Ошибка: " + (data.error || "неизвестная");
            return;
        }

        document.getElementById("result").innerText = data.result;
    }
    catch (e) {
        document.getElementById("result").innerText = "Ошибка сети: " + e.message;
    }

}

document.getElementById("btnEncrypt").addEventListener('click', send);
*/
