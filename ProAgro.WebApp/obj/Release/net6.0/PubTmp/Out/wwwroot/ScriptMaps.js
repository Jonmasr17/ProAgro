function initMap() {
    console.log("in init map")
}
window.initialize = async function () {
    const myLatLng = { lat: -25.363, lng: 131.044 };
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 4,
        center: myLatLng,
    });

    new google.maps.Marker({
        position: myLatLng,
        map,
        title: "Hello World!",
    });
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