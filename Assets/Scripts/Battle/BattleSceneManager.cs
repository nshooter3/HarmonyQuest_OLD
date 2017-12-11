using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour {

    public ActivatableSequence seq;

	// Use this for initialization
	void Start () {
        seq.StartSequence(2.0f);
        GlobalFunctions.instance.DelayedFunction(() => BattleCam.instance.IntroZoom(), 2.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
