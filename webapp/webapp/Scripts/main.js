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

    url = "api/InOutTemperatures/";
    getAllTemps(url);

    // Termostat ----------------------------------------------------------------
    $('#termostat button').click(function (e) {
        e.preventDefault();
        let setupValue = $('#preferredTemp').val();
        let url = "TurnHeat/temperature=" + setupValue;

        $.ajax({
            method: 'GET',
            url: url
        })

        alert("Temperature threshold has been set!");
    })

    window.setInterval(function () {
        checkFurnes();
        //console.log("seconds");
    }, 1000);

    function checkFurnes() {
        $.ajax({
            method: "GET",
            url: "api/Heats",
            success: function (response) {
                let last = response.length - 1;

                if (response[last].IsTurnedOn) {
                    $('#furnace').text("The furnace is burning like HELL (gives you warmth)!");
                    $('.heating').addClass('on');
                } else {
                    $('#furnace').text("The We Are out of HELL(fire)!");
                    $('.heating').removeClass('on');
                }
            }
        })
    }

    // lights -------------------------------------------------------------------
    function getCurrentState() {
        $.ajax({
            method: "GET",
            url: "api/Lamps",
            success: function (response) {
                let last = response.length - 1;
                $('#lights .switch input:checkbox').prop('checked', response[last].IsTurnedOn);

                $('#lights p').html(response[last].IsTurnedOn ? 'Turn off the <b>lights</b>, Save the world!' : 'Keep the <b>lights</b>. off, Save the world!')
            }
        })
    }
    getCurrentState();

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



    //-----------------------------------------------------------
    function create_timechart(diffMins) {
        $.ajax({
            method: "GET",
            url: "api/Lamps",
            success: function (response) {
                let length = response.length;
                //
                google.charts.load("current", { packages: ["timeline"] });
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {

                    var container = document.getElementById('timechart');
                    var chart = new google.visualization.Timeline(container);
                    var dataTable = new google.visualization.DataTable();
                    dataTable.addColumn({ type: 'string', id: 'status' });
                    dataTable.addColumn({ type: 'date', id: 'Start' });
                    dataTable.addColumn({ type: 'date', id: 'End' });
                    //

                    let onHours;
                    let diffMins = 0;

                    x = [];
                    for (i = 0; i < length - 2; i++) {
                        x.push([response[i].IsTurnedOn ? "on" : "off", new Date(response[i].DateRecorded), new Date(response[i + 1].DateRecorded)]);
                        if (response[i].IsTurnedOn) {
                            onHours = Math.floor(Math.abs(new Date((new Date(response[i + 1].DateRecorded) - new Date(response[i].DateRecorded))/1000/60)));
                            diffMins += onHours;
                        }
                    }
                    //x.push([response[length - 1].IsTurnedOn ? "on" : "off", new Date(response[length-1].DateRecorded), new Date()])

                    change_wattage(60, diffMins);
                        
                    dataTable.addRows(x);
                    chart.draw(dataTable);
                } 
            }
        })
    }
    create_timechart();

    // change wattage -----------------------------------------------------------
    function change_wattage(Wattage, diffMins){
        let consumption = Wattage * (diffMins / 60);
        $('#calculatedConsumption').html("Your daily consumption, on a <b>" + Wattage + "W</b> lightbulb: <b>" + consumption + "Wh</b> in total <b><span id='diffMins'>" + diffMins + "</span> minutes</b>");
        $('#differentWattage').val(Wattage);
    }

    $('#setDifferentWattage button').click(function (e) {
        e.preventDefault();
        change_wattage($('#differentWattage').val(), $('#diffMins').text());
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
        //console.log(news);
        $('#newsStatus').text(news.status);
    }
});