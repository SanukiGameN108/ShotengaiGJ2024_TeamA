using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public int shotable_count;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void NotifyShotFired()
    {
        shotable_count--;
        if(shotable_count == 0)
        {
            MainGameManager.Instance.ChangeState(MainGameManager.State.Result);
        }
    }

    public int CountPoint()
    {
        var target_areas = GameObject.FindGameObjectsWithTag("TargetArea");
        return target_areas.Count((item) => { return item.GetComponent<TargetArea>().is_hitting; });
    }
}
