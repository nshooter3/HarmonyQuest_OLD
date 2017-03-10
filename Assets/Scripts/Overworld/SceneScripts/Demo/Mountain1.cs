using UnityEngine;
using System.Collections;

public class Mountain1 : SceneScript{

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.A))
            PlayerMovementOverworld.instance.InitDialogue(gameObject);
    }

    public override void UpdateScene()
    {

    }

}
