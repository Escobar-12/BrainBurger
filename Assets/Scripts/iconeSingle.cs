using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class iconeSingle : MonoBehaviour
{
    [SerializeField] Image foodSprit;
    
    public void setfoodsprit(SOobjects objectfood){
        foodSprit.sprite = objectfood.sprite;
    }
}
