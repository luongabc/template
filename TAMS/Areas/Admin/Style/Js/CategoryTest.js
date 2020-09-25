
var listCategory = [];
CheckCategoriSelect = () => {
    $.ajax({
        url:"/Admin/Test/CountQuestionByCategoryQuestion?Category=" + $("#Create-categoryTest").val()
    }).done((res) => {
        $(".input-numQuestion").removeClass("d-none");
        $("#NumQuestion").attr("max",res);
    }).fail(() => {
        console.log("fail")
    })

}
function change() {
    removeQuestion();
    addQuestion();
}
function removeQuestion() {
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
function addQuestion() {
    var post = $(".btn-add").val();
    var cardsCheck = $(".checkbox-out:checked");
    if (cardsCheck.length > 0) {
        var arr = new Array();
        for (var i = 0; i < cardsCheck.length; i++) {
            arr.push(cardsCheck[i].value)
        }
        var resPost = JSON.stringify(arr);
        $.ajax({
            method: "POST",
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
                                <input type="checkbox" class="checkbox-in" value="`+ arr[i] + `" />
                            </td>
                            <td id="text-`+ arr[i] + `">
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
 function getCategoryQuestion() {
    var End = $(".cardCategory")[$(".cardCategory").length-1];
    var n;
    if (End == undefined && $(".cardCategory").length > 0) return;
    else if ($(".cardCategory").length == 0) n = 0;
    else {
        n = End.id.split("_")[1];
        n++;
    }
    if (n < 6) {
         $.ajax({
            url: "/Admin/Test/GetCategoryQuestion",
            dataType: "json",
        }).done((res) => {
            listCategory = [];
            listCategory = res;
            
        }).fail(() => {
        })
    }
}
function addnewCategory() {
    var End = $(".cardCategory")[$(".cardCategory").length - 1];
    var n;
    if (End == undefined && $(".cardCategory").length > 0) return;
    else if ($(".cardCategory").length == 0) n = 0;
    else {
        n = End.id.split("_")[1];
        n++;
    }
    $("#group_Category").append(`
                <div class="row cardCategory " id="cardCategory_`+ n + `">
                    <div class="form-group col-6" >
                        <select name="IdCategory" id="SelectCategory_`+ n + `" form="form-Create-Test" class="form-control col-10 Create-categoryTest"autocomplete="off">
                        </select>
                    </div>
                    <div class="form-group col-3 input-numQuestion">
                        <input id="NumQuestion_`+ n + `" type="number" name="NumQuestion"  class="form-control col-10 NumQuestion" autocomplete="off" min="0" max="" required="true" />
                    </div>
                    <div class="form-group col-2">
                        <input id="Score_`+ n + `" type="number" name="NumQuestion"  class="form-control col-10 Score" autocomplete="off" min="0" max="100" required="true" />
                    </div>
                    <a class="btn btn-danger mb-4 removeCategory" onclick="removeCategory" id="removeCategory_`+ n + `">x</a>
                </div>`)
}
function selectCategory() {
    listTemp = [...listCategory ];
    for (var i = 0; i < $(".Create-categoryTest").length; i++) {
        listTemp = listTemp.filter(obj => obj.Id != $(".Create-categoryTest")[i].value)
    }
    $(this).empty();
    for (var i = 0; i < listTemp.length; i++) {
        this.append(new Option(listTemp[i].Name, listTemp[i].Id))
    }
}
function removeCategory() {
    console.log( this)
    var n = this.id.split("_")[1];
    
    $("#cardCategory_" + n).remove();
    console.log($(".removeCategory"));

}
function sendCategoryAdd() {
    var n = $(".Create-categoryTest").length;
    if (n < 0) {
        alert("Choose category question and number question");
        return;
    }
    var search = location.search.split("=")[1];
    var categoryQuestionOfTests = [];
    for (var i = 0; i <n; i++) {
        if ( $(".Create-categoryTest")[i].value == null) {
            alert("Choose category question and number question");
            return;
        }
        categoryQuestionOfTests.push({
            IdCategoryQuestion: $(".Create-categoryTest")[i].value,
            Numquestion: $(".NumQuestion")[i].value,
            ScoreQuestion: $(".Score")[i].value,
            ScoreQuestion: $(".Score")[i].value,
            IdTest:search
        })
    }
    $.ajax({
        url: "/Admin/Test/ContentFormTest",
        method: "POST",
        dataType: "JSON",
        data: JSON.stringify(categoryQuestionOfTests),
        contentType: 'application/json',
    }).done((res) => {
        alert("added " + res + " category");
        window.location.href = "https://localhost:44355/Admin/Test/FormTest?search=&&IdForm=" + search+"&&Page=1";
    }).fail(() => {
        alert("check info test");
    })
    
}
function getCategoryQuestionOfTest() {
    var search = location.search.split("=")[1];
    $.ajax({
        url: "/Admin/Test/GetCategoryQuestionOfTest?IdFormTest=" + search,
        dataType:"json"
    }).done((res) => {
        for (var numCategory = 0; numCategory < res.length; numCategory++) {
            var nameCategory;
            for (var i = 0; i < listCategory.length; i++) {
                if (listCategory[i].Id == res[numCategory].IdCategoryQuestion)
                    nameCategory = listCategory[i].Name;
            }
            $("#group_Category").append(`
                <div class="row cardCategory " id="cardCategory_`+ numCategory + `">
                    <div class="form-group col-6" >
                        <select name="IdCategory" id="SelectCategory_`+ numCategory + `" form="form-Create-Test" class="form-control col-10 Create-categoryTest"autocomplete="off" >
                            <Option value="`+ res[numCategory].IdCategoryQuestion + `">` + nameCategory + `</Option>
                        </select>
                    </div>
                    <div class="form-group col-3 input-numQuestion">
                        <input id="NumQuestion_`+ numCategory + `" type="number" name="NumQuestion"  class="form-control col-10 NumQuestion" autocomplete="off" min="0" max="" required="true" value="` + res[numCategory].Numquestion + `" />
                    </div>
                    <div class="form-group col-2">
                        <input id="Score_`+ numCategory + `" type="number" name="NumQuestion"  class="form-control col-10 Score" autocomplete="off" min="0" max="100" required="true" value="` + res[numCategory].ScoreQuestion + `" />
                    </div>
                    <a class="btn btn-danger mb-4 removeCategory" onclick="removeCategory" id="removeCategory_`+ numCategory + `">x</a>
                </div>`)
        }
    }).fail(() => {
    })

}