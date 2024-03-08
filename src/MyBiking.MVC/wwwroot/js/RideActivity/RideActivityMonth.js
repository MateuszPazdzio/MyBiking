$(document).ready(function () {

    $(".dataRow").hide();
    $(".monthWrapper").click(function () {
        $(this).parent().parent().find(".dataRow").slideToggle()

        if ($(this).children("span").hasClass("text-danger")) {

            $(this).children("span").removeClass("text-danger").addClass("text-secondary")
        }
        else {

            $(this).children("span").addClass("text-danger")
        }


    });
});