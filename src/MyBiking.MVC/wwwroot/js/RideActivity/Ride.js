$(document).ready(async function () {

    setSlider()

});



const setSlider = async() => {
    $(".dataRow").hide();
    $(".previewBtn").click(async function (event) {

        let aggregatedDatarowHtmlElement = $(this).parent().next()
        console.log(aggregatedDatarowHtmlElement.children().length)
        if ($(this).next("span").hasClass("text-secondary") && aggregatedDatarowHtmlElement.children().length <= 1) {

            $(this).children("span").removeClass("text-secondary").addClass("text-danger")
            console.log(aggregatedDatarowHtmlElement)

            let rideId = $(this).data("id");


            var response = await getRideDetails(rideId, aggregatedDatarowHtmlElement);
            $(this).parent().parent().find(".dataRow").slideToggle()
        }
        else {
            $(this).parent().parent().find(".dataRow").slideToggle()
            console.log("xd2")
            $(this).children("span").removeClass("text-danger").addClass("text-secondary")
        }


    });
}
 

async function getRideDetails(rideId, aggregatedDatarowHtmlElement) {


    return await $.ajax({
        url: "/Ride/RideDetails",
        type: "post",
        data: {
            "rideId": rideId,
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
        $(`<ul class="list-group">
            <li class="list-group-item">Distance: ${data.distance} </li>
            <li class="list-group-item">Wheelie Max V: ${data.wheelieMaxV}</li>
            <li class="list-group-item">Wheelie Distance: ${data.totalWheelieDistance}</li>
            <li class="list-group-item">Wheelies: ${data.wheelies}</li>
        </ul>`).insertBefore(aggregatedDatarowHtmlElement.children("a"))
    //await sleep(5000);
    console.log(12432)

}

//function sleep(ms) {
//    console.log(1243)
//    return new Promise(resolve => setTimeout(resolve, ms));
//}

