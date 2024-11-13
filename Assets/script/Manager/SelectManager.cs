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
    }

    private void Shopping()
    {
        _shopManager.SetPanel();   //各パネルの初期セット
    }

    private void GoRunningScene()
    {
        SceneManager.LoadScene("RunningScene");
    }

    private void GoSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }
}



[System.Serializable]
public class SelectUI
{
    public Canvas selectCanvas;
    public Button shoppingButton;
    public Button goRunButton;
    public Button goBackButton;
}