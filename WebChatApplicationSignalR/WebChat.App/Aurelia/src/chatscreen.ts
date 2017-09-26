import 'ms-signalr-client';
import 'jquery';

/*
export class ChatScreen {

    constructor() {
        var test = $.hubConnection();
        var eventProxy = test.createHubProxy("groupChatHub");
        eventProxy.connection.start();
        console.debug(eventProxy.hubName+"abc");
    }

    sendMessage() {
        //$.connection.hub.start();
        this.connection.hub.start();

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
}*/

export class ChatScreen {
    connection: any;
    proxy = {};
    running = false;
    //connection = null;

    constructor() {
        this.createHub('groupChatHub');
        //this.setCallback('groupChatHub', 'sendMessage', console.log('test'), 'testCallback');
        this.start();
    }

    createHub(hubName) {
        if (!this.connection) {
            //this.connection = $.hubConnection('{hubBaseUrl}');
            //this.connection = $.hubConnection();
            this.connection = $.hubConnection('http://localhost:51907/signalr', { useDefaultPath: false });
            this.connection.logging = true;

            //The following can be used to pass certain data to the hub on connection such as user id.
            //this.connection.qs = { UserId: '{SomeUserId}', Token: '{SomeUserToken}' };
        }
        hubName = hubName.toLocaleLowerCase();
        if (!this.connection.proxies[hubName]) {
            var hubProxy = this.connection.createHubProxy(hubName);
            /*hubProxy.on('sendMessage', function (data) {
                console.log(data);
            });*/  
            this.connection.proxies[hubName].funcs = {};
            /*this.connection.proxies[hubName].on('sendMessage', function(data){
                console.log(data + ' data');
            });*/
            hubProxy.on('sendMessage', message => console.log('testing...'));
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

    /*
    sendMessage() {
        this.start();
        console.log("testing binding...");
    }*/
}
