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



function getLocationToChoosePlace() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(choosePlace);
    }
    else {
        x.innerHTML = "Geolokalizacja nie jest wpierana przez przegl¹darkê!";
    }
}

function choosePlace(position) {
   
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
        var latlng = new google.maps.LatLng(lat, lng);
        geocoder.geocode({ 'latLng': latlng },
            function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        infowindow.setContent(results[0].formatted_address);
                        infowindow.open(map, marker);
                        var s = document.getElementById('geo-width');
                        s.value = lat;
                        var t = document.getElementById('geo-height');
                        t.value = lng;
                        var u = document.getElementById('address');
                        u.value = results[0].formatted_address;
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

function getLocationForEvent() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showEventOnMap);
    }
    else {
        x.innerHTML = "Geolokalizacja nie jest wpierana przez przegl¹darkê!";
    }
}

function showEventOnMap() {

    var centerLocation = { lat: position.coords.latitude, lng: position.coords.longitude };

    // Ustawiamy punkt centralny mapy na nasz¹ obecn¹ lokalizacjê
    var map = new google.maps.Map(
        document.getElementById('right-box-map'), { zoom: 14, center: centerLocation, mapTypeControl: false });


    //!!!!!BARTEK!!! Tutaj potrzebuje szerokosci i d³ugosci danego zdarzenia
    var eventPosition = new google.maps.LatLng(52.242399, 20.874975);
}