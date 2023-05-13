var connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/chat", signalR.HttpTransportType.WebSockets).build();


connection.on("updateOnlineUserCount", (value) => {
    console.log(value)
    var newCountSpan = document.getElementById("viewsCounter");
    newCountSpan.innerText = value.toString();
});

//start connection
function fulfilled() {
    console.log("Connection to User Hub Successful");
    //updateOnlineUserCount();
}
function rejected() {

}

connection.start().then(fulfilled, rejected);