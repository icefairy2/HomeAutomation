$(function() {

    // temperatures -------------------------------
    function getAllTemps(url){
        $.ajax({
            dataType: "json",
            url: url,
            success: function (response) {
                let length = response.length - 1;
                setInnerTemp(response[length].IndoorTemperature + "° C");
                setOuterTemp(response[length].OutdoorTemperature + "° C");

                for (i = response.length - 1; i > 0; i--)
                    appendoToTable(response[i].OutdoorTemperature, response[i].IndoorTemperature, response[i].DateRecorded)
            }
        });
    }
	
	function toFarenheit($temp){
		return 1.8 * $temp + 32;
    }

    function setInnerTemp(celsius) {
        $('#innerTemp').text(celsius);
    }

    function setOuterTemp(celsius) {
        $('#outerTemp').text(celsius);
    }

    function appendoToTable(OutdoorTemperature, IndoorTemperature, date) {
        OutdoorTemperature = $('<td></td>').text(OutdoorTemperature + "° C");
        IndoorTemperature = $('<td></td>').text(IndoorTemperature + "° C");
        date = $('<td></td>').text(date);
        let row = $('<tr></tr>').append(OutdoorTemperature, IndoorTemperature,  date);

        $('#temperatures').append(row);
    }

    url = "http://localhost:50393/api/InOutTemperatures/";
    getAllTemps(url);

    // lights -------------------------------------------------------------------

    function turnTheLight(url) {
        $.ajax({
            method: "GET",
            url: url
        });
    }

    $('#lights .switch input:checkbox').change(function () {
        if (this.checked) {
            //this.prop('checked', false);
            turnTheLight("TurnLamp?turn=ON");
        } else {
            turnTheLight("TurnLamp?turn=OFF");
        }

        turnTheLight(url);
    })


    // news ---------------------------------------------------------------------
    function getNews(newsUrl) {
        $.ajax({
            dataType: "json",
            url: newsUrl,
            success: function (response) {
                setNews(response);
            }
        });
    }

    let newsUrl = 'https://newsapi.org/v2/everything?' +
        'q=Waste&' +
        'from=2018-03-24&' +
        'sortBy=popularity&' +
        'apiKey=f7901d1a8c2b45afbc78aa308382d735';
    getNews(newsUrl);

    function setNews(news) {
        slider('#slide-wrap', 4, 1, news.articles);
        console.log(news);
        $('#newsStatus').text(news.status);
    }
});