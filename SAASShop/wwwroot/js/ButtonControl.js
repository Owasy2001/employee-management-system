$('form').submit(function () {
    if ($(this).valid()) {
        $(':submit', this).attr('disabled', 'disabled');
        $("#SaveMsg").show();
    }
});