$(document).ready(function () {
    loadData();
   
});

function loadData() {
    $("#Id").hide();

    $.ajax({
        url: "/Admin/Admin/GetDataCategory",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
              
                html += '<td><a href="#" onclick="return getbyID(' + item.Id + ')">Edit</a> | <a href="#" onclick="Delele(' + item.Id + ')">Delete</a></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {

    var res = validate();
    if (res == false) {
        return false;
    }
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
            loadData();
            $('#myModal').modal('hide');
            alert("you are done");
        },
        error: function (response) {
            alert("Failed");
        }
    });
}
function getbyID(Id) {

    $("#Id").prop("readonly", true);
    $('#Name').css('border-color', 'lightgrey');


    $.ajax({
        url: "/Admin/Admin/GetByIdCategory/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#Id').val(Id);
            $('#Name').val(result.Name);
            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var empObj = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        ModifyDate: new Date().toISOString()
    };
    $.ajax({
        url: "/Admin/Admin/UpdateCategory",
        data: empObj,
        type: "POST",

        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#Id').val("");
            $('#Name').val("");
            $('#ModifyDate').val("");
            $('#CreateDate').val("");
            $("#Id").hide();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        

        }
    });
}
function Delele(ID) {
    var ans = confirm("Bạn có chắc xóa không?");
    if (ans) {
        $.ajax({
            url: "/Admin/Admin/DeleteCategory/" + ID,
            type: "POST",

            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
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
function validate() {
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