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
        x.innerHTML = "Geolokalizacja nie jest wpierana przez przegl�dark�!";
    }
}

function showPosition(position) {

    //Poni�ej pobieramy wsp�rz�dne geograficznej lokalizacji w kt�rej si� znajdujemy
    $("#coordinates").append("Szeroko�� geograficzna: " + position.coords.latitude +
        "<br>D�ugo�� geograficzna: " + position.coords.longitude);

    // Wsp�rz�dne geograficznej przygotowujemy w formacie akceptowalny przez API
    var latlon = position.coords.latitude + "," + position.coords.longitude;

    // Tworzymy statyczny obrazek korzystaj�c z Maps Static API do��czaj�c zdefiniowan� lokalizacj� oraz...
    // kilka parametr�w, tj. przybli�enie oraz nasz prywatny klucz
    var image_url = "https://maps.googleapis.com/maps/api/staticmap?center=" + latlon + " &zoom=14&key=AIzaSyDHwdYW-xzRa23-EuuiaKSqcoy2x7Lspwo";

    // wygenerowany powy�ej adres wstrzykujemy do naszego kontenera odpowiedzialnego za wy�wietlanie mapy
    document.getElementById("map").innerHTML = "<img src='" + image_url + "'>";

    // Definiujemy obiekt przechowuj�cy nasze po�o�enie...
    var centerLocation = { lat: position.coords.latitude, lng: position.coords.longitude };

    // Ustawiamy punkt centralny mapy na nasz� obecn� lokalizacj�
    var map = new google.maps.Map(
        document.getElementById('map'), { zoom: 14, center: centerLocation });

    // Oznaczamy nasz punkt za pomoc� markera
    var marker = new google.maps.Marker({ position: centerLocation, map: map });
}