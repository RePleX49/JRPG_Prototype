using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Queue<Move> moveQueue = new Queue<Move>();
    public List<Character> charactersInBattle = new List<Character>();

    BattleState currentState;

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocater.bm = this;
        currentState = BattleState.PlayerMoveSelect;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case BattleState.PlayerMoveSelect:
                break;
            case BattleState.AIMoveSelect:
                break;
            case BattleState.PlayMoves:
                PlayMoveQueue();
                break;
            case BattleState.BattleWon:
                break;
            case BattleState.BattleLost:
                break;
        }
    }

    void PlayMoveQueue()
    {
        while(moveQueue.Peek())
        {
            moveQueue.Dequeue().MoveAction();
        }
    }

    public void FinishPlayerSelect()
    {
        currentState = BattleState.AIMoveSelect;
    }

    enum BattleState
    {
        PlayerMoveSelect,
        AIMoveSelect,
        PlayMoves,
        BattleWon,
        BattleLost
    }
}
