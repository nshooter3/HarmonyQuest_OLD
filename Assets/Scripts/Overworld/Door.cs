using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public string destName = "", destScene = "";
    private float timer, timerMax = 1.1f;
    bool teleport = false;
    public bool musicFadeOut = false;

    public void Teleport()
    {
        GlobalVars.instance.saveData.destination = destName;
        teleport = true;
        timer = timerMax;
        if (musicFadeOut)
        {
            MusicManager.MM.FadeOutInit();
        }
    }

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (teleport)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                GlobalVars.instance.saveData.destination = destName;
                if (destScene != "")
                {
                    SceneManager.LoadScene(destScene);
                }
                else
                {
                    Debug.Log("Destination scene not set");
                }
                teleport = false;
            }
        }
	}
}
