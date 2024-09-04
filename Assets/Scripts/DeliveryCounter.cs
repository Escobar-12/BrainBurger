using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    [SerializeField] GameObject selectable;

    public void interact(){
        platesSO thisplate = movment.Instance.GetObjectOnPlayer() as platesSO;
        if (thisplate != null && thisplate.FoodOnPlate.Count > 0) {
            DeliveryRecipeCheck.Instance.DeliverTheRecipe(thisplate);
            thisplate.selfdestroy();
            movment.Instance.Cleartheplayer();
        }
    }

    public void show(){
        selectable.SetActive(true);
    }

    public void hide(){
        selectable.SetActive(false);
    }
}

