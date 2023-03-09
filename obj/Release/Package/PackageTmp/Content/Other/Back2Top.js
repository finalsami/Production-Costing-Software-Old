var t1 = 0;
window.onscroll = scroll1;

function scroll1() {
    var toTop = document.getElementById('toTop');
    window.scrollY > 0 ? toTop.style.display = 'Block' : toTop.style.display = 'none';
}

function abcd() {
    var y1 = window.scrollY;
    y1 = y1 - 1000;
    window.scrollTo(0, y1);
    window.scrollTo({ top: 0, behavior: 'smooth' });

    if (y1 > 0) {
        t1 = setTimeout("abcd()", 100);
    } else {
        clearTimeout(t1);
    }
}