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
    public int reaFlowerPoint = 3;
    public int flowersScore = 0;//花。換金アイテム
    private int bouquet = 0;//花束。花の倍の換金率
    private float flightRimitTime = 0;//取得すると一定時間飛べるアイテム
    private int money = 0;//所持金

    //[SerializeField] private Tilemap flowers;//シーン上に配置するアイテム
    //[SerializeField] private Tilemap flightItems;//シーン上に配置するアイテム

    //[SerializeField] private Image flower;//UIで表示するイメージ
    //[SerializeField] private Canvas flightItemCanvas;//UIで表示するイメージ



    void Start()
    {
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

    }

    //花の所持数更新
    public void GetFlower()
    {
        flowersScore += flowerPoint;
        Debug.Log("Flower："+ flowersScore);

    }

    public void GetReaFlower()
    {
        flowersScore += reaFlowerPoint;
        Debug.Log("Flower：" + flowersScore);

    }


    public void Changebouquet()
    {
        //flowersScoreが10ごとに花束１つに交換
        if (flowersScore == 10)
        {
            bouquet++;
            flowersScore -= 10;

        }

    }

    //フライトアイテムの効果
    public void GetFightItem()
    {
        Debug.Log("GetFightItemメソッドが呼び出されました。");
        // ジャンプ制御の無効化と時間制限の減少をコルーチンで処理
        StartCoroutine(EnableTemporaryGrounded());

        // flightItemCanvas.gameObject.SetActive(true);
        // _runGameDirector.rimitTimeText.text = flightRimitTime.ToString("F1");

        // 無限ジャンプを10秒間有効にする
        flightRimitTime += 10.0f;

        if (flightRimitTime < 0)
        {
            flightRimitTime = 0;

            // flightItemCanvas.gameObject.SetActive(false);

        }



    }

    private IEnumerator EnableTemporaryGrounded()
    {
        if (_runPlayerController != null)
        {
            Debug.Log("EnableTemporaryGroundedメソッドが呼び出されました。");

            //flightRimitTimeの減少処理
            while (flightRimitTime > 0)
            {
                flightRimitTime -= Time.deltaTime;
                Debug.Log("残り時間："+flightRimitTime.ToString("F1"));

                //残り時間がある間は待機
                yield return null;

            }


            // 10秒経過後にジャンプ制御を再度有効に
            _runPlayerController.isGrounded = false;
            Debug.Log("ジャンプ制御が再度有効になりました。");
        }
    }



}
