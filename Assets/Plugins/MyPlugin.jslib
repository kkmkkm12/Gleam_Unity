// Module 객체가 정의되어 있는지 확인하고, 없으면 초기화
var Module = Module || {};

// mergeInto를 사용하여 라이브러리 기능 추가
mergeInto(LibraryManager.library, {
  // 상태 변수 추가
  naverMapVisible: true,
  kakaoMapVisible: true,
  map: null, // 전역 변수 선언

  InitNaverMap: function () {
    // 네이버 지도 초기화 코드
    var mapElement = document.getElementById("map");

    if (mapElement) {
      console.log(mapElement.style.display);
      mapElement.style.display = "block"; // 지도를 보이게
      map = new naver.maps.Map(mapElement, {
        center: new naver.maps.LatLng(37.3700065, 127.115),
        zoom: 14,
      });
      console.log(mapElement.style.display);
      console.log("네이버지도 초기화 완료");

      // 지도 클릭 시 숨기기 / 보이기
      mapElement.addEventListener("click", function () {
        console.log(this.style.display);

        if (this.style.display === "none") {
          this.style.display = "block"; // 지도를 보이게
          console.log("지도가 보였습니다.");
        } else {
          this.style.display = "none"; // 지도를 숨김
          console.log("지도가 닫혔습니다.");
          console.log(this.style.display);
        }
      });
    } else {
      console.error("네이버지도 요소를 찾을 수 없습니다.");
    }
  },

  AddNaverPolyline: function (pathPtr, size) {
    var polylinePath = [];
    for (var i = 0; i < size; i += 2) {
      var lat = HEAPF64[(pathPtr >> 3) + i]; // 위도
      var lng = HEAPF64[(pathPtr >> 3) + i + 1]; // 경도
      polylinePath.push(new naver.maps.LatLng(lat, lng));
    }

    var polyline = new naver.maps.Polyline({
      map: map,
      path: polylinePath,
      strokeColor: "#FF0000",
      strokeWeight: 5,
    });
    console.log("폴리라인 추가 완료");
  },

  InitKakaoMap: function () {
    var mapContainer = document.getElementById("map"); // 지도를 표시할 div
    if (mapContainer) {
      var mapOption = {
        center: new kakao.maps.LatLng(37.371657839593894, 127.11645126342773), // 지도의 중심좌표
        level: 6, // 지도의 확대 레벨
      };
      map = new kakao.maps.Map(mapContainer, mapOption); // 전역 변수로 사용
      console.log("카카오지도 초기화 완료");

      // 지도 클릭 시 숨기기 / 보이기
      mapContainer.addEventListener("click", function () {
        if (this.style.display === "none") {
          this.style.display = "block"; // 지도를 보이게
          kakaoMapVisible = true; // 상태 업데이트
          console.log("지도가 보였습니다.");
        } else {
          this.style.display = "none"; // 지도를 숨김
          kakaoMapVisible = false; // 상태 업데이트
          console.log("지도가 닫혔습니다.");
        }
      });
    } else {
      console.error("카카오지도 요소를 찾을 수 없습니다.");
    }
  },

  AddKakaoPolyline: function (pathPtr, size) {
    var polylinePath = [];
    for (var i = 0; i < size; i += 2) {
      var lat = HEAPF64[(pathPtr >> 3) + i]; // 위도
      var lng = HEAPF64[(pathPtr >> 3) + i + 1]; // 경도
      polylinePath.push(new kakao.maps.LatLng(lat, lng)); // 좌표 추가
    }

    var polyline = new kakao.maps.Polyline({
      path: polylinePath,
      strokeWeight: 5,
      strokeColor: "#FFAE00",
      strokeOpacity: 0.7,
      strokeStyle: "solid",
    });

    polyline.setMap(map);
    console.log("폴리라인 추가 완료");
  },

  GetOSRMRoute: function (originLonPtr, originLatPtr, destLonPtr, destLatPtr) {
    var originLon = Pointer_stringify(originLonPtr);
    var originLat = Pointer_stringify(originLatPtr);
    var destLon = Pointer_stringify(destLonPtr);
    var destLat = Pointer_stringify(destLatPtr);

    getRoute(originLon, originLat, destLon, destLat)
      .then(function (result) {
        //유니티로 전달
        // var jsonResult = JSON.stringify(result);
        // Module['SendMessage']('YourGameObjectName', 'ReceiveRouteData', jsonResult);

        // 결과를 HTML 페이지에 표시하기 위한 함수 호출
        Module.displayRouteOnMap(result); // 수정된 부분
        //displayRouteOnMap(result);
      })
      .catch(function (error) {
        console.error("Error fetching route:", error);
      });
  },

  displayRouteOnMap: function (route) {
    // Leaflet 지도 초기화
    const map = L.map("map").setView(
      [
        (route.startPoint[0] + route.endPoint[0]) / 2,
        (route.startPoint[1] + route.endPoint[1]) / 2,
      ],
      13
    );

    // OpenStreetMap 타일 레이어 추가
    L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
      maxZoom: 19,
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
    }).addTo(map);

    // 출발지 및 도착지 마커 추가
    const startPoint = [route.startPoint[0], route.startPoint[1]];
    const endPoint = [route.endPoint[0], route.endPoint[1]];

    L.marker(startPoint).addTo(map).bindPopup("대덕비즈센터").openPopup();
    L.marker(endPoint)
      .addTo(map)
      .bindPopup("IMC중부권광역 우편물류센터")
      .openPopup();

    const distanceContent =
      "<p>현재 길 남은 거리: " + (route.distance / 1000).toFixed(2) + " km</p>";
    let alternativeDistanceContent = "";
    if (route.alternativeDistance) {
      alternativeDistanceContent +=
        "<p>다른 길찾기의 거리: " +
        (route.alternativeDistance / 1000).toFixed(2) +
        " km</p>";
    }

    const popup = L.popup({ offset: L.point(0, -35) })
      .setLatLng(startPoint)
      .setContent(distanceContent + alternativeDistanceContent)
      .openOn(map);

    const routePoints = route.route;
    L.polyline(routePoints, { color: "blue", weight: 8, opacity: 0.6 }).addTo(
      map
    );

    const alternativeRoutePoints = route.alternativeRoute;
    if (alternativeRoutePoints.length > 0) {
      L.polyline(alternativeRoutePoints, {
        color: "gray",
        weight: 6,
        opacity: 0.5,
      }).addTo(map);
    }
  },
});

// displayRouteOnMap을 Module에 추가
Module["displayRouteOnMap"] = Module.displayRouteOnMap; // 올바르게 할당
