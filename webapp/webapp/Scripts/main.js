$(function() {
	
    function getTemp(url) {
		$.ajax({
            dataType: "json",
            url: url,
            success: function (response) {
                setTemp(response.Temperature + "° C");
			}
        });
	}
	
	function toFarenheit($temp){
		return 1.8 * $temp + 32;
    }

    function setTemp(celsius) {
        $('#innerTemp').text(celsius);
    }
	
	let url = "http://localhost:50393/api/TemperatureRecords/1";
	getTemp(url);
	
});