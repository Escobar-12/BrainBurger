using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "scriptableobject" ,menuName = "SO")]
public class scriptableobjects : ScriptableObject
{
    public string SO_name;
    public Sprite sprite;
    public GameObject prefab;
}
