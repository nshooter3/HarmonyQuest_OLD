using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwilitHollow2_Boulder : SceneScript {

    public GameObject dad, rock;
    public Transform d1, d2, d3, d4, c1;
    public ParticleSystem rockExplode, rockSmoke;
    public CutsceneTrigger ct1;

    bool rockExploded = false;

    // Use this for initialization
    public override void StartScene()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (ct1.isCol)
        {

        }
	}

    public override void UpdateScene()
    {

    }

    IEnumerator BlowUpRock()
    {
        rockExplode.Play();
        rockSmoke.Play();
        rockExploded = true;
        yield return new WaitForSeconds(.25f);
        rock.SetActive(false);
    }
}
