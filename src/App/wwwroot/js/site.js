function AjaxModal() {
    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $("#myModalContent").load(this.href,
                        function () {
                            $("#myModal").modal({
                                keyboard: true
                            },
                                "show");
                            bindForm(this);
                        });
                    return false;
                });
        });

        function bindForm(dialog) {
            $("form", dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $("#myModal").modal("hide");
                            $("#AddressTarget").load(result.url); // Load Html result to div
                        } else {
                            $("#myModalContent").html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }
    });
}

function SearchZipCode() {
    $(document).ready(function () {

        function clear_zip_data() {
            $("#Address_Street").val("");
            $("#Address_Neighborhood").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        $("#Address_ZipCode").blur(function () {
            var zipCode = $(this).val().replace(/\D/g, '');

            if (zipCode !== "") {

                var validZipCode = /^[0-9]{8}$/;

                if (validZipCode.test(zipCode)) {
                    $("#Address_Street").val("loading...");
                    $("#Address_Neighborhood").val("loading...");
                    $("#Address_City").val("loading...");
                    $("#Address_State").val("loading...");

                    $.getJSON("https://viacep.com.br/ws/" + zipCode + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                $("#Address_Street").val(dados.logradouro);
                                $("#Address_Neighborhood").val(dados.bairro);
                                $("#Address_City").val(dados.localidade);
                                $("#Address_State").val(dados.uf);
                            }
                            else {
                                clear_zip_data();
                                alert("Zip Code not fount.");
                            }
                        });

                } else {
                    clear_zip_data();
                    alert("Invalid Zip Code format.");
                }
            } else {
                clear_zip_data();
            }
        });
    });
}