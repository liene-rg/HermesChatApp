"use-strict"

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

let _connectionId = "";

connection.on("ReceiveMessage", function (data) {
    let message = messageBuilder()
        .createMessage()
        .withHeader(data.name)
        .withParagraph(data.text)
        .withFooter(data.timestamp)
        .build();

    document.querySelector(".chat-body").append(message);

});


connection.start()
    .then(function () {
        connection.invoke("joinRoom", "@Model.Id");
    })
    .catch(function (err) {
        console.log(err)
    });

window.addEventListener("onunload", function () {
    connection.invoke("leaveRoom", "@Model.Id");
});

let sendMessage = function (event)
{
    event.preventDefault();

    let data = new FormData(event.target);
    document.getElementById("message-input").value = "";
    axious.post("/Home/SendMessage", data)
        .then(res => {
            console.log("Message Sent")
        })
        .catch(err => {
            console.log("Message failed to send")
        })
};