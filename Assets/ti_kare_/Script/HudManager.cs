using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoSingleton<HudManager>
{
    public TMPro.TextMeshProUGUI ato_n_da;
    void Start()
    {
        ato_n_da.text = "";
    }

    void Update()
    {
        if (!MainGameManager.Instance.IsMainGame()) { return; }

        ato_n_da.text = "‚ ‚Æ" + ScoreManager.Instance.GetShottableCount().ToString() + "‘Å";
    }
}
