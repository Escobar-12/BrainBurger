using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "client")]
public class client : ScriptableObject
{
    [SerializeField] public List<RecipeSO> FaveredFood;
    [SerializeField] public string name;

    //set the visual of your clients 
    [SerializeField] public GameObject prefab;
}
