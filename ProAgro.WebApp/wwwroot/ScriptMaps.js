function initMap() {
    console.log("in init map")
}
window.initialize = async function (ltlist, lgtlist) {
    var myLatlng1 = new google.maps.LatLng(ltlist[0], lgtlist[0]);
    console.log(myLatlng1);
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 4,
        center: myLatlng1
    });

    Object.entries(ltlist).forEach(([key, value]) => {
        Object.entries(ltlist).forEach(([key2, value2]) => {
            if (key == key2) {
                console.log("value")
                console.log(value)
                console.log("value2")
                console.log(value2)
                const myLatLng = { lat: value, lng: value2 };
                new google.maps.Marker({
                    position: myLatLng,
                    map,
                    title: "Hello World!",
                });
            }
        })
    })
    
    
    
    initMap();
    console.log("displaying map");
}

window.AlertSuccess = async function () {
    await Swal.fire({
        position: 'center',
        title: "Usuario autenticado",
        timmer: 200
    });
}

window.GetMarkers = function () {
    new google.maps.Marker({
        position: myLatLng,
        map,
        title: "Hello World!",
    });
}