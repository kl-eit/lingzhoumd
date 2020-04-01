
// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitValidation();

    //$('input').iCheck({
    //    checkboxClass: 'icheckbox_square-blue',
    //    radioClass: 'iradio_square-blue',
    //    increaseArea: '20%' // optional
    //});
});

// INITIALIZATION VALIDATION
function InitValidation() {
    $("#frmLogin").validate({
        rules: {
            Username: { required: true },
            Password: { required: true }
        },
        messages: {
            Username: "Please enter your username or email",
            Password: "Please enter your password",
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
        }
    });

    $("#btnSignIn").click(function () {
        if ($("#frmLogin").valid()) {
            $("#frmLogin").submit();
        }
    });
}

// ENTER KEY EVENT
$('#Password').keypress(function (event) {
    if (event.keyCode == 13) {
        if ($("#frmLogin").valid()) {
            $("#frmLogin").submit();
        }
    }
});