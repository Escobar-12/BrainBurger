using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconsUIscript : MonoBehaviour
{
    [SerializeField] platesSO onthisplate;
    [SerializeField] Transform icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void iconspawn (SOobjects sOobject) {
        // foreach(SOobjects objectonplate in onthisplate.getobjectlist())
        // {
            Transform object_icon =  Instantiate(icon,transform);
            object_icon.GetComponent<iconeSingle>().setfoodsprit(sOobject);
        // }
    }
    public void iconspawnlist (List<SOobjects> sOobjects) {

        foreach(SOobjects objectonplate in sOobjects)
        {
            Transform object_icon =  Instantiate(icon,transform);
            object_icon.GetComponent<iconeSingle>().setfoodsprit(objectonplate);
        }
    }
    public void clearcanvas(){
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
