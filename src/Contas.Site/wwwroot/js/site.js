function validacoesConta() {
    $.validator.methods.range = function(value, elem, param) {
        var globalizeValue = value.replace(",", ".");
        return this.optional(elem) || (globalizeValue >= param[0] && globalizeValue <= param[1]);
    }

    $.validator.methods.number = function(value, elem) {
        return this.optional(elem) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }

    $("#Data").datepicker({
        format: "dd/mm/yyyy",
        startDate: "tomorrow",
        language: "pt-BR",
        orientation: "bottom right",
        autoclose: true
    });

    $(document).ready(function () {
        function mostrarNumeroParcelas() {
            if ($("#Parcelado").is(":checked")) {
                $("#NumeroParcelas").prop("disabled", false);
            } else {
                $("#NumeroParcelas").prop("disabled", true);
            };
        }

        $("#Parcelado").click(function () {
            mostrarNumeroParcelas();
        });

        mostrarNumeroParcelas();
    });
}