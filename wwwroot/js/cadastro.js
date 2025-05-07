document.getElementById("form-cadastro").addEventListener("submit", function (event) {
    event.preventDefault();

    const nome = document.getElementById("nome-cadastro").value;
    const email = document.getElementById("email-cadastro").value;
    const nickname = document.getElementById("nickname-cadastro").value;
    const senha = document.getElementById("senha-cadastro").value;

    fetch("http://localhost/seu_projeto/cadastrar_usuario.php", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ nome, email, nickname, senha })
    })
    .then(res => res.json())
    .then(data => {
        if (data.success) {
            alert("Usuário cadastrado com sucesso!");
            window.location.href = "index.html";
        } else {
            document.getElementById("mensagem-erro-cadastro").innerText = data.message;
        }
    })
    .catch(error => {
        console.error("Erro:", error);
        document.getElementById("mensagem-erro-cadastro").innerText = "Erro ao cadastrar usuário.";
    });
});