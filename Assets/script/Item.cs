using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    [SerializeField] private SaveLoadManager _saveLoadManager;
    private RunPlayerController _runPlayerController;
    private RunGameDirector _runGameDirector;
    public int blueFlowersScore = 0;//花。換金アイテム
    public int glayFlowersScore = 0;
    public int orangeFlowersScore = 0;
    public int whiteFlowersScore = 0;
    private float flightRimitTime = 0;//取得すると一定時間飛べるアイテム
    private float rareItem = 0;//ご褒美アイテム(仮)
    public int runMoney = 0;//所持金


    private bool isFlightTimeRunning = false; // フライトタイマーが動作中かどうか

    public MainUI mainUI;  // GoalCanvasのUI要素をまとめたもの


    void Start()
    {
        mainUI.mainCanvas.enabled = true;

        mainUI.flightItemImage.gameObject.SetActive(false);

        if (_runPlayerController == null)
        {
            _runPlayerController = FindObjectOfType<RunPlayerController>();

            if (_runPlayerController == null)
            {
                Debug.LogError("PlayerControllerが見つかりません。インスペクタで設定してください。");
            }
            else
            {
                Debug.Log("PlayerControllerがFindObjectOfTypeで自動設定されました。");
            }
        }
    }

    void Update()
    {
        StartFlightTimer();
    }

    // 取得データを保存するメソッド
    // アイテムデータを保存
    private void SaveItemData()
    {
        _saveLoadManager.SaveItemData(blueFlowersScore, glayFlowersScore, orangeFlowersScore,whiteFlowersScore);
    }

    //花の所持数更新
    public void GetBlueFlower()
    {
        blueFlowersScore++;
        mainUI.flowersText.text = blueFlowersScore.ToString();

        Debug.Log("Flower：" + blueFlowersScore);
        SaveItemData();
    }

    public void GetGlayFlower()
    {
        glayFlowersScore++;
        mainUI.glayFlowersText.text = glayFlowersScore.ToString();

        Debug.Log("Flower：" + glayFlowersScore);
        SaveItemData();
    }

    public void GetOrangeFlower()
    {
        orangeFlowersScore++;
        mainUI.orangeFlowerText.text = orangeFlowersScore.ToString();

        SaveItemData();

    }
    public void GetWhiteFlower()
    {
        whiteFlowersScore++;
        mainUI.whiteFlowerText.text = whiteFlowersScore.ToString();

        SaveItemData();

    }

    //フライトアイテムの効果
    public void GetFightItem()
    {
        isFlightTimeRunning = true;

        Debug.Log("GetFightItemメソッドが呼び出されました。");
        // ジャンプ制御の無効化と時間制限の減少をコルーチンで処理
        StartCoroutine(EnableFlightMode());

        mainUI.flightItemImage.gameObject.SetActive(true);
        mainUI.flightRimitTimeText.gameObject.SetActive(true);

        // 無限ジャンプを10秒間有効にする
        flightRimitTime += 10.0f;
    }

    public void GetRareItem()
    {
        rareItem++;
    }
    //フライトアイテムを入手した時の表示
    public void StartFlightTimer()
    {
        if (isFlightTimeRunning)
        {

            if (_runPlayerController.isFlightMode)
            {
                flightRimitTime -= Time.deltaTime;
                mainUI.flightRimitTimeText.text = flightRimitTime.ToString("F1");

                if (flightRimitTime < 0)
                {
                    flightRimitTime = 0;
                    mainUI.flightItemImage.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StopFlightTimer()
    {
        isFlightTimeRunning = false;
    }

    private IEnumerator EnableFlightMode()
    {
        if (_runPlayerController != null)
        {
            Debug.Log("フライトモード開始");

            //一時的にisGroundedをtrueに設定
            _runPlayerController.isFlightMode = true;//フライトモードを有効化
            yield return new WaitForSeconds(10.0f);//10秒待機
            _runPlayerController.isFlightMode = false;//フライトモードを無効化

            Debug.Log("フライトモード終了");
        }
    }

}


[System.Serializable]
public class MainUI
{
    public Canvas mainCanvas;
    public TextMeshProUGUI flowersText;    // 花の所持数のテキスト
    public TextMeshProUGUI glayFlowersText;    // 花の所持数のテキスト
    public TextMeshProUGUI orangeFlowerText;    // 花束の所持数のテキスト
    public TextMeshProUGUI whiteFlowerText;    // 花束の所持数のテキスト
    public TextMeshProUGUI flightRimitTimeText;
    public TextMeshProUGUI timerText;      // タイマーのテキスト

    public Image flightItemImage;


}