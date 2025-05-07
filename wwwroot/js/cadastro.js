document.addEventListener("DOMContentLoaded", function () {
    const btnCadastrar = document.getElementById("btn-cadastrar");

    btnCadastrar?.addEventListener("click", () => {
        const nickname = document.getElementById("cad-nickname").value.trim();
        const senha = document.getElementById("cad-senha").value;

        if (!nickname || !senha) {
            alert("Preencha todos os campos.");
            return;
        }

        const usuarios = JSON.parse(localStorage.getItem("usuarios")) || [];

        if (usuarios.some(u => u.nickname === nickname)) {
            alert("Nickname já cadastrado.");
            return;
        }

        const novoUsuario = { nickname, senha };
        usuarios.push(novoUsuario);
        localStorage.setItem("usuarios", JSON.stringify(usuarios));
        alert("Cadastro realizado com sucesso!");
        window.location.href = "index.html";
    });
});