using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Building : MonoBehaviour
{
    public TargetArea target_area;

    void Start()
    {
        
    }

    void Update()
    {
       
    }

    public void DrawTarget(bool active)
    {
        target_area?.SetDrawable(active);
    }
}
