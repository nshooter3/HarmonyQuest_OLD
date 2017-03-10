using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public static MusicManager MM;

    public AudioSource sceneMusic, cutScene, majorEvent;
    private AudioSource tempAudio;
    private AudioClip next;
    private float sMVol, cSVol, mEVol, tempVol, initVol;
    private bool fadeOut, fadeOutFaster, fadeIn, lowerMusic, raiseMusic, playNext, playCutscene, fadeOutCutscene, fadeOutCutsceneEndDialogue;
    private float fadeTime = 1, timer = 0;

    SceneScript sceneScript;

    //Makes first MusicManager permanent, destroys duplicates
    void Awake()
    {
        if (MM == null)
        {
            DontDestroyOnLoad(gameObject);
            MM = this;
        }
        else if (MM != this)
        {
            Destroy(gameObject);
        }

    }

    //Fades out current song and sets/plays new scene song
    public void SwitchSong(AudioClip song, float vol)
    {
        tempVol = sMVol;
        fadeOut = true;
        timer = fadeTime;
        playNext = true;
        next = song;
        sMVol = vol;
    }

    // Use this for initialization
    public void Init () {
        sceneScript = FindObjectOfType<SceneScript>();
        tempAudio = sceneScript.GetComponent<AudioSource>();
        if (tempAudio != null)
        {
            if(sceneMusic.clip == null || tempAudio.clip.name != sceneMusic.clip.name)
                PlayNewSong(tempAudio.clip, tempAudio.volume);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (fadeOut)
        {
            FadeOut();
        }
        else if (fadeOutFaster)
        {
            FadeOutFaster();
        }
        else if (fadeOutCutsceneEndDialogue)
        {
            FadeOutCutsceneEndDialogue();
        }
        else if (fadeOutCutscene)
        {
            FadeOutCutscene();
        }
        else if (playNext)
        {
            PlayNewSong(next, sMVol);
            playNext = false;
        }
        else if (playCutscene)
        {
            PlayCutsceneSong(next, cSVol);
        }
        else if (raiseMusic)
        {
            RaiseMusic(initVol);
        }
        else if (lowerMusic)
        {
            LowerMusic(initVol);
        }
        else if (fadeIn)
        {
            FadeIn();
        }
    }

    public void FadeOutInit()
    {
        tempVol = sMVol;
        fadeOut = true;
        timer = fadeTime;
    }

    //Fades out current song
    void FadeIn()
    {
        timer -= Time.deltaTime;
        sceneMusic.volume = Mathf.Lerp(tempVol, sMVol, timer / fadeTime);
        if (timer <= 0)
        {
            fadeOut = false;
            PauseSceneMusic();
        }
    }

    //Fades out current song
    void FadeOut()
    {
        timer -= Time.deltaTime;
        sceneMusic.volume = Mathf.Lerp(0, tempVol, timer/fadeTime);
        if (timer <= 0)
        {
            fadeOut = false;
            PauseSceneMusic();
        }
    }

    //Fades out current song
    void FadeOutFaster()
    {
        timer -= Time.deltaTime;
        sceneMusic.volume = Mathf.Lerp(0, tempVol, timer / (fadeTime/2f));
        if (timer <= 0)
        {
            fadeOutFaster = false;
            PauseSceneMusic();
        }
    }

    public void LowerMusicInit(float initVol)
    {
        lowerMusic = true;
        raiseMusic = false;
        timer = fadeTime;
        this.initVol = initVol;
    }

    void LowerMusic(float initVol)
    {
        timer -= Time.deltaTime;
        sceneMusic.volume = Mathf.Lerp(sMVol/1.5f, initVol, timer / fadeTime);
        if (timer <= 0)
        {
            lowerMusic = false;
        }
    }

    public void RaiseMusicInit(float initVol)
    {
        if (cutScene.isPlaying)
        {
            Debug.Log("test");
            FadeInSceneMusicResumeInit();
        }
        /*else
        {
            raiseMusic = true;
            lowerMusic = false;
            timer = fadeTime;
            this.initVol = initVol;
        }*/
    }

    void RaiseMusic(float initVol)
    {
        timer -= Time.deltaTime;
        sceneMusic.volume = Mathf.Lerp(sMVol, initVol, timer / fadeTime);
        if (timer <= 0)
        {
            raiseMusic = false;
        }
    }

    //Pauses main song
    public void PauseSceneMusic()
    {
        sceneMusic.Pause();
    }

    public void UnPauseSceneMusic()
    {
        sceneMusic.UnPause();
    }

    //Starts a new song
    public void PlayNewSong(AudioClip song, float vol)
    {
        sceneMusic.clip = song;
        sceneMusic.volume = vol;
        sMVol = vol;
        sceneMusic.Play();
    }

    public void PlayCutsceneSong(AudioClip song, float vol)
    {
        cutScene.clip = song;
        cutScene.volume = vol;
        cSVol = vol;
        cutScene.Play();
        playCutscene = false;
    }

    public void FadeInSceneMusicResumeInit()
    {
        UnPauseSceneMusic();
        fadeOutCutsceneEndDialogue = true;
        sceneMusic.volume = 0;
        timer = fadeTime*2;
        tempVol = cutScene.volume;
    }

    //Fades out current song, pauses, plays cutscene song
    public void PlayCutsceneSongInit(string song, float vol)
    {
        if (sceneMusic.isPlaying)
        {
            tempVol = sMVol;
            fadeOutFaster = true;
            timer = fadeTime / 2f;
        }
        else if (cutScene != null && cutScene.clip.name != "Silence")
        {
            tempVol = cSVol;
            fadeOutCutscene = true;
            timer = fadeTime / 2f;
        }
        playCutscene = true;
        next = Resources.Load("CutsceneMusic/" + song) as AudioClip;
        cSVol = vol;
    }

    void FadeOutCutscene()
    {
        timer -= Time.deltaTime;
        cutScene.volume = Mathf.Lerp(0, tempVol, timer / fadeTime);
        if (timer <= 0)
        {
            fadeOutCutscene = false;
            cutScene.Stop();
        }
    }

    void FadeOutCutsceneEndDialogue()
    {
        timer -= Time.deltaTime*2;
        cutScene.volume = Mathf.Lerp(0, tempVol, (timer-0.5f) / fadeTime);
        sceneMusic.volume = Mathf.Lerp(sMVol, 0, timer / fadeTime);
        if (timer <= 0)
        {
            fadeOutCutsceneEndDialogue = false;
            cutScene.Stop();
        }
    }

}
