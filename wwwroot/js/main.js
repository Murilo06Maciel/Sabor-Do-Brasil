document.addEventListener("DOMContentLoaded", function () {
    const perfilContainer = document.getElementById("perfil-container");

    let usuarioLogado = JSON.parse(localStorage.getItem("usuarioLogado"));
    if (usuarioLogado) {
        perfilContainer.style.display = "block";
        document.querySelector(".login").style.display = "none";
    }
});