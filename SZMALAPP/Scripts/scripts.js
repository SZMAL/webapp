function showRegisterPopup() {
	document.getElementById('register-wrapper').style.display='block'
}

function showLoginPopup() {
	document.getElementById('login-wrapper').style.display='block'
}


function showAboutPopup() {
    document.getElementById('about-wrapper').style.display = 'block'
}

function openFacebook() {
    var win = window.open("https://www.facebook.com/SZMALCompany", '_blank');
    win.focus();
}

function getLocation() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    }
    else {
        x.innerHTML = "Geolokalizacja nie jest wpierana przez przegl¹darkê!";
    }
}

function showPosition(position) {

    //Poni¿ej pobieramy wspó³rzêdne geograficznej lokalizacji w której siê znajdujemy
    $("#coordinates").append("Szerokoœæ geograficzna: " + position.coords.latitude +
        "<br>D³ugoœæ geograficzna: " + position.coords.longitude);

    // Wspó³rzêdne geograficznej przygotowujemy w formacie akceptowalny przez API
    var latlon = position.coords.latitude + "," + position.coords.longitude;

    // Tworzymy statyczny obrazek korzystaj¹c z Maps Static API do³¹czaj¹c zdefiniowan¹ lokalizacjê oraz...
    // kilka parametrów, tj. przybli¿enie oraz nasz prywatny klucz
    var image_url = "https://maps.googleapis.com/maps/api/staticmap?center=" + latlon + " &zoom=14&key=AIzaSyDHwdYW-xzRa23-EuuiaKSqcoy2x7Lspwo";

    // wygenerowany powy¿ej adres wstrzykujemy do naszego kontenera odpowiedzialnego za wyœwietlanie mapy
    document.getElementById("map").innerHTML = "<img src='" + image_url + "'>";

    // Definiujemy obiekt przechowuj¹cy nasze po³o¿enie...
    var centerLocation = { lat: position.coords.latitude, lng: position.coords.longitude };

    // Ustawiamy punkt centralny mapy na nasz¹ obecn¹ lokalizacjê
    var map = new google.maps.Map(
        document.getElementById('map'), { zoom: 14, center: centerLocation });

    // Oznaczamy nasz punkt za pomoc¹ markera
    var marker = new google.maps.Marker({ position: centerLocation, map: map });
}