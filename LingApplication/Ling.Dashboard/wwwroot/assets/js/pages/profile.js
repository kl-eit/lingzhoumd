﻿// document load event it will occure on page load
$(document).ready(function () {
    InitProfileFormValidation();
    InitChangePasswordFormValidation();
});

// Initialization Form Validation
function InitProfileFormValidation() {
    $("#frm_Profile").validate({
        ignore: [],
        rules: {
            Name: { required: true },
            PhoneNumber: {
                required: true,
                digits: true,
                maxlength: 10,
                minlength: 10
            },
            hdfImageName: {
                required: true,
                extension: "png|jpeg|jpg"
            }
        },
        messages: {
            Name: "Please enter name",
            PhoneNumber: {
                required: "Please enter phone",
                digits: "Please enter only digit",
                maxlength: "Please enter 10 digit number",
                minlength: "Please enter 10 digit number"
            },
            hdfImageName: {
                required: "",
                extension: ""
            }
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            if ($(element).attr("name") == "hdfImageName") {
                $(".avatar-profile").css("border-color", "#dd4b39");
            }
            else {
                $(element).removeClass('is-valid').addClass('is-invalid');
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
            }
        },
        unhighlight: function (element) {
            if ($(element).attr("name") == "hdfImageName") {
                var uploadFile = $("#hdfImageName").val();
                var extension = uploadFile.substr(uploadFile.lastIndexOf('.') + 1);
                if (extension == "png" || extension == "jpeg" || extension == "jpg")
                    $(".avatar-profile").css("border-color", "#00a65a");
            }
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
    });


    $("#btnSave").click(function () {
        if ($("#frm_Profile").valid()) {
            $("#frm_Profile").submit();
        }
    });
}

// INITIALIZATION FORM VALIDATION
function InitChangePasswordFormValidation() {
    $("#frmManage").validate({
        rules: {
            CurrentPassword: { required: true },
            Password: {
                required: true,
                minlength: 6,
                maxlength: 15
            },
            ConfirmPassword: {
                required: true,
                equalTo: "#Password"
            }
        },
        messages: {
            CurrentPassword: "Please enter current password",
            Password: {
                required: "Please enter password",
                minlength: "Minimum required 6 characters",
                maxlength: "Maximum 15 characters allowed"
            },
            ConfirmPassword:
            {
                required: "Please enter confirm password",
                equalTo: "Password not match"
            }
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

    $("#btnUpdatePassword").click(function () {
        if ($("#frmManage").valid()) {
            $("#frmManage").submit();
        }
    });
}

//TO SHOW PREVIEW OF IMAGE
function ShowPreview(input) {
    var fileName = $('input[type=file]').val();
    $("#hdfOldImageName").val("");
    $("#hdfImageName").val(fileName);
    if (input.files && input.files[0]) {
        var ImageDir = new FileReader();
        ImageDir.onload = function (e) {
            $("#profileImage").attr("src", e.target.result);
        }
        ImageDir.readAsDataURL(input.files[0]);
    }

    var validator = $("#frm_Profile").validate();
    validator.element("#hdfImageName");
}
