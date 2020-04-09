// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    var selectedMenu = $("#hdnSelectedMenu").val();
    $("#li" + selectedMenu).addClass("active");

    InitSummernoteForSectionOne();
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
            $("#dvSectionOneImage").removeClass("has-error").addClass("has-success");
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
            if ($('#SectionOneMediaWrapper').html().trim() === "") {
                $("#spnSectionOneUploadImage").show();
                $('#dvSectionOneImage .fileuploader-theme-dragdrop').addClass('has-error');
                $('#dvSectionOneImage').addClass('has-error');
            }

            if (!isValidSummernote) {
                $("#spnPhilosophyContentSummernoteError").show();
                $("#dvPhilosophyContent").removeClass("has-success").addClass("has-error");
                $("#dvPhilosophyContent .note-editor").css('margin-bottom', '1px');
                $("#dvPhilosophyContent .note-editor").css('border-color', '#dd4b39');
            }
        }

    });

    InitSummernoteForSectionTwo();
    $("#btnSaveSectionTwo").click(function () {
        var isValidSummernote = true;
        if ($("#txtSectionTwoDescription").summernote('isEmpty'))
            isValidSummernote = false;

        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid() && isValidSummernote) {
            $("#frm_home .form-control").removeAttr('disabled');
            $("#spnSectionTwoSummernoteError").hide();
            $("#dvSectionTwoContent .note-editor").css('margin-bottom', '14px');
            $("#dvSectionTwoContent").removeClass("has-error").addClass("has-success");
            $('#txtSectionTwoDescription.summernote').val($('#txtSectionTwoDescription.summernote').summernote('code'));
            CreateImageJson();
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
            if (!isValidSummernote) {
                $("#spnSectionTwoSummernoteError").show();
                $("#dvSectionTwoContent").removeClass("has-success").addClass("has-error");
                $("#dvSectionTwoContent .note-editor").css('margin-bottom', '1px');
                $("#dvSectionTwoContent .note-editor").css('border-color', '#dd4b39');
            }
        }

    });

    InitSummernoteForSectionThree();
    InitSectionThreeFileUpload();
    $("#btnSaveSection_three").click(function () {
        var isValidSummernote = true;
        if ($("#txtSectionThreeDescription").summernote('isEmpty'))
            isValidSummernote = false;

        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid() && isValidSummernote && $('#SectionThreeMediaWrapper').html().trim() !== "") {
            $("#frm_home .form-control").removeAttr('disabled');
            $("#spnSectionThreeSummernoteError").hide();
            $("#dvSectionThreeContent .note-editor").css('margin-bottom', '14px');
            $("#dvSectionThreeContent").removeClass("has-error").addClass("has-success");
            $('#txtSectionThreeDescription.summernote').val($('#txtSectionThreeDescription.summernote').summernote('code'));
            $("#dvSectionThreeImage").removeClass("has-error").addClass("has-success");
            CreateImageJson();
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
            if ($('#SectionThreeMediaWrapper').html().trim() === "") {
                $("#spnSectionThreeUploadImage").show();
                $('#dvSectionThreeImage .fileuploader-theme-dragdrop').addClass('has-error');
                $('#dvSectionThreeImage').addClass('has-error');
            }

            if (!isValidSummernote) {
                $("#spnSectionThreeSummernoteError").show();
                $("#dvSectionThreeContent").removeClass("has-success").addClass("has-error");
                $("#dvSectionThreeImage").removeClass("has-success").addClass("has-error");
                $("#dvSectionThreeContent .note-editor").css('margin-bottom', '1px');
                $("#dvSectionThreeContent .note-editor").css('border-color', '#dd4b39');
            }
        }
    });

    InitSectionFourFileUpload();
    $("#btnSaveSectionFour").click(function () {
        if ($('#SectionFourMediaWrapper').html().trim() !== "") {
            $("#dvSectionFourImage").removeClass("has-error").addClass("has-success");
            CreateImageJson();
            $("#frm_home").submit();
        }
        else {
            if ($('#SectionFourMediaWrapper').html().trim() === "") {
                $("#spnSectionFourUploadImage").show();
                $('#dvSectionFourImage .fileuploader-theme-dragdrop').addClass('has-error');
                $('#dvSectionFourImage').addClass('has-error');
            }
        }
    });

    $("#btnSaveSectionFive").click(function () {
        $("#frm_home .form-control").not($(this).closest('.no-gutters').find('.form-control')).attr('disabled', 'disabled');
        if ($("#frm_home").valid()) {
            $("#frm_home .form-control").removeAttr('disabled');
            CreateImageJson();
            $("#frm_home").submit();
        }
        else {
            $("#frm_home .form-control").removeAttr('disabled');
        }
    });
});

// INITIALIZATION SUMMERNOTE
function InitSummernoteForSectionOne() {
    $('#txtPhilosophyContent').summernote({
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