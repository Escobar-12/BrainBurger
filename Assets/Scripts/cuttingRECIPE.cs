using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "cutting recipe SO")]
public class cuttingRECIPE : ScriptableObject
{
    public SOobjects input;
    public SOobjects output;
    public int CuttingProgressMax;
}
