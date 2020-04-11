
// PAGE LOAD EVENT
$(document).ready(function () {


    $(".custom-file-input").on("change", function () {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });
    var formElement = $('#frmImageUpload');
    formElement.validate({
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            error.insertAfter(element.closest('.file-input-wrapper'))
        },
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            element.addClass('valid').closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        rules: {
            ImageUpload: {
                required: true,
                extension: "jpg|jpeg|png|JPEG"
            }
        },
        messages: {
            ImageUpload: {
                required: "Please select image",
                extension: "Please select valid image file"
            }
        }
    });

    var formElementVideo = $('#frmVideoUpload');

    formElementVideo.validate({
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            error.insertAfter(element.closest('.file-input-wrapper'))
        },
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            element.addClass('valid').closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        rules: {
            VideoUpload: {
                required: true,
                extension: "mp4"
            }
        },
        messages: {
            VideoUpload: {
                required: "Please select video",
                extension: "Please select mp4 file"
            }
        }
    });

    var formElementFile = $('#frmFileUpload');

    formElementFile.validate({
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            error.insertAfter(element.closest('.file-input-wrapper'))
        },
        highlight: function (element) {
            $(element).closest('.form-group').removeClass('has-success').addClass('has-error');
        },
        success: function (element) {
            element.addClass('valid').closest('.form-group').removeClass('has-error').addClass('has-success');
        },
        rules: {
            FileUpload: { required: true },
            extension: "txt|doc|docx|pdf|xls|xlsx|csv"
        },
        messages: {
            FileUpload: "Please select file",
            extension: "Please valid file"
        }
    });

    var fileData = null;

    //$('input[type=file]').bootstrapFileInput();

    // IMAGE UPLOAD
    $("#uploadImage").click(function () {
        $(".custom-file-label").text('');
        $(".modalImageUpload").modal('show');
    });

    $("#ImageUpload").change(function () {
        fileData = this.files[0];
    });

    $("#btnUploadImage").click(function () {
        var laddaContextCustom = Ladda.create(this);
        if ($('#frmImageUpload').valid()) {
            var formData = new FormData();

            formData.append('file', fileData);

            laddaContextCustom.start();
            var actionURl = _contentRoot + "Common/UploadImage";
            $.ajax({
                url: actionURl,
                type: 'POST',
                xhr: function () {
                    var xhr = $.ajaxSettings.xhr();
                    if (xhr.upload) {
                       laddaContextCustom.start();
                    }
                    return xhr;
                },
                success: function (data) {
                    laddaContextCustom.stop();
                    $(".modalImageUpload").modal('hide');
                    var node = $(data)[0];
                    $('.summernote').summernote('insertNode', node);
                    $("#ImageUpload").val('');
                    $("#ImageUpload").closest("a.file-input-wrapper").find("span").text("Browse");
                },
                error: function () {
                    laddaContextCustom.stop();
                },
                data: formData,
                cache: false,
                contentType: false,
                processData: false
            }, 'json');
        }
    });

    // VIDEO UPLOAD
    $("#uploadVideo").click(function () {
        $(".custom-file-label").text('');
        $(".modalVideoUpload").modal('show');
    });

    $('.modalVideoUpload').on('shown.bs.modal', function (e) {
        $(".modalVideoUpload .modal-body #frmVideoUpload #dvSearchText #Width").val('');
        $(document).find("#btnAlignMiddle").addClass('active');
    })

    $("#VideoUpload").change(function () {
        fileData = this.files[0];
    });

    $(".btn-group > .btnAlign").on("click", function () {
        $(".btn-group > .btnAlign").removeClass('active');
        $(this).addClass('active');
    });

    $("#btnUploadVideo").click(function () {
        var laddaContextCustom = Ladda.create(this);
        if ($('#frmVideoUpload').valid()) {
            var width = $(".modalVideoUpload .modal-body #frmVideoUpload #dvSearchText input").val();
            var align = $(".modalVideoUpload .modal-body #frmVideoUpload .btnAlign.active").val();
            var formData = new FormData();

            formData.append('file', fileData);
            formData.append('Width', width);
            formData.append('align', align);

            laddaContextCustom.start();
            var actionURl = _contentRoot + "Common/UploadVideo";

            $.ajax({
                url: actionURl,
                data: formData,
                type: 'POST',
                xhr: function () {
                    var xhr = $.ajaxSettings.xhr();
                    if (xhr.upload) {
                        laddaContextCustom.start();
                    }
                    return xhr;
                },
                success: function (data) {
                    laddaContextCustom.stop();
                    $(".modalVideoUpload").modal('hide');
                    var node = $(data)[0];
                    $('.summernote').summernote('insertNode', node);
                    $("#VideoUpload").val('');
                    $("#VideoUpload").closest("a.file-input-wrapper").find("span").text("Browse");
                },
                error: function () {
                    laddaContextCustom.stop();
                },
                data: formData,
                cache: false,
                contentType: false,
                processData: false
            }, 'json');
        }
    });

    $("#uploadFile").click(function () {
        $(".custom-file-label").text('');
        $(".modalFileUpload").modal('show');
    });

    $("#FileUpload").change(function () {
        fileData = this.files[0];
    });

    $("#btnUploadFile").click(function () {
        var laddaContextCustom = Ladda.create(this);
        if ($('#frmFileUpload').valid()) {
            var formData = new FormData();

            formData.append('file', fileData);

            laddaContextCustom.start();
            var actionURl = _contentRoot + "Common/UploadFile";
            $.ajax({
                url: actionURl,
                type: 'POST',
                xhr: function () {
                    var xhr = $.ajaxSettings.xhr();
                    if (xhr.upload) {
                        laddaContextCustom.start();
                    }
                    return xhr;
                },
                success: function (data) {
                    laddaContextCustom.stop();
                    $(".modalFileUpload").modal('hide');
                    var node = $(data)[0];
                    $('.summernote').summernote('insertNode', node);
                    $("#FileUpload").val('');
                    $("#FileUpload").closest("a.file-input-wrapper").find("span").text("Browse");
                },
                error: function () {
                    laddaContextCustom.stop();
                },
                data: formData,
                cache: false,
                contentType: false,
                processData: false
            }, 'json');
        }
    });

    $(".close-modal").click(function () {
        var frmFileUpload = $("#frmFileUpload").validate();
        var frmImageUpload = $("#frmImageUpload").validate();
        var frmVideoUpload = $("#frmVideoUpload").validate();

        frmFileUpload.resetForm();
        frmImageUpload.resetForm();
        frmVideoUpload.resetForm();
    });

    $('.modal').on('hidden.bs.modal', function () {
        var frmFileUpload = $("#frmFileUpload").validate();
        var frmImageUpload = $("#frmImageUpload").validate();
        var frmVideoUpload = $("#frmVideoUpload").validate();

        frmFileUpload.resetForm();
        frmImageUpload.resetForm();
        frmVideoUpload.resetForm();

        $("#frmFileUpload").find(".has-error").removeClass("has-error");
        $("#frmFileUpload").find(".has-success").removeClass("has-success");

        $("#frmImageUpload").find(".has-error").removeClass("has-error");
        $("#frmImageUpload").find(".has-success").removeClass("has-success");

        $("#frmVideoUpload").find(".has-error").removeClass("has-error");
        $("#frmVideoUpload").find(".has-success").removeClass("has-success");
    });
});
