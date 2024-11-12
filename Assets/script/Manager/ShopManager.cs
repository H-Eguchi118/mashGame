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
    [SerializeField] private Canvas shoppingCanvas;
    [SerializeField] private Canvas selectCanvas;
    [SerializeField] private Button closedButton;

    public ConfirmationUI ConfirmationUI;
    public UnabelUI unabelUI;



    private int totalMoney = 0;//所持金(SaveLoadManagerから取得)

    private List<ItemData> items = new List<ItemData>();//アイテムデータ一覧のリスト

    void Start()
    {
        //各パネルの初期セット
        //SetPanel();

        //所持金を取得
        _saveLoadManager.LoadTotalMoneyData(out totalMoney);

        //アイテムデータを初期化
        InitializeitemData();

        //アイテムリストを生成
        GeneateItemList();
    }

    public void SetPanel()
    {
        shoppingCanvas.gameObject.SetActive(true);
        selectCanvas.gameObject.SetActive(false);
        ConfirmationUI.confirmationCanvas.gameObject.SetActive(false);
        unabelUI.unableCanvas.gameObject.SetActive(false);

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
            priceText.text = item.Price + "yen";
            itemImage.sprite = item.Image;
            taskText.text = item.Task;

            // 各itemObj内のButtonを取得
            Button itemButton = itemObj.GetComponent<Button>();

            //ボタンの有効・無効
            itemButton.interactable = totalMoney >= item.Price;

            //ボタンのクリック処理
            itemButton.onClick.AddListener(() => OnSelectItem(item));
            ConfirmationUI.YesButton.onClick.AddListener(() => OnBuyItem(item));

            closedButton.onClick.AddListener(() => ClosedPanel());
            unabelUI.returnButton.onClick.AddListener(()=> ClosedPanel());
            ConfirmationUI.noButton.onClick.AddListener(() => ClosedPanel());

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

    private void ClosedPanel()
    {
        if (shoppingCanvas)
        {
            shoppingCanvas.gameObject.SetActive(false);
            selectCanvas.gameObject.SetActive(true);
        }
        if (unabelUI.unableCanvas)
        {
            unabelUI.unableCanvas.gameObject.SetActive(false);

        }

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
    public string Task;//条件(仮)

    public ItemData(string name, int price, Sprite image, string task)
    {
        Name = name;
        Price = price;
        Image = image;
        Task = task;
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