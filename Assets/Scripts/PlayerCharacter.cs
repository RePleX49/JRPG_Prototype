using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public bool isSelectingMove;

    public override void Initialize(string name)
    {
        base.Initialize(name);
        isSelectingMove = false;
    }

    public override void StartMoveSelect()
    {
        StartCoroutine(ChooseMove());
    }

    IEnumerator ChooseMove()
    {
        Debug.Log("Choose a move: ");
        Debug.Log("A for Attack");
        Debug.Log("B for Heal");

        isSelectingMove = true;
        bool selectedMove = false;

        while (!selectedMove)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Character target = ServiceLocater.bm.charactersInBattle[0];
                ServiceLocater.bm.AddToMoveQueue(new Attack(this, target));
                Debug.Log("Player selected attack");
                selectedMove = true;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                ServiceLocater.bm.AddToMoveQueue(new Heal(this));
                Debug.Log("Player selected heal");
                selectedMove = true;
            }

            yield return null;
        }

        isSelectingMove = false;

        yield return null;
    }
}
