$(document).ready(async function () {

    setSlider()

});



const setSlider = async() => {
    $(".dataRow").hide();
    $(".previewBtn").click(async function (event) {

        let aggregatedDatarowHtmlElement = $(this).parent().parent().parent().parent().next().find(".dataRow")
        if (aggregatedDatarowHtmlElement.children().length == 0) {
            $(this).text("Hide")
            let wheelieId = $(this).data("id");
            var response = await getRideDetails(wheelieId, aggregatedDatarowHtmlElement);
            $(this).parent().parent().parent().parent().next().find(".dataRow").slideToggle()
        }
        else {
            $(this).parent().parent().parent().parent().next().find(".dataRow").slideToggle()
            if ($(this).text() == "Show") {
                $(this).text("Hide")
            } else {
                $(this).text("Show")
            }
        }


    });
}
 

async function getRideDetails(wheelieId, aggregatedDatarowHtmlElement) {

    return await $.ajax({
        url: "/Wheelie/Details",
        type: "post",
        data: {
            "id": wheelieId,
        },
        success:async function (data) {
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
        aggregatedDatarowHtmlElement.append(
        `<ul class="list-group details">
            <li class="list-group-item">Addrees: <span class="detail-value">${data.addrees == 0 ? "---" : data.addrees}</span></li>
            <li class="list-group-item">Altitude: <span class="detail-value">${data.altitude == 0 ? "---" : data.altitude + " m"}</span></li>
            <li class="list-group-item">Max V: <span class="detail-value">${data.vMax == 0 ? "---" : data.vMax + " km/h"}</span></li>
            <li class="list-group-item">Rotate X: <span class="detail-value">${data.rotateX == 0 ? "---" : data.rotateX}</span></li>
        </ul>`)

}

