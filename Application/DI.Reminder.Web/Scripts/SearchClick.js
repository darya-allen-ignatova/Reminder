$(document).ready(function () {
    $('#search').click(function () {
        var counter = $('input[name=search]:checked').val();
        var val = document.getElementsByClassName('search-input')[0].value;
        $.getJSON(
            '/Prompt/GetItemsForSearch',
            { id: counter, value:val },
            function (jsonData) {
                if (jsonData.isRedirect) {
                    $("#JSON").text("");
                    $("#JSON").append('<div class="boton1" style="padding-top:20px">There are no any items like you want to find. Sorry<div>');
                }
                else
                {
                    $("#JSON").text("");
                    $.each(jsonData, function (i) {
                        $("#JSON").append('<div >' + this.Name + '<a style="float:right; margin-right:600px" href="/Prompt/Details/' + this.ID + '">Details</a> ' + '</div>');
                    });
                }
            });
        });
});