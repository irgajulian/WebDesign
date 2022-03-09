
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
//setInterval(update, updateInterval);
//plotAccordingToChoices();
//update();