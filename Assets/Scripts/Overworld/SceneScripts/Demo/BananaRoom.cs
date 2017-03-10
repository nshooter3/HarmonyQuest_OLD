using UnityEngine;
using System.Collections;

public class BananaRoom : SceneScript
{

    public GameObject banana, bBill;

    public void Start()
    {
        UpdateScene();
    }

    public override void UpdateScene() {
        if (GlobalVars.GV.saveData.movedBanana)
        {
            banana.GetComponent<Renderer>().enabled = false;
            banana.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (GlobalVars.GV.saveData.recruitedTranslator)
        {
            bBill.GetComponent<Renderer>().enabled = false;
            bBill.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
