 
    var dt = $('#example').DataTable({
        "processing": false,
        "serverSide": true,
        "searching": false,
        "ordering": false,
        "lengthing": false,
        "ajax": "/data/getcategory",
        "columns": [
            {
                "class": "details-control",
                "orderable": false,
                "data": null,
                "defaultContent": ""
            },
            { "data": "Name" }
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
        return '名称: <input type="text" value="' + d.Name + '"/> ' + ' ' +
            '<button onclick="saveDetail($(this))"> 保存</button>' +
            '<button onclick="deleteDetal(\'' + d.Id + '\')"> 删除</button>' +
            '';
    } 

    function saveDetail($this) {
        var name = $this.parent().find('input').val(); 
        var tr = $this.parent().closest('tr').prev();
        var row = dt.row(tr);

        if ($this.data("saving") != true)
        {
            $this.data("saving", true);

            $.post("/data/UpdateCategory", { id: row.data().Id, name: name }, function (data) {
                if (data.Success) {
                    dt.search('').draw(false);
                } else {
                    alert(data.Message);
                }
            }).always(function () {
                $this.data("saving", false);
            });
        } 
       
    }

    function deleteDetal(id) { 

        $.post("/data/DeleteCategory", { id: id }, function (data) {
            if (data.Success) {
                dt.search('').draw(false);
            } else {
                alert(data.Message);
            }
        });
    }

    $modal = $("#categoryModal");

    $modal.on('show.bs.modal', function () {
        $modal.find("#name-message").text('');
        $modal.find("input#categoryName").val('');
        $modal.find("input#categoryName").focus();
    });

    $modal.find("#categoryName").focus(function () { 
        $modal.find("#name-message").text('');
    }); 

    $modal.find("#save").click(function () {

        var name = $modal.find("input#categoryName").val().trim();

        if (name == '') {
            $modal.find("#name-message").text("名称不能为空"); 
            return;
        }

        if ($(this).data("saving") != true || $(this).data("saving")) {
            $(this).data("saving", true);

            $.post("/data/CreateCategory", { name: name }, function (data) {
                if (data.Success) {
                    $modal.modal('hide');
                    dt.search('').draw(false);
                } else {
                    alert(data.Message);
                }
            }).always(function () {
                $(this).data("saving", false);
            });
        }

       
    });
