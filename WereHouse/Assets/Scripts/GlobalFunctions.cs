using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rho;
using GameEvents;

public static class GlobalFunctions
{
    private static int TotalVictims = 0;
    public static int VictimCount
    {
        get { return TotalVictims; }
    }
    public static void ResetVictimCount()
    {
        TotalVictims = 0;
    }
    public static void VictimHasDied()
    {
        TotalVictims++;
    }
}
