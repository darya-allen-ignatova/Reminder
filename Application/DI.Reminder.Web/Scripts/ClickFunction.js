$(document).ready(function () {
    $('[id*=b_]').click(function () {
        var thebuttonclicked = $(this).attr("name");
        $.ajax({
            url:'/Prompt/GetCategoryPrompts',
            data: { id: thebuttonclicked },
            success: function (jsonData) {
                if (jsonData.isRedirect) {
                    window.location.href = "/Prompt/Navigation/" + jsonData.ID;
                }
                else if (jsonData.isEmpty) {
                    $("#JSON").text("");
                    $("#JSON").append('<div> <h3>There are no any prompts in chosen category</h3></div>');
                }
                else {
                    $("#JSON").text("");
                    $.each(jsonData, function (i) {
                        $("#JSON").append('<div class="row"> <div class="col-md-6 col-lg-6 col-xs-6 col-sm-6" > <div style="padding-top:10px"> <h5>' + this.Name + '</h5></div > </div > <div class="col-md-4 col-lg-4 col-xs-4 col-sm-4" style="padding-top:10px"><h5><a href="/Prompt/Details/' + this.ID + '">Details</a></h5></div ></div >');

                    });
                }
            }
        });
    });
});