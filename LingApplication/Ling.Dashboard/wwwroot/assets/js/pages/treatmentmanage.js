
// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitFormValidation();
    InitSummernote();
});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    $("#frmTreatment").validate({
        ignore: [],
        rules: {
            Name: { required: true },
            Description: { required: true },
            hdfTreatmentImage: {
                required: true,
                extension: "png|jpeg|jpg"
            }
        },
        messages: {
            Name: "Please enter name",
            Description: "Please enter description",
            hdfTreatmentImage: {
                required: "Please upload image",
                extension: "Only image allowed (png, jpeg, jpg)"
            }
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            debugger
            if ($(element).attr("name") == "hdfTreatmentImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#dd4b39 !important");
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
            else {
                $(element).removeClass('is-valid').addClass('is-invalid');
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
        },
        unhighlight: function (element) {
            if ($(element).attr("name") == "hdfTreatmentImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#00a65a !important");
            }
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorPlacement: function (error, element) {
            debugger
            if (element.attr("name") == "hdfTreatmentImage") {
                error.insertAfter(element.closest(".validate"));
            }
            else {
                error.insertAfter(element.closest(".form-control"));
            }
        }
    });

    $("#btnSave").click(function () {
        var isValidSummernote = true;
        if ($("#txtDescription").summernote('isEmpty'))
            isValidSummernote = false;

        if ($("#frmTreatment").valid() && isValidSummernote) {
            $("#spnSummernoteError").hide();
            $(".note-editor").css('margin-bottom', '14px');
            $("#dvDescription").removeClass("has-error").addClass("has-success");
            $('.summernote').val($('.summernote').summernote('code'));
            $("#frmTreatment").submit();
        }
        else {
            if (!isValidSummernote) {
                $("#spnSummernoteError").show();
                $("#dvDescription").removeClass("has-success").addClass("has-error");
                $(".note-editor").css('margin-bottom', '1px');
                $(".note-editor").css('border-color', '#dd4b39');
            }
        }
    });
}
function ShowPreview(input, previewFor) {
    if (previewFor == "bannerImage") {
        var fileName = $('input[name="fileImage"]').val();
        $("#hdfTreatmentImage").val(fileName);
        if (input.files.length > 0) {
            var ImageDir = new FileReader();
            ImageDir.onload = function (e) {
                $("#imagePreview").attr("src", e.target.result);
                $("#imagePreview").css("width", '100% !important');
            }
            ImageDir.readAsDataURL(input.files[0]);
        } else {
            $("#imageName").find(".fileinput-preview ").html('<img src="' + _defaultImage + '" id="imagePreview" name="BannerImage" onerror="this.src = ' + _defaultImage + '"  class="img-responsive" style="height:100% !important;width: 400px !important;" />');
        }
        var validator = $("#frmTreatment").validate();
        validator.element("#hdfTreatmentImage");
    }
}
// INITIALIZATION SUMMERNOTE
function InitSummernote() {
    $('#txtDescription').summernote({
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

    $("#txtDescription").on('summernote.change', function () {
        $("#spnSummernoteError").hide();
        $("#dvDescription").removeClass("has-error").addClass('has-success');;
        $(".note-editor").css('margin-bottom', '14px');
        $(".note-editor").css('border-color', '#00a65a');

        if ($("#txtDescription").summernote('isEmpty')) {
            $("#spnSummernoteError").show();
            $("#dvDescription").removeClass("has-success").addClass("has-error");
            $(".note-editor").css('margin-bottom', '1px');
            $(".note-editor").css('border-color', '#dd4b39');
        }
    });
}