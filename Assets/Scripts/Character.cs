using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int health;
    protected int maxHealth = 100;
    protected string characterName;
    protected bool isDead;

    public virtual void Initialize(string name)
    {
        characterName = name;
        health = maxHealth;
        isDead = false;
    }

    public string GetName()
    {
        return characterName;
    }

    public virtual void StartMoveSelect() { }

    public void ApplyDamage(int damage, Character damageSource)
    {
        health = Mathf.Max(health - damage, 0);
        Debug.Log(characterName + " took " + damage + " damage!");
        Debug.Log(characterName + " health is " + health + ".");

        if(health == 0)
        {
            OnDeath();
        }
    }

    public void ApplyHealing(int heal)
    {
        health = Mathf.Min(health + heal, maxHealth);
        Debug.Log(characterName + " healed " + heal + " health!");
        Debug.Log(characterName + " health is " + health + ".");
    }

    void OnDeath()
    {
        isDead = true;
        // other things like animations or affects
    }
    
    public bool IsDead()
    {
        return isDead;
    }
}