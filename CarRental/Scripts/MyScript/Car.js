$("#Searchdiv").off("click", "#Searchbtn").on("click", "#Searchbtn", function (e) {
    e.preventDefault();
    var myData = $('form').serialize();
    $.ajax({
        type: 'POST',
        url: '/Car/Search?' + myData,
        cache: false,
        success: function (result) {
            var marker = JSON.stringify(result);
            if (marker.indexOf('error1') > 0) {
                $('#par1').html(result);
                $('#par2').html('');
            }
            else {
                $('#par2').html(result);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $('#error1').text("Error encountered while Getting Cars.");
        }
    });

});


$("#error1").delay(3000).fadeOut();