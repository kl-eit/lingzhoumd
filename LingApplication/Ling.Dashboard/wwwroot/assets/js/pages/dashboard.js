// PAGE LOAD EVENT
$(document).ready(function () {

    InitDataTable();

    //$('#tblContactInquiries').on('processing.dt', function (e, settings, processing) {
    //    if (processing)
    //        ShowBlockUI();
    //    else
    //        HideBlockUI();
    //}).dataTable();

    $.fn.DataTable.ext.pager.numbers_length = 5;

    var table = $('#tblContactInquiries').DataTable();

    $('#txtGlobalSearch').on('keyup', function () {
        table.search(this.value).draw();
    });
});

// INITIALIZE DATA TABLE
function InitDataTable() {
    var ajaxUrl = _contentRoot + 'Home/GetContactInquiryList';
    $('#tblContactInquiries').DataTable({
        "processing": true,
        "language": {
            "emptyTable": "No record found",
            "sProcessing": "<div style='border: 'none';padding: '2px';backgroundColor: 'none';opacity: 1'><h3 style='margin: 10px 0px;'><img class='loading-image-radius' src='/assets/images/loader (2).gif')/></h3></div>"
        },
        order: [[5, 'desc']],
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
            { "width": "5%", "targets": 6 }
        ],
        aoColumns: [
            {
                mDataProp: "ID",
                visible: false
            },
            { mDataProp: "Name", "orderable": true },
            { mDataProp: "EmailAddress", "orderable": true },
            {
                mDataProp: "Subject", "orderable": true, "render": function (data, type, full, meta) {
                    return data.length > 100 ?
                        data.substr(0, 100) + '…' :
                        data;
                }
            },
            {
                "data": "Status", orderable: true, "render": function (data, type, full, meta) {
                    if (data == true) {
                        return '<span class="badge badge rounded-capsule badge-soft-success">Read<span class="ml-1 fa fa-check" data-fa-transform="shrink-2"></span></span>';
                    }
                    else return '<span class="badge badge rounded-capsule badge-soft-warning">New<span class="ml-1 fa fa-times" data-fa-transform="shrink-2"></span></span>';
                }
            },
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
                    var viewUrl = "ShowContactInquiryModalByID(" + d + ");";
                    var editUrl = "UpdateStatus(" + d + ");";
                    return '<div class="dropdown text-sans-serif"><button class="btn btn-link text-600 btn-sm dropdown-toggle btn-reveal custom-btn-reveal mr-3" type="button" id="dropdown' + d + '" data-toggle="dropdown" data-boundary="viewport" aria-haspopup="true" aria-expanded="false"><span class="fa fa-ellipsis-h fs--1"></span></button>' +
                        '<div class="dropdown-menu dropdown-menu-right border py-0" aria-labelledby="dropdown' + d + '">' +
                        '<div class="bg-white py-2"><a class="dropdown-item"  href="javascript:void(0);" onclick=\"' + viewUrl + '\" >View</a>' +
                        '<div class="bg-white py-2"><a class="dropdown-item"  href="javascript:void(0);" onclick=\"' + editUrl + '\" >Mark As Read</a>' +
                        '</div></div></div>';
                },
                "orderable": false
            }
        ],
        "fnCreatedRow": function (row, data, index) {
            debugger;
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

function ShowContactInquiryModalByID(id) {
    if (id > 0) {
        var ajaxUrl = _contentRoot + 'Home/SelectContactInquiryByID';
        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: { id: id },
            success: function (response) {
                $("#viewContactInquiryDetailModal").modal("show");
                if (response != null && response.resultCode == "SUCCESS") {
                    $('#dvMessage').html("<b>Message : </b>" + response.resultObject.message);
                }
            },
            error: function (x, e) {
                $("#viewContactInquiryDetailModal").modal("hide");
            }
        });
    }
}


function UpdateStatus(id) {
    if (id > 0) {
        var ajaxUrl = _contentRoot + 'Home/UpdateStatusByID';
        $.ajax({
            type: "POST",
            url: ajaxUrl,
            data: { id: id },
            success: function (response) {
                if (response.ResultCode == "SUCCESS") {
                    var table = $('#tblContactInquiries').DataTable();

                    table.draw();
                }
            }
        });
    }
}
