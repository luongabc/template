$(document).ready(function () {

    getProfile($("#UserName").text());
});


function getProfile(User) {


    $.ajax({
        url: "/Admin/GetDataProfile?UserName=" + User,
        type: "Post",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.Avatar != null) {
                var html = '';
                html += '<a href=""><img src="../adminlte/dist/img/' + result.Avatar + '" class="img-circle elevation-2" alt="User Image"></a>';
                $("#profile").html(html);
                var html1 = '';
                html1 += '<img src="../adminlte/dist/img/' + result.Avatar + '">';
                $(".User-img").html(html1);
            }
            else {
                var html = '';
                html += '<a href=""><img src="../adminlte/dist/img/Avatar.jpg" class="img-circle elevation-2" alt="User Image"></a>';
                $("#profile").html(html);
                var html1 = '';
                html1 += '<img src="../adminlte/dist/img/Avatar.jpg">';
                $(".User-img").html(html1);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
   
}
function UpdateProfile() {


    var formData = new FormData();
    formData.append("UserName", $("#UserName").text());
  
    formData.append(" Avatar", $('#Avatar')[0].files[0]['name']);
    formData.append("file", $('#Avatar')[0].files[0]);

    $.ajax({
        url: "/Admin/UpdateProfile",
        data: formData,
        type: "POST",
        processData: false,
        contentType: false,
        success: function (result) {
            
            getProfile($("#UserName").text());
            $('#myModal').modal('hide');
           
            $('#Avatar').val("");
        

        },
        error: function (errormessage) {
            alert(errormessage.responseText);


        }
    });
}
function ChangePassword() {


    var formData = new FormData();
    formData.append("Password", $("#Password").val());

    formData.append("PasswordNew", $('#PasswordNew').val());
   

    $.ajax({
        url: "/Admin/ChangePassword",
        data: formData,
        type: "POST",
        processData: false,
        contentType: false,
        success: function (result) {

           
            $('#myModal1').modal('hide');

            $('#Password').val("");


        },
        error: function (errormessage) {
            alert(errormessage.responseText);


        }
    });
}
