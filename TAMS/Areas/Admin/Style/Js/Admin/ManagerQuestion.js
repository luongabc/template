$(document).ready(function () {
    loaddataQuestion(1, size ,"", "", "");
    loadCategory();
    var PageStart = 1;
    $("#search").on("change", filter);
    $("#FilterCategoryQuestion").on("change", filter);
    $("#FilterCategoryAnswer").on("change", filter);
});

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
                html += '<td><a href="#" onclick=" CountAnswer(' + item.Id + ');">Edit</a> | <a href="#" onclick="Delele(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);

            loadPagination(result.Item2, Page)
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
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