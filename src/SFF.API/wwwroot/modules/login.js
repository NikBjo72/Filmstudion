import { app } from '../script/script.js';

export async function login(){
    app.loginBtn.addEventListener('click', async function() {
        debugger;
        let raw = JSON.stringify({
            "Password": `${app.password.value}`,
            "UserName": `${app.userName.value}`
          });

        let myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
          };

        let responce = await fetch("/api/users/authenticate", requestOptions)
        data = await responce.json();
        //console.log(await responce.text());


        console.log(data);
        app.userToken = data.token;
        console.log(app.userToken);
        app.logoutBtn.hidden = false;
        app.loginBtn.hidden = true;
    });
}