using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : InteractiveObject
{

    public GameObject bait, dest;
    Vector3 bottomPos, destPos;
    public GameObject baby, bike, unicycle;
    public LineRenderer lr;
    float maxTimer = 2;

    public bool active = false, raised = false;

	// Use this for initialization
	void Start () {
        bottomPos = bait.transform.position;
        destPos = dest.transform.position;
        UpdateLineRenderer();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Changes which catch object is visible
    public void SetActiveCatch(string id = "bike")
    {
        baby.SetActive(false);
        bike.SetActive(false);
        unicycle.SetActive(false);
        switch (id) {
            case "bike":
                bike.SetActive(true);
                break;
            case "unicycle":
                unicycle.SetActive(true);
                break;
            case "baby":
                baby.SetActive(true);
                break;
        }
    }

    public override void Interact(string id = "bike")
    {
        if (id == "")
        {
            int ran = Random.Range(0, 100);
            if (ran == 0)
            {
                id = "unicycle";
            }
            else
            {
                id = "bike";
            }
        }
        if (!active)
        {
            if (!raised)
            {
                Raise(id);
            }
            else
            {
                Lower();
            }
        }
    }

    public void Raise(string id = "bike")
    {
        SetActiveCatch(id);
        StartCoroutine(RaiseEvent());
    }

    public void Lower()
    {
        StartCoroutine(LowerEvent());
    }

    IEnumerator RaiseEvent()
    {
        active = true;
        for (float f = maxTimer; f >= 0; f -= Time.deltaTime)
        {
            bait.transform.position = Vector3.Lerp(destPos, bottomPos, f/maxTimer);
            UpdateLineRenderer();
            yield return new WaitForSeconds(Time.deltaTime);
        }
        bait.transform.position = destPos;
        UpdateLineRenderer();
        active = false;
        raised = true;
    }

    IEnumerator LowerEvent()
    {
        active = true;
        for (float f = maxTimer; f >= 0; f -= Time.deltaTime)
        {
            bait.transform.position = Vector3.Lerp(bottomPos, destPos, f / maxTimer);
            UpdateLineRenderer();
            yield return new WaitForSeconds(Time.deltaTime);
        }
        bait.transform.position = bottomPos;
        UpdateLineRenderer();
        active = false;
        raised = false;
    }

    void UpdateLineRenderer() {
        Vector3 temp = lr.GetPosition(1);
        lr.SetPosition(0, new Vector3(temp.x, bait.transform.position.y, temp.z));
        lr.SetPosition(1, temp);
    }

}
