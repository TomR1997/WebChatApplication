export class ChatScreen {
    private connection: SignalR;

    constructor() {
        this.connection = $.connection;
    }

    sendMessage() {
        $.connection.hub.start();

        var ticker = $.connection.hub.createHubProxy("groupChatHub");

        //ticker.client
        $.extend(ticker, {

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

            //ticker.server.
            //ticker.sendMsg($("#users").val(), msg);
            //ticker.invoke('sendMsg($("#users").val(), msg)');
        });
    }
}