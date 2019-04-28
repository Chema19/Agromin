$(document).ready(function () {
    $(document).on("keypress", ".onlyDniPeru", function (e) {
        debugger;
        e.preventDefault;
        let value = $(this).val();
        if (value.length + 1 > 8) {
            return false;
        }
        else { return true; }
    });

    $(document).on("keypress", ".onlyCelularPeru", function (e) {
        debugger;
        e.preventDefault;
        let value = $(this).val();
        if (value.length + 1 > 9) {
            return false;
        }
        else { return true; }
    });


    $(document).on("keypress", ".onlyNumbers", function (e) {
        e.preventDefault;
        var key = e.keyCode || e.which;
        if ((key >= 48 && key <= 57) || key == 8 || key == 13) {
            if (key == 13) { return false; }
            else { return true; }
        }
        else { return false; }
    })

    $(document).on("keypress", ".onlyNumberDecimal", function (e) {
        if (e.shiftKey == true) {
            e.preventDefault();
        }
        var key = e.keyCode || e.which;
        if ((key >= 48 && key <= 57) || key == 8 || key == 13 || key == 46) {
            if (key == 13) { return false; }
            else {
                if ($(this).val().indexOf('.') !== -1 && key == 46)
                    e.preventDefault();
                return true;
            }
        }
        else { return false; }
    });
    $(document).on("keypress", ".onlyDate", function (e) {
        e.preventDefault;
        var key = e.keyCode;
        if ((key >= 48 && key <= 57) || key == 8 || key == 13 || key == 47)
            return true;
        else
            return false;
    })
    $(document).on("keypress", ".onlyCharacters", function (e) {
        e.preventDefault;
        key = e.keyCode || e.which;
        tecla = String.fromCharCode(key).toLowerCase();
        letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
        especiales = "8-37-39-46";
        tecla_especial = false
        for (var i in especiales) {
            if (key == especiales[i]) {
                tecla_especial = true;
                break;
            }
        }
        if (letras.indexOf(tecla) == -1 && !tecla_especial) {
            return false;
        }
    })
    $(document).on("keypress", ".onlyNumbersCharacters", function (e) {
        e.preventDefault;
        key = e.keyCode || e.which;
        tecla = String.fromCharCode(key).toLowerCase();
        letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
        especiales = "8-37-39-46";
        tecla_especial = false
        for (var i in especiales) {
            if (key == especiales[i] || (key >= 48 && key <= 57) || key == 8 || key == 13) {
                tecla_especial = true;
                break;
            }
        }
        if (letras.indexOf(tecla) == -1 && !tecla_especial) {
            return false;
        }
    })

    $(document).on("blur", ".upper", function (e) {
        this.value = this.value.toUpperCase();
    })

    $(document).on("blur", ".onlyEmail", function (e) {
        e.preventDefault;

        var email = this.value;
        var regex = /[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}/igm;
        if (!regex.test(email))
            return false;
        else return true;

    })
})

function IsEmail(email) {
    var regex = /[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}/igm;
    if (!regex.test(email))
        return false;
    else return true;
}
