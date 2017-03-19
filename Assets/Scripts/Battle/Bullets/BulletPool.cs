using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

    public static BulletPool instance;

    public Bullet[] normalBullets;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

	// Use this for initialization
	void Start () {
        InitPool();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Alternate constructor that defaults dir to up
    public void SpawnNormalBullet(Vector3 pos)
    {
        SpawnNormalBullet(pos, new Vector3(0, 1, 0));
    }

    //Attempt to load a normal bullet
    public void SpawnNormalBullet(Vector3 pos, Vector3 dir)
    {
        NormalBullet temp = (NormalBullet)FindAvailableBullet(normalBullets);
        if (temp != null)
        {
            temp.Init(pos, dir.normalized);
        }
        else
        {
            Debug.LogError("No bullets available");
        }
    }

    //Util function for finding and returning available bullet out of a list
    private Bullet FindAvailableBullet(Bullet[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (!list[i].active)
            {
                return list[i];
            }
        }
        return null;
    }

    private void InitPool()
    {
        for (int i = 0; i < normalBullets.Length; i++)
        {
            normalBullets[i] = Instantiate(normalBullets[i]);
            normalBullets[i].transform.parent = gameObject.transform;
        }
    }
}
