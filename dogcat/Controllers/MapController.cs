using Microsoft.AspNetCore.Mvc;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace dogcat.Controllers
{
    public class AnimalHospital //동물병원 정보 클래스
    {
        public string Name { get; set; } //이름
        public string Address { get; set; } //주소
        public double Latitude { get; set; } //위도
        public double Longitude { get; set; } //경도
        public double Distance { get; set; } //거리
        public string OpenClose { get; set; } //영업상태
    }

    public class MapController : Controller
    {
        private const string KakaoRestId = "28cb4efa020eb94b32a4456b6de26ecd";
        private const string filePath = "JsonData/AnimalHospital.Json"; //Json 파일 경로
        public double distance; //거리
        double UserLat, UserLong; //사용자가 입력한 위치 위도경도
        double latitude, longitude; // 위도 경도 

        List<AnimalHospital> animalHospitals = new List<AnimalHospital>(); //동물병원 정보 클래스의 목록을 담을 리스트
        public IActionResult MapSelectView() // 지도 선택하기
        {
            return View();
        }

        public async Task<IActionResult> MapAnimalHospital(string address)
        {
            await ChangeCoordinate(address);
            ViewBag.UserLat = UserLat;
            ViewBag.UserLong = UserLong;
            await GetNearbyAnimalHospitals();
            ViewBag.AnimalHospitals = animalHospitals;
            return View();
        }


        public async Task<IActionResult> MapTrailAsync(string address) //산책로 보기
        {
            await ChangeCoordinate(address);
            ViewBag.UserLat = UserLat;
            ViewBag.UserLong = UserLong;
            return View();
        }

        private async Task<List<AnimalHospital>> GetNearbyAnimalHospitals()
        {
            // 파일 읽기
            string jsonData = System.IO.File.ReadAllText(filePath);

            // JSON 데이터 파싱
            JObject json = JObject.Parse(jsonData);

            // 동물병원 데이터 배열 추출
            JArray hospitals = (JArray)json["DATA"];

            foreach (JObject hospital in hospitals)
            {
                //"SITEPOSTNO":"소재지우편번호",
                //"RGTMBDSNO":"권리주체일련번호",
                //"TOTEPNUM":"총인원",
                //"UPTAENM":"업태구분명",
                //"LASTMODTS":"최종수정일자",
                //"CLGENDDT":"휴업종료일자",
                //"UPDATEGBN":"데이터갱신구분",
                //"RDNWHLADDR":"도로명주소", 
                //"DCBYMD":"폐업일자",
                //"SITEWHLADDR":"지번주소",
                //"Y":"좌표정보(Y)",
                //"X":"좌표정보(X)",
                //"DTLSTATEGBN":"상세영업상태코드",
                //"TRDSTATEGBN":"영업상태코드",
                //"OPNSFTEAMCODE":"개방자치단체코드",
                //"APVPERMYMD":"인허가일자",
                //"LINDSEQNO":"축산일련번호"
                //"UPDATEDT":"데이터갱신일자"
                //"LINDPRCBGBNNM":"축산물가공업구분명"
                //,"BPLCNM":"사업장명",
                //"CLGSTDT":"휴업시작일자"
                string hospitalName = (string)hospital["bplcnm"];//동물병원이름
                string hospitalAddress = (string)hospital["sitewhladdr"];//지번주소
                string hospitalOpenClose = (string)hospital["trdstatenm"];//영업 여부
                double hospitalLatitude = (double)hospital["lat"];
                double hospitalLongitude = (double)hospital["long"];

                //거리 추출 함수 실행 -- 사용자의 현위치와 병원의 좌표를 계산해서 구하기
                await CalculateDistance(UserLat, UserLong, hospitalLatitude, hospitalLongitude);
                animalHospitals.Add(new AnimalHospital
                {
                    Name = hospitalName,
                    Address = hospitalAddress,
                    Latitude = hospitalLatitude,
                    Longitude = hospitalLongitude,
                    OpenClose = hospitalOpenClose,
                    Distance = distance,
                });
            }

            // 가까운 순서로 동물병원 목록 정렬
            animalHospitals = animalHospitals.OrderBy(h => h.Distance).Take(100).ToList();

            return animalHospitals;
        } //사용자 기준 가까운 병원 5개 가져오기

        private async Task CalculateDistance(double latitude1, double longitude1, double latitude2, double longitude2) //거리계산하기 
        {
            const double EarthRadiusKm = 6371; // 지구 반지름 (단위: km)

            // 각도를 라디안으로 변환
            double lat1Rad = DegreesToRadians(latitude1);
            double lon1Rad = DegreesToRadians(longitude1);
            double lat2Rad = DegreesToRadians(latitude2);
            double lon2Rad = DegreesToRadians(longitude2);

            // 위도와 경도의 차이 계산
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // 위도와 경도 차이의 제곱 합 구하기
            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(deltaLon / 2), 2);

            // 중심각 계산
            double centralAngle = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            // 거리 계산 (단위: km)
            distance = EarthRadiusKm * centralAngle;
        }

        private double DegreesToRadians(double degrees) //각도를 라디안으로 변환
        {
            return degrees * Math.PI / 180;
        }

        private async Task ChangeCoordinate(string address) //주소의 좌표를 구하기
        {
            string url = $"https://dapi.kakao.com/v2/local/search/address.json?query={Uri.EscapeDataString(address)}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "KakaoAK " + KakaoRestId);

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(responseBody);

                    JArray documents = (JArray)json["documents"];
                    if (documents.Count > 0)
                    {
                        UserLong = (double)documents[0]["x"];
                        UserLat = (double)documents[0]["y"];
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}



