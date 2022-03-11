"use strict";
//SIGNALR
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.on("ReceiveMessage", function (roomtemp1, roomtemp2, machinetemp1, machinetemp2, roomhumid1, roomhumid2, machinehumid1, machinehumid2) {
    
    var time = new Date().getTime();

    datasets["room temp 1"].data.push([time, roomtemp1]);
    datasets["room temp 2"].data.push([time, roomtemp2]);
    datasets["machine temp 1"].data.push([time, machinetemp1]);
    datasets["machine temp 2"].data.push([time, machinetemp2]);
    datasets["room humid 1"].data.push([time, roomhumid1]);
    datasets["room humid 2"].data.push([time, roomhumid2]);
    datasets["machine humid 1"].data.push([time, machinehumid1]);
    datasets["machine humid 2"].data.push([time, machinehumid2]);

    var avgroomtemp = (parseInt(roomtemp1) + parseInt(roomtemp2)) / 2;
    var avgmachinetemp = (parseInt(machinetemp1) + parseInt(machinetemp2)) / 2;
    var avgroomhumid = (parseInt(roomhumid1) + parseInt(roomhumid2)) / 2;
    var avgmachinehumid = (parseInt(machinehumid1) + parseInt(machinehumid2)) / 2;
    rtg.refresh(avgroomtemp);
    rhg.refresh(avgroomhumid);
    mtg.refresh(avgmachinetemp);
    mhg.refresh(avgmachinehumid);

    document.getElementById("loc1temp").innerText = " " + roomtemp1 + "°C";
    document.getElementById("loc2temp").innerText = " " + machinetemp1 + "°C";
    document.getElementById("loc3temp").innerText = " " + machinetemp2 + "°C";
    document.getElementById("loc4temp").innerText = " " + roomtemp2 + "°C";
    document.getElementById("loc1humid").innerText = " " + roomhumid1 + "%";
    document.getElementById("loc2humid").innerText = " " + machinehumid1 + "%";
    document.getElementById("loc3humid").innerText = " " + machinehumid2 + "%";
    document.getElementById("loc4humid").innerText = " " + roomhumid2 + "%";
    
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
}

//GAUGE
var defs1 = {
    value: 35,
    min: 0,
    max: 100,
    decimals: 1,
    gaugeWidthScale: 1.0,
    percents: true,
    customSectors: [{
        color: "#ff0000",
        lo: 0,
        hi: 20
    }, {
        color: "#06FF00",
        lo: 21,
        hi: 40
    }, {
        color: "#ff0000",
        lo: 41,
        hi: 100
    }],
    counter: true,
}
var defs2 = {
    value: 35,
    min: 0,
    max: 100,
    decimals: 1,
    gaugeWidthScale: 1.0,
    counter: true,
}
var rtg = new JustGage({
    id: "roomtempgauge",
    label: "°C",
    defaults: defs1
});
var rhg = new JustGage({
    id: "roomhumidgauge",
    label: "%",
    defaults: defs2
});
var mtg = new JustGage({
    id: "mchinetempgauge",
    label: "°C",
    defaults: defs1
});
var mhg = new JustGage({
    id: "mchinehumidgauge",
    label: "%",
    defaults: defs2
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
    },
    "machine humid 1": {
        label: "Machine humid 1",
        data: []
    },
    "machine humid 2": {
        label: "Machine humid 2",
        data: []
    },
    "room humid 1": {
        label: "Room humid 1",
        data: []
    },
    "room humid 2": {
        label: "Room humid 2",
        data: []
    }
};
var i = 0;
$.each(datasets, function (key, val) {
    val.color = i;
    ++i;
});
var choiceContainer = $("#choices");
$.each(datasets, function (key, val) {
    choiceContainer.append(" <input type='checkbox' name='" + key +
        "' checked='checked' id='id" + key + "'></input>" +
        "<label for='id" + key + "'>"
        + val.label + "</label>");
});

choiceContainer.find("input").click(plotAccordingToChoices);

plotAccordingToChoices();
update();

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
                timeformat: '%H:%M:%S:%d',
                tickDecimals: 4
            }
        });
    }
}

var updateInterval = 5000 //Fetch data every x milliseconds
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
