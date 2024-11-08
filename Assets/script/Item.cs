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
    private RunGameDirector _runGameDirector;
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
    }

    void Update()
    {

    }

    //花の所持数更新
    public void GetFlower()
    {
        flowersScore++;
        Debug.Log("Flower："+ flowersScore);

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
        flightRimitTime += 10.0f;
       // flightItemCanvas.gameObject.SetActive(true);

       // _runGameDirector.rimitTimeText.text = flightRimitTime.ToString("F1");
        flightRimitTime -= Time.deltaTime;

        if (flightRimitTime < 0)
        {
           // flightItemCanvas.gameObject.SetActive(false);
        }

    }



}
