using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;//アイテムのプレハブのゲームオブジェクト
    [SerializeField] private Transform gridLayoutGroup;//Grid Layout Groupのトランスフォーム
    [SerializeField] private SaveLoadManager _saveLoadManager;//SaveLoadManagerのスクリプト

    [SerializeField] private Sprite itemImage1;
    [SerializeField] private Sprite itemImage2;
    [SerializeField] private Sprite itemImage3;

   // [SerializeField] private Button buyButton;

    // public ShoppingUI shoppingUI;

    private int totalMoney = 0;//所持金(SaveLoadManagerから取得)

    private List<ItemData> items = new List<ItemData>();//アイテムデータ一覧のリスト

    void Start()
    {
        //所持金を取得
        _saveLoadManager.LoadTotalMoneyData(out totalMoney);

        //アイテムデータを初期化
        InitializeitemData();

        //アイテムリストを生成
        GeneateItemList();
    }

    //アイテムデータを初期化するメソッド
    private void InitializeitemData()
    {
        items.Add(new ItemData("アイテム1", 0, null, ""));
        items.Add(new ItemData("アイテム2", 0, null, "青い花を5本取ってくる"));
        items.Add(new ItemData("アイテム3", 0, null, "ブーケを持ってくる"));
    }

    //アイテムリストを生成するメソッド
    private void GeneateItemList()
    {

        foreach (var item in items)
        {
            //アイテムプレハブを生成
            GameObject itemObj = Instantiate(itemPrefab, gridLayoutGroup);
            if (itemObj == null)
            {
                Debug.Log("itemPrefabの生成に失敗しました");
                continue;

            }

            // アイテムデータを設定
            var itemName = itemObj.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
            var priceText = itemObj.transform.Find("Price")?.GetComponent<TextMeshProUGUI>();
            var itemImage = itemObj.transform.Find("ItemImage")?.GetComponent<Image>();
            var taskText = itemObj.transform.Find("Task")?.GetComponent<TextMeshProUGUI>();

            if (itemName == null || priceText == null || itemImage == null || taskText == null)
            {
                Debug.LogError("プレハブ内の必要なコンポーネントが見つかりませんでした。");
                continue;
            }

            itemName.text = item.Name;
            priceText.text = item.Price + "yen";
            itemImage.sprite = item.Image;
            taskText.text = item.Task;

            // 各itemObj内のButtonを取得
            Button itemButton = itemObj.GetComponent<Button>();

            //ボタンの有効・無効
            itemButton.interactable = totalMoney >= item.Price;

            //ボタンのクリック処理
            itemButton.onClick.AddListener(() => OnBuyItem(item));

        }


    }

    //ボタンのクリック処理
    private void OnBuyItem(ItemData item)
    {
        if (totalMoney >= item.Price)
        {
            totalMoney -= item.Price;
            Debug.Log(item.Name + "を購入");

            //ボタンの再更新
            GeneateItemList();

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
    public string Task;//条件(仮)

    public ItemData(string name, int price, Sprite image, string task)
    {
        Name = name;
        Price = price;
        Image = image;
        Task = task;
    }
}

//[System.Serializable]
//public class ShoppingUI
//{
//    public Canvas shoppingCanvas;
//    public Button itemSelectButton;
//    public Image ItemImage;
//    public TextMeshProUGUI itemName;
//    public TextMeshProUGUI itemPrice;
//    public TextMeshProUGUI itemTask
//        ;
//}