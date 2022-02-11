console.log("Hej html!")

const app = {
    userToken: localStorage.getItem("token"),
    userId: localStorage.getItem("userId"),
    loggedIn: localStorage.getItem("loggedInName"),
    userName: document.getElementById("userName"),
    password: document.getElementById("password"),
    loginBtn: document.getElementById("loginBtn"),
    logoutBtn: document.getElementById("logoutBtn"),
    filmLibraryBtn: document.getElementById("filmLibraryBtn"),
    rentedFilmsBtn: document.getElementById("rentedFilmsBtn"),
    actionButtons: document.getElementById("actionButtons"),
    dynamicView: document.getElementById("dynamicView"),
    rentedFilmsBtnField: document.getElementById("rentedFilmsBtnField"),
    logoutBtnField: document.getElementById("logoutBtnField"),
    loginField: document.getElementById("loginField"),
    userNameField: document.getElementById("userNameField"),
    loggedInField: document.getElementById("loggedInField"),
    loggedInName: document.getElementById("loggedInName")
};

loggedInTrue();

//app.filmLibraryBtn.hidden = false;

app.filmLibraryBtn.addEventListener('click', async function()
{
    getAllFilms();
});

app.rentedFilmsBtn.addEventListener('click', async function()
{
    getRentedFilms();
});

function loggedInTrue()
{
    if (localStorage.getItem("token") != null)
    {
        app.rentedFilmsBtnField.hidden = false;
        app.logoutBtnField.hidden = false;
        app.loginField.hidden = true;
        app.loggedInField.hidden = false;
        app.loggedInName.innerText = app.loggedIn;
    }
}

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
    
    let response = await fetch("/api/users/authenticate", requestOptions)
    let ok = true;
    try {
            if(response.ok)
            {
                response = await response.json();
            }
            else throw new Error(response.status)
        }
        catch (error)
        {
            console.log(error)
            if (error == "Error: 400")
            {
                if (!checkExistingElement("rejected"))
                {
                    app.userNameField.insertAdjacentHTML('beforeend',` <div id="rejected" class="text-danger">Fel användarnamn eller lösenord</div>`);
                }
                    ok = false;
            }
        }

    //console.log(data)
    if (ok)
    {
        app.rentedFilmsBtnField.hidden = false;
        app.logoutBtnField.hidden = false;
        app.loginField.hidden = true;
        app.loggedInField.hidden = false;
        app.loggedInName.innerText = app.loggedIn;
        localStorage.setItem("token", await response.token);
        localStorage.setItem("userId", await response.filmStudioId);
        localStorage.setItem("loggedInName", await response.userName);
    }
});

//Loggar ut användare
app.logoutBtn.addEventListener('click', function() {
    app.logoutBtnField.hidden = true;
    app.loginField.hidden = false;
    app.rentedFilmsBtnField.hidden = true;
    localStorage.clear();
    app.loggedInField.hidden = true;
    //console.log(app.userToken);
});

async function getAllFilms()
{
    let myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${app.userToken}`);

    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
      };

    let response = await fetch("/api/films", requestOptions)
    let filmInfo = await response.json();

    app.dynamicView.innerHTML = "";

    for (let i = 0; i < filmInfo.length; i++)
    {  
        app.dynamicView.insertAdjacentHTML('beforeend',`
        <div class="card my-3">
            <div class="card-header">
            <span>Film</span><h5 class="d-inline mx-2 text-success" id="rented-${filmInfo[i].filmId}"></h5>
            </div>
            <div class="card-body">
                <h5 class="card-title">${filmInfo[i].name}</h5>
                <p class="card-text">
                    <strong>Regisör:</strong> ${filmInfo[i].director} </br>
                    <strong>Max antal hyrdagar:</strong> ${filmInfo[i].maxRentDays} dagar </br>
                    <strong>Releasedatum:</strong> ${filmInfo[i].releaseDate}</p>
                <a onclick="rentFilm('${filmInfo[i].filmId}')" class="btn btn-primary">Låna</a>
                <p id="film-${filmInfo[i].filmId}"class="card-text text-danger"><p/>

            </div>
        </div>`);

    }; 
}

async function getRentedFilms()
{
    let myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${app.userToken}`);

    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
      };

    let responseCopy = await fetch("/api/mystudio/rentals", requestOptions)
    let filmCopies = await responseCopy.json();
    console.log(filmCopies);
    app.dynamicView.innerHTML = "";

    for (let i = 0; i < filmCopies.length; i++)
    {  
        let responseFilm = await fetch(`/api/films/${filmCopies[i].filmId}`, requestOptions)
        let film = await responseFilm.json();
        console.log(film);

        app.dynamicView.insertAdjacentHTML('beforeend',`
        <div class="card my-3">
            <div class="card-header">
            Film
            </div>
            <div class="card-body">
                <h5 class="card-title">${film[0].name}</h5>
                <p class="card-text">
                    <strong>Regisör:</strong> ${film[0].director} </br>
                    <strong>Max antal hyrdagar:</strong> ${film[0].maxRentDays} dagar </br>
                    <strong>Releasedatum:</strong> ${film[0].releaseDate}</p>
                <a onclick="returnFilm('${film[0].filmId}')" class="btn btn-primary">Lämna tillbaka film</a>
            </div>
        </div>`);
    };
}

async function returnFilm(filmId)
{
    let myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${app.userToken}`);

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow'
      };

    let response = await fetch(`/api/Films/return?filmId=${filmId}&studioId=${app.userId}`, requestOptions)
    
    try {
        response = await response.json(); 
    } catch (error) {
        console.log(error)
    }
    getRentedFilms();
}

async function rentFilm(filmId)
{
    let ok = true;
    let myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${app.userToken}`);

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        redirect: 'follow'
      };

    
    let response = await fetch(`/api/Films/rent?filmId=${filmId}&studioId=${app.userId}`, requestOptions)
    try {
    if(response.ok){
        response = await response.json();
    } else throw new Error(response.status)

    } catch (error) {
        console.log(error)
        if (error == "Error: 403"){
            document.getElementById(`film-${filmId}`).innerText = "Du lånar redan denna filmen!";
            ok = false;
        }
        if (error == "Error: 409"){
            document.getElementById(`film-${filmId}`).innerText = "Det finns inga kopior kvar att låna av denna filmen!";
            ok = false;
        }
        if (ok){
            document.getElementById(`rented-${filmId}`).innerText = "LÅNAD";
        }
    }
}   

function checkExistingElement(elementId){ // Kollar om ett visst element (inmatad parameter) finns i DOM.
    let element = document.getElementById(elementId)
    if (typeof(element) != "undefined" && element != null){
        return true;
    }
        return false;
}

window = returnFilm;
windoe = rentFilm;