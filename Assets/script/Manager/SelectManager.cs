using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.Events;

public class SelectManager : MonoBehaviour
{
    [SerializeField]private ShopManager _shopManager;
    [SerializeField]private LoadImageManager _loadImageManager;
    [SerializeField] private ScoreCalculation _scoreCalculation;
    [SerializeField] private AudioManager _audioManager;

    public SelectUI selectUI;

    void Start()
    {
        OnButtonClick();
        _loadImageManager.DisplayLoadOutCanvas();
        //_scoreCalculation.SetRanGameScore();
    }

    private void ViewRanGameScore()
    {
    }
    public void SetCanvas()
    {
        selectUI.selectCanvas.gameObject.SetActive(true);
    }
    private void OnButtonClick()
    {
        
        selectUI.shoppingButton.onClick.AddListener(() => Shopping());
        selectUI.goRunButton.onClick.AddListener(() => StartCoroutine(GoRunningScene()));
        selectUI.goBackButton.onClick.AddListener(() => StartCoroutine(GoStartScene()));
        selectUI.goStompButton.onClick.AddListener(() => StartCoroutine(GoStompScene()));
    }

    private void Shopping()
    {
        _audioManager.PlayDecisionButtonSound();
        _shopManager.SetPanel();   //各パネルの初期セット
    }

    private IEnumerator GoRunningScene()
    {
        _audioManager.PlayDecisionButtonSound();

        _loadImageManager.DisplayLoadInCanvas();

        yield return new WaitForSeconds(4.0f);//4秒待機

        SceneManager.LoadScene("RunGameScene");
    }
    private IEnumerator GoStompScene()
    {
        _audioManager.PlayDecisionButtonSound();

        _loadImageManager.DisplayLoadInCanvas();

        yield return new WaitForSeconds(4.0f);//4秒待機

        SceneManager.LoadScene("StompGameScene");
    }


    private IEnumerator GoStartScene()
    {
        _audioManager.PlayDecisionButtonSound();

        _loadImageManager.DisplayLoadInCanvas();

        yield return new WaitForSeconds(4.0f);//4秒待機

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