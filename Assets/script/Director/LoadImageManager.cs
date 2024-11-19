using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LoadImageManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        //DisplayLoadCanvas();
        this.gameObject.SetActive(false);

    }
    public void DisplayLoadCanvas()
    {
        this.gameObject.SetActive(true);

        var transformCache = transform;

        //初期位置を保持
        var defaultPosition = transformCache.localPosition;

        //上に移動
        transformCache.localPosition = new Vector3(0, 500f);


        //移動アニメーションを開始する
        transformCache.DOLocalMove(defaultPosition, 1.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { });
    }

}
