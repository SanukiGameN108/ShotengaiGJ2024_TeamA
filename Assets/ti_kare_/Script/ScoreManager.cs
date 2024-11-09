using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class ScoreManager : MonoSingleton<ScoreManager>
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int CountPoint()
    {
        var target_areas = GameObject.FindGameObjectsWithTag("TargetArea");
        return target_areas.Count((item) => { return item.GetComponent<TargetArea>().is_hitting; });
    }
}
