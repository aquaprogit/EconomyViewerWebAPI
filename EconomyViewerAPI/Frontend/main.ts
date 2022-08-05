const GetApi = async <T>(url: string): Promise<T> => {
    return await fetch(url).then((response) => {
        if (!response.ok) {
            return "hui tebe ::)";
        }
        return response.json();
    });
};
const list: string[] = [];

GetApi<string[]>("https://localhost:7273/api/Main/server/names")
    .then((res) =>
        res.forEach((serverName) => {
            list.push(serverName);
            console.log(serverName);
        })
    )
    .then(() => {
        list.forEach(serverName => {
            let serverNavMenu = document.getElementById("servers");
            let item = document.createElement("a");
            item.innerText = serverName;
            console.log(serverName);
            serverNavMenu?.appendChild(item);
        });
    });
