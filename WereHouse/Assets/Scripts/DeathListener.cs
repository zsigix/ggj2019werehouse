using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rho;

public class DeathListener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventHandler.Register<GameEvents.VictimDied>(VictimHasDied);
    }
    private void VictimHasDied(GameEvents.VictimDied evt)
    {
        GlobalFunctions.VictimHasDied();
    }
}
