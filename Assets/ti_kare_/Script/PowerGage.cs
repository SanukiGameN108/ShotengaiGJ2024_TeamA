using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGage : MonoBehaviour
{
    public Slider slider;
    public float gage_speed;
    bool is_moving_gage;

    private void Start()
    {
        HideGage();
    }

    public void StartGage()
    {
        slider.value = 0;
        slider.gameObject.SetActive(true);
        is_moving_gage = true;
    }

    public float StopAndGet()
    {
        is_moving_gage = false;
        return slider.value;
    }

    public void HideGage()
    {
        is_moving_gage = false;
        slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!is_moving_gage) { return; }

        slider.value += gage_speed * Time.deltaTime;
        if (1.0f <= slider.value)
        {
            slider.value -= 1.0f;
        }
    }
}
