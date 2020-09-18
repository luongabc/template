
var max = 5;
function loadCategory() {
    $.ajax({
        url: "/Admin/Admin/GetDataCategory",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            html += "<option value>Chọn loại</option>";
            $.each(result, function (key, item) {
                html += "<option id='Catagory-" + item.Id + "'  value='" + item.Name + "'>" + item.Name + '</option>';
            });
            $("#CategoryQuestion option[value='']").prop('selected', 'selected');
            $('#CategoryQuestion').html(html);
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function loaddataQuestion( Page,  Size,  Search,  FilterQuestion,  FilterAnswer) {
        $.ajax({

            url: "/Admin/Admin/GetDataQuestion?Page=" + Page + "&&Size=" + Size + "&&Search=" + Search + "&&FilterQuestion=" + FilterQuestion + "&&FilterAnswer" + FilterAnswer,
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
                    html += '<td><a href="#" onclick=" CountAnswer(' + item.Id + ');">Edit</a> | <a href="#" onclick="Delele(' + item.Id + ')">Delete</a></td>';
                    html += '</tr>';
                });
                $('.tbody').html(html);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
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

function displayAnswer() {
    var st = to_slug($("#CategoryAnswer").val());
    if (st == "radio") {
        console.log("Radio");
        var html = "";
        for (var i = 1; i <= 2; i++) {
            html += '<div class="form-group">';
            html += '<div id="BlockAnswer-'+i+'"';
            html += '<label for="formGroupExampleInput2">Câu trả lời số ' + i +':</label>';
            html += '<div class="row">';
            html += '<div class="col-md-11">';
            html += '<input type="text" class="form-control answerText" id="Answer-'+i+'" placeholder="Câu trả lời số '+i+'"></div>';
            html += '<div class="col-md-1">';
            html += '<label class="SelectResult">';
            html += '<input type="radio" id="ResultAnswer-'+ i +'" name="radio">';
            html += '<span class="checkmark"></span>';
            html += '</label>';

            html +='<button type="button" onclick="deleteDisplayAnswer();" class="close">×</button></div></div></div></div>';
          
        }
        html += ' <div id="more"></div>';
        html += '<button type="button" class="btn btn-primary" onclick="addDisplayAnswer(); ">Thêm câu trả lời</button><br>';
        $('#answer').html(html);
        $("#ResultAnswer-1").prop('checked', true);
    }
    if (st == "text") {
        var html = "";
        
            html += '<div class="form-group">';
        html += '<label for="formGroupExampleInput2">Nhập câu trả lời:</label>';
           
        html += '<input type="text" class="form-control answerText" id="Answer-1" placeholder="Nhập câu trả lời"></div>';
           
           
        $('#answer').html(html);
    }
    if (st == "checkbox") {
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
        $("#ResultAnswer-1").prop('checked', true);
        $("#ResultAnswer-2").prop('checked', true);
    }
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
function deleteDisplayAnswer() {
    if ($(".answerText").length > 2) {
        $('#BlockAnswer-' + $(".answerText").length + '').remove();
    }
    else {
        alert("Cần ít nhất 2 câu trả lời?")
    }
}
function addDisplayAnswer() {
    if ($(".answerText").length != max) {
        if (to_slug($("#CategoryAnswer").val()) == "radio") {
            var html = "";
            html += '<div class="form-group">';
            html += '<div id="BlockAnswer-' + ($(".answerText").length + 1) + '"';
            html += '<label for="formGroupExampleInput2">Câu trả lời số ' + ($(".answerText").length + 1) + ':</label>';
            html += '<div class="row">';
            html += '<div class="col-md-11">';
            html += '<input type="text" class="form-control answerText" id="Answer-' + ($(".answerText").length + 1) + '" placeholder="Câu trả lời số ' + ($(".answerText").length + 1) + '"></div>';
            html += '<div class="col-md-1">';
            html += '<label class="SelectResult">';
            html += '<input type="radio" id="ResultAnswer-' + ($(".answerText").length + 1) + '" name="radio">';
            html += '<span class="checkmark"></span>';
            html += '</label>';
            html += '<button type="button" onclick="deleteDisplayAnswer();" class="close" data-dismiss="modal">×</button></div></div></div></div>';
            $('#more').append(html);
            total++;
        }
        if (to_slug($("#CategoryAnswer").val()) == "checkbox") {
            var html = "";
            html += '<div class="form-group">';
            html += '<div id="BlockAnswer-' + ($(".answerText").length + 1) + '"';
            html += '<label for="formGroupExampleInput2">Câu trả lời số ' + ($(".answerText").length + 1) + ':</label>';
            html += '<div class="row">';
            html += '<div class="col-md-11">';
            html += '<input type="text" class="form-control answerText" id="Answer-' + ($(".answerText").length + 1) + '" placeholder="Câu trả lời số ' + ($(".answerText").length + 1) + '"></div>';
            html += '<div class="col-md-1">';
            html += '<label class="SelectResult1">';
            html += '<input type="checkbox" id="ResultAnswer-' + ($(".answerText").length + 1) + '" name="radio">';
            html += '<span class="checkmark1"></span>';
            html += '</label>';
            html += '<button type="button" onclick="deleteDisplayAnswer();" class="close" data-dismiss="modal">×</button></div></div></div></div>';
            $('#more').append(html);
            total++;
        }
    }
    
}
function Add() {
    var res = validate1();
    if (res == false) {
        return false;
    }
    else {
        var empObj = {
            Name: $('#Name').val(),
            CreateDate: new Date().toISOString(),
            ModifyDate: new Date().toISOString()
        };
        $.ajax({
            url: "/Admin/Admin/AddCategoryQuestion",
            data: empObj,
            type: "POST",

            success: function (result) {

                $('#myModal').modal('hide');
                alert("you are done");
                loadCategory();
            },
            error: function (response) {
                alert("Failed");
            }
        });
    }
}
function clearTextBox() {
    $('#Name').css('border-color', 'lightgrey');


    $('#Id').val("");
    $('#Name').val("");
    $('#ModifyDate').val("");

    $('#CreateDate').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();

}

function Delele(ID) {
    var ans = confirm("Bạn có chắc xóa không?");
    if (ans) {
        $.ajax({
            url: "/Admin/Admin/DeleteQuestion/" + ID,
            type: "POST",

            success: function (result) {
                $("tbody").replaceAll();
                loadCategory();
                loaddataQuestion(1);
                loadPagination();
            },
            error: function (errormessage) {
                alert("Check your tests");
            }
        });
    }
}
function DeleleAnswer(ID) {
    
        $.ajax({
            url: "/Admin/Admin/DeleteAnswer?IdQuestion=" + ID,
            type: "POST",

            success: function (result) {
                
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    
}
function AddQuestion() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var category = $('#CategoryAnswer').val();
    var Qs = {
        Text: $('#Text').val().toString(),
        CategoryAnswer: category,
        CategoryName : $('#CategoryQuestion').val()
    };
    var obj;
    var Answer = [];
    if (validateAnswer() == false) {
        return false;
    }
    for (var i = 1; i <= $('.answerText').length; i++) {
        var result = $("#ResultAnswer-" + i + "").prop("checked");
        var TextAnswer = $("#Answer-" + i + "").val().toString();
        Answer.push({ "result": result, "TextAnswer": TextAnswer });
    }   
    obj = { 'Answer': Answer, 'Qs': Qs };
    $.ajax({
        contentType: 'application/json',
        url: "/Admin/Admin/AddQuestion",
        data: JSON.stringify(obj),
        type: "POST",
        dataType: "json",
        success: function (result) {
            alert("you are done");
        },
        error: function (response) {
            alert("Failed");
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
            Mark: $('#Mark').val(),
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
                DeleleAnswer($('#Id').val())
                UpdateAnswer();
            },
            error: function (response) {
                alert("Failed");
            }
        });
    }

}

function UpdateAnswer() {
    var res = validate2();
    if (res == false) {
        return false;
    }
    else {
        var Answer = [];

        for (var i = 1; i <= total; i++) {
            var IdQuestion = $('#Id').val();
            var result = $("#ResultAnswer-" + i + "").prop("checked");
            var TextAnswer = $("#Answer-" + i + "").val().toString();
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
                loaddataQuestion();

            }
            ,
            error: function (response) {



            }
        });

    }
}


function page(a) {
    currentPage = a;
    loadPagination();
}
function loadPagination() {
    $.ajax({
        url: "/Admin/Admin/CountQuestion",
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            html += '<nav aria-label="Page navigation example" ><ul class="pagination">';

            if (currentPage > 1) {
                html += ' <li class="page-item"><a onclick="page(1); loaddataQuestion(1);" class="page-link" href = "#" aria - label="Previous"><span aria-hidden="true">&laquo;</span><span class="sr-only">first</span></a></li>';
                html += ' <li class="page-item"><a onclick="page(' + (currentPage - 1) + '); loaddataQuestion(' + (currentPage - 1) +');" class="page-link" href = "#" aria - label="Previous"><span aria-hidden="true">‹</span><span class="sr-only">Previous</span></a></li>';
            }

            if (Math.floor(result / 10) < 5) {
                for (i = 1; i < (result / 10) + 1; i++) {
                    html += '<li class="page-item"><a class="page-link" href= "#" id="page-' + i + '" onclick="page(' + i + ');loaddataQuestion(' + i + ');">' + i + '</a></li>';
                }
            }
            if ((Math.floor(result / 10) > 5 && currentPage < 4)) {
                for (i = 1; i < 6; i++) {
                    html += '<li class="page-item"><a class="page-link" href= "#" id="page-' + i + '" onclick="page(' + i + ');loaddataQuestion(' + i + ');">' + i + '</a></li>';
                }

            }

            if ((Math.floor(result / 10) >= 6) && currentPage >= 4 && currentPage < Math.floor(result / 10)) {
                for (i = currentPage - 2; i < currentPage + 3; i++) {
                    html += '<li class="page-item"><a class="page-link" href= "#" id="page-' + i + '" onclick="page(' + i + ');loaddataQuestion(' + i + ');">' + i + '</a></li>';
                }
            }
            if ((Math.floor(result / 10) >= 6) && currentPage == Math.floor(result / 10)) {
                for (i = currentPage - 3; i < currentPage + 2; i++) {
                    html += '<li class="page-item"><a class="page-link" href= "#" id="page-' + i + '" onclick="page(' + i + ');loaddataQuestion(' + i + ');">' + i + '</a></li>';
                }
            }
            if ((Math.floor(result / 10) >= 6) && currentPage == Math.floor(result / 10) + 1) {
                for (i = currentPage - 4; i < currentPage + 1; i++) {
                    html += '<li class="page-item"><a class="page-link" href= "#" id="page-' + i + '" onclick="page(' + i + ');loaddataQuestion(' + i + ');">' + i + '</a></li>';
                }
            }
            var last = Math.floor(result / 10) + 1;
            var next = currentPage - 1 + 2;
            if (currentPage > 0 && currentPage < last) {
                html += '<li class="page-item"><a onclick="page(' + next + ');loaddataQuestion(' + next + ');" class="page-link" href= "#" aria - label="Next" ><span aria-hidden="true">›</span><span class="sr-only">Next</span></a></li>';
                html += '<li class="page-item"><a onclick="page(' + last + ');loaddataQuestion(' + last + ');" class="page-link" href= "#" aria - label="Next" ><span aria-hidden="true">&raquo;</span><span class="sr-only">Last</span></a></li>';
            }

            html += ' </ul></nav>';

            $('.next').html(html);
            $('#page-' + currentPage + '').addClass("bg-primary text-white");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

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
    if ($('#CategoryQuestion').val().trim() == "") {
        $('#CategoryQuestion').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#CategoryQuestion').css('border-color', 'lightgrey');
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
function UserManagerPage() {
    var pageIndex = this.id;
    var url = new URL(location.href);
    var search = url.searchParams.get("search");
    if (search == null) search = "";
    search = search.trim();
    $.ajax({
        type:"get",
        url: "/Admin/UserManager/GetUserByPage?search=" + search + "&&page=" + pageIndex,
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
    }).done((res) => {
        $("#tbody").empty();
        res.Item1.map((item) => {
            var listUsetHtml  = `<tr>
                                    <td class="text-center"><img src="/Content/Image/`+item.Avatar+`" height="50"></td>
                                    <td class="text-center">`+item.Name+`</td>
                                    <td class="text-center">`+item.Email+`</td>
                                    <td>`+item.Birthday+`</td>
                                    <td>
                                        <a class="btn-edit" href="/Admin/UserManager/Edit?id=`+item.Id+`" value="`+item.Id+`"><i class="fas fa-edit"></i>Edit</a>
                                        |
                                        <a class="btn-delete" href="/Admin/UserManager/Delete?id=`+item.Id+`" value="`+item.Id+`">
                                            <i class="fas fa-trash-alt"></i>
                                            Delete
                                        </a>
                                    </td>
                                </tr>`
            $("#tbody").append(listUsetHtml);
        })
    }).fail((res) => {
    })
}