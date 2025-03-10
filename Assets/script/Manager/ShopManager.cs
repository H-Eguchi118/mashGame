using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;//アイテムのプレハブのゲームオブジェクト
    [SerializeField] private Transform gridLayoutGroup;//Grid Layout Groupのトランスフォーム
    [SerializeField] private SaveLoadManager _saveLoadManager;//SaveLoadManagerのスクリプト
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private Sprite itemKnit;
    [SerializeField] private Sprite itemCreamSoda;
    [SerializeField] private Sprite itemBusket;
    [SerializeField] private Canvas shoppingCanvas;
    [SerializeField] private Canvas selectCanvas;
    [SerializeField] private Button closedButton;
    [SerializeField] private TextMeshProUGUI totalMoneyText;

    public ConfirmationUI ConfirmationUI;
    private int totalMoney = 0;//所持金(SaveLoadManagerから取得)

    private List<ItemData> items = new List<ItemData>();//アイテムデータ一覧のリスト
    private GameObject itemObj = null;

    private void Start()
    {

        shoppingCanvas.gameObject.SetActive(false);
        ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);
        ConfirmationUI.boughtCanvas.gameObject.SetActive(false);
    }

    public void SetPanel()
    {

        shoppingCanvas.gameObject.SetActive(true);
        selectCanvas.gameObject.SetActive(false);
        ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);
        ConfirmationUI.boughtCanvas.gameObject.SetActive(false);

        //所持金を取得
        _saveLoadManager.LoadTotalMoneyData(out totalMoney);
        totalMoneyText.text = "持っているお金：" + totalMoney + "マネ";


        if (itemObj == null)
        {
            //アイテムデータを初期化
            InitializeitemData();

            //itemObjがnllだったらアイテムリストを生成
            GeneateItemList();
        }

    }

    //アイテムデータを初期化するメソッド
    private void InitializeitemData()
    {
        items.Add(new ItemData("クリームソーダ", 10, itemCreamSoda, "ハウス装飾アイテム。家に置けるが飲めない。"));
        items.Add(new ItemData("かご", 50, itemBusket, "ハウス装飾アイテム。家における。"));
        items.Add(new ItemData("ニット帽", 100, itemKnit, "プレイヤー装飾アイテム。被れる。"));
    }

    //アイテムリストを生成するメソッド
    private void GeneateItemList()
    {
        //レングスが0なら生成しないでreturnする　ealdreturn nullというやり方もある
        //DestroyImmediateで全削除

        foreach (var item in items)
        {
            //アイテムプレハブを生成
            itemObj = Instantiate(itemPrefab, gridLayoutGroup);
            if (itemObj == null)
            {
                Debug.Log("itemPrefabの生成に失敗しました");
                continue;

            }

            // アイテムデータを設定
            var itemName = itemObj.transform.Find("ItemName")?.GetComponentInChildren<Text>();
            var priceText = itemObj.transform.Find("Price")?.GetComponent<Text>();
            var itemImage = itemObj.transform.Find("ItemImage")?.GetComponent<Image>();
            var taskText = itemObj.transform.Find("Task")?.GetComponent<Text>();

            if (itemName == null || priceText == null || itemImage == null || taskText == null)
            {
                Debug.LogError("プレハブ内の必要なコンポーネントが見つかりませんでした。");
                continue;
            }

            itemName.text = item.Name;
            priceText.text = item.Price + "マネ";
            itemImage.sprite = item.Image;
            taskText.text = item.Info;

            // 各itemObj内のButtonを取得
            Button itemButton = itemObj.GetComponent<Button>();

            // ボタンとアイテムを関連付ける
            BindButtonToItem(itemButton, item);

            //ボタンの有効・無効
            itemButton.interactable = totalMoney >= item.Price;


            //ボタンのクリック処理
            itemButton.onClick.AddListener(() => OnSelectItem(item));

            closedButton.onClick.AddListener(() => ClosedShoppingPanel());
            ConfirmationUI.noButton.onClick.AddListener(() => ClosedConfirmationPanel());

            ConfirmationUI.YesButton.onClick.AddListener(() => OnBuyItem1(item));

        }

    }

    // ボタンとアイテムを関連付けるヘルパーメソッド
    private void BindButtonToItem(Button button, ItemData item)
    {
        button.onClick.AddListener(() => OnSelectItem(item));
    }

    //各アイテムボタンのクリック処理
    private void OnBuyItem1(ItemData item)
    {
        _audioManager.PlayBuyButtonSound();

        if (totalMoney >= item.Price)
        {
            totalMoney -= item.Price;
            Debug.Log($"{item.Name}を{item.Price}マネで購入");
            ConfirmationUI.boughtText.text = $"{item.Name}を買いました";

            ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);
            ConfirmationUI.boughtCanvas.gameObject.SetActive(true);
            totalMoneyText.text = $"持っているお金：{totalMoney}マネ";

            // 所持金を保存
            SaveTotalMoney();

            // ボタンのリスナーを更新
            ConfirmationUI.closeButton.onClick.RemoveAllListeners();
            ConfirmationUI.closeButton.onClick.AddListener(() => ConfirmationClosed(item));
        }
    }



    private void ConfirmationClosed(ItemData item)
    {
        ConfirmationUI.boughtCanvas.gameObject.SetActive(false);

    }

    private void OnSelectItem(ItemData item)
    {
        _audioManager.PlayDecisionButtonSound();

        if (totalMoney >= item.Price)
        {
            //購入確認のポップアップ表示
            ConfirmationUI.confirmationCanvas.gameObject.SetActive(true);
            ConfirmationUI.boughtCanvas.gameObject.SetActive(false);
        }
        // ボタンリスナーを一旦クリア
        ConfirmationUI.YesButton.onClick.RemoveAllListeners();

        // 現在のアイテムでリスナーを設定
        ConfirmationUI.YesButton.onClick.AddListener(() => OnBuyItem1(item));
    }

    //各パネルを閉じる処理
    private void ClosedShoppingPanel()
    {
        _audioManager.PlayCancelButtonSound();

        if (shoppingCanvas)
        {
            shoppingCanvas.gameObject.SetActive(false);
            selectCanvas.gameObject.SetActive(true);
        }
    }

    //total Moneyのセーブ
    public void SaveTotalMoney()
    {
        _saveLoadManager.SaveTotalMoneyData(totalMoney);
    }

    //購入確認パネルの閉じる処理
    private void ClosedConfirmationPanel()
    {
        _audioManager.PlayCancelButtonSound();

        if (ConfirmationUI.confirmationCanvas)
        {
            ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);

        }
    }

}

//アイテムのデータクラス
[System.Serializable]
public class ItemData
{
    public string Name;//名前
    public int Price;//金額
    public Sprite Image; //イメージ
    public string Info;//条件(仮)

    public ItemData(string name, int price, Sprite image, string info)
    {
        Name = name;
        Price = price;
        Image = image;
        Info = info;
    }
}

[System.Serializable]
public class ConfirmationUI
{
    public Canvas confirmationCanvas;
    public TextMeshProUGUI checkText;
    public Button YesButton;
    public Button noButton;
    public Canvas boughtCanvas;
    public TextMeshProUGUI boughtText;
    public Button closeButton;
}

