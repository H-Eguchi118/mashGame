using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawner : MonoBehaviour
{
    [SerializeField]private Camera mainCamera; // メインカメラ（インスペクタで設定）

    public List<GameObject> bluePrefabs; // 青花のPrefabリスト
    public List<GameObject> glayPrefabs; // 灰花のPrefabリスト
    public List<GameObject> orangePrefabs; // 朱花のPrefabリスト
    public List<GameObject> whitePrefabs; // 白花のPrefabリスト

    public float spawnOffset = 2.0f; // カメラ端から少し外側に生成するオフセット

    public float blueFixedY = 0.0f; // 青い花のY座標
    public float glayFixedY = 0.0f; // 灰花のY座標
    public float orangeFixedY = 0.0f; // 朱色花のY座標
    public float whiteFixedY = 0.0f; // 白い花のY座標

    public Vector2 spawnIntervalRange = new Vector2(0.5f, 1.5f); // ランダムな生成間隔の範囲（秒）

    void Start()
    {
        // ランダム生成を開始
        StartCoroutine(SpawnItemsRandomly());
    }

    IEnumerator SpawnItemsRandomly()
    {
        while (true)
        {
            // ランダムな時間を待機
            float waitTime = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
            yield return new WaitForSeconds(waitTime);

            // アイテムを生成
            SpawnBlueFlower();
        }
    }

    private void SpawnBlueFlower()
    {
        // カメラの右端の座標を計算し、生成位置を決定
        float spawnX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x + spawnOffset;
        Vector3 spawnPosition = new Vector3(spawnX, blueFixedY, 0);

        // ランダムなアイテムを選択
        GameObject selectedPrefab = bluePrefabs[Random.Range(0, bluePrefabs.Count)];

        // アイテムを生成
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}
