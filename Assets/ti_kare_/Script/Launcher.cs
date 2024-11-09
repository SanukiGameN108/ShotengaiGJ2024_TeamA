using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct ShotDesc
{
    public Rigidbody2D building;
    public float spin_torque;
    public float impulse;
    public Vector2 direction;
}

public class Launcher : MonoSingleton<Launcher>
{


    Tamadii tamadii;

    void Start()
    {
        tamadii = GameObject.FindGameObjectWithTag("Player").GetComponent<Tamadii>();
    }

    void Update()
    {

    }

    public void Shot(ShotDesc desc)
    {
        desc.building.AddForce(desc.direction.normalized * desc.impulse,ForceMode2D.Impulse);
        desc.building.AddTorque(desc.spin_torque);
    }
}
