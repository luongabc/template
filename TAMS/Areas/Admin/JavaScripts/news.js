
$(document).ready(function(){
    var newsPage = new NewsPage();
    newsPage.init();
});

var select = 0;
class NewsPage {
    constructor() {
    }
    init() {
        $('#btn-create-new').on('click', this.showFormCreate);
        $('#btn-close-form').on('click', this.hideFormCreate);
        $('#news-form').on('submit', this.clickBtnSave);
        $('.page-link').on('click', this.clickPage);
        $('.valid').on('blur', this.valid);
    }
    valid() {
        if ($(this).val() == "") {
            $(this).addClass("border-red");
            $(this).attr('title', "Trường bắt buộc");
            $(this).addClass("lock");
            $("#btn-save").addClass("d-none");
        }
        else {
            $(this).removeClass("border-red");
            $(this).removeClass("lock");
            if ($(".lock").length == 0) $("#btn-save").removeClass("d-none");
        }
    }
    showFormCreate() {
        $('#form-create').removeClass("d-none");
        $('#background').removeClass("d-none");
        $("#Name").focus();
    }
    hideFormCreate() {
        $('#form-create').addClass("d-none");
        $('#background').addClass("d-none");
    }
    clickOnData() {
        select = this.id;
    }
    clickPage() {
        $.ajax({
            url: "ManagerNews/GetNewByPage?page=" + this.id,
            method: "GET",
            dataType: "json"
        }).done((response) => {
            $('#tb-data').empty();
            response.Item1.map((item) => {
                $('#tb-data').append(`
                    <tr class="data" id="`+ item.Id + `" value="` + item.Id + `">
                        <td class="text-center">`+ item.Id + `</td>
                        <td class="text-center">`+ item.Name + `</td>
                        <td class="text-center">`+ item.CategoryName + `</td>
                        <td class="center-block">
                            <a class="btn-edit" href="/Admin/ManagerNews/Edit?id=`+ item.Id + `" value="` + item.Id + `">Edit |</a>
                            <a class="btn-delete" href="/Admin/ManagerNews/Delete?id=`+ item.Id + `" value="` + item.Id + `">Delete |</a>
                            <a class="btn-view" href="/Admin/ManagerNews/View?id=`+ item.Id + `" value="` + item.Id + `">View</a>
                        </td>
                    </tr>`)
            })
            $(".page-link").removeClass("bg-primary text-white");
            $("#" + this.id).addClass("bg-primary text-white");
        }).fail(() => {
            console.log("error");
        })
    }
}