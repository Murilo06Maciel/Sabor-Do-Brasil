document.addEventListener("DOMContentLoaded", function () {
    const modalLogin = document.getElementById("modal-login");
    const btnCancelar = document.getElementById("btn-cancelar");
    const btnEntrar = document.getElementById("btn-entrar");
    const btnIrCadastro = document.getElementById("btn-ir-cadastro");
    const btnLogin = document.getElementById("btn-login");
    const mensagemErro = document.getElementById("mensagem-erro");

    btnLogin?.addEventListener("click", () => {
        modalLogin.style.display = "block";
    });

    btnCancelar?.addEventListener("click", () => {
        modalLogin.style.display = "none";
        mensagemErro.textContent = "";
    });

    btnIrCadastro?.addEventListener("click", () => {
        window.location.href = "cadastro.html";
    });

    btnEntrar?.addEventListener("click", () => {
        const nickname = document.getElementById("nickname").value.trim();
        const senha = document.getElementById("senha").value;

        const usuarios = JSON.parse(localStorage.getItem("usuarios")) || [];
        const usuario = usuarios.find(u => u.nickname === nickname && u.senha === senha);

        if (usuario) {
            localStorage.setItem("usuarioLogado", JSON.stringify(usuario));
            location.reload();
        } else {
            mensagemErro.textContent = "Nickname ou senha incorretos.";
        }
    });
});