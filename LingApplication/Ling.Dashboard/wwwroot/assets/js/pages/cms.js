﻿// PAGE LOAD EVENT
$(document).ready(function () {
    $('#tblCMS').on('processing.dt', function (e, settings, processing) {
        if (processing)
            ShowBlockUI();
        else
            HideBlockUI();
    }).dataTable();

    InitDataTable();

    $.fn.DataTable.ext.pager.numbers_length = 5;

});


// INITIALIZE DATA TABLE
function InitDataTable() {
    var ajaxUrl = _contentRoot + 'CMS/GetCMSData';
    $('#tblCMS').DataTable({
        "language": {
            "emptyTable": "No record found"
        },
        paging: true,
        filter: true,
        destroy: true,
        orderMulti: false,
        serverSide: true,
        ajax: {
            type: "POST",
            url: ajaxUrl,
            dataType: "json"
        },
        columnDefs: [
            { "width": "35%", "targets": 0 },
            { "width": "30%", "targets": 1 },
            { "width": "10%", "targets": 2 },
            { "width": "12%", "targets": 3 },
            { "width": "12%", "targets": 4 },
            { "width": "2%", "targets": 5 }
        ],
        aoColumns: [
            { mDataProp: "CMSKey", "orderable": true },
            { mDataProp: "Title", "orderable": true },
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
                "orderable": true
            },
            {
                mDataProp: "ID",
                className: 'text-center',
                render: function (d) {
                    var editUrl = _contentRoot + "CMS/Manage/" + d;
                    var deleteUrl = "ShowGlobalConfirmDeleteModal('" + _contentRoot + "CMS/Delete/" + d + "')";
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
        }
    });
}