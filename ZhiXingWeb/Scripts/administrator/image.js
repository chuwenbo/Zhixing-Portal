var imageViewModel = {
    categorys: ko.observableArray([]),
    uploadSeletedCategory: ko.observable()

};

ko.applyBindings(imageViewModel);

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


//实例化一个plupload上传对象
var uploader = new plupload.Uploader({
    browse_button: 'loadImage', //触发文件选择对话框的按钮，为那个元素id
    url: '/data/UploadImages', //服务器端的上传页面地址
    flash_swf_url: '/scripts/plupload/Moxie.swf', //swf文件，当需要使用swf方式进行上传时需要配置该参数
    silverlight_xap_url: '/scripts/plupload/Moxie.xap', //silverlight文件，当需要使用silverlight方式进行上传时需要配置该参数
    filters: {
        mime_types: [ //只允许上传图片和zip文件
          { title: "Image files", extensions: "jpg,gif,png" }
        ],
        max_file_size: '2048kb', //最大只能上传400kb的文件
    }
});

//在实例对象上调用init()方法进行初始化
uploader.init();

//绑定各种事件，并在事件监听函数中做你想做的事
uploader.bind('FilesAdded', function (uploader, files) {
    //每个事件监听函数都会传入一些很有用的参数，
    //我们可以利用这些参数提供的信息来做比如更新UI，提示上传进度等操作

    //console.log(files);

    for (var i = 0, len = files.length; i < len; i++) {
        var file_name = files[i].name; //文件名
        //构造html来更新UI
        var html = '<li id="file-' + files[i].id + '"><p class="file-name">' + file_name + '</p><p class="progress"></p></li>';
        $(html).appendTo('#file-list');
    }
});

uploader.bind('UploadProgress', function (uploader, file) {

    //console.log(file.percent);

    //每个事件监听函数都会传入一些很有用的参数，
    //我们可以利用这些参数提供的信息来做比如更新UI，提示上传进度等操作
    $('#file-' + file.id + ' .progress').css('width', file.percent + '%');//控制进度条
});

uploader.bind("UploadComplete", function (uploader, files) {
    console.log("finished");

    alert("上传完成");
});

//最后给"开始上传"按钮注册事件
document.getElementById('start_upload').onclick = function () {
    uploader.setOption({ multipart_params: { 'category': imageViewModel.uploadSeletedCategory() } });
    uploader.start(); //调用实例对象的start()方法开始上传文件，当然你也可以在其他地方调用该方法
    

    console.log(imageViewModel.uploadSeletedCategory());
}

$.get("/data/GetCategory", function (result) {
   // console.log(result.data);

    imageViewModel.categorys(result.data);
});


