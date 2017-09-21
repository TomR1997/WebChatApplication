export class ChatScreen {

    chatMessage() {
        var $: any;
        //$(function () {
        var ticker = $.connection.groupChatHub;
        $.connection.hub.start();

        $.extend(ticker.client, {

            publishMessage: function (data) {
                $('#msg').append('<li><span class="p">' + data.Name + '：</span>' + data.Msg + ' <span class="time">' + data.Time + '</span></li>')
                $('#msg').parents('div')[0].scrollTop = $('#msg').parents('div')[0].scrollHeight;
            },

            publishUser: function (data) {
                $('#count').text(data.length);
                $('#users').empty();
                $('#users').append('<option value="0">Everyone</option>');
                for (var i = 0; i < data.length; i++) {
                    $('#users').append('<option value=' + data[i] + '>' + data[i] + '</option>')
                }

            }
        });

        $('#btn-send').click(function () {
            var msg = $('#txt-msg').val();
            if (!msg) {
                alert('Error...'); return false;
            }
            $('#txt-msg').val('');

            ticker.server.sendMessage($('#users').val(), msg);
        });
        //});
    }
}