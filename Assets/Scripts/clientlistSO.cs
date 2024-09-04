using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;



// [CreateAssetMenu(menuName = "clientlistSO")]
public class clientlistSO : ScriptableObject
{
    [SerializeField] public List<client> ClientsList;

}

