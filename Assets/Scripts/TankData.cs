using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TankData : ScriptableObject
{
    public TankStats[] tankDatabase;

    public int TankCount
    {
        get
        {
            return tankDatabase.Length;
        }
    }
    public TankStats GetTank(int index)
    {
        return tankDatabase[index];
    }

    

}
