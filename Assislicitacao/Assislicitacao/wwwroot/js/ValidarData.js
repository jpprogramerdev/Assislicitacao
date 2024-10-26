const dataLicitacao = document.getElementById("data-licitacao");

function validateDate() {
    const hoje = new Date();
    hoje.setHours(0, 0, 0, 0);

    const dataSelecionada = new Date(dataLicitacao.value);

    if (dataSelecionada < hoje) {
        dataLicitacao.setCustomValidity("A data da licitação deve ser maior ou igual à data atual.");
    } else {
        dataLicitacao.setCustomValidity("");
    }
}

dataLicitacao.addEventListener("blur", validateDate);