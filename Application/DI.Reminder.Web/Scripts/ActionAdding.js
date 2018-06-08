 $('#AddingButton').data('count', 0)   
    .click(function ()
    {
        var count = $(this).data('count'); 
        $(this).data('count', count + 1); 
        $("#textBox").append('<input type="text" style="input" name="Actions[' + count + '].Name"/> ');
    });
