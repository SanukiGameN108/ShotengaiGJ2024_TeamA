using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Rigidbody2D cur_rb;
    public TargetArea target_area;
    public AudioClip hit_se;
    public float se_play_time;

    float se_play_timer;

    void Start()
    {
        
    }

    void Update()
    {
        if (0<se_play_timer)
        {
            se_play_timer -= Time.deltaTime;
        }
    }

    public void DrawTarget(bool active)
    {
        target_area?.SetDrawable(active);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Building"))
        {
            if(0 < se_play_timer) { return; }
            var other_rb = collision.collider.GetComponent<Rigidbody2D>();
            var other_velocity = other_rb.velocity;
            var cur_velocity = cur_rb.velocity;
            if (other_velocity.sqrMagnitude <= cur_velocity.sqrMagnitude)
            {
                SoundManager.Instance.PlaySE(hit_se);
                se_play_timer = se_play_time;
            }
        }
    }
}
