$(function() {
	
    function getTemp(url) {
        $.ajax({
            dataType: "json",
            url: url,
            success: function (response) {
                console.log(response);
                setTemp(response.Temperature + "° C");
			}
        });
    }

    function getAllTemps(url){
        $.ajax({
            dataType: "json",
            url: url,
            success: function (response) {
                console.log(response);
                for (i = 0; i < response.length; i++)
                    appendoToTable(response[i].ID, response[i].Temperature, response[i].DateRecorded)
            }
        });
    }
	
	function toFarenheit($temp){
		return 1.8 * $temp + 32;
    }

    function setTemp(celsius) {
        $('#innerTemp').text(celsius);
    }

    function appendoToTable(id, temperature, date) {
        id = $('<td></td>').text(id);
        temperature = $('<td></td>').text(temperature + "° C");
        date = $('<td></td>').text(date);
        let row = $('<tr></tr>').append(id, temperature, date);

        $('#temperatures').append(row);
    }

    //appendoToTable("6","666","2018/22/32");
	
	let url = "http://localhost:50393/api/TemperatureRecords/1";
    getTemp(url);

    url = "http://localhost:50393/api/TemperatureRecords";
    getAllTemps(url);

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