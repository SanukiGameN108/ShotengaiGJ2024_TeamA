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
    public float spin_max;
    public float impulse;

    void Start()
    {
        
    }

    void Update()
    {
        if (!kick_target)
        {
            state = State.Idle;
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


        if (state != State.Kick)
        {
            if (Input.GetMouseButtonDown(0)) // 0ÇÕç∂ÉNÉäÉbÉN
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider && hit.collider.gameObject.CompareTag("Building"))
                {
                    kick_target = hit.collider.GetComponent<Building>();
                }
            }
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var dir = mouse_pos - (Vector2)kick_target.transform.position;
                ShotDesc desc = new ShotDesc();
                desc.building = kick_target.GetComponent<Rigidbody2D>();
                desc.direction = dir.normalized;
                desc.spin_torque = Random.Range(-spin_max, spin_max);
                desc.impulse = impulse;
                Launcher.Instance.Shot(desc);
                state = State.Idle;
                kick_target = null;
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
