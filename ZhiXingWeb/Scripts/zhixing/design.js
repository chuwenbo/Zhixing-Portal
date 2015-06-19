(function () {

    function DesignViewModel() {
        this.ul1 = ko.observableArray([]);
        this.ul2 = ko.observableArray([]);
        this.ul3 = ko.observableArray([]);
        this.ul4 = ko.observableArray([]);

        this.pageIndex = ko.observable(1);
        this.loading = ko.observable(false);
    };

    var viewModel = new DesignViewModel(); 

    ko.applyBindings(viewModel, $("div.zx-design")[0]);

    loadData(true);

    // auto load content when scroll page to bottom 
    window.onscroll = function () {  
        var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;

        var pageHeight = document.body.scrollHeight;
 
        var viewHeight = window.screen.availHeight; 

        //when scroll to bottom
        if ((scrollTop + viewHeight) > (pageHeight - 20)) {
            if (scrollTop < 10000) {//keep scroll under control.    
                if (!viewModel.loading()) {
                    loadData(false);
                } 
            }
        }
    }; 


    function loadData(pageLoad) {
 
        viewModel.loading(true);
        
        var cid = $("#categoryId").val();

        $.post("/data/GetDesignWorks", { cid: cid, pageIndex: viewModel.pageIndex() }, function (data) {

            var j = 1;

            $.each(data, function (i, d) {

                switch (j) {
                    case 1:
                        viewModel.ul1.push(d);
                        break;
                    case 2:
                        viewModel.ul2.push(d);
                        break;
                    case 3:
                        viewModel.ul3.push(d);
                        break;
                    case 4:
                        viewModel.ul4.push(d);
                        break;
                }

                j++;

                if (j > 4) { j = 1; }
            });

            if (pageLoad) {
                // load animate
                $(".zx-design").animate({ 'width': '1200' });  
            }

            viewModel.pageIndex(viewModel.pageIndex() + 1);
        }).always(function () {
            viewModel.loading(false);
        });
    };

})(window);