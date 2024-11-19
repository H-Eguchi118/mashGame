using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // メインカメラ（インスペクタで設定）

    public List<GameObject> bluePrefabs; // 青花のPrefabリスト
    public List<GameObject> glayPrefabs; // 灰花のPrefabリスト
    public List<GameObject> orangePrefabs; // 朱花のPrefabリスト
    public List<GameObject> whitePrefabs; // 白花のPrefabリスト

    public float spawnOffset = 2.0f; // カメラ端から少し外側に生成するオフセット

    public float blueFixedY; // 青い花のY座標
    public float glayFixedY; // 灰花のY座標
    public float orangeFixedY; // 朱色花のY座標
    public float whiteFixedY; // 白い花のY座標

    public float cameraX = 0.0f;
    public float cameraY = 0.0f;
    public float triggerGlayXPosission;
    public float triggerOrangeXPosission;
    public float triggerWhiteXPosission;
    public float triggerStopXPosission;

    public Vector2 spawnIntervalRange = new Vector2(0.5f, 1.5f); // ランダムな生成間隔の範囲（秒）

    void Start()
    {

        // ランダム生成を開始
        StartCoroutine(SpawnItemsRandomly());
    }

    private void Update()
    {
        cameraX = mainCamera.transform.position.x;
        cameraY= mainCamera.transform.position.y;   

    }

    IEnumerator SpawnItemsRandomly()
    {
        while (true)
        {
            // ランダムな時間を待機
            float waitTime = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            yield return new WaitForSeconds(waitTime);

            if (cameraX <= triggerStopXPosission)
            {
                // アイテムを生成
                SpawnBlueFlower();
                SpawnGlayFlower();
                SpawnOrangeFlower();
                SpawnWhiteFlower();

            }
        }
    }

    //レイキャストでタイルマップの地面座標を取得
    private float GetGoundY(float x, float defaultY)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, defaultY), Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return hit.point.y;//地面のY座標を返す
        }
        return Mathf.NegativeInfinity;//Groundが見つからない場合

    }
    private void SpawnBlueFlower()
    {

        // カメラの右端の座標を計算し、生成位置を決定
        float spawnX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x + spawnOffset;

        //レイキャストを飛ばしてY座標を決定
        float rayFiixedBrue = GetGoundY(spawnX, blueFixedY);
        Debug.Log("青のレイキャストの位置" + rayFiixedBrue);
        if (rayFiixedBrue == Mathf.NegativeInfinity && rayFiixedBrue < -1) return;//Groudが見つからない＆指定のY値以下の場合は生成しない
        Vector3 spawnPosition = new Vector3(spawnX, rayFiixedBrue + 0.5f, 0);

        // ランダムなアイテムを選択
        GameObject selectedPrefab = bluePrefabs[Random.Range(0, bluePrefabs.Count)];

        // アイテムを生成
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnGlayFlower()
    {
        // カメラの右端の座標を計算し、生成位置を決定
        float spawnX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x + spawnOffset;

        float rayFiixedGlay = GetGoundY(spawnX, glayFixedY);
        Debug.Log("灰のレイキャストの位置" + rayFiixedGlay);
        if (rayFiixedGlay == Mathf.NegativeInfinity && rayFiixedGlay <= cameraY+0.4f) return;//Groudが見つからない＆指定のY値以下の場合は生成しない

        Vector3 spawnPosition = new Vector3(spawnX, rayFiixedGlay + 0.5f, 0);

        // ランダムなアイテムを選択
        GameObject selectedPrefab = glayPrefabs[Random.Range(0, glayPrefabs.Count)];

        if (cameraX >= triggerGlayXPosission)
            // アイテムを生成
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnOrangeFlower()
    {
        // カメラの右端の座標を計算し、生成位置を決定
        float spawnX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x + spawnOffset;

        float rayFiixedOrange = GetGoundY(spawnX, orangeFixedY);
        Debug.Log("朱のレイキャストの位置" + rayFiixedOrange);
        if (rayFiixedOrange == Mathf.NegativeInfinity && rayFiixedOrange < cameraY + 0.4f) return;//Groudが見つからない＆指定のY値以下の場合は生成しない

        Vector3 spawnPosition = new Vector3(spawnX, rayFiixedOrange + 0.4f, 0);

        // ランダムなアイテムを選択
        GameObject selectedPrefab = orangePrefabs[Random.Range(0, orangePrefabs.Count)];

        if (cameraX >= triggerOrangeXPosission)
            // アイテムを生成
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

    }

    private void SpawnWhiteFlower()
    {
        // カメラの右端の座標を計算し、生成位置を決定
        float spawnX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x + spawnOffset;

        float rayFiixedWhite = GetGoundY(spawnX, whiteFixedY);
        Debug.Log("白のレイキャストの位置" + rayFiixedWhite);
        if (rayFiixedWhite == Mathf.NegativeInfinity && rayFiixedWhite < cameraY + 0.4f) return;//Groudが見つからない＆指定のY値以下の場合は生成しない

        Vector3 spawnPosition = new Vector3(spawnX, rayFiixedWhite + 1f, 0);


        // ランダムなアイテムを選択
        GameObject selectedPrefab = whitePrefabs[Random.Range(0, whitePrefabs.Count)];

        if (cameraX >= triggerWhiteXPosission)
            // アイテムを生成
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        Debug.Log("白の生成位置" + spawnPosition);
    }

}
