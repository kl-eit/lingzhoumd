$(document).ready(function () {

    InitFormValidation();

    $("input[name=ImageVideo]:radio").click(function () {
        if ($('input[name=ImageVideo]:checked').val() == "Image") {
            $("#dvBannerImage").removeClass("d-none");
            $("#dvBannerNote").removeClass("d-none");
            $("#dvBannerVideo").addClass("d-none");
            $('#fileupload').removeAttr("name");
            $("#hdfBannerImage").removeClass("bannerImage");
            $("#hdfBannerVideo").addClass("bannerVideo");
            $('#imagePreview').attr("name", "BannerImage");

        } else if ($('input[name=ImageVideo]:checked').val() == "Video") {
            $("#dvBannerImage").addClass("d-none");
            $("#dvBannerNote").addClass("d-none");
            $("#dvBannerVideo").removeClass("d-none");
            $('#imagePreview').removeAttr("name");
            $("#hdfBannerVideo").removeClass("bannerVideo");
            $("#hdfBannerImage").addClass("bannerImage");
            $('#fileupload').attr("name", "fileupload");
        }
    });

    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
});

function InitFormValidation() {
    $("#frmManage").validate({
        ignore: ".bannerImage, .bannerVideo",
        rules: {
            Title: { required: true },
            Content: { required: true },
            hdfBannerImage: {
                required: true,
                extension: "png|jpeg|jpg"
            },
            hdfBannerVideo: {
                required: true,
                extension: "MP4|MPG|AVI"
            }
        },
        messages: {
            Title: "Please enter title",
            Content: "Please enter content",
            hdfBannerImage: {
                required: "Please upload banner image",
                extension: "Only image allowed (png, jpeg, jpg)"
            },
            hdfBannerVideo: {
                required: "Please upload banner video",
                extension: "Only video allowed (MP4, MPG, AVI)"
            }
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            debugger
            if ($(element).attr("name") == "hdfBannerImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#dd4b39 !important");
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
            else {
                $(element).removeClass('is-valid').addClass('is-invalid');
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
        },
        unhighlight: function (element) {
            if ($(element).attr("name") == "hdfBannerImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#00a65a !important");
            }
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorPlacement: function (error, element) {
            debugger
            if (element.attr("name") == "hdfBannerImage") {
                error.insertAfter(element.closest(".validate"));
            }
            else if (element.attr("name") == "hdfBannerVideo") {
                error.insertAfter(element.closest(".customvideoupload"));
            }
            else {
                error.insertAfter(element.closest(".form-control"));
            }
        }
    });


    $("#btnSave").click(function () {
        if ($("#frmManage").valid()) {
            ShowBlockUI();
            $("#frmManage")[0].submit();
        }
    });
}

function ShowPreview(input, previewFor) {
    if (previewFor == "bannerImage") {
        var fileName = $('input[name="fileImage"]').val();
        $("#hdfBannerImage").val(fileName);
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
        var validator = $("#frmManage").validate();
        validator.element("#hdfBannerImage");
    } else if (previewFor == "bannerVideo") {
        var videoFileName = $('input[name="fileupload"]').val();
        $("#hdfBannerVideo").val(videoFileName);
        var validator = $("#frmManage").validate();
        validator.element("#hdfBannerVideo");
    }
}

function RemoveExistingFile() {
    $("#hdfBannerVideo").val("");
    $("#hdfBannerVideo").removeClass("bannerVideo");
    $("#dvUploadSection").removeClass("d-none");
    $("#dvUploadedSection").addClass("d-none");
}