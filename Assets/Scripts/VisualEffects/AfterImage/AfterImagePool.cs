using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour {

    public static AfterImagePool instance;

    public AfterImageObject[] pool;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        InitPool();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Attempt to load an after image
    public void SpawnAfterImage(Transform trans, float fadeTime)
    {
        AfterImageObject temp = (AfterImageObject)FindAvailableAfterImage(pool);
        if (temp != null)
        {
            temp.ToggleOn(trans, fadeTime);
        }
        else
        {
            Debug.LogError("No after images available");
        }
    }

    //Attempt to load a shrinking after image
    public void SpawnShrinkingAfterImage(Transform trans, float fadeTime)
    {
        AfterImageObject temp = (AfterImageObject)FindAvailableAfterImage(pool);
        if (temp != null)
        {
            temp.ToggleOn(trans, fadeTime, true);
        }
        else
        {
            Debug.LogError("No after images available");
        }
    }

    //Util function for finding and returning available after image out of a list
    private AfterImageObject FindAvailableAfterImage(AfterImageObject[] list)
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
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(pool[i]);
            pool[i].transform.parent = transform;
            pool[i].originalParent = transform;
        }
    }
}
