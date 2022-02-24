"use strict";

//SIGNALR
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var temp = 0;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    var today = new Date();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    li.textContent = `${user}  ${time}`;
    rtg.value = user;
});

connection.start().then(function () {
   
}).catch(function (err) {
    return console.error(err.toString());
});

setInterval(update, 2000);

function update() {
   
    connection.invoke("SendMessage").catch(function (err) {
        return console.error(err.toString());
    });
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
    "machine 1": {
        label: "Machine 1",
        data: [[1988, 483994], [1989, 479060], [1990, 457648], [1991, 401949], [1992, 424705], [1993, 402375], [1994, 377867], [1995, 357382], [1996, 337946], [1997, 336185], [1998, 328611], [1999, 329421], [2000, 342172], [2001, 344932], [2002, 387303], [2003, 440813], [2004, 480451], [2005, 504638], [2006, 528692]]
    },
    "machine 2": {
        label: "Machine 2",
        data: [[1988, 218000], [1989, 203000], [1990, 171000], [1992, 42500], [1993, 37600], [1994, 36600], [1995, 21700], [1996, 19200], [1997, 21300], [1998, 13600], [1999, 14000], [2000, 19100], [2001, 21300], [2002, 23600], [2003, 25100], [2004, 26100], [2005, 31100], [2006, 34700]]
    },
    "room 1": {
        label: "Room 1",
        data: [[1988, 62982], [1989, 62027], [1990, 60696], [1991, 62348], [1992, 58560], [1993, 56393], [1994, 54579], [1995, 50818], [1996, 50554], [1997, 48276], [1998, 47691], [1999, 47529], [2000, 47778], [2001, 48760], [2002, 50949], [2003, 57452], [2004, 60234], [2005, 60076], [2006, 59213]]
    },
    "room 2": {
        label: "Room 2",
        data: [[1988, 55627], [1989, 55475], [1990, 58464], [1991, 55134], [1992, 52436], [1993, 47139], [1994, 43962], [1995, 43238], [1996, 42395], [1997, 40854], [1998, 40993], [1999, 41822], [2000, 41147], [2001, 40474], [2002, 40604], [2003, 40044], [2004, 38816], [2005, 38060], [2006, 36984]]
    }
};

// hard-code color indices to prevent them from shifting as
// countries are turned on/off

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
                tickDecimals: 0
            }
        });
    }
}

plotAccordingToChoices();