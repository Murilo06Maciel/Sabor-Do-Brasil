fetch('/api/cadastrar', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ nome, email, senha })
})
  .then(response => {
    if (response.status === 204) {
      return Promise.resolve(null); ;
    }
    const contentType = response.headers.get('content-type');
    if (!contentType || !contentType.includes('application/json')) {
      throw new Error('Resposta não é JSON!');
    }
    return response.json();
  })
  .then(data => {
    console.log('Dados recebidos:', data);
  })
  .catch(error => {
    console.error('Erro na requisição:', error);
  });