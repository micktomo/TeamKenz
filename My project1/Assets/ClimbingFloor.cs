using UnityEngine;

public class ClimbingFloor : MonoBehaviour
{
    [Header("足場の設定")]
    public GameObject platformPrefab;
    public int platformCount = 20;

    [Header("床の横幅（Xスケール）")]
    public float minWidth = 1f;
    public float maxWidth = 4f;

    [Header("フィールド内横方向の位置変化")]
    public float minXOffset = -2f;
    public float maxXOffset = 2f;

    [Header("下段からの高さ")]
    public float minYStep = 2f;     // 最低ジャンプ高さ（ジャンプ可能な最小段差）
    public float maxYStep = 3f;     // 最大ジャンプ高さ（プレイヤーがギリ届く）

    [Header("開始位置")]
    public Vector3 startPosition = Vector3.zero;

    void Start()
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {

        Vector3 currentPos = startPosition;

        for (int i = 0; i < platformCount; i++)
        {
            float randomWidth = Random.Range(minWidth, maxWidth);// フィールド内の床の長さをランダム生成
            float xOffset = Random.Range(minXOffset, maxXOffset);// フィールド内の床の位置を横方向にランダム生成
            float yStep = Random.Range(minYStep, maxYStep); // 下段との距離を制御

            currentPos += new Vector3(xOffset, yStep, 0f);//currentPosは、下段の足場の位置。どこにどうずらすか設定

            GameObject platform = Instantiate(platformPrefab, currentPos, Quaternion.identity);//platformPrefab→Cube Quaternion.identity→足場は回転させない

            Vector3 newScale = platform.transform.localScale;
            newScale.x = randomWidth;
            platform.transform.localScale = newScale;
        }
    }
}
