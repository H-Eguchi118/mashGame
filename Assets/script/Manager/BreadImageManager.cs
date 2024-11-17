using Boomerang2DFramework.Framework.Actors.ActorFinderFilters.Filters;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;

//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BreadImageManager : MonoBehaviour
{
    [SerializeField] private MashGameDirector _mashGameDirector;
    [SerializeField] private SaveLoadManager _saveLoadManager;

    //連打回数によって表示させる各パン
    [SerializeField] private GameObject breadPrefub;
    [SerializeField] private GameObject richBreadPrefub;
    [SerializeField] private GameObject croissantPrefub;
    [SerializeField] private GameObject carryPrefub;
    [SerializeField] Transform breadContainer;// Grid Layout Groupを持つ親オブジェクトを設定
    [SerializeField] Transform richBbreadContainer;// Grid Layout Groupを持つ親オブジェクトを設定
    [SerializeField] Transform croissantContainer;// Grid Layout Groupを持つ親オブジェクトを設定
    [SerializeField] Transform carryContainer;// Grid Layout Groupを持つ親オブジェクトを設定

    //各パンの数
    private int carryCount = 0;
    private int croissantCount = 0;
    private int richBreadCount = 0;
    private int breadCount = 0;

    //finishUIに表示させるパンのスコア
    public BreadsUI breadsUI;

    //各パンの価格
    private int carryPrice = 30;
    private int croissantPrice = 15;
    private int richBreadPrice = 8;
    private int breadPrice = 5;
    private int mashMoney = 0;//売上金
    private int totalMoney = 0;

    //各パンの合計金額
    public int carryScore = 0;
    public int croissantScore = 0;
    public int richBreadScore = 0;
    public int breadScore = 0;


    void Start()
    {
        _saveLoadManager.LoadTotalMoneyData(out totalMoney);
    }

    void Update()
    {
    }

    public void CheckBreadScore()
    {
        if (_mashGameDirector.mashCount <= 100)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                breadCount++;
                AddBreadImage();
                Debug.Log($"ふつうの食パン：{breadCount}こ");

            }
        }

        if (100 < _mashGameDirector.mashCount && _mashGameDirector.mashCount <= 200)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                richBreadCount++;
                AddRichBreadImage();
                Debug.Log($"リッチな食パン：{richBreadCount}こ");

            }
        }

        if (200 < _mashGameDirector.mashCount && _mashGameDirector.mashCount <= 300)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                croissantCount++;
                croissantBreadImage();
                Debug.Log($"ゆうがなクロワッサン：{croissantCount}こ");

            }
        }
        if (300 < _mashGameDirector.mashCount && _mashGameDirector.mashCount <= 400)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                carryCount++;
                AddCarryBreadImage();
                Debug.Log($"人気のカレーパン：{carryCount}こ");

            }
        }
    }

    void AddBreadImage()
    {
        //各prefabをインスタンス化し、breadContainerの子として配置
        GameObject newBread = Instantiate(breadPrefub, breadContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }

    void AddRichBreadImage()
    {
        GameObject newBread = Instantiate(richBreadPrefub, richBbreadContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }

    void croissantBreadImage()
    {
        GameObject newBread = Instantiate(croissantPrefub, croissantContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }

    void AddCarryBreadImage()
    {
        GameObject newBread = Instantiate(carryPrefub, carryContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }

    //ゲーム終了後に表示させるパンのスコア
    public void BreadScoreSet()
    {
        breadsUI.breadScore.gameObject.SetActive(false);
        breadsUI.richBreadScore.gameObject.SetActive(false);
        breadsUI.croissantScore.gameObject.SetActive(false);
        breadsUI.carryScore.gameObject.SetActive(false);
        breadsUI.moneyImage.gameObject.SetActive(false);
    }

    //スコア表示時間のメソッド
    public IEnumerator DisplayBreadData()
    {
        SetBreadScoreData();

        if (breadCount != 0)
        {
            yield return new WaitForSeconds(6.0f);//1秒待機
            breadsUI.breadScore.gameObject.SetActive(true);
        }

        if (richBreadCount != 0)
        {
            yield return new WaitForSeconds(1.0f);//1秒待機
            breadsUI.richBreadScore.gameObject.SetActive(true);
        }

        if (croissantCount != 0)
        {
            yield return new WaitForSeconds(1.0f);//1秒待機
            breadsUI.croissantScore.gameObject.SetActive(true);
        }

        if (carryCount != 0)
        {
            yield return new WaitForSeconds(1.0f);//1秒待機
            breadsUI.carryScore.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(1.0f);//1秒待機
        breadsUI.moneyImage.gameObject.SetActive(true);
    }

    //テキストの表示設定
    private void SetBreadScoreData()
    {
        ChangedMoney();

        breadsUI.breadText.text = "ふつうの食パン×" + breadCount;
        breadsUI.breadsScore.text = breadScore + "マネ";

        breadsUI.richBreadText.text = "リッチな食パン×" + richBreadCount;
        breadsUI.richBreadsScore.text = richBreadScore + "マネ";


        breadsUI.croissantText.text = "優雅なクロワッサン×" + croissantCount;
        breadsUI.croissantsScore.text = croissantScore + "マネ";


        breadsUI.carryText.text = "人気のカレーパン×" + breadCount;
        breadsUI.carrysScore.text = carryScore + "マネ";

        breadsUI.mashMoneyText.text = "トータル：" + mashMoney + "マネ";
    }


    //各パンの換金処理
    private void ChangedMoney()
    {
        carryScore = carryCount * carryPrice;
        croissantScore = croissantCount * croissantPrice;
        richBreadScore = richBreadCount * richBreadPrice;
        breadScore = breadCount * breadPrice;

        mashMoney = carryScore + croissantScore + richBreadScore + breadScore;
    }

    //public void SaveMashMoneyData()
    //{
    //    if (_saveLoadManager != null)
    //    {
    //        _saveLoadManager.SaveMashMoneyData(mashMoney);
    //    }
    //}


    public void AddTotalMoneyScore(out int totalMoney)
    {
        _saveLoadManager.LoadTotalMoneyData(out totalMoney);
        Debug.Log("totalMoneyにmashMoneyを加算します。"+ totalMoney+"+"+ mashMoney);
        totalMoney += mashMoney;

        _saveLoadManager.SaveTotalMoneyData(totalMoney);
    }

}
[System.Serializable]
public class BreadsUI
{
    //パンのスコア
    public GameObject breadScore;
    public TextMeshProUGUI breadText;
    public TextMeshProUGUI breadsScore;

    public GameObject richBreadScore;
    public TextMeshProUGUI richBreadText;
    public TextMeshProUGUI richBreadsScore;

    public GameObject croissantScore;
    public TextMeshProUGUI croissantText;
    public TextMeshProUGUI croissantsScore;

    public GameObject carryScore;
    public TextMeshProUGUI carryText;
    public TextMeshProUGUI carrysScore;
    public Image moneyImage;
    public TextMeshProUGUI mashMoneyText;

}

