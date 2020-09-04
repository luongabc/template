
$(document).ready(() => {
    var act =new adminCategoryTest();
    act.init();
    $("#Create-categoryTest").on("change", CheckCategoriSelect);
});
CheckCategoriSelect = () => {
    $.ajax({
        url:"/Admin/Test/CountQuestionByCategoryTest?Id=" + $("#Create-categoryTest").val()
    }).done((res) => {
        $(".input-numQuestion").removeClass("d-none");
        $("#NumQuestion").attr("max",res);
    }).fail(() => {
        console.log("fail")
    })
}
class adminCategoryTest {
    constructor() {
    }
    init() {
        $(".btn-add").on("click", this.change.bind(this));
    }
    change() {
        this.removeQuestion();
        this.addQuestion();
    }
     removeQuestion() {
         $.ajax({
             url: "/Admin/CategoriesTest/GetLengthMax?post=" + $(".btn-add").val(),
             method: "GET",
         }).done((res) => {
             var cardsCheckIn = $(".checkbox-in:checked");
             if (res > $(".checkbox-in").length - cardsCheckIn.length) {
                 alert("Check number question of test");
                 return;
             }
             if (cardsCheckIn.length > 0) {
                 var arr = new Array();
                 for (var i = 0; i < cardsCheckIn.length; i++) {
                     arr.push(cardsCheckIn[i].value)
                 }
                 var resPost = JSON.stringify(arr);
                 $.ajax({
                     url: "/Admin/CategoriesTest/removeQuestionForTest?post=" + $(".btn-add").val(),
                     method: "POST",
                     data: resPost,
                     contentType: 'application/json',
                 }).done((res) => {
                     if (res == 1) {
                         for (var i = 0; i < arr.length; i++) {
                             var text = $("#text-" + arr[i]).text();
                             $("#" + arr[i]).remove();
                             $("#tb-out").append(`
                            <tr class="" id="`+ arr[i] + `">
                                <td width="30">
                                    <input type="checkbox" class="checkbox-out" value="`+ arr[i] + `" />
                                </td>
                                <td id="text-`+ arr[i] + `">
                                   `+ text + `
                                </td>
                            </tr>`);
                         }
                     }
                     else {
                         console.log(arr);
                     };
                 }).fail(() => {
                     console.log("remove");
                     console.log("fail remove")
                 }).always(() => {
                 })
             }
         })
    }
    addQuestion() {
        var post = $(".btn-add").val();
        var cardsCheck = $(".checkbox-out:checked");
        if (cardsCheck.length > 0) {
            var arr = new Array();
            for (var i = 0; i < cardsCheck.length; i++) {
                arr.push(cardsCheck[i].value)
            }
            var resPost = JSON.stringify(arr);
            $.ajax({
                method:"POST",
                url: "/Admin/CategoriesTest/addQuestionForTest?post=" + post,
                data: resPost,
                contentType: 'application/json',
            }).done((res) => {
                if (res == 1) {
                    for (var i = 0; i < arr.length; i++) {
                        var text = $("#text-" + arr[i]).text();
                        $("#" + arr[i]).remove();
                        $("#tb-in").append(`
                        <tr class="" id="`+ arr[i] + `">
                            <td width="30">
                                <input type="checkbox" class="checkbox-in" value="`+arr[i]+`" />
                            </td>
                            <td id="text-`+arr[i]+`">
                               `+ text + `
                            </td>
                        </tr>`);
                    }               
                }
                else {
                    console.log("add");
                    console.log(arr);
                };
            }).fail(() => {
                console.log("fail add");
            })
        }   
        
    }
    
}