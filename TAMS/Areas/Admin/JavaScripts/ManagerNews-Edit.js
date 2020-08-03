$(document).ready(() => {
    var js = new ManaNewsEdit();
    js.init();
})

class ManaNewsEdit {
    constructor() {
    }

    init() {
        $("#fileAvatar").on("change",this.showImgSelect);
    }
    showImgSelect() {
        if (this.files && this.files[0]) {
            var read = new FileReader();
            read.onload = function (e) {
                $("#img-select").attr('src', e.target.result);
            }
            read.readAsDataURL(this.files[0]);
        }
    }
}