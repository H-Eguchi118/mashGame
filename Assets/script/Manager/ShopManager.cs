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

    [SerializeField] private Sprite itemKnit;
    [SerializeField] private Sprite itemCreamSoda;
    [SerializeField] private Sprite itemBusket;
    [SerializeField] private Canvas shoppingCanvas;
    [SerializeField] private Canvas selectCanvas;
    [SerializeField] private Button closedButton;

    public ConfirmationUI ConfirmationUI;
    public UnabelUI unabelUI;
    private int totalMoney = 0;//所持金(SaveLoadManagerから取得)

    private List<ItemData> items = new List<ItemData>();//アイテムデータ一覧のリスト
    private GameObject itemObj = null;

    private void Start()
    {
        shoppingCanvas.gameObject.SetActive(false);
        ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);

        //所持金を読み込む
        _saveLoadManager.LoadTotalMoneyData(out int totalMoney);

    }

    public void SetPanel()
    {

        shoppingCanvas.gameObject.SetActive(true);
        selectCanvas.gameObject.SetActive(false);
        ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);
        unabelUI.unableCanvas.gameObject.SetActive(false);

        //所持金を取得
        _saveLoadManager.LoadTotalMoneyData(out totalMoney);

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
        items.Add(new ItemData("クリームソーダ", 0, itemCreamSoda, "ハウス装飾アイテム。家に置けるが飲めない。"));
        items.Add(new ItemData("かご", 50, itemBusket, "ハウス装飾アイテム。家における。"));
        items.Add(new ItemData("ニット帽", 100, itemKnit, "プレイヤー装飾アイテム。被れる。(条件：白い花×5)"));
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

            //ボタンの有効・無効
            itemButton.interactable = totalMoney >= item.Price;

            //ボタンのクリック処理
            itemButton.onClick.AddListener(() => OnSelectItem(item));
            ConfirmationUI.YesButton.onClick.AddListener(() => OnBuyItem(item));

            closedButton.onClick.AddListener(() => ClosedShoppingPanel());
            ConfirmationUI.noButton.onClick.AddListener(() => ClosedConfirmationPanel());
            unabelUI.returnButton.onClick.AddListener(() => ClosedUnablePanel());
        }


    }

    //アイテムボタンのクリック処理
    private void OnBuyItem(ItemData item)
    {
        if (totalMoney >= item.Price)
        {
            totalMoney -= item.Price;
            Debug.Log(item.Name + "を購入");
        }
    }

    private void OnSelectItem(ItemData item)
    {
        if (totalMoney >= item.Price)
        {
            //購入確認のポップアップ表示
            ConfirmationUI.confirmationCanvas.gameObject.SetActive(true);
        }
        else
        {
            //買えないよのポップアップ表示
            unabelUI.unableCanvas.gameObject.SetActive(true);
        }
    }

    //各パネルを閉じる処理
    private void ClosedShoppingPanel()
    {
        if (shoppingCanvas)
        {
            shoppingCanvas.gameObject.SetActive(false);
            selectCanvas.gameObject.SetActive(true);

        }


    }

    //購入確認パネルの閉じる処理
    private void ClosedConfirmationPanel()
    {
        if (ConfirmationUI.confirmationCanvas)
        {
            ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);

        }
    }

    //買えないよパネルの閉じる処理
    private void ClosedUnablePanel()
    {
        if (unabelUI.unableCanvas)
        {
            unabelUI.unableCanvas.gameObject.SetActive(false);

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
}

[System.Serializable]
public class UnabelUI
{
    public Canvas unableCanvas;
    public Button returnButton;
}