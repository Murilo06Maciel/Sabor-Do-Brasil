document.addEventListener("DOMContentLoaded", function () {
    const btnCurtir = document.getElementById("btn-curtir");
    const btnDescurtir = document.getElementById("btn-descurtir");
    const contadorCurtidas = document.getElementById("contador-curtidas");
    const contadorDeslikes = document.getElementById("contador-deslikes");
    const btnComentar = document.getElementById("btn-comentar");
    const listaComentarios = document.getElementById("lista-comentarios");
    const inputComentario = document.getElementById("input-comentario");

    let curtidas = parseInt(localStorage.getItem("curtidas") || "0");
    let deslikes = parseInt(localStorage.getItem("deslikes") || "0");
    let comentarios = JSON.parse(localStorage.getItem("comentarios")) || [];

    contadorCurtidas.textContent = curtidas;
    contadorDeslikes.textContent = deslikes;
    atualizarComentarios();

    btnCurtir?.addEventListener("click", () => {
        curtidas++;
        localStorage.setItem("curtidas", curtidas);
        contadorCurtidas.textContent = curtidas;
    });

    btnDescurtir?.addEventListener("click", () => {
        deslikes++;
        localStorage.setItem("deslikes", deslikes);
        contadorDeslikes.textContent = deslikes;
    });

    btnComentar?.addEventListener("click", () => {
        const texto = inputComentario.value.trim();
        if (texto) {
            comentarios.push(texto);
            localStorage.setItem("comentarios", JSON.stringify(comentarios));
            atualizarComentarios();
            inputComentario.value = "";
        }
    });

    function atualizarComentarios() {
        listaComentarios.innerHTML = "";
        comentarios.forEach(comentario => {
            const li = document.createElement("li");
            li.textContent = comentario;
            listaComentarios.appendChild(li);
        });
    }
});