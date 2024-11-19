using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScoreUI
{
    public Canvas scoreCanvas;

    public Image blueFlowerImage;
    public Image glayFlowerImage;
    public Image orangeFlowerImage;
    public Image whiteFlowerImage;
    public Image timeImage;
    public Image moneyImage;

    public TextMeshProUGUI blueFlowerText;    //花の所持数のテキスト
    public TextMeshProUGUI glayFlowerText;    // 花の所持数のテキスト
    public TextMeshProUGUI orangeFlowerText;    // 花束の所持数のテキスト
    public TextMeshProUGUI whiteFlowerText;    // 花束の所持数のテキスト
    public TextMeshProUGUI timeText;      // タイマーのテキスト

    public TextMeshProUGUI blueFlowerPriceText;    // 花の所持数のテキスト
    public TextMeshProUGUI glayFlowerPriceText;    // 花の所持数のテキスト
    public TextMeshProUGUI orangeFlowerPriceText;    // 花の所持数のテキスト
    public TextMeshProUGUI whiteFlowerPriceText;    // 花の所持数のテキスト
    public TextMeshProUGUI timeBonusText;    // 花の所持数のテキスト
    public TextMeshProUGUI runMoneyText;    // 合計金のテキスト

}