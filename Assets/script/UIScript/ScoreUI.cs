using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScoreUI
{
    public Canvas scoreCanvas;

    public Image flowerImage;
    public Image rareFlowerImage;
    public Image bouquetImage;
    public Image timeImage;
    public Image totalMoneyImage;

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