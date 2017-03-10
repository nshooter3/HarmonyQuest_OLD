using UnityEngine;
using System.Collections;

public class UpBullet : Bullet {

    // Use this for initialization
    void Start()
    {
        this.direction = new Vector3(0, 1, 0);
        this.speed = 10;
        this.damage = 1;
    }
}
