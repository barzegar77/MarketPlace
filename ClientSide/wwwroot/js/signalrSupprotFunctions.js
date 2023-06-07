var supportConnection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl("/hubs/support", signalR.HttpTransportType.WebSockets).build();


supportConnection.on("GetRooms", loadRooms);


var roomListEL = document.getElementById("#roomList")

function removeAllChildren(node) {
    if (!node) return;
    while (node.lastChild) {
        node.removeChild(node.lastChild)
    }
}

function loadRooms(rooms) {
    if (!rooms) return;
    var roomIds = Object.keys(rooms);
    //console.log(roomIds)
    if (!roomIds) return;

    removeAllChildren(roomListEL)

    roomIds.forEach(function (id) {
        var roomInfo = rooms[id];
        if (!roomInfo) return;
        return $("#roomList").append("<a href='#'' class='d-flex align-items-center' data-id='" + id + "'> <div class='flex-grow-1 ms-3'> <h3>" + roomInfo + "</h3> </div> </a>");
    })
}



function Init() {
}

$(document).ready(function () {
    Init();

});




//start connection
function fulfilled() {
    console.log("Connection to User Hub Successful");
    //updateOnlineUserCount();
}
function rejected() {

}

supportConnection.start().then(fulfilled, rejected);