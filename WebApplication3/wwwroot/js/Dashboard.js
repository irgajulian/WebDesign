"use strict";

//SIGNALR
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var temp = 0;
var dataa = [];
connection.on("ReceiveMessage", function (roomtemp1, roomtemp2, machinetemp1, machinetemp2) {
    
    var time = new Date().getTime();
    
    datasets["room temp 1"].data.push([time, roomtemp1]);
    datasets["room temp 2"].data.push([time, roomtemp2]);
    datasets["machine temp 1"].data.push([time, machinetemp1]);
    datasets["machine temp 2"].data.push([time, machinetemp2]);
    
    rtg.refresh(roomtemp1);
    rhg.refresh(roomtemp2);
    mtg.refresh(machinetemp1);
    mhg.refresh(machinetemp2);
    
    document.getElementById("loc1temp").innerText = " " + roomtemp1;
    document.getElementById("loc2temp").innerText = " " + roomtemp2;
    document.getElementById("loc1humid").innerText = " " + machinetemp1 + " ";
    document.getElementById("loc2humid").innerText = " " + machinetemp2 + " ";
    
});

connection.start().then(function () {
   
}).catch(function (err) {
    return console.error(err.toString());
});

function update() {
   
    connection.invoke("SendMessage").catch(function (err) {
        return console.error(err.toString());
    });
    plotAccordingToChoices();
    event.preventDefault();
    
}

//GAUGE
var rtg = new JustGage({
    id: "roomtempgauge",
    value: temp,
    min: 0,
    max: 100,
    title: "Room AVG Temperature"
});
var rhg = new JustGage({
    id: "roomhumidgauge",
    value: temp,
    min: 0,
    max: 100,
    title: "Room AVG Humidity"
});
var mtg = new JustGage({
    id: "mchinetempgauge",
    value: temp,
    min: 0,
    max: 100,
    title: "Room AVG Temperature"
});
var mhg = new JustGage({
    id: "mchinehumidgauge",
    value: temp,
    min: 0,
    max: 100,
    title: "Room AVG Humidity"
});

//FLOT
var datasets = {
    "machine temp 1": {
        label: "Machine temp 1",
        data: []
    },
    "machine temp 2": {
        label: "Machine temp 2",
        data: []
    },
    "room temp 1": {
        label: "Room temp 1",
        data: []
    },
    "room temp 2": {
        label: "Room temp 2",
        data: []
    }
};

var i = 0;
$.each(datasets, function (key, val) {
    val.color = i;
    ++i;
});

// insert checkboxes
var choiceContainer = $("#choices");
$.each(datasets, function (key, val) {
    choiceContainer.append("<br/><input type='checkbox' name='" + key +
        "' checked='checked' id='id" + key + "'></input>" +
        "<label for='id" + key + "'>"
        + val.label + "</label>");
});

choiceContainer.find("input").click(plotAccordingToChoices);

function plotAccordingToChoices() {

    var data = [];

    choiceContainer.find("input:checked").each(function () {
        var key = $(this).attr("name");
        if (key && datasets[key]) {
            data.push(datasets[key]);
        }
    });

    if (data.length > 0) {
        $.plot("#placeholder", data, {
            yaxis: {
                min: 0
            },
            xaxis: {
                mode: "time",
                timezone: "browser",
                timeformat: '%H:%M:%S',
                tickDecimals: 0
            }
        });
    }
}

var updateInterval = 5000 //Fetch data ever x milliseconds
$("#updateInterval").val(updateInterval).change(function () {
    var v = $(this).val();
    if (v && !isNaN(+v)) {
        updateInterval = +v;
        if (updateInterval < 5000) {
            updateInterval = 5000;
        } else if (updateInterval > 60000) {
            updateInterval = 60000;
        }
        $(this).val("" + updateInterval);
    }
});
setInterval(update, updateInterval);
plotAccordingToChoices();
update();