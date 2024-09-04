using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class deliverymanegersingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI OrderNameTEXT;
    [SerializeField] Transform iconcontaner;
    [SerializeField] Transform icontemplate;

    void Awake()
    {
        icontemplate.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setorderUI(RecipeSO order){
        OrderNameTEXT.text = order.name ;
        foreach(Transform child in iconcontaner){
            if(child == icontemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(SOobjects OBJ in order.getrecipelist()){
            Transform iconTransform = Instantiate(icontemplate,iconcontaner);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = OBJ.sprite;
        }
    }
}
