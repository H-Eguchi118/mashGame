using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    // データ保存
    public void SaveItemData(int blueFlowersScore, int glayFlowersScore, int orangeFlowersScore,int whiteFlowersScore)
    {
        PlayerPrefs.SetInt("blueFlowersScore", blueFlowersScore);
        PlayerPrefs.SetInt("glayFlowersScore", glayFlowersScore);
        PlayerPrefs.SetInt("orangeFlowersScore", orangeFlowersScore);
        PlayerPrefs.SetInt("whiteFlowersScore", whiteFlowersScore);
        PlayerPrefs.Save(); // データを保存
        Debug.Log("アイテムデータを保存しました");

    }

    // データロード
    public void LoadItemData(out int blueFlowersScore, out int glayFlowersScore, out int orangeFlowersScore, out int whiteFlowersScore)
    {
        blueFlowersScore = PlayerPrefs.GetInt("blueFlowersScore", 0);
        glayFlowersScore = PlayerPrefs.GetInt("glayFlowersScore", 0);
        orangeFlowersScore = PlayerPrefs.GetInt("orangeFlowersScore", 0);
        whiteFlowersScore = PlayerPrefs.GetInt("whiteFlowersScore", 0);

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

    // トータルの持ちマネを保存するメソッド
    public void SaveTotalMoneyData(int totalMoney)
    {
        PlayerPrefs.SetInt("totalMoney", totalMoney);
        PlayerPrefs.Save();
        Debug.Log("合計金額データを保存しました。totalMoney："+totalMoney);
    }

    // トータルの持ちマネを読み込むメソッド
    public int LoadTotalMoneyData(out int totalMoney)
    {
        totalMoney = PlayerPrefs.GetInt("totalMoney", 0);
        Debug.Log("合計金額データを読み込みました");
        Debug.Log("totalMoney：" + totalMoney);
        return totalMoney;
    }

    public void SaveSceneName(string targetSceneName)
    {
        PlayerPrefs.SetString("targetSceneName", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        Debug.Log("シーンの名前をを保存しました。targetSceneName：" + targetSceneName);


    }

    public string LoadSceneName(out string targetSceneName)
    {
        targetSceneName = PlayerPrefs.GetString("targetSceneName","");
        return targetSceneName;
    }
    

    //run gameの金額を保存する
    //public void SaveRunMoneyData(int runMoney)
    //{
    //    PlayerPrefs.SetInt("mashMoney", runMoney);
    //    PlayerPrefs.Save();
    //    Debug.Log("mashgameの金額データを保存しました。mashMoney：" + runMoney);

    //}

    //public int LoadRunMoneyData(out int runMoney)
    //{
    //    runMoney = PlayerPrefs.GetInt("runMoney", 0);
    //    Debug.Log("mashgameの金額データを読み込みました");
    //    Debug.Log("mashgameの金額：" + runMoney);
    //    return runMoney;
    //}
    //public void SaveMashMoneyData(int mashMoney)
    //{
    //    PlayerPrefs.SetInt("mashMoney", mashMoney);
    //    PlayerPrefs.Save();
    //    Debug.Log("mashgameの金額データを保存しました。mashMoney：" + mashMoney);

    //}

    //public int LoadMashMoneyData(out int mashMoney)
    //{
    //    mashMoney = PlayerPrefs.GetInt("mashMoney", 0);
    //    Debug.Log("mashgameの金額データを読み込みました");
    //    Debug.Log("mashgameの金額：" + mashMoney);
    //    return mashMoney;
    //}
}
