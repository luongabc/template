
$(document).ready(function(){
    var newsPage = new NewsPage();
    newsPage.init();
});

var select = 0;
class NewsPage {
    constructor() {
    }
    init() {
        $('#form-create').hide();
        $('#background').hide();
        $('#btn-create-new').on('click', this.showFormCreate);
        $('#btn-close-form').on('click', this.hideFormCreate);
        $('#btn-save').on('click', this.clickBtnSave);
    }
    
    showFormCreate() {
        $('#form-create').show();
        $('#background').show();
    }
    hideFormCreate() {
        $('#form-create').hide();
        $('#background').hide();
    }
    clickOnData() {
        select = this.id;
    }
}