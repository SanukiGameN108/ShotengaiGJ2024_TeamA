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

   public int GetShottableCount()
    {
        return shotable_count;
    }

    public void NotifyShotFired()
    {
        shotable_count--;
        if(shotable_count == 0)
        {
            MainGameManager.Instance.StartResult();
        }
    }

    public int CountPoint()
    {
        var target_areas = GameObject.FindGameObjectsWithTag("TargetArea");
        var point = 0;
        foreach (var area in target_areas)
        {
            var distance = area.GetComponent<TargetArea>().Distance;
            Debug.Log(distance);
            point += 10 - Mathf.Clamp((int)distance, 0, ResulScript.maxScorepoint); // TODO:距離によってスコアを変える
        }
        return point;
    }
}
