$("#BillingForm").off("click", "#BillingFormSubmit").on("click", "#BillingFormSubmit", function (e) {
    var BookingId = $('#BookingId').val();
    var HandDT = $('#HandDT').val();
    var DistanceTraveled = $('#DistanceTraveled').val();
    if (!isNaN(Date.parse(HandDT)) && BookingId.length > 4 && !isNaN(DistanceTraveled)) {
        $.ajax({
            type: 'POST',
            url: '/Booking/GetBooking',
            data: { HandDT: HandDT, BookingId: BookingId, DistanceTraveled: DistanceTraveled },
            cache: false,
            success: function (result) {
                var marker = JSON.stringify(result);
                if (marker.indexOf('ErrInvalid') > 0) {
                    $('#Invalid').html('<span class="text-danger offset-sm-5 font-weight-bold" id="ErrInvalid">InValid or Billing Completed for given Booking Id </span>');
                    $("#ErrInvalid").delay(3000).fadeOut();
                    $('#PartialView').html('');
                }
                else {
                    $('#PartialView').html(result);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $('#Invalid').html('<span class="text-danger offset-sm-5 font-weight-bold" id="ErrInvalid">Fill the Details Correctly</span>');
                $("#ErrInvalid").delay(3000).fadeOut();
            }
        });
    }
    else {
        $('#Invalid').html('<span class="text-danger offset-sm-5 font-weight-bold" id="ErrInvalid">Fill the Details Correctly</span>');
        $("#ErrInvalid").delay(3000).fadeOut();
    }
});


