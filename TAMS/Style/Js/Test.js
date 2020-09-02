$(document).ready(() => {
    testTime();
    $("#btn-sendResult").on("click", testSendResult);
    $("#btn-finishTest").on("click", testFinishTest);
    init();
})
var listQuestion = [];
var listAnswer = [];
var search;
async function init() {
    this.search = this.getSearch()
    await this.loadQuestion($("#idTest").text());
    this.showTest();
    $(".itemSelect").on("change", this.checkChangeAnswer);
}
function checkChangeAnswer() {
    var self = this;
    function checkIsQuestion(item) {
        return item.Id == self.name.slice(2);
    }
    function checkIsAnswer(item) {
        return item.IdAnswer == self.id;
    }
    var qs = listQuestion.find(checkIsQuestion);
    var as = qs.answers.find(checkIsAnswer);
    if ($(this).hasClass("answerText")) {
        if (this.value == null || this.value.trim() == "") {
            $("#barQs-" + this.name.slice(2)).removeClass("btn-primary text-white");
            as.result = false;
        }
        else {
            $("#barQs-" + this.name.slice(2)).addClass("btn-primary text-white");
            as.result = true;
        }
    }
    else if ($(this).hasClass("answerCheck")) {

        if ($(this).prop("checked")) {
            as.result = true;
        }
        else {
            as.result = false;
        }
    }
    for (var i = 0; i < qs.answers.length; i++) {
        if (qs.answers[i].result == true) {
            $("#barQs-" + this.name.slice(2)).addClass("btn-primary  text-white");
            return;
        }
    }
    $("#barQs-" + this.name.slice(2)).removeClass("btn-primary  text-white");
}
function getSearch() {
    var t = [];
    $(location).attr('search').split("&&").map((item) => {
        t.push(item.split("=")[1]);
    })
    return t
}
async function loadQuestion(id) {
    var self = this;
    var result;
    try {
        result = await $.ajax({
            url: "/InfoUser/getQuestions?id=" + id
        }).done((res) => {
            var obj = JSON.parse(res);
            self.listQuestion = obj.Item1;
            self.listQuestion.map((qs) => {
                var check = false;
                var temp = [];
                for (var i = 0; i < obj.Item2.length; i++) {
                    if (qs.Id == obj.Item2[i].IdQuestion) {
                        temp.push(obj.Item2[i]);
                        if (obj.Item2[i].result === true) check = true;
                    }

                }
                qs.answers = temp;
                qs.check = check;
            })
        }).fail(() => {
            console.log("fail");
        })
    } catch (error) {

    }
}
function showTest() {
    function showNavQuestion() {
        for (var i = 0; i < this.listQuestion.length; i++) {
            if (this.listQuestion[i].check === true) {
                $("#list-Question").append(`
                    <a  class=" linkScroll border rounded-circle btn btn-primary text-white p-2 m-2 width-50px" id="barQs-` + this.listQuestion[i].Id + `" >` + (i + 1) + `</a>
                `)
            }
            else {
                $("#list-Question").append(`
                    <a  class="linkScroll border rounded-circle p-2 btn m-2 width-50px" id="barQs-` + this.listQuestion[i].Id + `" >` + (i + 1) + `</a>
                `)
            }
        }
        $(".linkScroll").on("click", scrollLink)
        function scrollLink() {
            $([document.documentElement, document.body]).animate({
                scrollTop: $("#Text-" + this.id.slice(6)).offset().top-80
            }, 1000);
        }
    }
    for (var i = 0; i < this.listQuestion.length; i++) {
        {
            $("#testContent").append(` <div class="pt-4" id="Text-` + this.listQuestion[i].Id +`">
                    <label class="text-primary" >Câu `+ (i + 1) + `: ` + this.listQuestion[i].Text + `</label><br />`);
            if (this.listQuestion[i].IdCategory == 5) {
                if (this.listQuestion[i].answers[0].TextAnswer == null) this.listQuestion[i].answers[0].TextAnswer = "";
                $("#testContent").append(`<input type="text" class="answerText w-100 p-1 m-2 itemSelect" id="` + this.listQuestion[i].answers[0].IdAnswer+`" name="Q_` + this.listQuestion[i].Id + `" value="` + this.listQuestion[i].answers[0].TextAnswer + `" />`)
            }
            else
                this.listQuestion[i].answers.map((item) => {
                    if (item.result == true)
                        $("#testContent").append(`
                                <input class="answerCheck m-2 itemSelect" type="checkbox" id="`+ item.IdAnswer + `" name="Q_` + this.listQuestion[i].Id + `" checked="checked" value="` + item.TextAnswer + `" />
                                <label>`+ item.TextAnswer + `</label><br />`);
                    else $("#testContent").append(`
                                <input class="answerCheck m-2 itemSelect" type="checkbox" id="`+ item.IdAnswer + `" name="Q_` + this.listQuestion[i].Id + `" value="` + item.TextAnswer + `" />
                                <label>`+ item.TextAnswer + `</label><br />`);
                })
            $("#testContent").append(` </div>`);
        }
    }
    showNavQuestion();
}
function testSendResult() {
    var res = [];
    $.each($(".answerCheck"), (key, value) => {
        res.push({
            "IdQuestion": value.name.slice(2),
            "IdAnswer": value.id,
            "TextAnswer": value.value,
            "result": $(value).is(":checked")
        });
    });
    $.each($(".answerText"), (key, value) => {
        var result = false;
        if (value.value != null && value.value.trim() != "") result = true;
        res.push({
            "IdAnswer": value.id,
            "IdQuestion": value.name.slice(2),
            "TextAnswer": value.value,
            "result": result
        });
    });
    var resultPost = JSON.stringify({ "userResults": res, "IdTest": $("#idTest").text() });
    $.ajax({
        method: 'POST',
        url: "/InfoUser/SaveResult",
        data: JSON.stringify({ "userResults": res, "IdTest": $("#idTest").text() }),
        contentType: 'application/json',
    }).done(() => {
        return true;
    }).fail(() => {
        console.log(resultPost);
    })
}
function testFinishTest() {
    testSendResult();
    window.location.href = "/InfoUser/FinishTest?IdTest=" + $("#idTest").text();
}
function testTime() {
    var hours = parseInt($("#h").text());
    var Mn = parseInt($("#Mn").text());
    var Ms = 59;
    testCount(hours, Mn, Ms);
}
function testCount(hours, minute, secon) {
    var x = setInterval(() => {
        if (secon == 0) {
            if (minute == 0) {
                if (hours == 0) {
                    //Nop bai
                    //
                    clearInterval(x);
                    return;
                }
                hours--;
                minute = 59;
                secon = 59;
            }
            else {
                minute--;
                secon = 59;
            }
        }
        else secon--;
        testShowCountDown(hours, minute, secon);
    }, 1000);
}
function testShowCountDown(hours, minute, secon) {
    if (hours < 10) {
        $("#hours2").text("0");
        $("#hours1").text(hours);
    }
    else {
        $("#hours2").text(hours.toString().charAt(0));
        $("#hours1").text(hours.toString().charAt(1));
    }
    if (minute < 10) {
        $("#minute2").text("0");
        $("#minute1").text(minute);
    }
    else {
        $("#minute2").text(minute.toString().charAt(0));
        $("#minute1").text(minute.toString().charAt(1));
    }
    if (secon < 10) {
        $("#secon2").text("0");
        $("#secon1").text(secon);
    }
    else {
        $("#secon2").text(secon.toString().charAt(0));
        $("#secon1").text(secon.toString().charAt(1));
    }
}
