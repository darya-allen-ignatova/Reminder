$('#AddingButton').data('count', $('#AddingButton').attr("col"))   
    .click(function ()
    {
        var count = Number($(this).data('count'));
        $(this).data('count', count + 1); 
        $("#textBox").append('<input type="text" class="input" name="Prompt.Actions[' + count + '].Name"/> ');
    });
