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

	
});