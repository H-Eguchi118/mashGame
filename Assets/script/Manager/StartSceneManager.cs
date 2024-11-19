using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private LoadImageManager _loadImageManager;
    [SerializeField] private Button stompGameBtn;
    [SerializeField] private Button runGameBtn;
    [SerializeField] private Button exitBtn;

    private void Start()
    {
        OnButtonClick();
    }
    private void OnButtonClick()
    {
        stompGameBtn.onClick.AddListener(() => StartCoroutine(GoStompGame()));
        runGameBtn.onClick.AddListener(() => StartCoroutine(GoRunGame()));
        exitBtn.onClick.AddListener(() => StartCoroutine(GoExit()));

    }
    private IEnumerator GoStompGame()
    {
        _loadImageManager.DisplayLoadCanvas();

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("StompGameScene");
    }
    private IEnumerator GoRunGame()
    {
        _loadImageManager.DisplayLoadCanvas();

        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("RunGameScene");

    }

    private IEnumerator GoExit()
    {
        _loadImageManager.DisplayLoadCanvas();
        _loadImageManager.text.text= "GOOD BYE!";

        yield return new WaitForSeconds(3.0f);

        Application.Quit();

    }

}
