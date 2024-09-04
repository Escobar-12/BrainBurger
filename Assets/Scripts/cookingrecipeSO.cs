using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "cookingrecipeSO")]
public class cookingrecipeSO : ScriptableObject
{
    public  SOobjects input;
    public SOobjects output;
    public float CookingProgressMax;
}

