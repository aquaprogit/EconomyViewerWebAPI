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
const GetApi = (url) => __awaiter(void 0, void 0, void 0, function* () {
    return yield fetch(url).then((response) => {
        if (!response.ok) {
            return "hui tebe ::)";
        }
        return response.json();
    });
});
const list = [];
GetApi("https://localhost:7273/api/Main/server/names")
    .then((res) => res.forEach((serverName) => {
    list.push(serverName);
    console.log(serverName);
}))
    .then(() => {
    list.forEach(serverName => {
        let serverNavMenu = document.getElementById("servers");
        let item = document.createElement("a");
        item.innerText = serverName;
        console.log(serverName);
        serverNavMenu === null || serverNavMenu === void 0 ? void 0 : serverNavMenu.appendChild(item);
    });
});
