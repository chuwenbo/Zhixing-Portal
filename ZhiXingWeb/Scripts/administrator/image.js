var dt = $('#example').DataTable({
    "processing": false,
    "serverSide": true,
    "searching": false,
    "ordering": false,
    "lengthing": false,
    "ajax": "/data/GetImages",
    "columns": [
        {
            "class": "details-control",
            "orderable": false,
            "data": null,
            "defaultContent": ""
        },
        { "data": "CategoryName" },
        { "data": "ImageName" }
    ],
    "order": [[1, 'asc']],
    "rowCallback": function (row, data, index) {
        var _row = dt.row(row);

        if ($.inArray(data.Id, detailRows) >= 0) {
            $(row).addClass('details');
            _row.child(format(data)).show();
        }
    }
});

// Array to track the ids of the details displayed rows
var detailRows = [];

$('#example tbody').on('click', 'tr td.details-control', function () {
    var tr = $(this).closest('tr');
    var row = dt.row(tr);
    var idx = $.inArray(row.data().Id, detailRows);

    if (row.child.isShown()) {
        tr.removeClass('details');
        row.child.hide();

        // Remove from the 'open' array
        detailRows.splice(idx, 1);
    }
    else {
        tr.addClass('details');
        row.child(format(row.data())).show();

        // Add to the 'open' array
        if (idx === -1) {
            detailRows.push(row.data().Id);
        }
    }
});

// On each draw, loop over the `detailRows` array and show any child rows
//dt.on('draw', function () {
//    $.each(detailRows, function (i, id) {
//        $('#' + id + ' td.details-control').trigger('click');
//    });
//});

function format(d) {
    return '<img style="height:300px;" src="' + d.ImageURL + '"/> ' + ' ' + 
        '<button onclick="deleteDetal(\'' + d.Id + '\')"> 删除</button>' +
        '';
}