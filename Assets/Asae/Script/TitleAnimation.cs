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
    // Start is called before the first frame update
    void Start()
    {
        titleLogoObj.DOMoveY(753, 2f);
        titleButtonObj.DOMoveY(331, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
