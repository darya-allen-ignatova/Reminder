$(document).ready(function () {
    $('#search').click(function () {
        var promptval = String(document.getElementsByClassName('prompt')[0].value);
        var categoryval = String(document.getElementsByClassName('category')[0].value);
        var dateval = String(document.getElementsByClassName('date')[0].value);
        $.ajax({
            type: "GET",
            url: '/Prompt/Search/',
            data: {
                promptval: promptval, categoryval: categoryval, dateval: dateval
            },
            success: function (jsonData) {
                if (jsonData.isEmpty) {
                    $("#JSON").text("");
                    $("#JSON").append('<div >There are no prompts that satisfy the condition</div>');

                }
                else {
                    $("#JSON").text("");
                    $.each(jsonData, function (i) {
                        $("#JSON").append('<div >' + this.Name + '<a style="float:right; margin-right:700px" href="/Prompt/Details/' + this.ID + '">Details</a> ' + '</div>');
                    });
                }
            }
        });
    });
});