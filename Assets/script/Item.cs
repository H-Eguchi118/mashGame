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
    public int flowerPoint = 1;
    public int rareFlowerPoint = 3;
    public int flowersScore = 0;//花。換金アイテム
    private int bouquet = 0;//花束。花の倍の換金率
    private float flightRimitTime = 0;//取得すると一定時間飛べるアイテム
    private int money = 0;//所持金

    [SerializeField]private Image flowerImage;
    [SerializeField]private Image bouquetImage;
    [SerializeField]private Image flightItemImage;

    [SerializeField] private TextMeshProUGUI flowerText;
    [SerializeField] private TextMeshProUGUI bouquetText ;
    [SerializeField] private TextMeshProUGUI flightRimitTimeText;

    void Start()
    {
        flowerImage.gameObject.SetActive(true);
        flowerText.gameObject.SetActive(true);

        bouquetImage.gameObject.SetActive(false);
        flightItemImage.gameObject.SetActive(false);

        flowerText.text=flowersScore.ToString();

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
        flowerText.text = flowersScore.ToString();

        if (_runPlayerController.isFlightMode)
        {
            flightRimitTime -= Time.deltaTime;
            flightRimitTimeText.text = flightRimitTime.ToString("F1");

        }
    }

    //花の所持数更新
    public void GetFlower()
    {
        flowersScore += flowerPoint;
        Debug.Log("Flower："+ flowersScore);

    }

    public void GetRareFlower()
    {
        flowersScore += rareFlowerPoint;
        Debug.Log("Flower：" + flowersScore);

    }

    public void Changebouquet()
    {
        //flowersScoreが10ごとに花束１つに交換
        if (flowersScore == 10)
        {
            bouquet++;
            flowersScore -= 10;

            bouquetImage.gameObject.SetActive(true);
            bouquetText.text= bouquet.ToString();


        }

    }

    //フライトアイテムの効果
    public void GetFightItem()
    {
        Debug.Log("GetFightItemメソッドが呼び出されました。");
        // ジャンプ制御の無効化と時間制限の減少をコルーチンで処理
        StartCoroutine(EnableFlightMode());

        flightItemImage.gameObject.SetActive(true);
        flightRimitTimeText.gameObject.SetActive(true);

        // 無限ジャンプを10秒間有効にする
        flightRimitTime += 10.0f;

        if (flightRimitTime < 0)
        {
            flightRimitTime = 0;
            flightRimitTimeText.gameObject.SetActive(false);


        }

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
