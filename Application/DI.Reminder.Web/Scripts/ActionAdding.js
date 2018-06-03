$(document).ready(function () {
<<<<<<< HEAD
    $('#AddingButton').click(function () {
        $('#textBox').append('<input type="text" id="Actions_0__Name" name="Actions[0].Name" class="input" style="margin-top:5px" />');
     })
});
=======
    $('#AddingButton').data('counter', 0)   
    .click(function ()
    {
        var counter = $(this).data('counter'); 
        $(this).data('counter', counter + 1); 
        $("#textBox").append('<input type="text" style="input" name="Actions[' + counter + '].Name"/> ');
    });
});
>>>>>>> re-7
