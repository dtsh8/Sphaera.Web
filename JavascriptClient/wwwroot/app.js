/// <reference path="oidc-client.js" />

function log() {
    document.getElementById('results').innerText = "";

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error " + msg.message;
        }
        else if (typeof msg !== "string") {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("api").addEventListener("click", api, false);
document.getElementById("logout").addEventListener("click", logout, false);
document.getElementById("cards").addEventListener("click", cards, false);

var config = {
    authority: "https://localhost:44333",
    client_id: "js",
    redirect_uri: "https://localhost:44358/callback.html",
    response_type: "code",
    scope: "openid profile api1",
    post_logout_redirect_uri: "https://localhost:44358/index.html"
};

var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function logout() {
    mgr.signoutRedirect();
}

function api() {
    mgr.getUser().then(function (user) {
        var url = "http://localhost:44343/identity";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        };
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

//function cards() {
//    mgr.getUser().then(function (user) {
//        var url = "http://localhost:5001/api/cards";

//        var xhr = new XMLHttpRequest();
//        xhr.open("GET", url);
//        //console.log(xhr);
//        xhr.onload = function () {
//            log(JSON.parse(xhr.responseText));
//        };
//        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
//        xhr.send();
//    });
//}