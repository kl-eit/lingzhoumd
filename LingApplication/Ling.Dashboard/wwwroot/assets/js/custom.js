$(document).ready(function () {
    
    $(".searchdata").on("keyup", function () {
        if ($("div.dataTables_wrapper div.dataTables_filter input").length) {
            $("div.dataTables_wrapper div.dataTables_filter input").val($(this).val());
            $("div.dataTables_wrapper div.dataTables_filter input").keyup();
        }
    }).keydown(function (event) {
        if (event.which == 13) {
            event.preventDefault();
        }
    });

    $(".searchdata").on("click", function () {
        if ($(this).val() != "") {
            $("div.dataTables_wrapper div.dataTables_filter input").val("");
            $("div.dataTables_wrapper div.dataTables_filter input").keyup();
        }
    });
    
});


function IsNullOrEmptyString(e) {
    e = $.trim(e);

    switch (e) {
        case "":
        case 0:
        case "0":
        case null:
        case false:
        case typeof this == "undefined":
            return true;
        default: return false;
    }
}

// SHOW GLOBAL DELETE POPUP
function ShowGlobalConfirmDeleteModal(pDeleteActionUrl) {
    $("#btnGlobalDelete").attr("href", pDeleteActionUrl);
    $("#globalConfirmDeleteModal").modal("show");
}

