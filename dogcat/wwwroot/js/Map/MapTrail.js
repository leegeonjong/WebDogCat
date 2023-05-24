var mapOptions = {
    center: new naver.maps.LatLng(userLat, userLong),
    zoom: 14,
    mapTypeControl: true,
    mapTypeControlOptions: {
        style: naver.maps.MapTypeControlStyle.DROPDOWN
    },
    scaleControl: false,
    logoControl: false,
    mapDataControl: false,
    zoomControl: true,
    minZoom: 6,

};

var map = new naver.maps.Map('map', mapOptions);

var bicycleLayer = new naver.maps.BicycleLayer();

var btn = $('#bicycle');

naver.maps.Event.addListener(map, 'bicycleLayer_changed', function (bicycleLayer) {
    if (bicycleLayer) {
        btn.addClass('control-on');
    } else {
        btn.removeClass('control-on');
    }
});

btn.on("click", function (e) {
    e.preventDefault();

    if (bicycleLayer.getMap()) {
        bicycleLayer.setMap(null);
    } else {
        bicycleLayer.setMap(map);
    }
});

naver.maps.Event.once(map, 'init', function () {
    bicycleLayer.setMap(map);
});
