using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeIcone;
    // Start is called before the first frame update
    void Start()
    {
        recipeIcone.gameObject.SetActive(false);

        DeliveryRecipeCheck.Instance.OrderSpawned += visualspawn;
        DeliveryRecipeCheck.Instance.OrderCompleted += visualcomplete;

        UpdateVisual();
    }
    void visualspawn(){
        UpdateVisual();
    }
    void visualcomplete(){
        UpdateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateVisual(){
        foreach(Transform child in container){
            if(child != recipeIcone){
                Destroy(child.gameObject);
            }
        }
        if(DeliveryRecipeCheck.Instance.getwaitingrecipes() != null)
        {
            foreach(RecipeSO recipe in DeliveryRecipeCheck.Instance.getwaitingrecipes()){
            Transform currentrecipeorder = Instantiate(recipeIcone,container);
            currentrecipeorder.GetComponent<deliverymanegersingleUI>().setorderUI(recipe);
            currentrecipeorder.gameObject.SetActive(true);
            }
        }
        
    }
    
}
