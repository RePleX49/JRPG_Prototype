using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    protected int moveCost;
    protected Character moveUser;
    public abstract void MoveAction();
}

public class Attack : Move
{
    int damage = 5;
    Character moveTarget;

    public Attack(Character user, Character target)
    {
        moveUser = user;
        moveTarget = target;
    }    

    public override void MoveAction()
    {
        AttackTarget();
    }

    void AttackTarget()
    {
        moveTarget.ApplyDamage(damage, moveUser);
    }
}

public class Heal : Move
{
    int healAmount = 8;

    public Heal(Character user)
    {
        moveUser = user;
    }

    public override void MoveAction()
    {
        HealSelf();
    }

    void HealSelf()
    {
        moveUser.ApplyHealing(healAmount);
    }
}
