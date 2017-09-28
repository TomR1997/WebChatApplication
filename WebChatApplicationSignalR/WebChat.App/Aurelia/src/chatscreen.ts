import 'ms-signalr-client';
import 'jquery';

export class ChatScreen {
    connection: any;
    proxy = {};
    running = false;
    hubProxy: any;
    message: string;
    userBox: any;

    constructor() {
        //this.createHub('groupChatHub');
        this.createHub('simpleHub');
        //this.setCallback('groupChatHub', 'sendMessage', console.log('test'), 'testCallback');
        this.start();
    }

    createHub(hubName) {
        if (!this.connection) {
            //this.connection = $.hubConnection('{hubBaseUrl}');
            this.connection = $.hubConnection();
            //this.connection = $.hubConnection('http://localhost:51907/signalr', { useDefaultPath: false });
            this.connection.logging = true;

            //The following can be used to pass certain data to the hub on connection such as user id.
            //this.connection.qs = { UserId: '{SomeUserId}', Token: '{SomeUserToken}' };
        }
        hubName = hubName.toLocaleLowerCase();
        if (!this.connection.proxies[hubName]) {
            this.hubProxy = this.connection.createHubProxy(hubName);
            /*hubProxy.on('sendMessage', function (data) {
                console.log(data);
            });*/  
            this.connection.proxies[hubName].funcs = {};
            /*this.connection.proxies[hubName].on('sendMessage', function(data){
                console.log(data + ' data');
            });*/

            //this.hubProxy.on('sendMessage', message => this.onMessageReceived(message));
            this.hubProxy.on('addNewMessageToPage', message => console.log(message));
        }
    }

    setCallback(hubName, funcName, callBack, cbNameOverride = null) {
        hubName = hubName.toLocaleLowerCase();
        if (!this.connection.proxies[hubName].funcs[funcName]) {
            this.connection.proxies[hubName].funcs[funcName] = {};
            this.connection.proxies[hubName].on(funcName, function (data) {
                for (var func of Object.keys(this.connection.proxies[hubName].funcs[funcName])) {
                    this.connection.proxies[hubName].funcs[funcName][func](data);
                }
            });
        }
        this.connection.proxies[hubName].funcs[funcName][cbNameOverride || callBack.name] = function (data) {
            callBack(data);
        };
    }

    start() {
        if (!this.running) {
            this.connection.start({ jsonp: true })
                .done(function () {
                    console.debug('Connection succeeded');
                })
                .fail(function (err) {               
                    console.debug('Could not connect '+err);
                });
            this.running = true;
        }
    }

    stop(hubName, funcName, callBack, cbNameOverride = null) {
        if (this.running) {
            console.debug('Hub Stopping');
            if (this.connection.proxies[hubName]) {
                if (this.connection.proxies[hubName].funcs[funcName]) {
                    delete this.connection.proxies[hubName].funcs[funcName][cbNameOverride || callBack.name];
                }

                if (Object.keys(this.connection.proxies[hubName].funcs[funcName]).length === 0)
                    delete this.connection.proxies[hubName].funcs[funcName];

                if (Object.keys(this.connection.proxies[hubName].funcs).length === 0)
                    delete this.connection.proxies[hubName];
            }

            if (Object.keys(this.connection.proxies).length === 0) {
                this.connection.stop();
                this.running = false;
            }
        }
    }

    sendMessage() {
        var hub = this.hubProxy;

        $("#msg").append("<li><span class='p'>" +'someName' + "：</span>" + this.message + " <span class='time'>" + 'date' + "</span></li>")
        $("#msg").parents("div")[0].scrollTop = $("#msg").parents("div")[0].scrollHeight;
        hub.invoke('Send', 'TestName', this.message);
        /*
        $.extend(hub.client, {


            publshMsg: function (data) {
                $("#msg").append("<li><span class='p'>" + data.Name + "：</span>" + data.Msg + " <span class='time'>" + data.Time + "</span></li>")
                $("#msg").parents("div")[0].scrollTop = $("#msg").parents("div")[0].scrollHeight;
                console.log(data.Name + ' ' + data.Msg);
            },

            publshUser: function (data) {
                $("#count").text(data.length);
                $("#users").empty();
                $("#users").append('<option value="0">Everyone</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#users").append('<option value="' + data[i] + '">' + data[i] + '</option>')
                }

            }
        });*/

        /*$("#btn-send").click(function () {
            var msg = $("#txt-msg").val();
            if (!msg) {
                alert('Error...'); return false;
            }
            $("#txt-msg").val('');

            //hub.server.sendMessage($("#users").val(), msg);
            hub.invoke('SendMessage', { 'name': String, msg: String }).done(function () {
                console.log('new message invocation');
            }).fail(function (err) {
                console.log('new message failed ' + err);
                });
        });*/
        /*
        hub.invoke('Send', 'TestName', this.message);

        /*hub.client.addNewMessageToPage = function (name, message) {
            $("#id").append('<ul style="list-style-type:square"><li><strong style="color:red;font-style:normal;font-size:medium;text-transform:uppercase">' + 'NameTest' + '  ' + '<strong style="color:black;font-style:normal;font-size:medium;text-transform:lowercase">said</strong>'
                + '</strong>: ' + '<strong style="color:blue;font-style:oblique;font-size:medium">' + message + '</strong>' + '</li></ul>');
        }*/


        //hub.server.send('nameTest', msg);
        //$("#txt-msg").val();


        //hub.invoke('SendMessage', 'name', msg);
        //this.hubProxy.invoke('invokeMessage', msg);
    }

    onMessageReceived(latestMessage: string) {
        console.log('New message received: ' + latestMessage);
    }
}
