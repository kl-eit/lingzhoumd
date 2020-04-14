// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {

    InitSectionValidation();
    InitSummernoteForPhilosophy();
    $("#btnSavePhilosophy").click(function () {
        var isValidSummernote = true;
        if ($("#txtPhilosophyContent").summernote('isEmpty'))
            isValidSummernote = false;

        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid() && isValidSummernote) {
            $("#frm_home .form-control").removeAttr('disabled');
            $("#spnPhilosophyContentSummernoteError").hide();
            $("#dvPhilosophyContent .note-editor").css('margin-bottom', '14px');
            $("#dvPhilosophyContent").removeClass("has-error").addClass("has-success");
            $('#txtPhilosophyContent.summernote').val($('#txtPhilosophyContent.summernote').summernote('code'));
            $("#frm_home").submit();
        }
        else {
            if (!isValidSummernote) {
                $("#spnPhilosophyContentSummernoteError").show();
                $("#dvPhilosophyContent").removeClass("has-success").addClass("has-error");
                $("#dvPhilosophyContent .note-editor").css('margin-bottom', '1px');
                $("#dvPhilosophyContent .note-editor").css('border-color', '#dd4b39');
            }
        }
    });

    $("#btnSaveProfileImage").click(function () {
        $("#dvImage").removeClass("has-error").addClass("has-success");
        $("#frm_home").submit();
    });

    InitSummernoteForEducation();
    $("#btnSaveEducation").click(function () {
        var isValidSummernote = true;
        if ($("#txtEducationContent").summernote('isEmpty'))
            isValidSummernote = false;

        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid() && isValidSummernote) {
            $("#frm_home .form-control").removeAttr('disabled');
            $("#spnEducationContentSummernoteError").hide();
            $("#dvEducationContent .note-editor").css('margin-bottom', '14px');
            $("#dvEducationContent").removeClass("has-error").addClass("has-success");
            $('#txtEducationContent.summernote').val($('#txtEducationContent.summernote').summernote('code'));
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
            if (!isValidSummernote) {
                $("#spnEducationContentSummernoteError").show();
                $("#dvEducationContent").removeClass("has-success").addClass("has-error");
                $("#dvEducationContent .note-editor").css('margin-bottom', '1px');
                $("#dvEducationContent .note-editor").css('border-color', '#dd4b39');
            }
        }

    });

    InitSummernoteForTraining();
    $("#btnSaveTraining").click(function () {
        var isValidSummernote = true;
        if ($("#txtTrainingContent").summernote('isEmpty'))
            isValidSummernote = false;

        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid() && isValidSummernote) {
            $("#frm_home .form-control").removeAttr('disabled');
            $("#spnTrainingContentSummernoteError").hide();
            $("#dvTrainingContent .note-editor").css('margin-bottom', '14px');
            $("#dvTrainingContent").removeClass("has-error").addClass("has-success");
            $('#txtTrainingContent.summernote').val($('#txtTrainingContent.summernote').summernote('code'));
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
            if (!isValidSummernote) {
                $("#spnTrainingContentSummernoteError").show();
                $("#dvTrainingContent").removeClass("has-success").addClass("has-error");
                $("#dvTrainingContent .note-editor").css('margin-bottom', '1px');
                $("#dvTrainingContent .note-editor").css('border-color', '#dd4b39');
            }
        }
    });

    InitSummernoteForCertificate();
    $("#btnSaveCertificate").click(function () {
        var isValidSummernote = true;
        if ($("#txtCertificateContent").summernote('isEmpty'))
            isValidSummernote = false;

        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid() && isValidSummernote) {
            $("#frm_home .form-control").removeAttr('disabled');
            $("#spnCertificateContentSummernoteError").hide();
            $("#dvCertificateContent .note-editor").css('margin-bottom', '14px');
            $("#dvCertificateContent").removeClass("has-error").addClass("has-success");
            $('#txtCertificateContent.summernote').val($('#txtCertificateContent.summernote').summernote('code'));
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
            if (!isValidSummernote) {
                $("#spnCertificateContentSummernoteError").show();
                $("#dvCertificateContent").removeClass("has-success").addClass("has-error");
                $("#dvCertificateContent .note-editor").css('margin-bottom', '1px');
                $("#dvCertificateContent .note-editor").css('border-color', '#dd4b39');
            }
        }
    });

    InitSummernoteForMedical();
    $("#btnSaveMedical").click(function () {
        var isValidSummernote = true;
        if ($("#txtMedicalContent").summernote('isEmpty'))
            isValidSummernote = false;

        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid() && isValidSummernote) {
            $("#frm_home .form-control").removeAttr('disabled');
            $("#spnMedicalContentSummernoteError").hide();
            $("#dvMedicalContent .note-editor").css('margin-bottom', '14px');
            $("#dvMedicalContent").removeClass("has-error").addClass("has-success");
            $('#txtMedicalContent.summernote').val($('#txtMedicalContent.summernote').summernote('code'));
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
            if (!isValidSummernote) {
                $("#spnMedicalContentSummernoteError").show();
                $("#dvMedicalContent").removeClass("has-success").addClass("has-error");
                $("#dvMedicalContent .note-editor").css('margin-bottom', '1px');
                $("#dvMedicalContent .note-editor").css('border-color', '#dd4b39');
            }
        }
    });
});

// INITIALIZATION FORM VALIDATION
function InitSectionValidation() {
    $("#frm_home").validate({
        ignore: '.note-editable',
        rules: {
            PhilosophyTitle: { required: true },
            EducationTitle: { required: true },
            TrainingTitle: { required: true },
            CertificateTitle: { required: true },
            MedicalTitle: { required: true }
        },
        messages: {
            PhilosophyTitle: "Please enter title",
            EducationTitle: "Please enter title",
            TrainingTitle: "Please enter title",
            EducationTitle: "Please enter title",
            CertificateTitle: "Please enter title",
            MedicalTitle: "Please enter title"
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            if ($(element).attr("name") == "hdfProfileImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#dd4b39 !important");
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
            else {
                $(element).removeClass('is-valid').addClass('is-invalid');
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
        },
        unhighlight: function (element) {
            if ($(element).attr("name") == "hdfProfileImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#00a65a !important");
            }
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.closest(".form-control"));
        },
        onsubmit: false
    });
}

// INITIALIZATION SUMMERNOTE
function InitSummernoteForPhilosophy() {
    $('#txtPhilosophyContent').summernote({
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

    $("#txtPhilosophyContent").on('summernote.change', function () {
        $("#spnPhilosophyContentSummernoteError").hide();
        $("#dvPhilosophyContent").removeClass("has-error").addClass('has-success');;
        $("#dvPhilosophyContent .note-editor").css('margin-bottom', '14px');
        $("#dvPhilosophyContent .note-editor").css('border-color', '#00a65a');

        if ($("#txtPhilosophyContent").summernote('isEmpty')) {
            $("#spnPhilosophyContentSummernoteError").show();
            $("#dvPhilosophyContent").removeClass("has-success").addClass("has-error");
            $("#dvPhilosophyContent .note-editor").css('margin-bottom', '1px');
            $("#dvPhilosophyContent .note-editor").css('border-color', '#dd4b39');
        }
    });

}

// INITIALIZATION SUMMERNOTE
function InitSummernoteForEducation() {
    $('#txtEducationContent').summernote({
        minHeight: 200,
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

    $("#txtEducationContent").on('summernote.change', function () {
        $("#spnEducationContentSummernoteError").hide();
        $("#dvEducationContent").removeClass("has-error").addClass('has-success');;
        $("#dvEducationContent .note-editor").css('margin-bottom', '14px');
        $("#dvEducationContent .note-editor").css('border-color', '#00a65a');

        if ($("#txtEducationContent").summernote('isEmpty')) {
            $("#spnEducationContentSummernoteError").show();
            $("#dvEducationContent").removeClass("has-success").addClass("has-error");
            $("#dvEducationContent .note-editor").css('margin-bottom', '1px');
            $("#dvEducationContent .note-editor").css('border-color', '#dd4b39');
        }
    });
}

// INITIALIZATION SUMMERNOTE
function InitSummernoteForTraining() {
    $('#txtTrainingContent').summernote({
        minHeight: 200,
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

    $("#txtTrainingContent").on('summernote.change', function () {
        $("#spnTrainingContentSummernoteError").hide();
        $("#dvTrainingContent").removeClass("has-error").addClass('has-success');;
        $("#dvTrainingContent .note-editor").css('margin-bottom', '14px');
        $("#dvTrainingContent .note-editor").css('border-color', '#00a65a');

        if ($("#txtTrainingContent").summernote('isEmpty')) {
            $("#spnTrainingContentSummernoteError").show();
            $("#dvTrainingContent").removeClass("has-success").addClass("has-error");
            $("#dvTrainingContent .note-editor").css('margin-bottom', '1px');
            $("#dvTrainingContent .note-editor").css('border-color', '#dd4b39');
        }
    });
}


// INITIALIZATION SUMMERNOTE
function InitSummernoteForCertificate() {
    $('#txtCertificateContent').summernote({
        minHeight: 200,
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

    $("#txtCertificateContent").on('summernote.change', function () {
        $("#spnCertificateContentSummernoteError").hide();
        $("#dvCertificateContent").removeClass("has-error").addClass('has-success');;
        $("#dvCertificateContent .note-editor").css('margin-bottom', '14px');
        $("#dvCertificateContent .note-editor").css('border-color', '#00a65a');

        if ($("#txtCertificateContent").summernote('isEmpty')) {
            $("#spnCertificateContentSummernoteError").show();
            $("#dvCertificateContent").removeClass("has-success").addClass("has-error");
            $("#dvCertificateContent .note-editor").css('margin-bottom', '1px');
            $("#dvCertificateContent .note-editor").css('border-color', '#dd4b39');
        }
    });
}

// INITIALIZATION SUMMERNOTE
function InitSummernoteForMedical() {
    $('#txtMedicalContent').summernote({
        minHeight: 200,
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

    $("#txtMedicalContent").on('summernote.change', function () {
        $("#spnMedicalContentSummernoteError").hide();
        $("#dvMedicalContent").removeClass("has-error").addClass('has-success');;
        $("#dvMedicalContent .note-editor").css('margin-bottom', '14px');
        $("#dvMedicalContent .note-editor").css('border-color', '#00a65a');

        if ($("#txtMedicalContent").summernote('isEmpty')) {
            $("#spnMedicalContentSummernoteError").show();
            $("#dvMedicalContent").removeClass("has-success").addClass("has-error");
            $("#dvMedicalContent .note-editor").css('margin-bottom', '1px');
            $("#dvMedicalContent .note-editor").css('border-color', '#dd4b39');
        }
    });
}

function ShowPreview(input) {
    var fileName = $('input[name="fileImage"]').val();
    $("#hdfProfileImage").val(fileName);
    if (input.files.length > 0) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $("#imagePreview").attr("src", e.target.result);
            $("#imagePreview").css("width", '100% !important');
        }
        ImageDir.readAsDataURL(input.files[0]);
    } else {
        $("#imageName").find(".fileinput-preview").html('<img src="' + _defaultImage + '" id="imagePreview" name="BannerImage" onerror="this.src = ' + _defaultImage + '"  class="img-responsive" style="height:100% !important;width: 400px !important;" />');
    }
    var validator = $("#frm_home").validate();
    validator.element("#hdfProfileImage");
}