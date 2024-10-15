// hariboTestOSRM.js
// Axios와 Polyline을 전역으로 사용하기 위해 CDN에서 로드해야 함
// 예를 들어, index.html에서 아래와 같이 추가합니다.
// <script src="https://cdn.jsdelivr.net/npm/axios/dist/axios.min.js"></script>
// <script src="https://unpkg.com/@mapbox/polyline@1.0.0/index.js"></script>

// 위도, 경도값으로 경로 정보를 가져오는 함수(OSRM서버에 요청함)
async function getRoute(originLon, originLat, destLon, destLat) {
    const loc = `${originLon},${originLat};${destLon},${destLat}`;
    const url = `http://localhost:5000/route/v1/driving/${loc}?alternatives=true&overview=full`; 

    try { 
        const response = await axios.get(url);
        const res = response.data;

        const numberOfRoutes = res.routes.length; // 경로의 수
        const numberOfPoints = res.routes[0] ? polyline.decode(res.routes[0].geometry).length : 0; // 첫 번째 경로의 점 수

        console.log(`Number of routes: ${numberOfRoutes}`);
        console.log(`Number of points in the first route: ${numberOfPoints}`);

        const routes = polyline.decode(res.routes[0].geometry); 
        const alternativeRoutes = res.routes.length > 1 ? polyline.decode(res.routes[1].geometry) : []; 
        const startPoint = [res.waypoints[0].location[1], res.waypoints[0].location[0]];
        const endPoint = [res.waypoints[1].location[1], res.waypoints[1].location[0]];
        const distance = res.routes[0].distance;
        const alternativeDistance =  res.routes.length > 1 ? res.routes[1].distance : []; 

        return { 
            route: routes,
            alternativeRoute: alternativeRoutes,
            startPoint: startPoint,
            endPoint: endPoint,
            distance: distance,
            alternativeDistance: alternativeDistance
        };

    } catch (error) {
        console.error("Error fetching route:", error.message);
        return {};
    }
}

// 이 파일을 전역에서 사용할 수 있도록 export하는 대신 스크립트 내부에서 직접 호출하도록 변경

