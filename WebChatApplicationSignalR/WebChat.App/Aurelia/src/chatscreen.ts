import 'ms-signalr-client';
import 'jquery';

export class ChatScreen {
    private connection: any;
    private running: boolean;
    private hubProxy: any;
    private message: string;

    constructor() {
        this.running = false;
        this.createHub('groupChatHub');
        //this.createHub('simpleHub');
        this.start();
    }

    private createHub(hubName) {
        if (!this.connection) {
            this.connection = $.hubConnection();

            //this.connection = $.hubConnection('http://localhost:51907/signalr', { useDefaultPath: false });
            this.connection.logging = true;

            //The following can be used to pass certain data to the hub on connection such as user id.
            //this.connection.qs = { UserId: '{SomeUserId}', Token: '{SomeUserToken}' };
        }
        hubName = hubName.toLocaleLowerCase();
        if (!this.connection.proxies[hubName]) {
            this.hubProxy = this.connection.createHubProxy(hubName); 
            this.connection.proxies[hubName].funcs = {};

            //Subscribe to the hub, when publishMessage is called, call onMessageReceived.
            this.hubProxy.on('publishMessage', message => this.onMessageReceived(message));
            //this.hubProxy.on('addNewMessageToPage', message => this.onMessageReceived(message));
        }
    }

    private setCallback(hubName, funcName, callBack, cbNameOverride = null) {
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

    private start() {
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

    private stop(hubName, funcName, callBack, cbNameOverride = null) {
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

    private sendMessage() {
        var hub = this.hubProxy;

        //Invoke SendMessage at server side.
        hub.invoke('SendMessage', 'name', this.message);
        //hub.invoke('Send', 'TestName', this.message); 
    }

    private onMessageReceived(data){
        $("#msg").append("<li><span class='p'>" + data.Name + "：</span>" + data.Msg + " <span class='time'>" +  data.Time + "</span></li>");
        $("#msg").parents("div")[0].scrollTop = $("#msg").parents("div")[0].scrollHeight;
    }

    //FOR SIMPLEHUB
    /*onMessageReceivedlatestMessage: string) {
        console.log('New message received: ' + latestMessage);
        $("#msg").append("<li><span class='p'>" + 'someName' + "：</span>" + latestMessage + " <span class='time'>" + 'date' + "</span></li>");
        $("#msg").parents("div")[0].scrollTop = $("#msg").parents("div")[0].scrollHeight;
    }*/
}
