"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class Item {
    constructor() {
        this.id = 0;
        this.header = "";
        this.count = 1;
        this.price = 1;
        this.mod = "";
    }
}
function getApi(url) {
    return __awaiter(this, void 0, void 0, function* () {
        return yield fetch(url).then((response) => {
            if (!response.ok) {
                console.log("error");
            }
            return response.json();
        });
    });
}
function getServerNames() {
    return __awaiter(this, void 0, void 0, function* () {
        let list = [];
        yield getApi("https://localhost:7273/api/Main/server/names").then((res) => res.forEach((serverName) => {
            list.push(serverName);
        }));
        return list;
    });
}
function getItemsFromServer(server) {
    return __awaiter(this, void 0, void 0, function* () {
        let items = [];
        yield getApi("https://localhost:7273/api/Main/server/" + server).then((json) => {
            items = Object.assign([], json);
        });
        return items;
    });
}
function initMenu() {
    getServerNames().then((names) => {
        names.forEach((serverName) => {
            let serverNavMenu = document.getElementById("servers");
            let item = document.createElement("a");
            item.onclick = () => {
                let select = document.getElementById("server-items-select");
                getItemsFromServer(item.innerText)
                    .then((all) => {
                    all.forEach((item) => {
                        let option = document.createElement("option");
                        option.innerText = item.header;
                        select === null || select === void 0 ? void 0 : select.appendChild(option);
                    });
                });
            };
            item.innerText = serverName;
            serverNavMenu === null || serverNavMenu === void 0 ? void 0 : serverNavMenu.appendChild(item);
        });
    });
}
window.onload = initMenu;
