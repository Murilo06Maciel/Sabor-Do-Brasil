document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.publicacao').forEach(function(pub) {
        const likeBtn = pub.querySelector('.like');
        const dislikeBtn = pub.querySelector('.dislike');
        const likesSpan = pub.querySelector('.likes');
        const dislikesSpan = pub.querySelector('.dislikes');
        const chatBtn = pub.querySelector('.comentarios');
        const numComentariosSpan = pub.querySelector('.num-comentarios');
        const comentarioContainer = pub.querySelector('.comentario-container');
        const inputComentario = comentarioContainer.querySelector('input');
        const btnComentar = comentarioContainer.querySelector('.btn-comentar');
        const comentariosLista = comentarioContainer.querySelector('.comentarios-lista');

        let likes = 0;
        let dislikes = 0;
        let comentarios = [];
        let interacao = null; // null, 'like', 'dislike'

        likeBtn.addEventListener('click', () => {
            if (interacao === 'like') return; // já curtiu
            if (interacao === 'dislike') {
                dislikes--;
                dislikesSpan.textContent = dislikes;
            }
            likes++;
            likesSpan.textContent = likes;
            interacao = 'like';
            atualizarTotais();
        });

        dislikeBtn.addEventListener('click', () => {
            if (interacao === 'dislike') return; // já deu dislike
            if (interacao === 'like') {
                likes--;
                likesSpan.textContent = likes;
            }
            dislikes++;
            dislikesSpan.textContent = dislikes;
            interacao = 'dislike';
            atualizarTotais();
        });

        chatBtn.addEventListener('click', () => {
            comentarioContainer.style.display = comentarioContainer.style.display === 'none' ? 'block' : 'none';
        });

        btnComentar.addEventListener('click', () => {
            const texto = inputComentario.value.trim();
            if (texto) {
                comentarios.push(texto);
                numComentariosSpan.textContent = comentarios.length;
                const li = document.createElement('div');
                li.textContent = texto;
                comentariosLista.appendChild(li);
                inputComentario.value = '';
            }
        });
    });

    function atualizarTotais() {
        let totalLikes = 0;
        let totalDislikes = 0;

        document.querySelectorAll('.publicacao').forEach(pub => {
            totalLikes += parseInt(pub.querySelector('.likes').textContent) || 0;
            totalDislikes += parseInt(pub.querySelector('.dislikes').textContent) || 0;
        });

        document.getElementById('total-likes').textContent = totalLikes;
        document.getElementById('total-dislikes').textContent = totalDislikes;
    }

    // Atualiza ao carregar a página
    document.addEventListener('DOMContentLoaded', atualizarTotais);

    document.querySelectorAll('.btn-comentar').forEach((btn) => {
        btn.addEventListener('click', function () {
            const container = btn.closest('.comentario-container');
            const input = container.querySelector('input');
            const lista = container.querySelector('.comentarios-lista');
            const comentario = input.value.trim();

            if (comentario) {
                // Recupera o nome do usuário logado do localStorage
                const usuario = localStorage.getItem('usuarioLogado') || 'Anônimo';

                // Cria o elemento do comentário com nome do usuário
                const divComentario = document.createElement('div');
                divComentario.innerHTML = `<strong>${usuario}</strong><br>${comentario}`;
                lista.appendChild(divComentario);

                input.value = '';
            }
        });
    });
});