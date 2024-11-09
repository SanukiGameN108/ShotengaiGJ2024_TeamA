using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ResulScript : MonoBehaviour
{
    private int maxScorepoint = 10;//デフォルトで10
    private int scorePoint = 0;

    private float timer=0;
    private float maxtime=1;
    private bool isTime;//指定時間経過したか（スコアカウントアニメーション用
    private bool isReStart=false;

    [SerializeField]
    private TextMeshProUGUI showScorePointText=null;
    [SerializeField]
    private GameObject returnTextobj = null;
    [SerializeField]
    private Image image;

    [SerializeField]
    private GameObject badTextobj = null;
    [SerializeField]
    private GameObject goodTextobj = null;
    [SerializeField]
    private GameObject greatTextobj = null;
   

    // Start is called before the first frame update

    public void SetMaxScorePoint(int i)
    {
        maxScorepoint = i;
    }
    public void SetScorePoint(int i)
    {
        scorePoint = i;
    }
  

    //呼び出すとリザルト表示
    public void StartResultEvent()
    {
        StartCoroutine("ShowResultEvent");

    }
    // Update is called once per frame
    void Update()
    {
        

        if (isTime)
        {
            timer += Time.deltaTime;
           int val = Random.Range(0,maxScorepoint);
            showScorePointText.text = "Point  " + val.ToString() + "/" + maxScorepoint.ToString();
            if (timer >= maxtime)
            {
                showScorePointText.text = "Point  " + scorePoint.ToString() + "/" + maxScorepoint.ToString();
                isTime = false;
            }
        }

        //ゲーム終了後のリスタート機能
        if (isReStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
                isReStart = false;
            }
        }
    }
    int aa;
    IEnumerator ShowResultEvent()
    {
        //Result表示
        this.transform.GetChild(1).gameObject.SetActive(true);
        image.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        //スコアの表示とカウントアニメーション
        isTime = true;
        this.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Debug.Log(aa =maxScorepoint / 3);
        //評価の表示
        //スコアの範囲によって表示する評価オブジェクトを変更する

        if (scorePoint > maxScorepoint / 3)
        {
            if (scorePoint >= maxScorepoint - 2)
            {
                //スコアがMaxから-2した数以上ならGreat
                greatTextobj.SetActive(true);
            }
            else if (scorePoint < maxScorepoint - 2 || scorePoint >= maxScorepoint / 3)
            {
                //スコアがMaxから-2した数と1/3以上ならGood
                goodTextobj.SetActive(true);
            }
        }
        else
        {
            //スコアが1/3以下ならBad評価
            badTextobj.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        isReStart = true;
        returnTextobj.SetActive(true);

        yield return null;
    }
}
