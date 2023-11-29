using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData : MonoBehaviour
{
    public InGameStats[] playerTeam;

    public InGameStats[] opponentTeam;

    public TankData tankDB;
    // Start is called before the first frame update
    void Start()
    {
        playerTeam = new InGameStats[3];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
