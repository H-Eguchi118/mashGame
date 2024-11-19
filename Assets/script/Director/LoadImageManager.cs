using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadImageManager : MonoBehaviour
{

    public void DisplayLoadCanvas()
    {
        var transformCache = transform;

        //初期位置を保持
        var defaultPosition = transformCache.localPosition;

        //上に移動
        transformCache.localPosition = new Vector3(0, 500f);


        //移動アニメーションを開始する
        transformCache.DOLocalMove(defaultPosition, 4.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { });
    }

}
