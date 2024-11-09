using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamadii : MonoBehaviour
{
    public enum State
    {
        Idle,
        Running,
        Kick,
    }

    public SpriteRenderer sprite_renderer;
    public State state;
    public float speed;
    public Building kick_target;

    void Start()
    {
        
    }

    void Update()
    {
        if (!kick_target)
        {
            state = State.Idle;
            return;
        }
        else
        {
            bool is_arrived = MoveTo(kick_target.transform.position.x);
            if (is_arrived)
            {
                state = State.Kick;
            }
            else
            {
                state = State.Running;
            }
        }

    }

    bool MoveTo(float destination_x)
    {
        var dir = destination_x - transform.position.x;
        var move_delta = Time.deltaTime * speed;
        if (Mathf.Abs(dir) <= move_delta)
        {
            var pos = transform.position;
            pos.x = destination_x;
            transform.position = pos;
            return true;
        }
        else
        {
            if (dir < 0)
            {
                sprite_renderer.flipX = true;
            }
            if (0 < dir)
            {
                sprite_renderer.flipX = false;
            }
            dir = Mathf.Clamp(dir, -1, 1);

            transform.Translate(dir * move_delta * Vector2.right);
            return false;
        }
    }

    void MoveToKickTarget()
    {

    }
}
