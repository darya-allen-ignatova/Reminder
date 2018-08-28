$('#AddingButton').data('count', $('#AddingButton').attr("col"))   
    .click(function ()
    {
        var count = Number($(this).data('count'));
        $(this).data('count', count + 1); 
        $("#textBox").append('<h4> <input type="text" class="input form-control" name="Prompt.Actions[' + count + '].Name"/></h4> ');
    });
