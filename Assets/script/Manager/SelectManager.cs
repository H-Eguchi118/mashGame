using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    [SerializeField]private ShopManager _shopManager;
    public SelectUI selectUI;

    void Start()
    {
        OnButtonClick();
    }
    public void SetCanvas()
    {
        selectUI.selectCanvas.gameObject.SetActive(true);
    }
    private void OnButtonClick()
    {
        selectUI.shoppingButton.onClick.AddListener(() => Shopping());
        selectUI.goRunButton.onClick.AddListener(() => GoRunningScene());
        selectUI.goBackButton.onClick.AddListener(() => GoSelectScene());
        selectUI.goStompButton.onClick.AddListener(() => GoStompScene());
    }

    private void Shopping()
    {
        _shopManager.SetPanel();   //各パネルの初期セット
    }

    private void GoRunningScene()
    {
        SceneManager.LoadScene("RunGameScene");
    }
    private void GoStompScene()
    {
        SceneManager.LoadScene("StompGameScene");
    }


    private void GoSelectScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}



[System.Serializable]
public class SelectUI
{
    public Canvas selectCanvas;
    public Button shoppingButton;
    public Button goRunButton;
    public Button goStompButton;
    public Button goBackButton;
}