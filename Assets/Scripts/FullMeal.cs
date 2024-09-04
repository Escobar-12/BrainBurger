using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullMeal : MonoBehaviour
{
    [SerializeField] GameObject[] FoodInTheFullMeal;

    void Awake()
    {
        InitializeFoodVisuals(); // Ensure all food visuals are inactive at the start
    }

    void InitializeFoodVisuals()
    {
        foreach (GameObject food in FoodInTheFullMeal)
        {
            food.SetActive(false);
        }
    }

    public void ActivateFoodVisual(SOobjects passedObject)
    {
        GameObject foodToActivate = GetObjectByTag(passedObject.tag);
        //Debug.Log(passedObject.tag);
        if (foodToActivate != null)
        {
            foodToActivate.SetActive(true);
            //Debug.Log("Activating food: " + foodToActivate.name);
        }
        else
        {
            Debug.LogWarning("No matching food found for: " + passedObject.tag);
        }
    }
    public void DeactivateFoodVisual(SOobjects passedObject)
    {
        GameObject foodTodeActivate = GetObjectByTag(passedObject.tag);
        //Debug.Log(passedObject.tag);
        if (foodTodeActivate != null)
        {
            foodTodeActivate.SetActive(false);
            //Debug.Log("Activating food: " + foodToActivate.name);
        }
        else
        {
            Debug.LogWarning("No matching food found for: " + passedObject.tag);
        }
    }
    public void deactivateall(List<SOobjects> list)
    {
        foreach(SOobjects foodobject in list){
            foodobject.gameObject.SetActive(false);
        }
    }
    public void activateall(List<SOobjects> list)
    {
        foreach(SOobjects foodobject in list){
            foodobject.gameObject.SetActive(true);
        }
    }

    GameObject GetObjectByTag(string tag)
    {
        foreach (GameObject food in FoodInTheFullMeal)
        {
            if (food.CompareTag(tag))
            {
                return food;
            }
        }
        return null;
    }
}