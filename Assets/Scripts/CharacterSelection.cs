using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public TankData tankDB;
    
    public InGameStats[] playerTeam;

    public InGameStats[] opponentTeam;

    public TMP_Text playerNameText;

    public TMP_Text opponentNameText;

    public int playerTeamSelector = 0;

    public int opponentTeamSelector = 0;
    public int i = 0;
    public int j = 0;

    

    public void AddToPlayerTeam()
    {
        TankStats teamMember = tankDB.GetTank(playerTeamSelector);
        playerTeam[i].tankName = teamMember.tankName;
        playerTeam[i].speedPrio = teamMember.speedPrio;
        playerTeam[i].fuelCap = teamMember.fuelCap;
        playerTeam[i].maxHealth = teamMember.maxHealth;
        playerNameText.text = teamMember.tankName;
        playerTeamSelector++;
        i++;
        if (i < 2) { }
          
    }

    public void AddToPlayerOpponentTeam()
    {
        TankStats teamMember = tankDB.GetTank(opponentTeamSelector);

       opponentTeam[j].tankName = teamMember.tankName;
        opponentTeam[j].speedPrio = teamMember.speedPrio;
        opponentTeam[j].fuelCap = teamMember.fuelCap;
        opponentTeam[j].maxHealth = teamMember.maxHealth;
        opponentNameText.text = opponentTeam[0].tankName + opponentTeam[1].tankName + opponentTeam[2].tankName;
        opponentTeamSelector++;
        j++;
        if (j < 2) { }

    }

    public void ResetTeams()
    {
        
            playerTeam[0].tankName = "NoTank";
            playerTeam[0].speedPrio = 0;
            playerTeam[0].fuelCap = 0;
            playerTeam[0].currentHealth = 0;
            playerTeam[0].maxHealth = 0;

            playerTeam[1].tankName = "NoTank";
            playerTeam[1].speedPrio = 0;
            playerTeam[1].fuelCap = 0;
            playerTeam[1].currentHealth = 0;
            playerTeam[1].maxHealth = 0;

            playerTeam[2].tankName = "NoTank";
            playerTeam[2].speedPrio = 0;
            playerTeam[2].fuelCap = 0;
            playerTeam[2].currentHealth = 0;
            playerTeam[2].maxHealth = 0;

            opponentTeam[0].tankName = "NoTank";
            opponentTeam[0].speedPrio = 0;
            opponentTeam[0].fuelCap = 0;
            opponentTeam[0].currentHealth = 0;
            opponentTeam[0].maxHealth = 0;

            opponentTeam[1].tankName = "NoTank";
            opponentTeam[1].speedPrio = 0;
            opponentTeam[1].fuelCap = 0;
            opponentTeam[1].currentHealth = 0;
            opponentTeam[1].maxHealth = 0;

            opponentTeam[2].tankName = "NoTank";
            opponentTeam[2].speedPrio = 0;
            opponentTeam[2].fuelCap = 0;
            opponentTeam[2].currentHealth = 0;
            opponentTeam[2].maxHealth = 0;

            tankDB.tankDatabase[0].tankName = "Artillery";
            tankDB.tankDatabase[0].speedPrio = 2;
            tankDB.tankDatabase[0].fuelCap = 20;
            tankDB.tankDatabase[0].currentHealth = 4;
            tankDB.tankDatabase[0].maxHealth = 4;

            tankDB.tankDatabase[1].tankName = "Big Boy Chunky";
            tankDB.tankDatabase[1].speedPrio = 1;
            tankDB.tankDatabase[1].fuelCap = 10;
            tankDB.tankDatabase[1].currentHealth = 75;
            tankDB.tankDatabase[1].maxHealth = 75;

            tankDB.tankDatabase[2].tankName = "Mechanic";
            tankDB.tankDatabase[2].speedPrio = 3;
            tankDB.tankDatabase[2].fuelCap = 15;
            tankDB.tankDatabase[2].currentHealth = 45;
            tankDB.tankDatabase[2].maxHealth = 45;



    }
   

    public void loadMap()
    {
       SceneManager.LoadSceneAsync("Level 1");
    }

}
