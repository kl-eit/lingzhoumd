﻿// PAGE LOAD EVENT
$(document).ready(function () {

    InitDataTable();

    //$('#tblContactInquiries').on('processing.dt', function (e, settings, processing) {
    //    if (processing)
    //        ShowBlockUI();
    //    else
    //        HideBlockUI();
    //}).dataTable();

    $.fn.DataTable.ext.pager.numbers_length = 5;
});

// INITIALIZE DATA TABLE
function InitDataTable() {
    var ajaxUrl = _contentRoot + 'Home/GetContactInquiryList';
    $('#tblContactInquiries').DataTable({
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
            { "width": "10%", "targets": 1 },
            { "width": "20%", "targets": 2 },
            { "width": "20%", "targets": 3 },
            { "width": "10%", "targets": 4 },
            { "width": "15%", "targets": 5 }
        ],
        aoColumns: [
            {
                mDataProp: "ID",
                visible: false
            },
            { mDataProp: "Name", "orderable": false },
            { mDataProp: "EmailAddress", "orderable": false },
            {
                mDataProp: "Subject", "orderable": false, "render": function (data, type, full, meta) {
                    return data.length > 100 ?
                        data.substr(0, 100) + '…' :
                        data;
                }
            },
            {
                mDataProp: "Message", "orderable": false
            },
            {
                "data": "Status", orderable: false, "render": function (data, type, full, meta) {
                    if (data == true) {
                        return '<span class="badge badge rounded-capsule badge-soft-success">New<span class="ml-1 fa fa-check" data-fa-transform="shrink-2"></span></span>';
                    }
                    else return '<span class="badge badge rounded-capsule badge-soft-warning">Old<span class="ml-1 fa fa-times" data-fa-transform="shrink-2"></span></span>';
                }
            },
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
                    var editUrl = _contentRoot + "blog/manage/" + d;
                    return '<div class="dropdown text-sans-serif"><button class="btn btn-link text-600 btn-sm dropdown-toggle btn-reveal custom-btn-reveal mr-3" type="button" id="dropdown' + d + '" data-toggle="dropdown" data-boundary="viewport" aria-haspopup="true" aria-expanded="false"><span class="fa fa-ellipsis-h fs--1"></span></button>' +
                        '<div class="dropdown-menu dropdown-menu-right border py-0" aria-labelledby="dropdown' + d + '">' +
                        '<div class="bg-white py-2"><a class="dropdown-item" href=\"' + editUrl + '\" >Reply</a>' +
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