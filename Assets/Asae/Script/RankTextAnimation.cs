using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RankTextAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject badTextobj = null;
    [SerializeField]
    private GameObject goodTextobj = null;
    [SerializeField]
    private GameObject greatTextobj = null;

    private bool isOnetime = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (greatTextobj.activeSelf)
        {
            if (!isOnetime)
            {
                GreatAnimation();
                isOnetime = true;
            }
        }

        if (goodTextobj.activeSelf)
        {
            if (!isOnetime)
            {
                GoodAnimation();
                isOnetime = true;
            }
        }

        if (badTextobj.activeSelf)
        {
            if (!isOnetime)
            {
                BadAnimation();
                isOnetime = true;
            }
        }
    }

    void GreatAnimation()
    {
        greatTextobj.transform.DOScale(
        new Vector3(1.5f,1.5f,1.5f),  //終了時点のScale
        1.0f       //時間
        ).SetLoops(-1, LoopType.Yoyo);
    }

    void BadAnimation()
    {
             badTextobj.transform.DORotate(
            new Vector3(0f,0f,45f),   // 終了時点のRotation
            1.0f                    // アニメーション時間
        ).SetLoops(-1, LoopType.Yoyo); ;
    }

    void GoodAnimation()
    {
            goodTextobj.transform.DOMove(
            new Vector2(goodTextobj.transform.position.x, goodTextobj.transform.position.y+50),  //移動後の座標
            0.7f       //時間
            ).SetLoops(-1, LoopType.Yoyo);
    }


}
