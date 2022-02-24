"use strict";

//SIGNALR
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (roomtemp1, roomtemp2, machinetemp1, machinetemp2) {
    let tbody = document.createElement('tbody');
    var time = new Date().getTime();
    let row_ = document.createElement('tr');
    let row_data_1 = document.createElement('td');
    row_data_1.innerText = roomtemp1;
    let row_data_2 = document.createElement('td');
    row_data_2.innerText = roomtemp2;
    let row_data_3 = document.createElement('td');
    row_data_3.innerHTML = machinetemp1 ;

    row_.appendChild(row_data_1);
    row_.appendChild(row_data_2);
    row_.appendChild(row_data_3);
    tbody.appendChild(row_);
    document.getElementById('example1').appendChild(tbody);

});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

function update() {

    connection.invoke("SendMessage").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

}

function getData() {
    let row_2 = document.createElement('tr');
    let row_2_data_1 = document.createElement('td');
    row_2_data_1.innerHTML = "1.";
    let row_2_data_2 = document.createElement('td');
    row_2_data_2.innerHTML = "James Clerk";
    let row_2_data_3 = document.createElement('td');
    row_2_data_3.innerHTML = "Netflix";

    row_2.appendChild(row_2_data_1);
    row_2.appendChild(row_2_data_2);
    row_2.appendChild(row_2_data_3);
    tbody.appendChild(row_2);
}

var updateInterval = 20000 //Fetch data ever x milliseconds
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
update();