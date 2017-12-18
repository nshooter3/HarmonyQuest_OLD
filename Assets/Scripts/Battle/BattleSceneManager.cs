using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour {

    public static BattleSceneManager instance;

    public ActivatableSequence seq;

    public PlayerMovementBattle player;
    public Enemy[] enemies;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        //assign player and enemy variables, and set their transparency to 0 for the intro sequence
        player = (PlayerMovementBattle)FindObjectOfType(typeof(PlayerMovementBattle));
        enemies = (Enemy[])FindObjectsOfType(typeof(Enemy));
        GlobalFunctions.instance.FadeOut(player.GetComponent<SpriteRenderer>().color, 0.1f, player.GetComponent<SpriteRenderer>());
        foreach (Enemy enemy in enemies)
        {
            GlobalFunctions.instance.FadeOut(enemy.GetComponent<SpriteRenderer>().color, 0.1f, enemy.GetComponent<SpriteRenderer>());
        }

        //Kick off intro sequence
        seq.StartSequence(2.0f);
        GlobalFunctions.instance.DelayedFunction(() => BattleCam.instance.IntroZoom(), 2.0f);
    }

    //Fade in player and enemies, as part of the intro sequence
    public void FadeInCharacters()
    {
        GlobalFunctions.instance.FadeIn(new Color(player.GetComponent<SpriteRenderer>().color.r, player.GetComponent<SpriteRenderer>().color.g, player.GetComponent<SpriteRenderer>().color.b, 1),
                                        0.5f, player.GetComponent<SpriteRenderer>());
        foreach (Enemy enemy in enemies)
        {
            GlobalFunctions.instance.FadeIn(new Color (enemy.GetComponent<SpriteRenderer>().color.r, enemy.GetComponent<SpriteRenderer>().color.g, enemy.GetComponent<SpriteRenderer>().color.b, 1),
                                            0.5f, enemy.GetComponent<SpriteRenderer>());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
