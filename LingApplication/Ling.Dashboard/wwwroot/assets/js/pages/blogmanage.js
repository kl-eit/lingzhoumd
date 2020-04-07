
// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitFormValidation();
});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    $("#frmBlog").validate({
        ignore: [],
        rules: {
            Slug: { required: true },
            Title: { required: true },
            Description: { required: true },
            hdfBlogImage: {
                required: true,
                extension: "png|jpeg|jpg"
            }
        },
        messages: {
            Slug: "Please enter slug",
            Title: "Please enter title",
            Description: "Please enter description",
            hdfBlogImage: {
                required: "Please upload image",
                extension: "Only image allowed (png, jpeg, jpg)"
            }
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            debugger
            if ($(element).attr("name") == "hdfBlogImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#dd4b39 !important");
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
            else {
                $(element).removeClass('is-valid').addClass('is-invalid');
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
        },
        unhighlight: function (element) {
            if ($(element).attr("name") == "hdfBlogImage") {
                $(element).closest("#imageName").find(".thumbnail").css("border-color", "#00a65a !important");
            }
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorPlacement: function (error, element) {
            debugger
            if (element.attr("name") == "hdfBlogImage") {
                error.insertAfter(element.closest(".validate"));
            }
            else {
                error.insertAfter(element.closest(".form-control"));
            }
        }
    });

    $("#btnSave").click(function () {
        if ($("#frmBlog").valid()) {
            ShowBlockUI();
            $("#frmBlog")[0].submit();
        }
    });
}
function ShowPreview(input) {
    var fileName = $('input[name="fileImage"]').val();
    $("#hdfBlogImage").val(fileName);
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
    var validator = $("#frmBlog").validate();
    validator.element("#hdfBlogImage");
}