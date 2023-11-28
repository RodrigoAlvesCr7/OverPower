using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public TankData tankDB;
    
    public TankData playerTeam;

    public TankData opponentTeam;

    public TMP_Text nameText;

    public int team1Selector = 0;
    public int i = 0;

    public void AddToPlayerTeam()
    {
        TankStats teamMember = tankDB.GetTank(team1Selector);
        playerTeam.tankDatabase[i] = teamMember;
        nameText.text = teamMember.tankName;
        team1Selector++;
        i++;
        if (i < 2) { }
          
    }

}
