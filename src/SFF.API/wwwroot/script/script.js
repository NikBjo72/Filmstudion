console.log("Hej html!")

const app = {
    userToken: "",
    userId: "",
    userName: "",
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
    loginField: document.getElementById("loginField")
};

//app.filmLibraryBtn.hidden = false;

app.filmLibraryBtn.addEventListener('click', async function()
{
    getAllFilms();
});

app.rentedFilmsBtn.addEventListener('click', async function()
{
    getRentedFilms();
});

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
    data = await response.json();

    //console.log(data);
    app.userToken = data.token;
    app.userId = data.filmStudioId;
    app.userName = data.userName;
    //console.log(app.userToken);
    app.rentedFilmsBtnField.hidden = false;
    app.logoutBtnField.hidden = false;
    app.loginField.hidden = true;
});

//Loggar ut användare
app.logoutBtn.addEventListener('click', function() {
    app.logoutBtnField.hidden = true;
    app.loginField.hidden = false;
    app.rentedFilmsBtnField.hidden = true;
    app.userToken = "";
    app.password.value = "";
    app.userName.value ="";
    console.log(app.userToken);
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
    console.log(filmInfo);
    app.dynamicView.innerHTML = "";

    for (let i = 0; i < filmInfo.length; i++)
    {  
        app.dynamicView.insertAdjacentHTML('beforeend',`
        <div class="card my-3">
            <div class="card-header">
                Film
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
        document.getElementById(`film-${filmId}`).innerText = "Du hyr redan denna filmen!";
        }
        if (error == "Error: 409"){
            document.getElementById(`film-${filmId}`).innerText = "Det finns inga kopior kvar att låna av denna filmen!";
        }
    }
}

window = returnFilm;
windoe = rentFilm;