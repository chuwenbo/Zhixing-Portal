//(function () {

    function DesignViewModel() {
        this.ul1 = ko.observableArray([]);
        this.ul2 = ko.observableArray([]);
        this.ul3 = ko.observableArray([]);
        this.ul4 = ko.observableArray([]);

        this.pageIndex = ko.observable(1);
        this.loading = ko.observable(false);
    };

    var designViewModel = new DesignViewModel(); 

    ko.applyBindings(designViewModel, $("div.zx-design")[0]);
 
  //  designViewModel.ul1.push({ ImageURL: "http://www.flamesun.com/upload/20150514/143158729821520.jpg", Description: " 为中国传统家常菜量身定制高水准的复合调味料，简化做菜流程，提升三餐质量，倡导人们回归家庭餐桌，并且把回家吃饭变成可轻松实现的生活方式。" });

    loadData(true);

    // auto load content when scroll page to bottom 
    window.onscroll = function () {  
        var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;

        var pageHeight = document.body.scrollHeight;
 
        var viewHeight = window.screen.availHeight; 

        //when scroll to bottom
        if ((scrollTop + viewHeight) > (pageHeight - 20)) {
            if (scrollTop < 10000) {//keep scroll under control.    
                if (!designViewModel.loading()) {
                    loadData(false);
                } 
            }
        }
    }; 


    function loadData(pageLoad) {
 
        designViewModel.loading(true);
        
        var cid = $("#categoryId").val();

        $.post("/data/GetDesignWorks", { cid: cid, pageIndex: designViewModel.pageIndex() }, function (data) {

            var j = 1;

            $.each(data, function (i, d) {
                console.log(d);
                switch (j) {
                    case 1: 
                        designViewModel.ul1.push(d);
                        break;
                    case 2:
                        designViewModel.ul2.push(d);
                        break;
                    case 3:
                        designViewModel.ul3.push(d);
                        break;
                    case 4:
                        designViewModel.ul4.push(d);
                        break;
                }

                j++;

                if (j > 4) { j = 1; }
            });
            //console.log(designViewModel.ul1());
            if (pageLoad) {
                // load animate
                $(".zx-design").animate({ 'width': '1200' });  
            }

            designViewModel.pageIndex(designViewModel.pageIndex() + 1);
        }).always(function () {
            designViewModel.loading(false);
        });
    };

//})(window);