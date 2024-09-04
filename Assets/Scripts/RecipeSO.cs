using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RecipeS")]
public class RecipeSO : ScriptableObject
{
    public List<SOobjects> RecipeList;
    public string Recipename;

    public List<SOobjects> getrecipelist(){
        return RecipeList;
    }
}

