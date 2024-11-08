using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private TilemapCollider2D tmCollider;
    private int flowersScore = 0;//花。換金アイテム
    private int bouquet = 0;//花束。花の倍の換金率
    private float flightRimitTime = 0;//取得すると一定時間飛べるアイテム
    private int money = 0;//所持金

    [SerializeField] private TextMeshProUGUI flowersScoreText;//花の所持数のテキスト
    [SerializeField] private TextMeshProUGUI rimitTimeText;//フライト制限時間のテキスト

    [SerializeField] private Tilemap flowers;//シーン上に配置するアイテム
    [SerializeField] private Tilemap flightItems;//シーン上に配置するアイテム

    [SerializeField] private Image flower;//UIで表示するイメージ
    [SerializeField] private Image flightItem;//UIで表示するイメージ


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //花の所持数更新
    public void GetFlower()
    {
        flowersScore++;


    }

    public void Changebouquet()
    {
        //flowersScoreが10ごとに花束１つに交換

    }

    //フライトアイテムの効果
    public void GetFightItem()
    {
        flightRimitTime += 10.0f;
        rimitTimeText.gameObject.SetActive(true);

        rimitTimeText.text= flightRimitTime.ToString("F1");
        flightRimitTime-=Time.deltaTime;

        if(flightRimitTime < 0)
        {
            rimitTimeText.gameObject.SetActive(false);

        }

    }


}
