$(document).ready(async function () {

    setSlider()

});



const setSlider = async() => {
    $(".dataRow").hide();
    $(".previewBtn").click(async function (event) {

        let aggregatedDatarowHtmlElement = $(this).parent().parent().parent().next().find(".dataRow")
        console.log(aggregatedDatarowHtmlElement.children())
        console.log("y")

        if (aggregatedDatarowHtmlElement.children().length <= 1) {
            $(this).text("Hide")
            let rideId = $(this).data("id");

            var response = await getRideDetails(rideId, aggregatedDatarowHtmlElement);
            $(this).parent().parent().parent().next().find(".dataRow").slideToggle()
        }
        else {
            console.log("x")
            $(this).parent().parent().parent().next().find(".dataRow").slideToggle()
            if ($(this).text() == "Show") {
                $(this).text("Hide")
            } else {
                $(this).text("Show")
            }
            
            console.log("xd2")
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
                console.log("works")
            }
        },
        error: function () {
            console.log("fail")
        }
    })
}

const fillAggrData = async (data, aggregatedDatarowHtmlElement) => {
        $(`<ul class="list-group details">
            <li class="list-group-item">Wheelie Max V: <span class="detail-value">${data.wheelieMaxV}</span></li>
            <li class="list-group-item">Wheelie Distance: <span class="detail-value">${data.totalWheelieDistance}</span></li>
            <li class="list-group-item">Wheelies: <span class="detail-value">${data.wheelies}</span></li>
        </ul>`).insertBefore(aggregatedDatarowHtmlElement.children("a"))
    //await sleep(5000);
    console.log(12432)

}

//function sleep(ms) {
//    console.log(1243)
//    return new Promise(resolve => setTimeout(resolve, ms));
//}

