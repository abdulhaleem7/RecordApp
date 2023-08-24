function correctAllTimeTags() {
    var elements = document.getElementsByTagName("time");
    for (var pos in elements) {
        if (elements.hasOwnProperty(pos)) {
            var elem = elements[pos];
            const d = new Date(elem.innerHTML);
            if (!isNaN(d.getDate()))
            {
                elem.innerHTML = correctDate(d);
            }
        }
    }
}

function correctDate(d)
{
    const ye = new Intl.DateTimeFormat('en', { year: 'numeric' }).format(d);
    const mo = new Intl.DateTimeFormat('en', { month: 'numeric' }).format(d);
    const da = new Intl.DateTimeFormat('en', { day: '2-digit' }).format(d);

    return `${ye}-${mo}-${da}`;
}


setInterval(() => correctAllTimeTags(), 2000);