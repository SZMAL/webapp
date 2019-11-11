function showRegisterPopup() {
    document.getElementById('register-wrapper').style.display = 'block'
}

function showLoginPopup() {
    document.getElementById('login-wrapper').style.display = 'block'
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
        document.getElementById('map'), { zoom: 14, center: centerLocation, mapTypeControl: false });
    var southWest = new google.maps.LatLng(52.242399, 20.874975);
    var northEast = new google.maps.LatLng(52.258082, 20.912744);
    var lngSpan = northEast.lng() - southWest.lng();
    var latSpan = northEast.lat() - southWest.lat();
    // Oznaczamy nasz punkt za pomoc¹ markera
    var iconBase = 'https://developers.google.com/maps/documentation/javascript/examples/full/images/';




    //!!!!!BARTEK!!!!!! daj mi tutaj jakiœ vector albo liste zg³oszeñ z bazy. Najlepiej tylko tych potwierdzonych




    for (var i = 1; i < 5; i++) {
        // init markers
        var marker = new google.maps.Marker({
            position: new google.maps.LatLng(southWest.lat() + latSpan * Math.random(), southWest.lng() + lngSpan * Math.random()),
            map: map,
            title: 'Zobacz szczegó³y',
            icon: 'http://maps.google.com/mapfiles/ms/icons/blue-dot.png'
        });

        // process multiple info windows
        (function (marker, i) {
            // add click event
            google.maps.event.addListener(marker, 'click', function () {

                infowindow = new google.maps.InfoWindow({
                    content: 'Jestem tutaj',

                });

                infowindow.open(map, marker);
            });
        })(marker, i);
    }

}

function getLocationToChoosePlace() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(choosePlace);
    }
    else {
        x.innerHTML = "Geolokalizacja nie jest wpierana przez przegl¹darkê!";
    }
}

function choosePlace(position) {
    $("#coordinates").append("Szerokoœæ geograficzna: " + position.coords.latitude +
        "<br>D³ugoœæ geograficzna: " + position.coords.longitude);
    var latlon = position.coords.latitude + "," + position.coords.longitude;
    var image_url = "https://maps.googleapis.com/maps/api/staticmap?center=" + latlon + " &zoom=14&key=AIzaSyDHwdYW-xzRa23-EuuiaKSqcoy2x7Lspwo";
    document.getElementById("form-map").innerHTML = "<img src='" + image_url + "'>";
    var centerLocation = { lat: position.coords.latitude, lng: position.coords.longitude };
    var map = new google.maps.Map(
        document.getElementById('form-map'), { zoom: 14, center: centerLocation, disableDoubleClickZoom: true });

    var geocoder = new google.maps.Geocoder();
    var infowindow = new google.maps.InfoWindow;
    var marker;
    marker = new google.maps.Marker({
        position: centerLocation,
        map: map
    });
    map.addListener('click', function (e) {
        placeMarker(e.latLng, map);
    });

    var lat, lng;
    function placeMarker(position, map) {
        if (marker) {
            marker.setPosition(position);
        }
        else {
            marker = new google.maps.Marker({
                position: position,
                map: map,
                title: 'Ustaw to miejsce'
            });
        }
        lat = marker.getPosition().lat();
        lng = marker.getPosition().lng();

        ///!!!!!!BARTEK !!!!!! lat i lng to szerokosc i d³ugoœæ, trzeba to wprowadziæ do bazy
        var latlng = new google.maps.LatLng(lat, lng);
        geocoder.geocode({ 'latLng': latlng },
            function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        infowindow.setContent(results[0].formatted_address);
                        infowindow.open(map, marker);
                        //!!!!BARTEK!!!! result[0] to adres. Adres bedzie teraz jednym ciagiem stringów
                        //nie bedziemy go rozbijac na ulice itp. trzeba to wprowadzic do bazy 
                    }
                    else {
                        alert('No results found');
                    }
                }
                else {
                    alert('Geocoder failed due to: ' + status);
                }
            });
        map.panTo(position);
    }
}




function showEventOnMap() {
    //!!!!!BARTEK!!! Tutaj potrzebuje szerokosci i d³ugosci danego zdarzenia
    var lat1 = 52.242399;
    var lng1 = 20.874975;
    var position = new google.maps.LatLng(lat, lng);
    var image_url = "https://maps.googleapis.com/maps/api/staticmap?center=" + latlon + " &zoom=14&key=AIzaSyDHwdYW-xzRa23-EuuiaKSqcoy2x7Lspwo";

   
    document.getElementById("right-box-map").innerHTML = "<img src='" + image_url + "'>";
    var map = new google.maps.Map(
        document.getElementById('right-box-map'), { zoom: 14, center: { lat: lat1, lng: lng1 }, mapTypeControl: false });
   
    var iconBase = 'https://developers.google.com/maps/documentation/javascript/examples/full/images/';

    marker = new google.maps.Marker({
        position: position,
        map: map,
        title: 'Ustaw to miejsce'
    });
 

}
