/*validacion de entrada de los inputs*/
var validacion = (function () {
    /* variables y funciones privadas */
    var 
    /*validar numeros enteros*/
        validarEntero = function (elemento, e, validarKeypress) {
            if (validarKeypress == undefined) {
                /*validar para devexpress*/
                /* Backspace = 8, Enter = 13, ’0′ = 48, ’9′ = 57, ‘.’ = 46
                cursores (key >= 37 && key <= 40)
                inicio y fin (key >= 35 && key <= 36)
                */
                var key = e.htmlEvent.charCode || e.htmlEvent.keyCode || 0;
                if (key == 8 || key == 9 ||
                    (key >= 37 && key <= 40) ||
                        (key >= 35 && key <= 36) ||
                            (key >= 48 && key <= 57)) {
                    return true;
                } else {
                    return _aspxPreventEvent(e.htmlEvent);
                }
            } else {
                /*validar para html*/
                $(elemento).on({
                    keyup: function () {
                        $(this).val($(this).val().replace(/[^0-9]/g, "")); /*para enteros*/
                    }
                });
            }
        },
    /*validar numeros enteros*/
        validarDecimal = function (elemento, e, validarKeypress) {
            validarKeypress = (validarKeypress == undefined ? 0 : validarKeypress);
            switch (validarKeypress) {
                case 1: /*validar para html*/
                    $(elemento).on({
                        keyup: function () {
                            $(this).val($(this).val().replace(/[^0-9\.]/g, "")); /*para decimales*/
                        }
                    });
                    break;
                case 2:/*aplicanto una mascara*/
                    $.mask.definitions['~'] = '[0-9|.]';
                    $("input.abcd").mask("9?~9"); 
                    break;
                default:
                    var key = e.htmlEvent.charCode || e.htmlEvent.keyCode || 0;
                    if (key == 8 || key == 9 ||
                    (key >= 37 && key <= 40) ||
                        (key >= 35 && key <= 36) ||
                            (key >= 48 && key <= 57)) {
                        return true;
                    } else {
                        return _aspxPreventEvent(e.htmlEvent);
                    }
                    break;
            }
        },
    /*validacion alfanumerico*/
        validarAlfanumerico = function (elemento, e, validarKeypress) {
            if (validarKeypress == undefined) {
                /*validar para devexpress*/
                /*var regex = new RegExp(/[^a-zA-Z0-9]/g);
                var valor = elemento.GetText(); //elemento.value
                var valido = valor.match(regex);
                if (valido)
                elemento.SetText(valor.replace(regex, ''));*/

                // allow backspace, tab, delete, arrows, letters, numbers and keypad numbers ONLY
                var key = e.htmlEvent.charCode || e.htmlEvent.keyCode || 0;
                if (key == 8 || key == 9 || key == 46 ||
                    (key >= 37 && key <= 40) ||
                        (key >= 35 && key <= 36) ||
                            (key >= 48 && key <= 57) ||
                                (key >= 65 && key <= 90) ||
                                    (key >= 96 && key <= 105)) {
                    return true;
                } else {
                    return _aspxPreventEvent(e.htmlEvent);
                }
            } else {
                /*validar para html*/
                $(elemento).keypress(function () {
                    var regex = new RegExp(/[^a-zA-Z0-9]/g);
                    var valido = this.value.match(regex);
                    if (valido)
                        this.value = this.value.replace(regex, '');
                });
            }
        };
    /* API publica */
    return {
        validarAlfanumerico: validarAlfanumerico,
        validarEntero: validarEntero,
        validarDecimal: validarDecimal
    };
})();