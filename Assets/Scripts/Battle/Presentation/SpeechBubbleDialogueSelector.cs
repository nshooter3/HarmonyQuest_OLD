using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleDialogueSelector : MonoBehaviour {

    public List<SpeechBubbleTriggerObject> triggers;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ActivateTrigger("EnemyHit");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ActivateTrigger("PlayerHit");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActivateTrigger("Idle");
        }
    }

    public void ActivateTrigger(string triggerType)
    {
        foreach (SpeechBubbleTriggerObject trigger in triggers)
        {
            if (trigger.triggerType == triggerType)
            {
                SpeechBubblePresenter.instance.Show(trigger.GetQuip());
                break;
            }
        }
    }
}
