using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LoadImageManager : MonoBehaviour
{
    public TextMeshProUGUI loadText;

    private void Start()
    {
        //DisplayLoadCanvas();
        SetDefaultPosition();
    }

    public void HideLoadImage()
    {
        this.gameObject.SetActive(false);

    }


    public void DisplayLoadInCanvas()
    {
        this.gameObject.SetActive(true);
        loadText.fontSize = 40;
        loadText.text = "NOW LOADING...";


        var transformCache = transform;

        //初期位置を保持
        //var defaultPosition = transformCache.localPosition;

        //移動
        var newPosition = new Vector3(0, 0);


        //移動アニメーションを開始する
        transformCache.DOLocalMove(newPosition, 2f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { });
    }

    public void DisplayLoadOutCanvas()
    {
        this.gameObject.SetActive(true);

        var transformCache = transform;

        //移動位置を設定
        var newPosition= new Vector3(0, 500f);

        //移動アニメーションを開始する
        transformCache.DOLocalMove(newPosition, 1.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { });
    }


    public IEnumerator SetOpenning()
    {
        this.gameObject.SetActive(true);
        loadText.fontSize = 30;
        loadText.text = "みんなが手伝いをほしがっている...";

        var transformCache = transform;

        //移動位置を設定
        var newPosition = new Vector3(0, 500f);


        yield return new WaitForSeconds(4.0f);//4秒待機

        //移動アニメーションを開始する
        transformCache.DOLocalMove(newPosition, 1.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { });
    }

public IEnumerator SetStompOpenning()
    {
        this.gameObject.SetActive(true);
        loadText.fontSize = 30;
        loadText.text = "生地をこねてパンをつくろう";

        var transformCache = transform;

        //移動位置を設定
        var newPosition = new Vector3(0, 500f);


        yield return new WaitForSeconds(4.0f);//4秒待機

        //移動アニメーションを開始する
        transformCache.DOLocalMove(newPosition, 1.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { });

    }

    public IEnumerator SetRunOpenning()
    {
        this.gameObject.SetActive(true);
        loadText.fontSize = 30;
        loadText.text = "走って花を積んでこよう";

        var transformCache = transform;

        //移動位置を設定
        var newPosition = new Vector3(0, 500f);


        yield return new WaitForSeconds(4.0f);//4秒待機

        //移動アニメーションを開始する
        transformCache.DOLocalMove(newPosition, 1.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { });

    }


    private void SetDefaultPosition()
    {
        var transformCache = transform;

        //初期位置を保持
        var defaultPosition = transformCache.localPosition;

    }
}
