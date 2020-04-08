// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitFormValidation();
    InitSummernote();
});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    $("#frm_FAQManage").validate({
        rules: {
            Question: { required: true }
        },
        messages: {
            Question: "Please enter question"
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
        var isValidSummernote = true;
        if ($("#txtAnswer").summernote('isEmpty'))
            isValidSummernote = false;

        if ($("#frm_FAQManage").valid() && isValidSummernote) {
            $("#spnSummernoteError").hide();
            $(".note-editor").css('margin-bottom', '14px');
            $("#dvAnswer").removeClass("has-error").addClass("has-success");
            $('.summernote').val($('.summernote').summernote('code'));
            $("#frm_FAQManage").submit();
        }
        else {
            if (!isValidSummernote) {
                $("#spnSummernoteError").show();
                $("#dvAnswer").removeClass("has-success").addClass("has-error");
                $(".note-editor").css('margin-bottom', '1px');
                $(".note-editor").css('border-color', '#dd4b39');
            }
        }
    });
}

// INITIALIZATION SUMMERNOTE
function InitSummernote() {
    $('#txtAnswer').summernote({
        minHeight: 300,
        'empty': ('<p><br/></p>', '<p><br></p>'),
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'italic', 'underline', 'clear']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['clear', ['clear']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'hr']],
            ['view', ['fullscreen', 'codeview']],
            ['help', ['help']]
        ]
    });

    $("#txtAnswer").on('summernote.change', function () {
        $("#spnSummernoteError").hide();
        $("#dvAnswer").removeClass("has-error").addClass('has-success');
        $(".note-editor").css('margin-bottom', '14px');
        $(".note-editor").css('border-color', '#00a65a');

        if ($("#txtAnswer").summernote('isEmpty')) {
            $("#spnSummernoteError").show();
            $("#dvAnswer").removeClass("has-success").addClass("has-error");
            $(".note-editor").css('margin-bottom', '1px');
            $(".note-editor").css('border-color', '#dd4b39');
        }
    });
}