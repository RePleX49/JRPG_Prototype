using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Queue<Move> moveQueue = new Queue<Move>();
    public List<Character> charactersInBattle = new List<Character>();

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [HideInInspector]
    public PlayerCharacter player;

    bool startedPlayerSelect;
    
    public enum BattleState
    {
        PlayerMoveSelect,
        AIMoveSelect,
        PlayMoves,
        BattleWon,
        BattleLost
    }

    BattleState currentState;

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocater.bm = this;
        startedPlayerSelect = false;
        player = Instantiate(playerPrefab).GetComponent<PlayerCharacter>();
        player.Initialize("RePleX49");

        Character newEnemy = Instantiate(enemyPrefab).GetComponent<ElectricRat>();
        newEnemy.Initialize("Electric Rat");
        charactersInBattle.Add(newEnemy);
        currentState = BattleState.PlayerMoveSelect;
        Debug.Log("Battle Start!");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case BattleState.PlayerMoveSelect:
                if(!startedPlayerSelect)
                {
                    startedPlayerSelect = true;
                    player.StartMoveSelect();
                }
                else
                {
                    if(player.isSelectingMove)
                    {
                        break;
                    }
                    else
                    {
                        startedPlayerSelect = false;
                        ChangeBattleState(BattleState.AIMoveSelect);
                    }                
                }

                break;
            case BattleState.AIMoveSelect:

                foreach(Character enemy in charactersInBattle)
                {
                    enemy.StartMoveSelect();
                }

                ChangeBattleState(BattleState.PlayMoves);
                break;
            case BattleState.PlayMoves:
                PlayMoveQueue();
                CheckBattleState();
                break;
            case BattleState.BattleWon:
                Debug.Log("Enemies Defeated! Battle won!");
                break;
            case BattleState.BattleLost:
                Debug.Log("You Lost!");
                break;
        }
    }

    void CheckBattleState()
    {
        bool doEnemiesRemain = false;

        if(player.IsDead())
        {
            ChangeBattleState(BattleState.BattleLost);
            return;
        }

        foreach (Character enemy in charactersInBattle)
        {
            if (!enemy.IsDead())
            {
                doEnemiesRemain = true;
            }
        }

        if (!doEnemiesRemain)
        {
            ChangeBattleState(BattleState.BattleWon);
            return;
        }

        ChangeBattleState(BattleState.PlayerMoveSelect);
    }

    void PlayMoveQueue()
    {
        while(moveQueue.Count > 0)
        {
            moveQueue.Dequeue().MoveAction();
        }

        Debug.Log("Finished move queue");
    }

    public void FinishPlayerSelect()
    {
        currentState = BattleState.AIMoveSelect;
    }

    public void AddToMoveQueue(Move move)
    {
        moveQueue.Enqueue(move);
    }

    public void ChangeBattleState(BattleState newState)
    {
        currentState = newState;
    }    
}
