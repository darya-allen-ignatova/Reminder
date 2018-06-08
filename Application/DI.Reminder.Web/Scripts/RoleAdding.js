$('#AddButton').data('counter', 0)
    .click(function () {
        var counter = $(this).data('counter');
        $(this).data('counter', counter + 1);
        $("#text").append('<input type="text" style="input" name="Roles[' + counter + '].Name"/> ');
    });