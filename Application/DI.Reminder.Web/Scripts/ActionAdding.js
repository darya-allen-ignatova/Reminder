$(document).ready(function () {
    $('#AddingButton').data('counter', 0)   
    .click(function ()
    {
        var counter = $(this).data('counter'); 
        $(this).data('counter', counter + 1); 
        $("#textBox").append('<input type="text" style="input" name="Actions[' + counter + '].Name"/> ');
    });
});