using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamadii : MonoBehaviour
{
    public enum State
    {
        Idle,
        Running,
        KickWait,
        Charge,
    }

    public SpriteRenderer sprite_renderer;
    public PowerGage power_gage;
    public SimpleAnimation simple_animation;
    public State state;
    public float speed;
    Building kick_target;
    public float spin_max;
    public float max_impulse;

    void Start()
    {

    }

    void Update()
    {
        // 移動処理
        if (!kick_target)
        {
            state = State.Idle;
            simple_animation.Play("Default");
        }
        else if (state == State.Idle || state == State.Running)
        {
            bool is_arrived = MoveTo(kick_target.transform.position.x);
            if (is_arrived)
            {
                state = State.KickWait;
                simple_animation.Play("Default");
            }
            else
            {
                state = State.Running;
                simple_animation.Play("Dash");
            }
        }

        if (state == State.KickWait)
        {
            // ビルキックシーケンス開始
            if (Input.GetMouseButtonDown(0))
            {
                power_gage.StartGage();
                state = State.Charge;
                simple_animation.Play("Default");
            }
        }
        else if (state == State.Charge)
        {
            //キック発射
            if (Input.GetMouseButtonDown(0))
            {
                var gage_value = power_gage.StopAndGet();
                Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var dir = mouse_pos - (Vector2)kick_target.transform.position;
                ShotDesc desc = new ShotDesc();
                desc.building = kick_target.GetComponent<Rigidbody2D>();
                desc.direction = dir.normalized;
                desc.spin_torque = Random.Range(-spin_max, spin_max);
                desc.impulse = max_impulse * gage_value;
                Launcher.Instance.Shot(desc);
                state = State.Idle;
                simple_animation.Play("Default");
                kick_target = null;
            }
        }
        else
        {
            // ビル選択
            if (Input.GetMouseButtonDown(0)) // 0は左クリック
            {
                power_gage.HideGage();
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                mousePosition.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider && hit.collider.gameObject.CompareTag("Building"))
                {
                    kick_target = hit.collider.GetComponent<Building>();
                }
                state = State.Running;
                simple_animation.Play("Dash");
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
