// JavaScript Document
//Busca o elemento pela id
function id(elementId) {
    return document.getElementById(elementId);
}

//Desabilita a visuaização do campo RG para o estado do PR
$(document).ready(function () {
    var elem = id('hUf').innerText;
    if (elem == 'SC') {
        $('#divRg').show();
    } else {
        $('#divRg').hide();
    }
})

//Converte o icone da grid de filtro
$('#headPanel a').click(function () {
    $(this).find('i').toggleClass('glyphicon-plus-sign glyphicon-minus-sign');
});

//Valida a idade da pessoa a ser cadastrada
function VerificarIdade(data) {
    var uf = id('hUf').innerText;
    var pr = uf == "PR";
    var dt = new Date(data.value).getFullYear();
    var ano = new Date().getFullYear();
    var dif = ano - dt;
    var val = id('valData');
    if (dif < 18 && pr) {
        if (val.innerText.length > 0) {
            val.innerText += '\n';
        }
        val.innerText += 'Não é permitido cadastro para menores de 18 anos.';
        DesabilitarBotaoSavar();
    }
    else {
        HabilitarBotaoSavar();
    }
    ConverterEmCampoText(data, val);
}

//Desabilita o botão de salvar cadastro
function DesabilitarBotaoSavar() {
    var btn = id('btnSave');
    btn.disabled = true;
}

//Habilita o botão de salvar cadastro
function HabilitarBotaoSavar() {
    var btn = id('btnSave');
    btn.disabled = false;
}

//Converte o input de data em texto
function ConverterEmCampoText(data, val) {
    if (data.value.length == 0) {
        data.type = 'text';
        if (val != null) {
            val.innerText = '';
        }
    }
}

//Converte o input de texto em data
function ConverterEmCampoData(cmp) {
    cmp.type = 'date';
}

//Adiciona mascara ao telefone
function MascaraTelefone(tel) {
    if (VerificarDigito(tel) == false) {
        event.returnValue = false;
    }
    switch (tel.value.length) {
        case 0:
            return;
        case 15:
            return FormataCampo(tel, '(00) 00000-0000');
        default:
            return FormataCampo(tel, '(00) 0000-0000');
    }
}

//Adiciona mascara ao CPF
function MascaraCPF(cpf) {
    if (VerificarDigito(cpf) == false) {
        event.returnValue = false;
    }
    return FormataCampo(cpf, '000.000.000-00');
}

//Adiciona mascara ao RG
function MascaraRgSC(rg) {
    if (VerificarDigito(rg) == false) {
        event.returnValue = false;
    }
    return FormataCampo(rg, '0.000.000');
}

//Valida telefone
function ValidaTelefone(tel) {
    var exp = '';
    switch (tel.value.length) {
        case 15:
            exp = /\(\d{2}\)\ \d{5}\-\d{4}/;
            break;
        default:
            exp = /\(\d{2}\)\ \d{4}\-\d{4}/;
            break;
    }
    var elem = id('valTelefone');
    if (!exp.test(tel.value)) {
        elem.innerText = 'Numero de telefone invalido';
        DesabilitarBotaoSavar();
    }
    else HabilitarBotaoSavar()
}

//Valida o CPF digitado
function ValidarCPF(obj) {
    var cpf = obj.value;
    exp = /\.|\-/g;
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;

    var elem = id('valCpf');
    if (digitoGerado != digitoDigitado && cpf.length > 0) {
        elem.innerText = 'CPF invalido'
        DesabilitarBotaoSavar();
    } else HabilitarBotaoSavar()
}

//Valida digitos 
function VerificarDigito() {
    if (event.keyCode < 48 || event.keyCode > 57) {
        event.returnValue = false;
        return false;
    }
    return true;
}

//Formata de forma generica os campos
function FormataCampo(campo, Mascara) {
    var boleanoMascara;

    var apagar = event.keyCode == 8;
    exp = /\-|\.|\/|\(|\)| /g;
    campoSoNumeros = campo.value.toString().replace(exp, "");

    var posicaoCampo = 0;
    var NovoValorCampo = "";
    var TamanhoMascara = campoSoNumeros.length;;

    if (apagar) {
        return true;
    } else {
        for (i = 0; i <= TamanhoMascara; i++) {
            boleanoMascara = ((Mascara.charAt(i) == "-")
                || (Mascara.charAt(i) == ".")
                || (Mascara.charAt(i) == "/"))
            boleanoMascara = boleanoMascara
                || ((Mascara.charAt(i) == "(")
                    || (Mascara.charAt(i) == ")")
                    || (Mascara.charAt(i) == " "))
            if (boleanoMascara) {
                NovoValorCampo += Mascara.charAt(i);
                TamanhoMascara++;
            } else {
                NovoValorCampo += campoSoNumeros.charAt(posicaoCampo);
                posicaoCampo++;
            }
        }
        campo.value = NovoValorCampo;
        return true;
    }
}
