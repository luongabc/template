$(document).ready(function () {
    var managerCate = new managerCategory();
    managerCate.init();
});
class managerCategory {
    constructor() {

    }
    init() {
        $(".page-link").on("click", this.clickPage);
    }
    clickPage() {
        $.ajax({
            url: "ManagerCategory/GetCategoriesByPage?page="+this.id,
            method: "GET",
            dataType: "json"
        }).done((response) => {
            $('#tb-data').empty();
            response.Item1.map((item) => {
                $('#tb-data').append(`<tr>
                        <td>`+ item.Id + `</td>
                        <td>` + item.Name +`</td>
                        <td>`+ item.Slug +`</td>
                        <td>`+ item.ParentId +`</td>
                        <td>`+ item.Active +`</td>
                        <td>`+ item.CreateDate +`</td>
                        <td>
                           <a href="/Admin/ManagerCategory/Edit?Id=`+ item.Id+`" >Edit  </a>|
                           <a href="/Admin/ManagerCategory/Delete?Id=`+ item.Id +`" >Datele</a>
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