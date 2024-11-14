using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private Button stompGameBtn;
    [SerializeField] private Button runGameBtn;
    [SerializeField] private Button exitBtn;

    private void Start()
    {
        OnButtonClick();
    }
    private void OnButtonClick()
    {
        stompGameBtn.onClick.AddListener(()=>GoStompGame());
        runGameBtn.onClick.AddListener(() => GoRunGame());
        exitBtn.onClick.AddListener(() => GoExit());

    }
    private void GoStompGame()
    {
        SceneManager.LoadScene("StompGamescene");
    }
    private void GoRunGame()
    {
        SceneManager.LoadScene("StoryScene");

    }

    private void GoExit()
    {
        Application.Quit();

    }

}
