// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitFormValidation();
});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    $("#frm_ReviewsManage").validate({
        rules: {
            Review: { required: true},
            Comment: { required: true },
            Type: { required: true }
        },
        messages: {
            Review: "Please enter review",
            Comment: "Please enter comment",
            Type: "Please enter type"
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
        if ($("#frm_ReviewsManage").valid()) {
            $("#frm_ReviewsManage").submit();
        }
    });
}

