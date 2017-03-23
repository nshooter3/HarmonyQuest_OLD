using UnityEngine;
using System.Collections;

public class CharacterChatter : MonoBehaviour {

    public AudioClip[] sounds;
    AudioSource curSound;
    int sample;

	// Use this for initialization
	void Start () {
        curSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayChatter()
    {
        if (curSound == null){
            curSound = GetComponent<AudioSource>();
        }
        if (sounds.Length > 1)
        {
            sample = Random.Range(0, sounds.Length);
            curSound.clip = sounds[sample];
        }
        curSound.Play();
    }
}
