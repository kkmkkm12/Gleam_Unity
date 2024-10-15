using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MapTileLoader : MonoBehaviour
{
    public float latitude = 36.43236669162717f;
    public float longitude = 127.39501476287842f;
    public int zoomLevel = 5;

    // private bool isDragging = false; // 마우스 드래그 됐는지 안됐는지
    // private Vector3 lastMousePosition; // 마지막 마우스 위치


    private string baseUrl = "http://192.168.0.168:8080/tile/";
    private GameObject currentTileObject;
    public GameObject tilePrefab; // 타일 프리펩을 저장할 변수

    void Start()
    {
        LoadTileAtCurrentPosition();
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        // {
        //     isDragging = true;
        //     lastMousePosition = Input.mousePosition; 
        // }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     isDragging = false;
        //     LoadNewTile(); 
        // }

        // if (isDragging) // 드래그 중일 때 타일을 이동시킴
        // {
        //     Vector3 delta = Input.mousePosition - lastMousePosition; // 마우스 이동 거리 계산
        //     latitude -= delta.y * 0.01f; // 위도 조정
        //     longitude -= delta.x * 0.01f; // 경도 조정
        //     lastMousePosition = Input.mousePosition; 
        // }

        //Haribo: 키보드로 줌인 줌아웃 하고 싶을때
        // if (Input.GetKeyDown(KeyCode.UpArrow))

        // 마우스 스크롤로 입력 감지 //출처: https://ddecode.tistory.com/entry/C-%EC%9C%A0%EB%8B%88%ED%8B%B0%EC%97%90%EC%84%9C-%EB%A7%88%EC%9A%B0%EC%8A%A4-%ED%9C%A0%EB%A1%9C-%EC%B9%B4%EB%A9%94%EB%9D%BC%EB%A5%BC-%EB%8F%99%EC%9E%91%ED%95%98%EB%8A%94-%EB%B0%A9%EB%B2%95
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0f)
        {
            zoomLevel++;
            LoadNewTile();
        }
        // else if (Input.GetKeyDown(KeyCode.DownArrow))
        else if (scrollInput < 0f)
        {
            zoomLevel--;
            LoadNewTile();
        }

        zoomLevel = Mathf.Clamp(zoomLevel, 0, 18); // 줌레벨 제한걸기
    }

    void LoadNewTile()
    {
        if (currentTileObject != null)
        {
            Destroy(currentTileObject);
        }
        LoadTileAtCurrentPosition();
    }

    void LoadTileAtCurrentPosition()
    {
        int tileX = LongitudeToTileX(longitude, zoomLevel);
        int tileY = LatitudeToTileY(latitude, zoomLevel);
        StartCoroutine(LoadTile(tileX, tileY, zoomLevel));
    }

    // 경도와 위도를 기반으로 타일 좌표 계산해주는 함수
    int LongitudeToTileX(float lon, int zoom)
    {
        return (int)((lon + 180.0) / 360.0 * Mathf.Pow(2, zoom));
    }

    int LatitudeToTileY(float lat, int zoom)
    {
        float latRad = lat * Mathf.Deg2Rad;
        return (int)((1.0f - Mathf.Log(Mathf.Tan(latRad) + 1.0f / Mathf.Cos(latRad)) / Mathf.PI) / 2.0f * Mathf.Pow(2, zoom));
    }

    IEnumerator LoadTile(int x, int y, int z)
    {
        string url = $"{baseUrl}{z}/{x}/{y}.png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            webRequest.SetRequestHeader("Origin", "http://192.168.0.168:3000"); // Origin 헤더 추가. 이걸 해줘야 cors에러 안뜸
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"타일서버에서 로딩이 실패했습니다.: {webRequest.error}");
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                ApplyTileTexture(texture);
            }
        }
    }

    // 타일을 프리펩으로 적용
    void ApplyTileTexture(Texture2D texture)
    {
        currentTileObject = Instantiate(tilePrefab, transform); // 어디 밑으로 생성할지는 transform 이부분 변경하면 됨
        currentTileObject.transform.localScale = new Vector3(1, 1, 1);

        // SpriteRenderer 사용할때
        currentTileObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        // MeshRenderer 사용할때
        //currentTileObject.GetComponent<Renderer>().material.mainTexture = texture;
    }
}
