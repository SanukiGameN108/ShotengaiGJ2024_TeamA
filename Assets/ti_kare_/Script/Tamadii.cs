using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamadii : MonoSingleton<Tamadii>
{
    public enum State
    {
        Idle,
        Running,
        KickWait,
        Charge,
        Clear,
    }

    public SpriteRenderer sprite_renderer;
    public PowerGage power_gage;
    public SimpleAnimation simple_animation;
    public Launcher launcher;
    public State state;
    public float speed;
    Building kick_target;
    public float spin_max;
    public float max_impulse;
    public float shot_dir_circle_interval;
    public int shot_dir_circle_count;
    public GameObject circle_prefab;
    public AudioClip shot_se;
    Vector2 kickable_dir;
    List<GameObject> circles = new List<GameObject>();

    void Start()
    {

    }

    Vector3 GetMouseWorld()
    {
        Vector3 mouse_position = Input.mousePosition;
        mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);
        mouse_position.z = 0;
        return mouse_position;
    }

    void DestroyMouseDirObjects()
    {
        foreach (GameObject obj in circles)
        {
            Destroy(obj);
        }
    }

    void InstantiateMouseDirObjects()
    {
        if (!kick_target) { return; }

        var mouse_world = GetMouseWorld();
        var kick_target_pos = kick_target.transform.position;
        var shot_dir = mouse_world - kick_target_pos;
        shot_dir.Normalize();

        for (int i = 0; i < shot_dir_circle_count; ++i)
        {
            var circle = Instantiate(circle_prefab);
            circle.transform.position = kick_target_pos + shot_dir * shot_dir_circle_interval * i;
            circles.Add(circle);
        }
    }

    public static Building RayCastByMouse(Vector3 mouse_world)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouse_world, Vector2.zero);
        if (hit.collider && hit.collider.gameObject.CompareTag("Building"))
        {
            return hit.collider.GetComponent<Building>();
        }

        return null;
    }

    static void ResetBuildingDraw()
    {
        var buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (var building in buildings ){
            var building_comp= building.GetComponent<Building>();
            building_comp.DrawTarget(false);
        }
    }

    void Update()
    {
        DestroyMouseDirObjects();
        if (!MainGameManager.Instance.IsMainGame()) { return; }

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

        var mouse_world = GetMouseWorld();
        var hit_building = RayCastByMouse(mouse_world);
        ResetBuildingDraw();

        if (state == State.KickWait)
        {
            InstantiateMouseDirObjects();
            kick_target.DrawTarget(true);


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
            InstantiateMouseDirObjects();
            kick_target.DrawTarget(true);

            //キック発射
            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }
        }
        else
        {
            // ビル選択
            if (Input.GetMouseButtonDown(0)) // 0は左クリック
            {
                power_gage.HideGage();
                kick_target = hit_building;

                state = State.Running;
                simple_animation.Play("Dash");
            }
            else
            {
                if (hit_building)
                {
                    hit_building.DrawTarget(true);
                }
            }
        }
    }

    void CheckBuildingRayCast()
    {

    }

    void Shot()
    {
        var gage_value = power_gage.StopAndGet();
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var dir = mouse_pos - (Vector2)kick_target.transform.position;
        FaceToDir(dir.x);
        ShotDesc desc = new ShotDesc();
        desc.building = kick_target.GetComponent<Rigidbody2D>();
        desc.direction = dir.normalized;
        desc.spin_torque = Random.Range(-spin_max, spin_max);
        desc.impulse = max_impulse * gage_value;
        launcher.Shot(desc);
        ScoreManager.Instance.NotifyShotFired();
        state = State.Idle;
        simple_animation.Play("Kick");
        kick_target = null;
        SoundManager.Instance.PlaySE(shot_se);
    }

    void FaceToDir(float dir)
    {
        if (dir < 0)
        {
            sprite_renderer.flipX = true;
        }
        if (0 < dir)
        {
            sprite_renderer.flipX = false;
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
            FaceToDir(dir);
            dir = Mathf.Clamp(dir, -1, 1);

            transform.Translate(dir * move_delta * Vector2.right);
            return false;
        }
    }
}
