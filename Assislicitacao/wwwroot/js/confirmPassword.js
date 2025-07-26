document.querySelector('form').addEventListener('submit', function (e) {
    const senha = document.getElementById('senha-usuario').value;
    const confirmarSenha = document.getElementById('confirm-senha-usuario').value;

    if (senha !== confirmarSenha) {
        e.preventDefault(); 
        alert('As senhas não coincidem.');
    }
});