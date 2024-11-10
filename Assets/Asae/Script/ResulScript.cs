using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ResulScript : MonoBehaviour
{
    public const int maxScorepoint = 30;//�f�t�H���g��10

    private float timer=0;
    private float maxtime=1;
    private bool isTime;//�w�莞�Ԍo�߂������i�X�R�A�J�E���g�A�j���[�V�����p
    private bool isReStart=false;

    [SerializeField]
    private CanvasGroup canvasGroup = null;

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
        //maxScorepoint = i;
    }

    //�Ăяo���ƃ��U���g�\��
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
            showScorePointText.text = val.ToString() + "/" + maxScorepoint.ToString() + " pts";
            if (timer >= maxtime)
            {
                var scorePoint = ScoreManager.Instance.CountPoint();
                showScorePointText.text = scorePoint.ToString() + "/" + maxScorepoint.ToString() + " pts";
                isTime = false;
            }
        }

        //�Q�[���I����̃��X�^�[�g�@�\
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
        var time = 0f;
        while (canvasGroup.alpha < 1)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = time / 0.5f;
            yield return null;
        }

        //Result�\��
        this.transform.GetChild(1).gameObject.SetActive(true);
        image.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        //�X�R�A�̕\���ƃJ�E���g�A�j���[�V����
        isTime = true;
        this.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Debug.Log(aa =maxScorepoint / 3);
        //�]���̕\��
        //�X�R�A�͈̔͂ɂ���ĕ\������]���I�u�W�F�N�g��ύX����

        var scorePoint = ScoreManager.Instance.CountPoint();
        if (scorePoint > maxScorepoint / 3)
        {
            if (scorePoint >= maxScorepoint - 2)
            {
                //�X�R�A��Max����-2�������ȏ�Ȃ�Great
                greatTextobj.SetActive(true);
            }
            else if (scorePoint < maxScorepoint - 2 || scorePoint >= maxScorepoint / 3)
            {
                //�X�R�A��Max����-2��������1/3�ȏ�Ȃ�Good
                goodTextobj.SetActive(true);
            }
        }
        else
        {
            //�X�R�A��1/3�ȉ��Ȃ�Bad�]��
            badTextobj.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        isReStart = true;
        returnTextobj.SetActive(true);

        yield return null;
    }
}
