$(document).ready(function () {

    $(".dataRow").hide();
    $(".monthWrapper").click(function (event) {
        $(this).parent().parent().find(".dataRow").slideToggle()

        if (!$(this).children("span").hasClass("text-danger")) {

            $(this).children("span").removeClass("text-danger").addClass("text-secondary")
            //call db for ride details
            let year = $('#Year').val;
            //wiersz tytu³owy
            let titleRowHtmlElement = event.target.parentElement.parentElement
            //wiersz z danymi zaagregowanymi
            let aggregatedDatarowHtmlElement = titleRowHtmlElement.nextSibling
            
            let month = titleRowHtmlElement.firstElementChild.textContent.trim()
            var response = getRideDetails(month, aggregatedDatarowHtmlElement);
        }
        else {

            $(this).children("span").addClass("text-danger")
        }


    });
});


function getRideDetails(month, aggregatedDatarowHtmlElement) {


    return $.ajax({
        url: "/RideController/Details",
        type: "post",
        data: {
            "Year": $("#Year").val(),
            "Month": month,
        },
        success: function (data) {
            console.log(data)
            console.log("succses")
            if (!data.results.length) {
                aggregatedDatarowHtmlElement.html("No repositories found")
            } else {
                fillCodeReposWrapper(data, codeReposWrapper)
            }
        },
        error: function () {
            console.log("fail")
        }
    })
}