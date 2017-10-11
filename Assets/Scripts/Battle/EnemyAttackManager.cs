using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    //List of information for each attack frame, includes damage and hitbox. Automatically populates by searching children.
    private EnemyAttack[] attacks;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        DisableAllAttacks();
        attacks = GetComponentsInChildren<EnemyAttack>();
    }

    //Enables an attack in attacks based on name matching
    //disableOtherAttacks disables all other attacks if it is equal to 1 (this is so that it will work with animation events, since they don't support bools for some reason)
    public void EnableAttack(string attack, int disableOtherAttacks = 1)
    {
        foreach (EnemyAttack en in attacks)
        {
            if (en.name == attack)
            {
                en.gameObject.SetActive(true);
                return;
            }
            else if (disableOtherAttacks == 1)
            {
                en.gameObject.SetActive(false);
            }
        }
    }

    //Disables an attack in attacks based on name matching
    public void DisableAttack(string attack)
    {
        foreach (EnemyAttack en in attacks)
        {
            if (en.name == attack)
            {
                en.gameObject.SetActive(false);
                return;
            }
        }
        Debug.Log("No attack with name " + attack + " found.");
    }

    //Disables every gameobject in the attacks array
    public void DisableAllAttacks()
    {
        foreach (EnemyAttack en in attacks)
        {
            en.gameObject.SetActive(false);
        }
    }
}
