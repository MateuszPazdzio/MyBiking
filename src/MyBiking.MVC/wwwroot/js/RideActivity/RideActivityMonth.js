$(document).ready(async function () {

    $(".dataRow").hide();
    $(".monthWrapper").click(async function (event) {

        let aggregatedDatarowHtmlElement = $(this).parent().next()
        if ($(this).children("span").hasClass("text-secondary") && aggregatedDatarowHtmlElement.children().length==0) {
            
            $(this).children("span").removeClass("text-secondary").addClass("text-danger")
            let year = $('#Year').val();
            let month = $(this).prev().children('p').text()
            console.log(aggregatedDatarowHtmlElement)

            console.log(aggregatedDatarowHtmlElement)
            var response =await getRideDetails(month, aggregatedDatarowHtmlElement);
            $(this).parent().parent().find(".dataRow").slideToggle() 
        }
        else {
            $(this).parent().parent().find(".dataRow").slideToggle()
            console.log("xd2")
            $(this).children("span").removeClass("text-danger").addClass("text-secondary")
        }


    });
});


async function getRideDetails(month, aggregatedDatarowHtmlElement) {


    return await $.ajax({
        url: "/Ride/Details",
        type: "post",
        data: {
            "Year": $("#Year").val(),
            "Month": month,
        },
        success:async function (data) {
            //console.log(data)
            //console.log("succses")
            if (!data) {
                aggregatedDatarowHtmlElement.html("No data found")
            } else {
                await fillAggrData(data, aggregatedDatarowHtmlElement)
                console.log("w")
            }
        },
        error: function () {
            console.log("fail")
        }
    })
}

const fillAggrData = async (data, aggregatedDatarowHtmlElement) => {
    console.log("123")
    aggregatedDatarowHtmlElement.append(
        `<ul class="list-group">
            <li class="list-group-item">Distance: ${data.distance} </li>
            <li class="list-group-item">Rides: ${data.rides}</li>
            <li class="list-group-item">Wheelie Max V: ${data.wheelieMaxV}</li>
            <li class="list-group-item">Wheelie Distance: ${data.totalWheelieDistance}</li>
            <li class="list-group-item">Wheelies: ${data.wheelies}</li>
        </ul>`)
    //await sleep(5000);
    console.log(12432)

}

function sleep(ms) {
    console.log(1243)
    return new Promise(resolve => setTimeout(resolve, ms));
}