using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    //public SpriteRenderer sprite_renderer;
    public Building target;
    public bool is_hitting;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetDrawable(bool active)
    {
        //sprite_renderer.enabled = active;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            is_hitting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            is_hitting = false;
        }
    }
}
