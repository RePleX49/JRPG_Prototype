using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int health;
    protected int maxHealth = 100;
    protected string characterName;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void ApplyDamage(int damage, Character damageSource)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log(characterName + " took " + damage + " damage!");
    }

    public void ApplyHealing(int heal)
    {
        health = Mathf.Min(health + heal, maxHealth);
        Debug.Log(characterName + " healed " + heal + " health!");
    }

    IEnumerator ChooseMove()
    {
        Debug.Log("Choose a move: ");
        Debug.Log("A for Attack");
        Debug.Log("B for Heal");

        bool selectedMove = false;

        while(!selectedMove)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                Character target = ServiceLocater.bm.charactersInBattle[0];
                ServiceLocater.bm.moveQueue.Enqueue(new Attack(this, target));

                selectedMove = true;
            }

            if(Input.GetKeyDown(KeyCode.B))
            {
                ServiceLocater.bm.moveQueue.Enqueue(new Heal(this));
            }

            yield return null;
        }



        yield return null;
    }
}
