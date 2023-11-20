using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, OPPONENTTURN, WON, LOST }

public class BattleScript : MonoBehaviour
{
    public BattleState state;

    //get the tanks
    public GameObject playerTank;
    public GameObject opponentTank;

    //get the spawnpoints
    public Transform playerSpawnPoint;
    public Transform opponentSpawnPoint;

    private TankStats playerStats;
    private TankStats opponentStats;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        Debug.Log(state.ToString());
        SetupBattle();
    }

    void SetupBattle()
    {
        playerStats = playerTank.GetComponent<TankStats>();
        Debug.Log(playerStats.tankName);

        opponentStats = opponentTank.GetComponent<TankStats>();
        Debug.Log(opponentStats.tankName);

        NextTurn();
    }
    
    void NextTurn()
    {
        if(state == BattleState.START)
        {
            if(playerStats.speedPrio >= opponentStats.speedPrio)
            {
                state = BattleState.PLAYERTURN;
            }
            else
            {
                state = BattleState.OPPONENTTURN;
            }
        }
        else if(state == BattleState.PLAYERTURN)
        {
            state = BattleState.OPPONENTTURN;
        }
        else if(state == BattleState.OPPONENTTURN)
        {
            state = BattleState.PLAYERTURN;
        }
        Debug.Log(state);
    }

}
