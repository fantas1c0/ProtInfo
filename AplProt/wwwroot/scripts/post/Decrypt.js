document.getElementById("btnEncrypt").addEventListener('click', send);
async function send() {
    const response = await fetch("/api/sss", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: document.getElementById("name").value,
            age: document.getElementById("age").value
        })
    });
    const message = await response.json();
    document.getElementById("message").innerText = message.text;
}