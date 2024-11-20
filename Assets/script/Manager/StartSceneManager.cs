using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] private LoadImageManager _loadImageManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Button stompGameBtn;
    [SerializeField] private Button runGameBtn;
    [SerializeField] private Button exitBtn;

    private void Start()
    {

        StartCoroutine(_loadImageManager.SetOpenning());

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
        _audioManager.PlayDecisionButtonSound();
        _loadImageManager.DisplayLoadInCanvas();
        _loadImageManager.loadText.text = "�p����������`��";


        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("StompGameScene");
    }
    private IEnumerator GoRunGame()
    {
        _audioManager.PlayDecisionButtonSound();
        _loadImageManager.DisplayLoadInCanvas();
        _loadImageManager.loadText.text = "�ԉ�������`��";


        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("RunGameScene");

    }

    private IEnumerator GoExit()
    {
        _audioManager.PlayDecisionButtonSound();

        _loadImageManager.DisplayLoadInCanvas();
        _loadImageManager.loadText.text = "�����ꂳ��!";

        yield return new WaitForSeconds(5.0f);
        _audioManager.StopStartBgm();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;  // �G�f�B�^�p
#else
    Application.Quit();  // �r���h�ς݃A�v���p
#endif

    }

}
