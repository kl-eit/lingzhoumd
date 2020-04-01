// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitChangePasswordFormValidation();
});

// INITIALIZATION FORM VALIDATION
function InitChangePasswordFormValidation() {
    $("#frmManage").validate({
        rules: {
            CurrentPassword: { required: true },
            Password: {
                required: true,
                minlength: 6,
                maxlength: 15
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            }
        },
        messages: {
            CurrentPassword: "Please enter current password",
            Password: {
                required: "Please enter password",
                minlength: "Minimum required 6 characters",
                maxlength: "Maximum 15 characters allowed"
            },
            ConfirmPassword:
            {
                required: "Please enter confirm password",
                equalTo: "Password not match"
            }
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

    $("#btnUpdatePassword").click(function () {
        if ($("#frmManage").valid()) {
            $("#frmManage").submit();
        }
    });
}