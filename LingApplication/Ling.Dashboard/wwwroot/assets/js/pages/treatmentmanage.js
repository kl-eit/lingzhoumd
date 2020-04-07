
// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitFormValidation();
});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    $("#frmTreatment").validate({
        rules: {
            Name: { required: true },
            Description: { required: true },
            hdfImageName: {
                required: true,
                extension: "png|jpeg|jpg"
            }
        },
        messages: {
            Name: "Please enter name",
            Description: "Please enter description",
            hdfImageName: {
                required: "Please upload image",
                extension: "Only image allowed (png, jpeg, jpg)"
            }
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            debugger
            if ($(element).attr("name") == "hdfImageName") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#dd4b39 !important");
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
            else {
                $(element).removeClass('is-valid').addClass('is-invalid');
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
        },
        unhighlight: function (element) {
            if ($(element).attr("name") == "hdfImageName") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#00a65a !important");
            }
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorPlacement: function (error, element) {
            debugger
            if (element.attr("name") == "hdfImageName") {
                error.insertAfter(element.closest(".validate"));
            }
            else {
                error.insertAfter(element.closest(".form-control"));
            }
        }
    });

    $("#btnSave").click(function () {
        if ($("#frmTreatment").valid()) {
            ShowBlockUI();
            $("#frmTreatment")[0].submit();
        }
    });
}
function ShowPreview(input, previewFor) {
    if (previewFor == "bannerImage") {
        var fileName = $('input[name="fileImage"]').val();
        $("#hdfImageName").val(fileName);
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
        validator.element("#hdfImageName");
    }
}