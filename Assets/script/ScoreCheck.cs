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
        scoreUI.flowersText.text = "" + _item.GetFlowersScore() * 1;
        scoreUI.rareFlowersText.text = "" + _item.GetRareFlowersScore();
        scoreUI.bouquetText.text = "" + _item.GetBouquetsScore();
        scoreUI.timeText.text = "" + _runGameDirector.GetTimeBonus();

        scoreUI.flowerPriceText.text = _item.GetFlowersScore() * 1 + "yen";
        scoreUI.rareFlowersText.text = _item.GetRareFlowersScore() * 5 + "yen";
        scoreUI.bouquetText.text = _item.GetBouquetsScore() * 20 + "yen";

        TimeBonusList();

    }

    public void TimeBonusList()
    {
        float lastTimeScore = _runGameDirector.GetTimeBonus();

        if (lastTimeScore <= 30.0)
        {
            timeBonus += 30;

        }
        else if (lastTimeScore <= 60.0)
        {
            timeBonus += 10;
        }
        else
        {
            return;
        }

        scoreUI.timeBonusText.text=timeBonus.ToString();
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

    }