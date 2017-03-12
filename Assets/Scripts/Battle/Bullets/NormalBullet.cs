using UnityEngine;
using System.Collections;

public class NormalBullet : Bullet {

    public override void Init(Vector3 pos, Vector3 dir)
    {
        //Override in child
        active = true;
        speed = 10;
        this.direction = dir;
        transform.position = pos;
        ToggleCollider(true);
        ToggleRenderer(true);
    }
}
