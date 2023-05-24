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

//// 거리뷰 레이어를 생성합니다.
//var streetLayer = new naver.maps.StreetLayer();
//streetLayer.setMap(map);

//// 거리뷰 버튼에 이벤트를 바인딩합니다.
//var btn = $('#street');
//btn.on("click", function (e) {
//    e.preventDefault();

//    // 거리뷰 레이어가 지도 위에 있으면 거리뷰 레이어를 지도에서 제거하고,
//    // 거리뷰 레이어가 지도 위에 없으면 거리뷰 레이어를 지도에 추가합니다.
//    if (streetLayer.getMap()) {
//        streetLayer.setMap(null);
//    } else {
//        streetLayer.setMap(map);
//    }
//});

//// 거리뷰 레이어가 변경되면 발생하는 이벤트를 지도로부터 받아 버튼의 상태를 변경합니다.
//naver.maps.Event.addListener(map, 'streetLayer_changed', function (streetLayer) {
//    if (streetLayer) {
//        btn.addClass('control-on');
//    } else {
//        btn.removeClass('control-on');
//    }
//});

//// 지도를 클릭했을 때 발생하는 이벤트를 받아 거리뷰를 표시합니다.
//naver.maps.Event.addListener(map, 'click', function (e) {
//    var latlng = e.coord;

//    // 주변의 거리뷰 정보를 가져옵니다.
//    // 주변의 거리뷰 정보를 가져옵니다.
//    naver.maps.Service.getPanoramaByLocation(latlng, 50, function (status, data) {
//        if (status === naver.maps.Service.Status.OK) {
//            if (data && data.result && data.result.panoId) {
//                var panoId = data.result.panoId;

//                // 거리뷰 표시를 위해 파노라마 객체를 생성하고 위치를 설정합니다.
//                var pano = new naver.maps.Panorama("pano", {
//                    position: latlng
//                });

//                // 파노라마 위치가 갱신되었을 때 발생하는 이벤트를 받아 지도의 중심 위치를 갱신합니다.
//                naver.maps.Event.addListener(pano, 'pano_changed', function () {
//                    var position = pano.getPosition();

//                    if (!position.equals(map.getCenter())) {
//                        map.setCenter(position);
//                    }
//                });

//                // 파노라마 표시
//                pano.setPano(panoId);
//            } else {
//                // 해당 위치에 거리뷰가 없는 경우에 대한 처리를 할 수 있습니다.
//                console.log("해당 위치에 거리뷰가 없습니다.");
//            }
//        } else {
//            // 주변의 거리뷰 정보를 가져오는데 실패한 경우에 대한 처리를 할 수 있습니다.
//            console.log("거리뷰 정보를 가져오는데 실패하였습니다.");
//        }
//    });
//});






animalHospitals.forEach(function (hospital) {
    var latitude = hospital.latitude;
    var longitude = hospital.longitude;
    var address = hospital.address;
    var name = hospital.name;
    var tel = hospital.sitetel
    var openClose = hospital.openClose;
    var distance = hospital.distance;

    //거리 계산
    distance = distance.toFixed(2);
    distance = distance + "km";

    // 동물병원 마커 옵션 설정
    var markerOptions = {
        position: new naver.maps.LatLng(latitude, longitude),
        map: map,
        icon: {
            url: 'https://cdn-icons-png.flaticon.com/512/1292/1292629.png',
            scaledSize: new naver.maps.Size(20, 20),
            origin: new naver.maps.Point(0, 0),
        }
    };

    //동물병원 마커 지도에 표시
    var marker = new naver.maps.Marker(markerOptions);

    var contentName = [
        '<div class="iw_innerName">',
        '   <h3>' + name + '</h3>',
        '</div>'
    ].join('');

    var contentString = [
        '<div class="iw_inner">',
        '   <h3>이름 : ' + name + '</h3>',
        '   <p>주소 : ' + address + '</p>',
        '   <p>영업여부 : ' + openClose + '</p>',
        '   <p>전화번호: ' + tel + '</p>',
        '   <p>거리: ' + distance + '</p>',
        '</div>'
    ].join('');


    var infowindow = new naver.maps.InfoWindow({
        content: contentName
    });

    naver.maps.Event.addListener(marker, "click", function (e) { // 마커를 클릭하면 정보가 표시된다.
        if (infowindow.getMap()) {
            infowindow.close();
        } else {
            infowindow.open(map, marker);
            document.getElementById("detail").innerHTML = contentString; // detail 요소에 contentString 할당
        }
    });
});

var marker = new naver.maps.Marker({ //사용자 현위치
    position: new naver.maps.LatLng(userLat, userLong),
    map: map,
    icon: {
        url: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRF1rdL5BciGb7ROzW5D5vht8Vn3XIuhEUxNA&usqp=CAU',
        scaledSize: new naver.maps.Size(50, 50),
        origin: new naver.maps.Point(0, 0),
    }
});