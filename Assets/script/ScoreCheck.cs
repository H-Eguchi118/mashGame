using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCheck : MonoBehaviour
{
    [SerializeField]private SaveLoadManager _saveLoadManager;
    public ScoreUI scoreUI;
    int flowersScore = 0;
    int rareFlowersScore = 0;
    int bouquetScore = 0;
    float lastTimeScore = 0;

    int flowerHung = 1;
    int rareFlowerHung = 5;
    int bouquetHung = 20;

    public int timeBonusScore = 0;//タイムに伴うボーナス
    public int SspeedBonus = 30;
    public int normalBonus = 10;
    public int money = 0;
    public int totalMoney = 0;


    void Start()
    {
        //キャンバス内順次表示
        StartCoroutine(DisplayScoreCanvas());

        //保存した合計金額を読み込む
        LoadMoneyData();
    }

    //順番にアイテムのスコアを表示させるメソッド
    public IEnumerator DisplayScoreCanvas()
    {
        //スコアデータ取得
        SetScoreData();

        //スコアキャンバスの設定
        SetScoreCanvas();



        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.flowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.rareFlowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.bouquetImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.timeImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.totalMoneyText.gameObject.SetActive(true);

    }

    //最初はキャンバスのみ表示させるメソッド
    public void SetScoreCanvas()
    {
        scoreUI.scoreCanvas.enabled = true;
        scoreUI.flowerImage.gameObject.SetActive(false);
        scoreUI.rareFlowerImage.gameObject.SetActive(false);
        scoreUI.bouquetImage.gameObject.SetActive(false);
        scoreUI.timeImage.gameObject.SetActive(false);
        scoreUI.totalMoneyText.gameObject.SetActive(false);

    }

    public void SetScoreData()
    {
        //各アイテムの換金処理
        ChangedMoney();

        //アイテムの所持数表示
        scoreUI.flowersText.text = "×" + Item.Instance.GetFlowersScore();
        scoreUI.rareFlowersText.text = "×" + Item.Instance.GetRareFlowersScore();
        scoreUI.bouquetText.text = "×" + Item.Instance.GetBouquetsScore();
        scoreUI.timeText.text = "" + RunGameDirector.Instance.GetTimeBonus();

        // 各金額の表示（Nullチェックを追加）
        if (scoreUI.flowerPriceText != null)
            scoreUI.flowerPriceText.text = flowersScore + "yen";

        if (scoreUI.rareFlowerPriceText != null)
            scoreUI.rareFlowerPriceText.text = rareFlowersScore + "yen";

        if (scoreUI.bouquetPriceText != null)
            scoreUI.bouquetPriceText.text = bouquetScore + "yen";

        if (scoreUI.timeBonusText != null)
            scoreUI.timeBonusText.text = timeBonusScore + "yen";

        if (scoreUI.totalMoneyText != null)
            scoreUI.totalMoneyText.text = "Total Money " + totalMoney + " yen";
    }

    public void TimeBonusList()
    {
        lastTimeScore = RunGameDirector.Instance.GetTimeBonus();

        if (lastTimeScore <= 30.0)
        {
            timeBonusScore += SspeedBonus;

        }
        else if (lastTimeScore <= 60.0)
        {
            timeBonusScore += normalBonus;
        }
        else
        {
            timeBonusScore=0;
        }
    }

    //各アイテムの換金処理
    private void ChangedMoney()
    {
        Debug.Log("flowersScore calculation started");
        if (Item.Instance == null)
        {
            Debug.LogError("Item.Instance is null");
            return;
        }

        //所持アイテムごとの換金計算
        flowersScore = Item.Instance.GetFlowersScore() * flowerHung;
        rareFlowersScore = Item.Instance.GetRareFlowersScore() * rareFlowerHung;
        bouquetScore = Item.Instance.GetBouquetsScore() * bouquetHung;

        Debug.Log("RunGameDirector.Instance check");
        if (RunGameDirector.Instance == null)
        {
            Debug.LogError("RunGameDirector.Instance is null");
            return;
        }

        lastTimeScore = RunGameDirector.Instance.GetTimeBonus();
        TimeBonusList();
        Debug.Log("Money calculation completed");

        //合計金額
        money = flowersScore + rareFlowersScore + bouquetScore + timeBonusScore;
        totalMoney += money;
    }

    // 金額データを保存
    public void SaveMoneyData()
    {
        if (_saveLoadManager != null)
        {
            _saveLoadManager.SaveTotalMoneyData(totalMoney);
        }
    }

    // 金額データを読み込む
    public void LoadMoneyData()
    {
        if (_saveLoadManager != null)
        {
            totalMoney = _saveLoadManager.LoadTotalMoneyData();
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