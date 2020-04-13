// PAGE LOAD EVENT
$(document).ready(function () {
    InitDataTable();

    $.fn.DataTable.ext.pager.numbers_length = 5;

    $('#tablefaq tbody').sortable({
        items: "tr",
        cursor: 'move',
        opacity: 0.6,
        update: function (event, ui) {
            var sortOrderIDs = $(this).sortable("toArray").toString();
            var ajaxUrl = _contentRoot + "FAQ/UpdateSortOrderID";

            if (!IsNullOrEmptyString(sortOrderIDs)) {
                $.ajax({
                    type: "POST",
                    url: ajaxUrl,
                    data: {
                        sortedRowIDs: sortOrderIDs
                    },
                    success: function (response) {
                    },
                    error: function (x, e) {
                    }
                });
            }
        }
    });
});


// INITIALIZE DATA TABLE
function InitDataTable() {
    var ajaxUrl = _contentRoot + 'faq/GetFAQList';
    $('#tablefaq').DataTable({
        "processing": true,
        "language": {
            "emptyTable": "No record found",
            "sProcessing": "<div style='border: 'none';padding: '2px';backgroundColor: 'none';opacity: 1'><h3 style='margin: 10px 0px;'><img class='loading-image-radius' src='/assets/images/loader (2).gif')/></h3></div>"
        },
        paging: false,
        filter: true,
        destroy: true,
        orderMulti: false,
        serverSide: true,
        "info": false,
        ajax: {
            type: "POST",
            url: ajaxUrl,
            dataType: "json"
        },
        columnDefs: [
            { "width": "5%", "targets": 6 }
        ],
        aoColumns: [
            {
                mDataProp: "ID",
                visible: false
            },
            {
                mDataProp: null,
                className: 'custom-sortorder',
                render: function (d) {
                    return '<i class="metismenu-icon fa fa-arrows dragLink"></i>'
                },
                "orderable": false
            },
            { mDataProp: "Question", "orderable": false },
            {
                "data": "IsActive", orderable: false, "targets": [5], "render": function (data, type, full, meta) {
                    if (data == true) {
                        return '<span class="badge badge rounded-capsule badge-soft-success">YES<span class="ml-1 fa fa-check" data-fa-transform="shrink-2"></span></span>';
                    }
                    else return '<span class="badge badge rounded-capsule badge-soft-warning">No<span class="ml-1 fa fa-times" data-fa-transform="shrink-2"></span></span>';
                }
            },
            { mDataProp: "CreatedBy", "orderable": false },
            {
                mDataProp: "CreatedDate",
                render: function (d) {
                    return moment(d).format("MM/DD/YYYY");
                },
                "orderable": false
            },
            {
                mDataProp: "ID",
                className: 'text-center',
                render: function (d) {
                    var editUrl = _contentRoot + "faq/manage/" + d;
                    var deleteUrl = "ShowGlobalConfirmDeleteModal('" + _contentRoot + "FAQ/Delete/" + d + "')";
                    return '<div class="dropdown text-sans-serif"><button class="btn btn-link text-600 btn-sm dropdown-toggle btn-reveal custom-btn-reveal mr-3" type="button" id="dropdown' + d + '" data-toggle="dropdown" data-boundary="viewport" aria-haspopup="true" aria-expanded="false"><span class="fa fa-ellipsis-h fs--1"></span></button>' +
                        '<div class="dropdown-menu dropdown-menu-right border py-0" aria-labelledby="dropdown' + d + '">' +
                        '<div class="bg-white py-2"><a class="dropdown-item" href=\"' + editUrl + '\" >Edit</a>' +
                        '<div class="dropdown-divider"></div><a class="dropdown-item text-danger" href="javascript:void(0);" onclick=\"' + deleteUrl + '\">Delete</a>' +
                        '</div></div></div>';
                },
                "orderable": false
            }
        ],
        "fnCreatedRow": function (row, data, index) {
            $(row).attr('id', data.ID);
        },
        select: true,
        "drawCallback": function (settings) {
            $('[data-toggle="tooltip"]').tooltip({
                container: 'body',
            });
            $(".dataTables_paginate .pagination").addClass('pagination-sm');
            $('#tablefaq_wrapper').addClass('mt-2');
        }
    });
}