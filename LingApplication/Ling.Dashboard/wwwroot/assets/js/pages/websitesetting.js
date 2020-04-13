$(document).ready(function () {
    InitDataTable();
    $.fn.DataTable.ext.pager.numbers_length = 5;
});

// INITIALIZE DATA TABLE
function InitDataTable() {
    var ajaxUrl = _contentRoot + 'WebsiteSetting/GetAllWebSiteSetting';
    $('#tblwebsitesettings').DataTable({
        "processing": true,
        "language": {
            "emptyTable": "No record found",
            "sProcessing": "<div style='border: 'none';padding: '2px';backgroundColor: 'none';opacity: 1'><h3 style='margin: 10px 0px;'><img class='loading-image-radius' src='/assets/images/loader (2).gif')/></h3></div>"
        },
        paging: true,
        filter: true,
        destroy: true,
        orderMulti: false,
        serverSide: true,
        columnDefs: [
            { "width": "20%", "targets": 0 },
            { "width": "35%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            { "width": "2%", "targets": 4 }
        ],
        ajax: {
            type: "POST",
            url: ajaxUrl,
            dataType: "json"
        },
        aoColumns: [
            { mDataProp: "Name", "orderable": true },
            { mDataProp: "Value", "orderable": true },
            { mDataProp: "ModifiedBy", "orderable": true },
            {
                mDataProp: "ModifiedDate",
                render: function (d) {
                    return moment(d).format("MM/DD/YYYY");
                },
                "orderable": true
            },
            {
                mDataProp: "ID",
                className: 'text-center',
                render: function (d) {
                    var editUrl = _contentRoot + "websitesetting/manage/" + d;
                    return '<div class="dropdown text-sans-serif"><button class="btn btn-link text-600 btn-sm dropdown-toggle btn-reveal custom-btn-reveal mr-3" type="button" id="dropdown' + d + '" data-toggle="dropdown" data-boundary="viewport" aria-haspopup="true" aria-expanded="false"><span class="fa fa-ellipsis-h fs--1"></span></button>' +
                        '<div class="dropdown-menu dropdown-menu-right border py-0" aria-labelledby="dropdown' + d + '">' +
                        '<div class="bg-white py-2"><a class="dropdown-item" href=\"' + editUrl + '\" >Edit</a>' +
                        '</div></div></div>';
                },
                "orderable": false
            }
        ],
        select: true,
        "drawCallback": function (settings) {
            $('[data-toggle="b-tooltip"]').tooltip({
                container: 'body'
            });

            $(".dataTables_paginate .pagination").addClass('pagination-sm');
        }
    });
}

function ShowExceptionLogModalByID(id) {
    if (id > 0) {
        var ajaxUrl = _contentRoot + 'ExceptionLog/SelectExceptionMessageByID';
        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: { id: id },
            success: function (response) {
                $("#viewExceptionDetailModal").modal("show");
                if (response != null && response.ResultCode == "SUCCESS") {
                    $('#dvExceptionMessage').html("<b>Message : </b>" + response.ResultObject.ErrorMessage);
                    $('#dvStackTrace').hide();
                    if (response.ResultObject.StackTrace.length > 0) {
                        $('#dvStackTrace').html(response.ResultObject.StackTrace).show();
                    }
                }
            },
            error: function (x, e) {
                $("#viewExceptionDetailModal").modal("hide");
            }
        });
    }
}


function GetAllWebSiteSetting(pageIndex, pageSize, searchText) {

    var searchText = $("#searchText").val();
    var optionselected = $("#drp_index option:selected").val();
    pageSize = optionselected;

    var ajaxURL = _contentRoot + 'WebsiteSetting/GetAllWebSiteSetting';

    ShowBlockUI();
    ajaxObject = $.ajax({
        url: ajaxURL,
        data: {
            pPageIndex: parseInt(pageIndex),
            pPageSize: parseInt(pageSize),
            pSearch: searchText
        },
        success: function (response) {
            HideBlockUI();
            $("#dvListContainer").empty().html(response);
        },
        error: function () {
        }
    });

}