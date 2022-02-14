"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
      document.getElementById("messagesList").appendChild(li);
    li.textContent = `room temp 1 = ${user} room temp 2 = ${message}`;
});

connection.start().then(function () {
  
}).catch(function (err) {
    return console.error(err.toString());
});

setInterval(displayHello, 3000);

function displayHello() {
    connection.invoke("SendMessage").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}
