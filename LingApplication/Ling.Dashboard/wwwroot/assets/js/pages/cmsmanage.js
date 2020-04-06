// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitSummernote();
    InitFormValidation();

});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    jQuery.validator.addMethod("noSpaceAllowed", function (value, element) { //Code used for blank space Validation
        return value.indexOf(" ") < 0 && value != "";
    }, "No space please and don't leave it empty");

    $("#frm_CMSManage").validate({
        ignore: '.note-editable',
        rules: {
            CMSKey: { required: true, noSpaceAllowed: true },
            Title: { required: true }
        },
        messages: {
            CMSKey: "Please enter cms key",
            Title: "Please enter title"
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
        if ($("#txtAreaDescription").summernote('isEmpty'))
            isValidSummernote = false;

        if ($("#frm_CMSManage").valid() && isValidSummernote) {
            $("#spnSummernoteError").hide();
            $(".note-editor").css('margin-bottom', '14px');
            $("#dvContent").removeClass("has-error").addClass("has-success");
            $('.summernote').val($('.summernote').summernote('code'));
            $("#frm_CMSManage").submit();
        }
        else {
            if (!isValidSummernote) {
                $("#spnSummernoteError").show();
                $("#dvContent").removeClass("has-success").addClass("has-error");
                $(".note-editor").css('margin-bottom', '1px');
                $(".note-editor").css('border-color', '#dd4b39');
            }
        }
    });
}

// INITIALIZATION SUMMERNOTE
function InitSummernote() {
    $('#txtAreaDescription').summernote({
        minHeight: 400,
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

    $("#txtAreaDescription").on('summernote.change', function () {
        $("#spnSummernoteError").hide();
        $("#dvContent").removeClass("has-error").addClass('has-success');;
        $(".note-editor").css('margin-bottom', '14px');
        $(".note-editor").css('border-color', '#00a65a');

        if ($("#txtAreaDescription").summernote('isEmpty')) {
            $("#spnSummernoteError").show();
            $("#dvContent").removeClass("has-success").addClass("has-error");
            $(".note-editor").css('margin-bottom', '1px');
            $(".note-editor").css('border-color', '#dd4b39');
        }
    });
}