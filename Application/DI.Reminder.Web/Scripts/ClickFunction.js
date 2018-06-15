$(document).ready(function () {
    $('[id*=b_]').click(function () {
        var thebuttonclicked = $(this).attr("name");
        $.ajax({
            url:'/Prompt/GetCategoryPrompts',
            data: { id: thebuttonclicked },
            success: function (jsonData) {
                if (jsonData.isRedirect) {
                    window.location.href = "/Prompt/ShowCategoryList/" + jsonData.ID;
                }
                else if (jsonData.isEmpty) {
                    $("#JSON").text("");
                    $("#JSON").append('<div> There are no any prompts in chosen category</div>');
                    $("#JSON").append('<a style="padding-top:20px" href="/Prompt/Add"> Add new </a>');
                }
                else {
                    $("#JSON").text("");
                    $.each(jsonData, function (i) {
                        $("#JSON").append('<div >' + this.Name + '<a style="float:right; margin-right:600px" href="/Prompt/Details/' + this.ID + '">Details</a> ' + '</div>');

                    });
                }
            }
        });
    });
});