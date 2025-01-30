$(document).ready(function () {
    $("#btn-preview-csv").click(function () {
        var divPreview = $("#preview-json-convertido");
        divPreview.css('display', 'none');

        var objetoJson = $("#input-json").val();

        if (ValidarJson(objetoJson)) {

            $.post("/Home/PreviewDadosEmCSV", { objetoJson: objetoJson }, function (data) {
                if (data != "")
                    divPreview.html(data).css('display', 'block');
            });

        }
    });

    $("#btn-converter-json").click(function () {
        var objetoJson = $("#input-json").val();

        if (ValidarJson(objetoJson))
            window.location.href = "/Home/ConverterJsonCSV?objetoJson=" + objetoJson;
    });

    $("#btn-limpar-dados").click(function () {
        $("#input-json").val("");
        $("#preview-json-convertido").css('display', 'none');
    });

    function ValidarJson(objetoJson) {
        var jsonvalido = true;
        var msgErro = "O valor do input não pode ser vazio.";

        if (objetoJson == "" || objetoJson == undefined) {
            msgErro = "O valor do input não pode ser vazio.";
            jsonvalido = false;
        } else {

            try {
                $.parseJSON(objetoJson);
            }
            catch (err) {
                msgErro = "Formato JSON inválido.";
                jsonvalido = false;
            }
        }

        if (!jsonvalido) {
            Swal.fire({
                title: "Atenção!",
                text: msgErro,
                icon: "error"
            });
        }

        return jsonvalido;
    }
});


