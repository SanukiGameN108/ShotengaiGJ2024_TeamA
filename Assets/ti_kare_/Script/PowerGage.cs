using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGage : MonoBehaviour
{
    public Image fillImage;
    public float gage_speed;
    bool is_moving_gage;

    private void Start()
    {
        HideGage();
    }

    public void StartGage()
    {
        fillImage.fillAmount = 0;
        fillImage.gameObject.SetActive(true);
        is_moving_gage = true;
    }

    public float StopAndGet()
    {
        is_moving_gage = false;
        return fillImage.fillAmount;
    }

    public void HideGage()
    {
        is_moving_gage = false;
        fillImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!is_moving_gage) { return; }

        fillImage.fillAmount += gage_speed * Time.deltaTime;
        if (1.0f <= fillImage.fillAmount)
        {
            fillImage.fillAmount -= 1.0f;
        }
    }
}
