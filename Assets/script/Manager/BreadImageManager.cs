using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class BreadImageManager : MonoBehaviour
{
    [SerializeField] private MashGameDirector _mashGameDirector;
    [SerializeField] private GameObject breadPrefub;
    [SerializeField] private GameObject richBreadPrefub;
    [SerializeField] private GameObject croissantPrefub;
    [SerializeField] private GameObject carryPrefub;
    [SerializeField] Transform breadContainer;// Grid Layout Groupを持つ親オブジェクトを設定
    [SerializeField] Transform richBbreadContainer;// Grid Layout Groupを持つ親オブジェクトを設定
    [SerializeField] Transform croissantContainer;// Grid Layout Groupを持つ親オブジェクトを設定
    [SerializeField] Transform carryContainer;// Grid Layout Groupを持つ親オブジェクトを設定

    //各パンのスコア
    private int carryScore = 0;
    private int croissantScore = 0;
    private int richBreadScore = 0;
    private int breadScore = 0;


    void Start()
    {
    }

    void Update()
    {
    }

    public void CheckBreadScore()
    {
        if (_mashGameDirector.mashCount <= 100)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                breadScore++;
                AddBreadImage();
                Debug.Log($"ふつうの食パン：{breadScore}こ");

            }
        }

        if (100 < _mashGameDirector.mashCount && _mashGameDirector.mashCount <= 200)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                richBreadScore++;
                AddRichBreadImage();
                Debug.Log($"リッチな食パン：{richBreadScore}こ");

            }
        }

        if (200 < _mashGameDirector.mashCount && _mashGameDirector.mashCount <= 300)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                croissantScore++;
                croissantBreadImage();
                Debug.Log($"ゆうがなクロワッサン：{croissantScore}こ");

            }
        }
        if (300 < _mashGameDirector.mashCount && _mashGameDirector.mashCount <= 400)
        {
            if (_mashGameDirector.mashCount % 20 == 0)
            {
                carryScore++;
                AddCarryBreadImage();
                Debug.Log($"みんな大好きカレーパン：{carryScore}こ");

            }
        }
    }

    void AddBreadImage()
    {
        //各prefabをインスタンス化し、breadContainerの子として配置
        GameObject newBread = Instantiate(breadPrefub, breadContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }

    void AddRichBreadImage()
    {
        GameObject newBread = Instantiate(richBreadPrefub, richBbreadContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }

    void croissantBreadImage()
    {
        GameObject newBread = Instantiate(croissantPrefub, croissantContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }

    void AddCarryBreadImage()
    {
        GameObject newBread = Instantiate(carryPrefub, carryContainer);
        newBread.transform.localScale = Vector3.one;//サイズ調整
    }
}
