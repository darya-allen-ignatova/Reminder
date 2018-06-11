$('#AddingButtonRole').data('counter', $('#AddingButtonRole').attr("col"))
    .click(function () {
        var counter = Number($(this).data('counter'));
        $(this).data('counter', counter + 1);
        $("#textBoxRole").append('<input type="text" style="input" name="Roles[' + counter + '].Name"/> ');
    });