class Item {
  id: number = 0;
  header: string = "";
  count: number = 1;
  price: number = 1;
  mod: string = "";
}

async function getApi<T>(url: string): Promise<T> {
  return await fetch(url).then((response) => {
    if (!response.ok) {
      console.log("error");
    }
    return response.json();
  });
}
async function getServerNames(): Promise<string[]> {
  let list: string[] = [];
  await getApi<string[]>("https://localhost:7273/api/Main/server/names").then(
    (res) =>
      res.forEach((serverName) => {
        list.push(serverName);
      })
  );
  return list;
}
async function getItemsFromServer(server: string): Promise<Item[]> {
  let items: Item[] = [];
  await getApi<Item[]>("https://localhost:7273/api/Main/server/" + server).then(
    (json) => {
      items = Object.assign([], json);
    }
  );
  return items;
}
function initMenu(): void {
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
            select?.appendChild(option);
          });
        });
      };
      item.innerText = serverName;
      serverNavMenu?.appendChild(item);
    });
  });
}

window.onload = initMenu;
