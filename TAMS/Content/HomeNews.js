$(document).ready(function () {
    var newsPage = new NewsPage();
    newsPage.init();
});
class NewsPage {
    constructor(){
        this.loadFirst.bind(this);
    }
    init() {
        $(".page-link").on("click", this.clickPage);
    }
    clickPage( ) {
        $.ajax({
            url: "/HomeNews/GetNewsOfPage?Page=" + this.id,
            dataType: "json",
            method: "GET",
        }).done((data) => {
            $("#more-news").empty();
            data.Item1.map((news) => {
                
                $("#more-news").append(`<div class="row m-b-20">
                            <div class="w-100">
                                <img style="float: left;margin-right: 20px" src="../../Content/imgs/`+ news.Avatar + `" width="220" />
                                <div class="text-secondary text-14px">`+ news.ModifyDate + `</div>
                                <h6>`+ news.Name + `</h6>
                                <p class="text-14px ">`+ news.Sapo + `</p>
                            </div>
                        </div>`)
            });
            $(".page-link").removeClass("bg-primary text-white");
            $("#" + this.id).addClass("bg-primary text-white");

        }).fail(() => {
            console.log("error");
        })
    }
}