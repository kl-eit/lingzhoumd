// document load event it will occure on page load
$(document).ready(function () {
    InitValidation();
});

// init validation
function InitValidation() {
    $("#frmWebSetting").validate({
        rules: {
            Name: { required: true },
            Value: { required: true }
        },
        messages: {
            Name: "Please enter name",
            Value: "Please enter value"
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            $(element).removeClass('is-valid').addClass('is-invalid');
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
    });

    $("#btnSave").click(function () {

        if ($("#frmWebSetting").valid()) {
            ShowBlockUI();
            $("#frmWebSetting").submit();
        }
    });
}