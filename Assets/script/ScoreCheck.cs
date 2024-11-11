using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCheck : MonoBehaviour
{
    private Item _item;
    private RunGameDirector _runGameDirector;

    public ScoreUI scoreUI;
    public int timeBonus = 0;//タイムに伴うボーナス
    public int SspeedBonus = 30;
    public int normalBonus = 10;
    public int totalMoney = 0;


    void Start()
    {
        //各スクリプトの参照を取得
        _item = FindObjectOfType<Item>();
        _runGameDirector = FindObjectOfType<RunGameDirector>();

        //データをロード
        _item.LoadItemData();
        _runGameDirector.LoadTimeData();

        //キャンバス内順次表示
        StartCoroutine(DisplayScoreCanvas());
    }
    void Update()
    {

    }

    //順番にアイテムのスコアを表示させるメソッド
    public IEnumerator DisplayScoreCanvas()
    {
        SetScoreCanvas();

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.flowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.rareFlowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.bouquetImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.timeImage.gameObject.SetActive(true);


    }

    //最初はキャンバスのみ表示させるメソッド
    public void SetScoreCanvas()
    {
        scoreUI.scoreCanvas.enabled = true;
        scoreUI.flowerImage.gameObject.SetActive(false);
        scoreUI.rareFlowerImage.gameObject.SetActive(false);
        scoreUI.bouquetImage.gameObject.SetActive(false);
        scoreUI.timeImage.gameObject.SetActive(false);

    }

    public void SetScoreData()
    {
        //アイテムの所持数表示
        scoreUI.flowersText.text = "" + _item.GetFlowersScore();
        scoreUI.rareFlowersText.text = "" + _item.GetRareFlowersScore();
        scoreUI.bouquetText.text = "" + _item.GetBouquetsScore();
        scoreUI.timeText.text = "" + _runGameDirector.GetTimeBonus();

        //所持アイテムごとの換金計算
        int flowersScore = _item.GetFlowersScore() * 1;
        int rareFlowersScore = _item.GetRareFlowersScore() * 5;
        int bouquetScore = _item.GetBouquetsScore() * 20;
        TimeBonusList();
        int timeBonusScore = timeBonus;

        //合計金額
        totalMoney = flowersScore + rareFlowersScore + bouquetScore + timeBonusScore;

        //各金額の表示
        scoreUI.flowerPriceText.text = _item.GetFlowersScore() + "yen";
        scoreUI.rareFlowersText.text = _item.GetRareFlowersScore() + "yen";
        scoreUI.bouquetText.text = _item.GetBouquetsScore() + "yen";
        TimeBonusList();
        scoreUI.timeBonusText.text = timeBonusScore.ToString();
        scoreUI.totalMoneyText.text = totalMoney.ToString();


    }

    public void TimeBonusList()
    {
        float lastTimeScore = _runGameDirector.GetTimeBonus();

        if (lastTimeScore <= 30.0)
        {
            timeBonus += SspeedBonus;

        }
        else if (lastTimeScore <= 60.0)
        {
            timeBonus += normalBonus;
        }
        else
        {
            return;
        }

    }
}
[System.Serializable]
public class ScoreUI
{
    public Canvas scoreCanvas;

    public Image flowerImage;
    public Image rareFlowerImage;
    public Image bouquetImage;
    public Image timeImage;

    public TextMeshProUGUI flowersText;    // 花の所持数のテキスト
    public TextMeshProUGUI rareFlowersText;    // 花の所持数のテキスト
    public TextMeshProUGUI bouquetText;    // 花束の所持数のテキスト
    public TextMeshProUGUI timeText;      // タイマーのテキスト

    public TextMeshProUGUI flowerPriceText;    // 花の所持数のテキスト
    public TextMeshProUGUI rareFlowerPriceText;    // 花の所持数のテキスト
    public TextMeshProUGUI bouquetPriceText;    // 花の所持数のテキスト
    public TextMeshProUGUI timeBonusText;    // 花の所持数のテキスト
    public TextMeshProUGUI totalMoneyText;    // 合計金のテキスト

}