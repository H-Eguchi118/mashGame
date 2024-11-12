using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // データ保存
    public void SaveItemData(int flowersScore, int rareFlowersScore, int bouquetScore)
    {
        PlayerPrefs.SetInt("flowersScore", flowersScore);
        PlayerPrefs.SetInt("rareFlowersScore", rareFlowersScore);
        PlayerPrefs.SetInt("bouquetScore", bouquetScore);
        PlayerPrefs.Save(); // データを保存
        Debug.Log("アイテムデータを保存しました");
    }

    // データロード
    public void LoadItemData(out int flowersScore, out int rareFlowersScore, out int bouquetScore)
    {
        flowersScore = PlayerPrefs.GetInt("flowersScore", 0);
        rareFlowersScore = PlayerPrefs.GetInt("rareFlowersScore", 0);
        bouquetScore = PlayerPrefs.GetInt("bouquetScore", 0);

        Debug.Log("アイテムデータを読み込みました");
    }

    public void SaveTimeData(float time)
    {
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.Save(); // データを保存
        Debug.Log("タイムデータを保存しました");
    }

    public void LoadTimeData(out float time)
    {
        time = PlayerPrefs.GetFloat("time", 0.0f);
        Debug.Log("タイムデータを読み込みしました");
    }

    // スコアデータを保存するメソッド
    public void SaveTotalMoneyData(int totalMoney)
    {
        PlayerPrefs.SetInt("totalMoney", totalMoney);
        PlayerPrefs.Save();
        Debug.Log("合計金額データを保存しました");
    }

    // スコアデータを読み込むメソッド
    public int LoadTotalMoneyData(out int totalMoney)
    {
        totalMoney = PlayerPrefs.GetInt("totalMoney", 0);
        Debug.Log("合計金額データを読み込みました");
        return totalMoney;
    }
}
