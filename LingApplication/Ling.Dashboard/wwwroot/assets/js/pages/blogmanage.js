﻿
// DOCUMENT LOAD EVENT IT WILL OCCURE ON PAGE LOAD
$(document).ready(function () {
    InitSummernote();
    InitFormValidation();

    InitCategoryValidation();

    $('#CategoryModal').on('hide.bs.modal', function (e) {
        $('#txtCategory').removeClass('is-valid is-invalid').val('');
        $('#txtCategory').parents('.form-group').removeClass('has-error has-success');
        $('#txtCategory-error').remove();
        $('#categoryExists').addClass('d-none');
    })
});

// INITIALIZATION FORM VALIDATION
function InitFormValidation() {
    $("#frmBlog").validate({
        ignore: [],
        rules: {
            Slug: { required: true },
            Title: { required: true },
            hdfBlogImage: {
                required: true,
                extension: "png|jpeg|jpg"
            },
            MetaTitle: { required: true },
            MetaDescription: { required: true },
            BlogCategoryID: { required: true }
        },
        messages: {
            Slug: "Please enter slug",
            Title: "Please enter title",
            hdfBlogImage: {
                required: "Please upload image",
                extension: "Only image allowed (png, jpeg, jpg)"
            },
            MetaTitle: "Please select meta title",
            MetaDescription: "Please select meta description",
            BlogCategoryID: "Please select category",
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
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
            if (element.attr("name") == "hdfBlogImage") {
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

        if ($("#frmBlog").valid() && isValidSummernote) {
            $("#spnSummernoteError").hide();
            $(".note-editor").css('margin-bottom', '14px');
            $("#dvDescription").removeClass("has-error").addClass("has-success");
            $('.summernote').val($('.summernote').summernote('code'));
            $("#frmBlog").submit();
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

// CONVERT BLOG TITLE TO SLUG
function convertToSlug(ele) {
    var $slug = '';
    var trimmed = $.trim($(ele).val());
    $slug = trimmed.replace(/[^a-z0-9-]/gi, '-').
        replace(/-+/g, '-').
        replace(/^-|-$/g, '');

    $('#txtSlug').val($slug.toLowerCase());
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

function ShowCategoryModal() {
    $('#CategoryModal').modal('show');
}

// INITIALIZATION CATEGORY FORM VALIDATION
function InitCategoryValidation() {
    $("#formCategory").validate({
        ignore: [],
        rules: {
            txtCategory: { required: true }
        },
        messages: {
            txtCategory: "Please enter category"
        },
        errorElement: 'span',
        errorClass: 'invalid-feedback',
        highlight: function (element) {
            $(element).removeClass('is-valid').addClass('is-invalid');
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        unhighlight: function (element) {
        },
        success: function (element, object) {
            $(object).removeClass('is-invalid').addClass('is-valid');
            $(element).closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.closest(".form-control"));
        }
    });
}

function SaveCategory(ele) {
    var laddaContextCustom = Ladda.create(ele);

    if ($("#formCategory").valid()) {
        var data = new FormData();
        data.append("BlogCategoryName", $('#txtCategory').val());
        laddaContextCustom.start();
        var ajaxUrl = _contentRoot + "Blog/SaveBlogCategory";
        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: data,
            dataType: "json",
            contentType: false,
            processData: false,
            success: function (response) {
                laddaContextCustom.stop();
                if (response.ResultCode == "SUCCESS") {
                    setTimeout(function () {
                        $("#btnsubmit").html('submit');
                    }, 8000);
                    $('#BlogCategoryID')
                        .append($("<option></option>")
                            .attr("value", response.ResultObjectID)
                            .text($('#txtCategory').val()));

                    $('#BlogCategoryID').val(response.ResultObjectID);
                    $('#CategoryModal').modal('hide');

                } else if (response.ResultCode == "EXISTS") {
                    $('#categoryExists').removeClass('d-none');
                }

            },
            error: function (x, e) {
                laddaContextCustom.stop();
            }
        });
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
            ['insert', ['link', 'picture', 'hr', 'video']],
            ['view', ['fullscreen', 'codeview']],
            ['help', ['help']]
        ]
    });

    $("#txtDescription").on('summernote.change', function () {
        $("#spnSummernoteError").hide();
        $("#dvDescription").removeClass("has-error").addClass('has-success');
        $(".note-editor").css('margin-bottom', '14px');
        $(".note-editor").css('border-color', '#00a65a');

        if ($("#txtDescription").summernote('isEmpty')) {
            $("#spnSummernoteError").show();
            $("#dvDescription").removeClass("has-success").addClass("has-error");
            $(".note-editor").css('margin-bottom', '1px');
            $(".note-editor").css('border-color', '#dd4b39');
        }
    });

    $('.note-modal .modal-header').addClass('bg-gradient');
    $('.note-modal .modal-header h4').each(function () {
        $(this).insertBefore($(this).parent().find('button'));
    });
}

