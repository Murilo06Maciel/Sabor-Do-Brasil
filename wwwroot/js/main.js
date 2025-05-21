document.addEventListener("DOMContentLoaded", function () {
    const perfilContainer = document.getElementById("perfil-container");

    let usuarioLogado = JSON.parse(localStorage.getItem("usuarioLogado"));
    if (usuarioLogado) {
        perfilContainer.style.display = "block";
        document.querySelector(".login").style.display = "none";
    }

    document.getElementById('btn-alterar-perfil').addEventListener('click', function() {
        window.location.href = 'alterar_usuario.html';
    });
});