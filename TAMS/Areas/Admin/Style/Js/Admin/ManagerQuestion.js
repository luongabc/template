$(document).ready(function () {
    loaddataQuestion(1, size ,"", "", "");
    loadCategory();
    var PageStart = 1;
    $("#search").on("change", filter);
    $("#FilterCategoryQuestion").on("change", filter);
    $("#FilterCategoryAnswer").on("change", filter);
    $("#CategoryAnswer").on("change", displayAnswer1);
});
$("#Id").hide(); var total = 4; var currentPage = 1;
var size = 10;
var listCategoryQuestion = [];
function filter() {
    $('.tbody').empty();
    loaddataQuestion(1, size, $("#search").val(), $("#FilterCategoryQuestion").val(), $("#FilterCategoryAnswer").val());
}
function reloaddataQuestion() {
    $('.tbody').empty();
    var Page = this.id.slice(5);
    loaddataQuestion(Page, size, $("#search").val(), $("#FilterCategoryQuestion").val(), $("#FilterCategoryAnswer").val());
}
function Delele(ID) {
        $.ajax({
            url: "/Admin/Admin/DeleteQuestion/" + ID,
            type: "POST",

            success: function (result) {
                $("tbody").replaceAll();
                loadCategory();
                loaddataQuestion(1, size, "", "", "");
            },
            error: function (errormessage) {
                console.log("Delele");
                alert("Check your tests");
            }
        });
}
function loaddataQuestion(Page, Size, Search, FilterQuestion, FilterAnswer) {
    $.ajax({
        url: "/Admin/Admin/GetDataQuestion?Page=" + Page + "&&Size=" + Size + "&&Search=" + Search + "&&FilterQuestion=" + FilterQuestion + "&&FilterAnswer=" + FilterAnswer,
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result.Item1, function (key, item) {
                html += '<tr id=Question-' + item.Id + '>';
                html += '<td>' + item.Text + '</td>';
                html += '<td>' + item.CategoryName + '</td>';
                html += '<td>' + item.CategoryAnswer + '</td>';
                html += '<td><a href="" onclick=" CountAnswer(' + item.Id + ');">Edit</a> | <a href="#" class="state_hover" onclick="DeleteQuestionAndAnswer(' + item.Id + ',' + item.Id + ');">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
            
            loadPagination(result.Item2, Page)
        },
        error: function (errormessage) {
            alert("loaddataQuestion");
        }
    });
}
function DeleteQuestionAndAnswer(IdQs,IdAs) {
    var ans = confirm("Bạn có chắc xóa không?");
    if (ans) {
        Delele(IdQs);
    }
}

function loadCategory() {
    $.ajax({
        url: "/Admin/Admin/GetDataCategory",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += `<option name="FilterCategoryQUestion" value="` + item.Name + `">` + item.Name + `</option>`;
            });
            $('#FilterCategoryQuestion').append(html);
            html = "<option value>Chọn loại</option>";
            $.each(result, function (key, item) {
                html += "<option id='Catagory-" + item.Id + "'  value='" + item.Name + "'>" + item.Name + '</option>';
            });
            $("#CategoryId option[value='']").prop('selected', 'selected');
            $('#CategoryId').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function loadPagination(result, currentPage) {
    var numPage = result / size;
    if (result % size > 0) numPage++;
    var html = '';
    var Previous = currentPage - 1;
    html += '<nav aria-label="Page navigation example" ><ul class="pagination">';
    if (currentPage > 1) {
        html += ' <li class="page-item"><a  class="page-link" href = "#" aria - label="Previous" id="page-1"><span aria-hidden="true">&laquo;</span><span class="sr-only">first</span></a></li>';
        html += ' <li class="page-item"><a class="page-link" href = "#" aria - label="Previous" id="page-' + Previous + '"><span aria-hidden="true">‹</span><span class="sr-only">Previous</span></a></li>';
    }
    for (var i = currentPage-5; i <= currentPage+5; i++) {
        if (i > 0 && i <= numPage) html += '<li class="page-item"><a class="page-link" href= "#" id="page-' + i + '" value="' + i + '">' + i + '</a></li>';
    }

    var last = Math.floor(result / 10) + 1;
    var Next = currentPage +1;
    if (currentPage < last) {
        html += '<li class="page-item"><a  class="page-link" href= "#" aria - label="Next" id="page-' + Next + '" ><span aria-hidden="true" value="' + Next + '">›</span><span class="sr-only">Next</span></a></li>';
        html += '<li class="page-item"><a  class="page-link" href= "#" aria - label="Next" id="page-' + numPage+'" ><span aria-hidden="true"value="' + numPage  + '">&raquo;</span><span class="sr-only">Last</span></a></li>';
    }

    html += ' </ul></nav>';
    $('.next').html(html);
    $('#page-' + currentPage + '').addClass("bg-primary text-white");   
    $(".page-link").on("click", reloaddataQuestion);
}
function CountAnswer(Id) {
    $.ajax({
        url: "/Admin/Admin/CountAnswer?IdQuestion=" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            total = result;
            GetByIdQuestion(Id);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function GetByIdQuestion(Id) {
    $.ajax({
        url: "/Admin/Admin/GetByIdQuestion/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(Id);
            $('#Text').val(result.Text);
            $('#CategoryId').val(result.CategoryName);
            $('#CategoryAnswer').val(result.CategoryAnswer);
            $('#myModal').modal('show');
            displayAnswer1(total);
            GetByIdAnswer(Id);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function UpdateQuestion() {
    var res = validate();
    if (res == false) {
        return false;
    }
    else {
        var empObj = {
            Id: $('#Id').val(),
            Text: $('#Text').val().toString(),
            CategoryAnswer: $('#CategoryAnswer').val(),
            CategoryName: $('#CategoryQuestion').val(),

            ModifyDate: new Date().toISOString()
        };
        $.ajax({
            contentType: 'application/json',
            url: "/Admin/Admin/UpdateQuestion",
            data: JSON.stringify(empObj),
            type: "POST",
            dataType: "json",
            success: function (result) {
                alert("you are done");
                DeleleAnswer($('#Id').val());
                UpdateAnswer();
            },
            error: function (response) {
                alert("Failed");
            }
        });
    }

}
function UpdateAnswer() {
    var res = validateAnswer();
    if (res == false) {
        return false;
    }
    else {
        var Answer = [];
        for (var i = 1; i <= $(".answerText").length; i++) {
            var IdQuestion = $('#Id').val();
            var result = $("#ResultAnswer-" + i + "").prop("checked");
            var TextAnswer = $("#Answer-" + i + "").val();
            Answer.push({ "IdQuestion": IdQuestion, "result": result, "TextAnswer": TextAnswer });
        }
        $.ajax({
            contentType: 'application/json',
            url: "/Admin/Admin/UpdateAnswer",
            data: JSON.stringify(Answer),
            method: "POST",
            dataType: "json",
            success: function (result) {
                $('#myModal').modal('hide');
                loaddataQuestion(1, size, "", "", "");
            }
            ,
            error: function (response) {
            }
        });
    }
}
function to_slug(str) {
    // Chuyển hết sang chữ thường
    str = str.toLowerCase();

    // xóa dấu
    str = str.replace(/(à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)/g, 'a');
    str = str.replace(/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/g, 'e');
    str = str.replace(/(ì|í|ị|ỉ|ĩ)/g, 'i');
    str = str.replace(/(ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)/g, 'o');
    str = str.replace(/(ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)/g, 'u');
    str = str.replace(/(ỳ|ý|ỵ|ỷ|ỹ)/g, 'y');
    str = str.replace(/(đ)/g, 'd');

    // Xóa ký tự đặc biệt
    str = str.replace(/([^0-9a-z-\s])/g, '');

    // Xóa khoảng trắng thay bằng ký tự -
    str = str.replace(/(\s+)/g, '-');

    // xóa phần dự - ở đầu
    str = str.replace(/^-+/g, '');

    // xóa phần dư - ở cuối
    str = str.replace(/-+$/g, '');

    // return
    return str;
}
function GetByIdAnswer(Id) {
    $.ajax({
        url: "/Admin/Admin/GetByIdAnswer?IdQuestion=" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (res) {
            if (res.length > 1) {
                $.each(res, function (key, item) {
                    $("#Answer-" + (key + 1) + "").val(item.TextAnswer);
                    if (item.result == true) {
                        $("#ResultAnswer-" + (key + 1) + "").prop("checked", "true");
                    }
                });
            }
            else if (res.length == 1) {
                $("#Answer-1").val(res[0].TextAnswer);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}         
function displayAnswer1() {
    if (to_slug($("#CategoryAnswer").val()) == "radio") {
        var html = "";
        for (var i = 1; i <= 2; i++) {
            html += '<div class="form-group">';
            html += '<div id="BlockAnswer-' + i + '"';
            html += '<label for="formGroupExampleInput2">Câu trả lời số ' + i + ':</label>';
            html += '<div class="row">';
            html += '<div class="col-md-11">';
            html += '<input type="text" class="form-control answerText" id="Answer-' + i + '" placeholder="Câu trả lời số ' + i + '"></div>';
            html += '<div class="col-md-1">';
            html += '<label class="SelectResult">';
            html += '<input type="radio" id="ResultAnswer-' + i + '" name="radio">';
            html += '<span class="checkmark"></span>';
            html += '</label>';
            html += '<button type="button" onclick="deleteDisplayAnswer();" class="close">×</button></div></div></div></div>';
        }
        html += ' <div id="more"></div>';
        html += '<button type="button" class="btn btn-primary" onclick="addDisplayAnswer(); ">Thêm câu trả lời</button><br>';
        $('#answer').html(html);
    }
    if (to_slug($("#CategoryAnswer").val()) == "text") {
        var html = "";
        html += '<div class="form-group">';
        html += '<label for="formGroupExampleInput2">Nhập câu trả lời:</label>';
        html += '<input type="text" class="form-control answerText" id="Answer-1" placeholder="Nhập câu trả lời"></div>';
        $('#answer').html(html);
    }
    if (to_slug($("#CategoryAnswer").val()) == "checkbox") {
        var html = "";
        for (var i = 1; i <= 2; i++) {
            html += '<div class="form-group">';
            html += '<div id="BlockAnswer-' + i + '"';
            html += '<label for="formGroupExampleInput2">Câu trả lời số ' + i + ':</label>';
            html += '<div class="row">';
            html += '<div class="col-md-11">';
            html += '<input type="text" class="form-control answerText" id="Answer-' + i + '" placeholder="Câu trả lời số ' + i + '"></div>';
            html += '<div class="col-md-1">';
            html += '<label class="SelectResult1">';
            html += '<input type="checkbox" id="ResultAnswer-' + i + '" name="radio">';
            html += '<span class="checkmark1"></span>';
            html += '</label>';
            html += '<button type="button" onclick="deleteDisplayAnswer();" class="close" data-dismiss="modal">×</button></div></div></div></div>';
        }
        html += ' <div id="more"></div>';
        html += '<button type="button" class="btn btn-primary" onclick="addDisplayAnswer(); ">Thêm câu trả lời</button>';
        $('#answer').html(html);

    }
}
function validate1() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    return isValid;
}
function validate() {
    var isValid = true;

    if ($('#Text').val().trim() == "") {
        $('#Text').css('border-color', 'Red');
        isValid = false;
        var test = 2;
    }
    else {
        $('#Text').css('border-color', 'lightgrey');
    }
    if ($('#CategoryId').val().trim() == "") {
        $('#CategoryId').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CategoryId').css('border-color', 'lightgrey');
    }
    return isValid;
}
function validateAnswer() {
    var isValid = true;
    for (var i = 1; i <= $('.answerText').length; i++) {
        var val = $('#Answer-' + i).val();
        if (val == null
            || val.trim() == "") {
            $('#Answer-' + i + '').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#Answer-' + i + '').css('border-color', 'lightgrey');
        }
    }
    return isValid;
}
function DeleleAnswer(ID) {
    $.ajax({
        url: "/Admin/Admin/DeleteAnswer?IdQuestion=" + ID,
        type: "POST",
        success: function (result) {
        },
        error: function (errormessage) {
        }
    });

}