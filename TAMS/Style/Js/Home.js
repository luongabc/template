$(document).ready(() => {
    $("#btn-Register").on("click", showRegister)
    $(".form-register").on("click", showRegister)
    $("#btn-Login").on("click", showLogIn)
    $(".form-login").on("click", showLogIn)
})
showLogIn = () => {
    if ($(".login").hasClass("d-none")) $(".login").removeClass("d-none");
    else $(".login").addClass("d-none");
}
showRegister = () => {
    if ($(".register").hasClass("d-none")) $(".register").removeClass("d-none");
    else $(".register").addClass("d-none");
}
