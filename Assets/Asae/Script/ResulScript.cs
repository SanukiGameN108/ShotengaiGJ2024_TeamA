using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResulScript : MonoBehaviour
{
    private int maxScorepoint = 0;
    private int scorePoint = 0;

    private float timer=0;
    private float maxtime=1;
    private bool isTime;//�w�莞�Ԍo�߂������i�X�R�A�J�E���g�A�j���[�V�����p

    [SerializeField]
    private TextMeshProUGUI showScorePointText=null;
    // Start is called before the first frame update

    public void SetMaxScorePoint(int i)
    {
        maxScorepoint = i;
    }
    public void SetScorePoint(int i)
    {
        scorePoint = i;
        showScorePointText.text = "Point  " + scorePoint.ToString() + "/" + maxScorepoint.ToString();
    }
    void Start()
    {
        SetMaxScorePoint(10);
      
        StartCoroutine("ShowResultEvent");
    }

    public void StartResultEvent()
    {
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isTime)
        {
            timer += Time.deltaTime;
            scorePoint = Random.Range(0,maxScorepoint);
            showScorePointText.text = "Point  " + scorePoint.ToString() + "/" + maxScorepoint.ToString();
            if (timer >= maxtime)
            {
                SetScorePoint(5);
                isTime = false;
            }
        }
    }

    IEnumerator ShowResultEvent()
    {
        //Result�\��
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        //�X�R�A�̕\���ƃJ�E���g�A�j���[�V����
        isTime = true;
        this.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        //�]���̕\��
        this.transform.GetChild(2).gameObject.SetActive(true);
        yield return null;
    }
}
