$(document).ready(async function () {

    setSlider()

});



const setSlider = async() => {
    $(".dataRow").hide();
    $(".previewBtn").click(async function (event) {

        let aggregatedDatarowHtmlElement = $(this).parent().parent().parent().next().find(".dataRow")

        if (aggregatedDatarowHtmlElement.children().length <= 1) {
            $(this).text("Hide")
            let rideId = $(this).data("id");

            var response = await getRideDetails(rideId, aggregatedDatarowHtmlElement);
            $(this).parent().parent().parent().next().find(".dataRow").slideToggle()
        }
        else {
            $(this).parent().parent().parent().next().find(".dataRow").slideToggle()
            if ($(this).text() == "Show") {
                $(this).text("Hide")
            } else {
                $(this).text("Show")
            }
        }


    });
}
 

async function getRideDetails(rideId, aggregatedDatarowHtmlElement) {


    return await $.ajax({
        url: "/Ride/RideDetails",
        type: "post",
        data: {
            "id": rideId,
        },
        success:async function (data) {
            //console.log(data)
            //console.log("succses")
            if (!data) {
                aggregatedDatarowHtmlElement.html("No data found")
            } else {
                await fillAggrData(data, aggregatedDatarowHtmlElement)
            }
        },
        error: function () {
            console.log("fail")
        }
    })
}

const fillAggrData = async (data, aggregatedDatarowHtmlElement) => {
        $(`<ul class="list-group details">
            <li class="list-group-item">Wheelie Max V: <span class="detail-value">${data.wheelieMaxV} km/h</span></li>
            <li class="list-group-item">Wheelie Distance: <span class="detail-value">${data.totalWheelieDistance} m.</span></li>
            <li class="list-group-item">Wheelies: <span class="detail-value">${data.wheelies}</span></li>
        </ul>`).insertBefore(aggregatedDatarowHtmlElement.children("a"))

}


