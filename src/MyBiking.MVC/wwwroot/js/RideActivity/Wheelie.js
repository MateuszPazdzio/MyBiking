$(document).ready(async function () {

    setSlider()

});



const setSlider = async() => {
    $(".dataRow").hide();
    $(".previewBtn").click(async function (event) {

        let aggregatedDatarowHtmlElement = $(this).parent().parent().next().find(".dataRow")
        console.log(aggregatedDatarowHtmlElement.children())
        if (aggregatedDatarowHtmlElement.children().length <= 1) {

            let wheelieId = $(this).data("id");

            var response = await getRideDetails(wheelieId, aggregatedDatarowHtmlElement);
            $(this).parent().parent().next().find(".dataRow").slideToggle()
        }
        else {
            $(this).parent().parent().next().find(".dataRow").slideToggle()
            console.log("xd2")
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
        aggregatedDatarowHtmlElement.append(
        `<ul class="list-group">
            <li class="list-group-item">Addrees1: ${data.wheelieMaxV}</li>
            <li class="list-group-item">Addrees2: ${data.wheelieMaxV}</li>
            <li class="list-group-item">Max V: ${data.wheelies}</li>
            <li class="list-group-item">Rotate X: ${data.wheelies}</li>
        </ul>`)
    //await sleep(5000);
    console.log(12432)

}

//function sleep(ms) {
//    console.log(1243)
//    return new Promise(resolve => setTimeout(resolve, ms));
//}

