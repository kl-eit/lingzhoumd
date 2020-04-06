// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitFormValidation();
});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    $("#frm_FAQManage").validate({
        rules: {
            Question: { required: true },
            Answer: { required: true }
        },
        messages: {
            Question: "Please enter question",
            Answer: "Please enter answer"
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
        if ($("#frm_FAQManage").valid()) {
            ShowBlockUI();
            $("#frm_FAQManage")[0].submit();
        }
    });
}