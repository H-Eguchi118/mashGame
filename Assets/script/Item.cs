using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    private RunPlayerController _runPlayerController;
    private RunGameDirector _runGameDirector;
    public int rareFlowersScore = 0;
    public int flowersScore = 0;//花。換金アイテム
    public int bouquetScore = 0;//花束。花の倍の換金率
    private float flightRimitTime = 0;//取得すると一定時間飛べるアイテム
    private int money = 0;//所持金

    private bool isFlightTimeRunning = false; // フライトタイマーが動作中かどうか

    public MainUI mainUI;  // GoalCanvasのUI要素をまとめたもの


    //[SerializeField] private Image flowerImage;
    //[SerializeField] private Image bouquetImage;
    //[SerializeField] private Image flightItemImage;

    //[SerializeField] private TextMeshProUGUI flowerText;
    //[SerializeField] private TextMeshProUGUI rareFlowerText;
    //[SerializeField] private TextMeshProUGUI bouquetText;
    //[SerializeField] private TextMeshProUGUI flightRimitTimeText;

    // データの確認用
    public int GetFlowerScore() => flowersScore;
    public int GetRareFlowerScore() => rareFlowersScore;
    public int GetBouquetScore() => bouquetScore;


    void Start()
    {
        mainUI.mainCanvas.enabled = true;

        //flowerImage.gameObject.SetActive(true);
        //flowerText.gameObject.SetActive(true);
        //flowerText.text = flowersScore.ToString();
        //rareFlowerText.text = rareFlowersScore.ToString();

        mainUI.bouquetImage.gameObject.SetActive(false);
        mainUI.flightItemImage.gameObject.SetActive(false);



        if (_runPlayerController == null)
        {
            _runPlayerController = FindObjectOfType<RunPlayerController>();

            if (_runPlayerController == null)
            {
                Debug.LogError("PlayerControllerが見つかりません。インスペクタで設定してください。");
            }
            else
            {
                Debug.Log("PlayerControllerがFindObjectOfTypeで自動設定されました。");
            }
        }
    }

    void Update()
    {
        StartFlightTimer();
    }

    // 取得データを保存するメソッド
    private void SaveItemData()
    {
        PlayerPrefs.SetInt("flowersScore", flowersScore);
        PlayerPrefs.SetInt("rareFlowersScore", rareFlowersScore);
        PlayerPrefs.SetInt("bouquetScore", bouquetScore);
        PlayerPrefs.Save(); // データを保存
        Debug.Log("データを保存しました");
    }


    //花の所持数更新
    public void GetFlower()
    {
        flowersScore++;
        mainUI.flowersText.text = flowersScore.ToString();

        Debug.Log("Flower：" + flowersScore);
        SaveItemData();
    }

    public void GetRareFlower()
    {
        rareFlowersScore++;
        mainUI.rareFlowersText.text = rareFlowersScore.ToString();

        Debug.Log("Flower：" + flowersScore);
        SaveItemData();
    }

    public void Changebouquet()
    {
        //flowersScoreが10ごとに花束１つに交換
        if (flowersScore >= 10)
        {
            bouquetScore++;
            flowersScore -= 10;

            mainUI.bouquetImage.gameObject.SetActive(true);
            mainUI.bouquetText.text = bouquetScore.ToString();
        }
        SaveItemData();

    }

    //フライトアイテムの効果
    public void GetFightItem()
    {
        isFlightTimeRunning = true;

        Debug.Log("GetFightItemメソッドが呼び出されました。");
        // ジャンプ制御の無効化と時間制限の減少をコルーチンで処理
        StartCoroutine(EnableFlightMode());

        mainUI.flightItemImage.gameObject.SetActive(true);
        mainUI.flightRimitTimeText.gameObject.SetActive(true);

        // 無限ジャンプを10秒間有効にする
        flightRimitTime += 10.0f;
    }

    public void StartFlightTimer()
    {
        if (isFlightTimeRunning)
        {
            // mainUI.flowersText.text = flowersScore.ToString();

            if (_runPlayerController.isFlightMode)
            {
                flightRimitTime -= Time.deltaTime;
                mainUI.flightRimitTimeText.text = flightRimitTime.ToString("F1");

                if (flightRimitTime < 0)
                {
                    flightRimitTime = 0;
                    mainUI.flightItemImage.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StopFlightTimer()
    {
        isFlightTimeRunning = false;
    }

    private IEnumerator EnableFlightMode()
    {
        if (_runPlayerController != null)
        {
            Debug.Log("フライトモード開始");

            //一時的にisGroundedをtrueに設定
            _runPlayerController.isFlightMode = true;//フライトモードを有効化
            yield return new WaitForSeconds(10.0f);//10秒待機
            _runPlayerController.isFlightMode = false;//フライトモードを無効化

            Debug.Log("フライトモード終了");
        }
    }

}


[System.Serializable]
public class MainUI
{
    public Canvas mainCanvas;
    public TextMeshProUGUI flowersText;    // 花の所持数のテキスト
    public TextMeshProUGUI rareFlowersText;    // 花の所持数のテキスト
    public TextMeshProUGUI bouquetText;    // 花束の所持数のテキスト
    public TextMeshProUGUI flightRimitTimeText;
    public TextMeshProUGUI timerText;      // タイマーのテキスト

    public Image bouquetImage;
    public Image flightItemImage;


}