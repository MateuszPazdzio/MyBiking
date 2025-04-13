$(document).ready(async function () {

    setSlider()


    $(".form-select").change(function () {
        var selectedOption = $(this).val();

        getRideActivities(selectedOption);
    })
});



const setSlider = async() => {
    $(".dataRow").hide();
    $(".monthWrapper").click(async function (event) {

        let aggregatedDatarowHtmlElement = $(this).parent().next()

        if ($(this).children("span").hasClass("text-secondary") && aggregatedDatarowHtmlElement.children().length <= 1) {

            $(this).children("span").removeClass("text-secondary").addClass("text-danger")
            let year = $('#Year').val();
            let month = $(this).prev().children('p').text()

            var response = await getRideDetails(month, aggregatedDatarowHtmlElement);
            $(this).parent().parent().find(".dataRow").slideToggle()
        }
        else {
            $(this).parent().parent().find(".dataRow").slideToggle()
            $(this).children("span").removeClass("text-danger").addClass("text-secondary")
        }


    });
}

const getRideActivities = async (selectedOption) => {
    return await $.ajax({
        url: "/Ride/Index",
        type: "post",
        data: {
            "Year": selectedOption,
        },
        success: async function (data) {
            if (!data) {
                aggregatedDatarowHtmlElement.html("No data found")
            } else {
                $(".ride-activities").empty();
                await fillRideActivity(data)
            }
        },
        error: function () {
            console.log("fail")
        }
    })
}

const fillRideActivity = async (rideActivities) => {


    for (const rideActivity of rideActivities) {

        let month = new Date(rideActivity).toLocaleString('en-US', { month: 'long' })
        let year = new Date(rideActivity).getFullYear()

        $('.ride-activities').append(`
                <div class="row monthRideDetails ">

                        <div class="row d-flex justify-content-between">
                            <div class="monthName col-1  d-flex justify-content-center align-content-center">
                                <p class="month col text text-black my-2">${month}</p>
                            </div>
                            <div class="monthWrapper col-1 d-flex justify-content-center align-content-center align-self-center">
                            <span class="dropdown-toggle month-toggle text-secondary"></span>
                            </div>
                        </div>
                        <div class="row dataRow dataRow">
                            <a href="/Ride/MonthlyRides/${month}?year=${year}">Watch monthly details</a>
                        </div>

                </div>

        `)
    }
    setSlider()

}  

async function getRideDetails(month, aggregatedDatarowHtmlElement) {


    return await $.ajax({
        url: "/Ride/Details",
        type: "post",
        data: {
            "Year": $("#Year").val(),
            "Month": month,
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
    $(`<ul class="list-group details">
            <li class="list-group-item">Rides: <span class="detail-value">${data.rides}</span></li>
            <li class="list-group-item">Wheelies: <span class="detail-value">${data.wheelies}</span></li>
            <li class="list-group-item">Distance: <span class="detail-value">${data.distance} km.</span></li>
            <li class="list-group-item">Wheelie Max V: <span class="detail-value">${data.wheelieMaxV ?? "--"} km/h</span></li>
            <li class="list-group-item">Wheelie Distance: <span class="detail-value">${data.totalWheelieDistance} m.</span></li>
        </ul>`).insertBefore(aggregatedDatarowHtmlElement.children("a"))

}

