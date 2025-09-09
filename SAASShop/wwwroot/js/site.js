function changeSignature(name, sign) {
    document.getElementById(sign).value = document.getElementById(name).value
}

function SideBarToggle() {
    if (document.getElementById('menu-side').clientWidth == 200) {
        SetToMin();
    } else {
        SetToMax();
    }
}

function CreateCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function GetCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function SetMenuPosition() {
    var cName = "SideMenu";
    var cValue = GetCookie(cName);

    if (cValue == null) {
        CreateCookie(cName, "Max", 7);
    }

    if (cValue == "Max") {
        SetToMax();
    }
    else if (cValue == "Min") {
        SetToMin();
    }
}

function SetToMax() {
    document.getElementById("menu-side").style.width = "200px";
    document.getElementById("menu-head").style.width = "200px";
    var sideItems = document.getElementsByClassName('side-item'), i;
    for (var i = 0; i < sideItems.length; i++) {
        sideItems[i].style.display = 'inline-block';
    }
    document.getElementById('menu-side-icon').classList.add('glyphicon-chevron-right');
    document.getElementById('menu-side-icon').classList.remove('glyphicon-align-justify');
    CreateCookie("SideMenu", "Max", 7);
}

function SetToMin() {
    document.getElementById("menu-side").style.width = "50px";
    document.getElementById("menu-head").style.width = "50px";
    var sideItems = document.getElementsByClassName('side-item'), i;
    for (var i = 0; i < sideItems.length; i++) {
        sideItems[i].style.display = 'none';
    }
    document.getElementById('menu-side-icon').classList.remove('glyphicon-chevron-right');
    document.getElementById('menu-side-icon').classList.add('glyphicon-align-justify');
    CreateCookie("SideMenu", "Min", 7);
}