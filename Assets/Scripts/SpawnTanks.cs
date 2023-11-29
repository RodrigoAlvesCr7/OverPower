using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTanks : MonoBehaviour
{
    public Transform playerSpawnPoint1;
    public Transform playerSpawnPoint2;
    public Transform playerSpawnPoint3;
    public Transform opponentSpawnPoint1;
    public Transform opponentSpawnPoint2;
    public Transform opponentSpawnPoint3;

    public GameObject playerTank;

    public GameObject opponentTank;

    public TankData playerTeamData;

    public TankData opponentTeamData;

    InGameStats pTank1Stats;
    InGameStats pTank2Stats;
    InGameStats pTank3Stats;

    InGameStats oTank1Stats;
    InGameStats oTank2Stats;
    InGameStats oTank3Stats;

    public class TanksComparer : IComparer<GameObject>
    {
        public int Compare(GameObject x, GameObject y)
        {
            InGameStats gameStats1 = x.GetComponent<InGameStats>();
            InGameStats gameStats2 = y.GetComponent<InGameStats>();

            return gameStats1.speedPrio - gameStats2.speedPrio;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> tanks = new List<GameObject>();
        tanks.Add(Instantiate(playerTank, playerSpawnPoint1));
        tanks.Add(Instantiate(playerTank, playerSpawnPoint2));
        tanks.Add(Instantiate(playerTank, playerSpawnPoint3));

        tanks.Add(Instantiate(opponentTank, opponentSpawnPoint1));
        tanks.Add(Instantiate(opponentTank, opponentSpawnPoint2));
        tanks.Add(Instantiate(opponentTank, opponentSpawnPoint3));

        //playertank1.name = "Artillery";
        //pTank1Stats.tankName = "Artillery";
        //Debug.Log(pTank1Stats.tankName);
        /*
        pTank1Stats.fuelCap = 5;
        pTank1Stats.speedPrio = 5;
        pTank2Stats.speedPrio = 4;
        pTank3Stats.speedPrio = 3;
        oTank1Stats.speedPrio = 2;
        oTank2Stats.speedPrio = 1;
        oTank3Stats.speedPrio = 1;
        */

        for (int i = 0; i < tanks.Count; i++)
        {
            InGameStats gameStats = tanks[i].GetComponent<InGameStats>();
            gameStats.speedPrio = 5 - i;
            if (gameStats.speedPrio < 1)
                gameStats.speedPrio = 1;
        }

        tanks.Sort(new TanksComparer());
    }
    void NextTank()
    {
        Debug.Log("Wip");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            NextTank();
            Debug.Log("Sorted");
        }
    }
}
