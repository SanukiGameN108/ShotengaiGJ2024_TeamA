using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropEffectObject : MonoBehaviour
{
    public Rigidbody2D rb;
    public float impulse_max;
    public float impulse_min;

    void Start()
    {
        
    }

    void Update()
    {

    }

    [ContextMenu("ShotTest")]
    public void NotifyShotFired()
    {
        var x_dir = Random.Range(-1.0f, 1.0f);
        var y_dir = Random.Range(0.0f, 1.0f);
        var dir = new Vector2(x_dir, y_dir);
        if(dir == Vector2.zero) { return; }
        dir.Normalize();
        var impulse = Random.Range(impulse_min, impulse_max);
        rb.AddForce(dir * impulse, ForceMode2D.Impulse);
    }
}
