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
            var html = '';
            html += '<a href="/Admin/Profile?UserName=admin@gmail"><img src="../adminlte/dist/img/'+result.Avatar+'" class="img-circle elevation-2" alt="User Image"></a>';
            $("#profile").html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
