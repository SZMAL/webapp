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