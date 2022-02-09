console.log("Hej html!")

const app = {
    userToken: "",
    userName: document.getElementById("userName"),
    password: document.getElementById("password"),
    loginBtn: document.getElementById("loginBtn"),
    logoutBtn: document.getElementById("logoutBtn")
};


app.loginBtn.addEventListener('click', async function() {
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

app.logoutBtn.addEventListener('click', function() {
    app.logoutBtn.hidden = true;
    app.loginBtn.hidden = false;
    app.userToken = "";
    app.password.value = "";
    app.userName.value ="";
    console.log(app.userToken);
});