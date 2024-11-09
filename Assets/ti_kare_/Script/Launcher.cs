using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoSingleton<Launcher>
{
    public Rigidbody2D building;
    public float spin_max;
    public float impulse;

    Tamadii tamadii;

    void Start()
    {
        tamadii = GameObject.FindGameObjectWithTag("Player").GetComponent<Tamadii>();
    }

    void Update()
    {
        if (building == null) { return; }
        if(tamadii.state!= Tamadii.State.Kick) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = mouse_pos -(Vector2)building.transform.position;
            ShotDesc desc = new ShotDesc();
            desc.building = building;
            desc.direction = dir.normalized;
            desc.spin_torque = Random.Range(-spin_max, spin_max);
            desc.impulse = impulse;
            shot(desc);
        }
    }

    struct ShotDesc
    {
        public Rigidbody2D building;
        public float spin_torque;
        public float impulse;
        public Vector2 direction;
    }

    void shot(ShotDesc desc)
    {
        desc.building.AddForce(desc.direction.normalized * desc.impulse,ForceMode2D.Impulse);
        desc.building.AddTorque(desc.spin_torque);
    }
}
