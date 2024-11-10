using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoSingleton<HudManager>
{
    public TMPro.TextMeshProUGUI ato_n_da;
    public Color normal_color;
    public Color two_color;
    public Color one_color;

    public GameObject ato_n_da_obj;

    void Start()
    {
        ato_n_da.text = "";
    }

    void Update()
    {
        if (!MainGameManager.Instance.IsMainGame())
        {
            ato_n_da.text = "";
            ato_n_da_obj.SetActive(false);
            return;
        }
        ato_n_da_obj.SetActive(true);

        var count = ScoreManager.Instance.GetShottableCount();
        ato_n_da.text = count.ToString();

        if (count == 1)
            ato_n_da.color = one_color;
        else if (count == 2)
            ato_n_da.color = two_color;
        else
            ato_n_da.color = normal_color;
    }
}
