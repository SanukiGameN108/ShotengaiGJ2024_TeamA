using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform titleLogoObj = null;
    [SerializeField]
    private Transform titleButtonObj = null;
    [SerializeField]
    private TMPro.TextMeshProUGUI click_to_start_text;
    [SerializeField]
    private float text_flash_interval;

    float text_flash_timer;
    void Start()
    {
        titleLogoObj.DOMoveY(753, 2f);
        titleButtonObj.DOMoveY(331, 2f);
    }

    void Update()
    {
        text_flash_timer += Time.deltaTime;
        if(text_flash_interval <= text_flash_timer)
        {
            click_to_start_text.enabled = !click_to_start_text.enabled;
            text_flash_timer = 0;
        }
    }

    public void StartButtonClicked()
    {
        MainGameManager.Instance.StartMainGame();
    }
}
