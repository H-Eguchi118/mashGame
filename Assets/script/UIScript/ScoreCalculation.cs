using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCalculation : MonoBehaviour
{
    [SerializeField] private SaveLoadManager _saveLoadManager;
    [SerializeField] private SelectManager _selectManager;
    [SerializeField] private Button CloseButtonl;

    public ScoreUI scoreUI;
    int blueFlowersScore = 0;
    int glayFlowersScore = 0;
    int orangeFlowersScore = 0;
    int whiteFlowersScore = 0;
    float time = 0;

    int blueFlowerHung = 1;
    int glayFlowerHung = 5;
    int orangeFlowerHung = 10;
    int whiteFlowerHung = 20;

    public int timeBonusScore = 0;//タイムに伴うボーナス
    public int SspeedBonus = 30;
    public int normalBonus = 10;
    public int runMoney = 0;
    public int totalMoney = 0;

    public string targetSceneName = "";

    void Start()
    {
        scoreUI.scoreCanvas.gameObject.SetActive(false);
        _saveLoadManager.LoadSceneName(out targetSceneName);
        Debug.Log("取得したシーン名：" + targetSceneName);

        if (targetSceneName == "RunGameScene")
        {
            scoreUI.scoreCanvas.gameObject.SetActive(true);


            // 遷移元が指定したシーンの場合のみキャンバス内順次表示
            StartCoroutine(DisplayScoreCanvas());
        }
        else return;

    }



    //順番にアイテムのスコアを表示させるメソッド
    public IEnumerator DisplayScoreCanvas()
    {
        //スコアデータ取得
        SetScoreData();

        //スコアキャンバスの設定
        SetScoreCanvas();

        yield return new WaitForSeconds(2.0f);//1秒待機
        scoreUI.blueFlowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.glayFlowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.orangeFlowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.whiteFlowerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.timeImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);//1秒待機
        scoreUI.moneyImage.gameObject.SetActive(true);

        CloseButtonl.onClick.AddListener(() => closedCanvas());


    }

    //最初はキャンバスのみ表示させるメソッド
    public void SetScoreCanvas()
    {
        scoreUI.scoreCanvas.enabled = true;
        scoreUI.blueFlowerImage.gameObject.SetActive(false);
        scoreUI.glayFlowerImage.gameObject.SetActive(false);
        scoreUI.orangeFlowerImage.gameObject.SetActive(false);
        scoreUI.whiteFlowerImage.gameObject.SetActive(false);
        scoreUI.timeImage.gameObject.SetActive(false);
        scoreUI.moneyImage.gameObject.SetActive(false);

    }

    public void SetScoreData()
    {
        //saveLoadManagerの各データを取得する
        _saveLoadManager.LoadItemData(out blueFlowersScore, out glayFlowersScore, out orangeFlowersScore, out whiteFlowersScore);
        _saveLoadManager.LoadTimeData(out time);

        //アイテムの所持数表示
        scoreUI.blueFlowerText.text = "×" + blueFlowersScore;
        scoreUI.glayFlowerText.text = "×" + glayFlowersScore;
        scoreUI.orangeFlowerText.text = "×" + orangeFlowersScore;
        scoreUI.whiteFlowerText.text = "×" + whiteFlowersScore;
        scoreUI.timeText.text = "" + time.ToString("F1");

        //各アイテムの換金処理
        ChangedMoney();

        // 各金額の表示（Nullチェックを追加）
        if (scoreUI.blueFlowerPriceText != null)
            scoreUI.blueFlowerPriceText.text = blueFlowersScore + "マネ";

        if (scoreUI.glayFlowerPriceText != null)
            scoreUI.glayFlowerPriceText.text = glayFlowersScore + "マネ";

        if (scoreUI.orangeFlowerPriceText != null)
            scoreUI.orangeFlowerPriceText.text = orangeFlowersScore + "マネ";

        if (scoreUI.whiteFlowerPriceText != null)
            scoreUI.whiteFlowerPriceText.text = whiteFlowersScore + "マネ";


        if (scoreUI.timeBonusText != null)
            scoreUI.timeBonusText.text = timeBonusScore + "マネ";

        if (scoreUI.runMoneyText != null)
            scoreUI.runMoneyText.text = "合計マネ：" + runMoney + " マネ";

        AddTotalMoneyScore();
    }

    public void TimeBonusList()
    {

        if (time <= 30.0)
        {
            timeBonusScore += SspeedBonus;

        }
        else if (time <= 60.0)
        {
            timeBonusScore += normalBonus;
        }
        else
        {
            timeBonusScore = 0;
        }
    }

    //各アイテムの換金処理
    private void ChangedMoney()
    {
        blueFlowersScore = blueFlowersScore * blueFlowerHung;
        glayFlowersScore = glayFlowersScore * glayFlowerHung;
        orangeFlowersScore = orangeFlowersScore * orangeFlowerHung;
        whiteFlowersScore = whiteFlowersScore * whiteFlowerHung;

        TimeBonusList();
        Debug.Log("Money calculation completed");

        //合計金額
        runMoney = blueFlowersScore + glayFlowersScore + orangeFlowersScore + whiteFlowersScore + timeBonusScore;
    }



    public void AddTotalMoneyScore()
    {
        if (_saveLoadManager != null)
        {
            totalMoney = _saveLoadManager.LoadTotalMoneyData(out totalMoney);
            Debug.Log("totalMoneyにrunMoneyを加算します。" + totalMoney + "+" + runMoney);

            totalMoney += runMoney;
            _saveLoadManager.SaveTotalMoneyData(totalMoney);
        }
    }


    private void closedCanvas()
    {
        scoreUI.scoreCanvas.gameObject.SetActive(false);
        _selectManager.SetCanvas();
    }
}
