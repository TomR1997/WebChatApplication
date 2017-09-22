export class ChatScreen {
    private connection: SignalR;
    private proxy: SignalR.Hub.Proxy;
    //private proxyConnection: SignalR;

    constructor() {
        this.connection = $.connection;
        //this.proxyConnection = $.hubConnection;
        //this.proxy = this.proxyConnection.createHubProxy("groupChatHub");
        this.proxy.on('messageReceived', (latestMsg) => this.onMessageReceived(latestMsg));
        this.connection.hub.start();
    }

    private onMessageReceived(latestMsg: string) {
        console.log('New message received: ' + latestMsg);
    }

    broadcastMessage(msg: string) {
        this.proxy.invoke('sendMessage', msg);
    }


    sendMessage() {
        //var $: any;
        //$(function () {

        var ticker = $.connection.groupChatHub;
            $.connection.hub.start();

            $.extend(ticker.client, {

                publshMsg: function (data) {
                    $("#msg").append("<li><span class='p'>" + data.Name + "：</span>" + data.Msg + " <span class='time'>" + data.Time + "</span></li>")
                    $("#msg").parents("div")[0].scrollTop = $("#msg").parents("div")[0].scrollHeight;
                },

                publshUser: function (data) {
                    $("#count").text(data.length);
                    $("#users").empty();
                    $("#users").append('<option value="0">Everyone</option>');
                    for (var i = 0; i < data.length; i++) {
                        $("#users").append('<option value="' + data[i] + '">' + data[i] + '</option>')
                    }

                }
            });

            $("#btn-send").click(function () {
                var msg = $("#txt-msg").val();
                if (!msg) {
                    alert('Error...'); return false;
                }
                $("#txt-msg").val('');

                ticker.server.sendMsg($("#users").val(), msg);
            });

        //});
    }
}