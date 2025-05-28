document.addEventListener('DOMContentLoaded', function() {
    const formCadastro = document.getElementById('form-cadastro');
    const btnCadastrar = document.getElementById('btn-cadastrar');
    
    formCadastro.addEventListener('submit', async function(e) {
        e.preventDefault();
        btnCadastrar.disabled = true; // Desabilita o botão durante o cadastro
        
        // Coleta os dados do formulário
        const usuario = {
            nome: document.getElementById('nome').value,
            email: document.getElementById('email').value,
            senha: document.getElementById('senha').value
        };

        // Validação básica
        if (!usuario.nome || !usuario.email || !usuario.senha) {
            alert('Preencha todos os campos!');
            btnCadastrar.disabled = false;
            return;
        }

        try {
            // Envia os dados para o endpoint /api/cadastrar
            const response = await fetch('http://localhost:5120/api/cadastrar', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Nome: document.getElementById('nome').value,
                    UserName: document.getElementById('usuario').value,
                    Email: document.getElementById('email').value,
                    Nickname: document.getElementById('usuario').value,
                    PasswordHash: document.getElementById('senha').value
                })
            });

            const data = await response.json();

            if (response.ok) {
                alert('Cadastro realizado com sucesso! Faça login.');
                window.location.href = 'login.html'; // Redireciona para a página de login
            } else {
                throw new Error(data.message || 'Erro ao cadastrar');
            }
        } catch (error) {
            console.error('Erro:', error);
            alert(error.message);
        } finally {
            btnCadastrar.disabled = false; // Reabilita o botão
        }
    });
});