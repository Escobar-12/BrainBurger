using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOobjects : MonoBehaviour
{
    [SerializeField] scriptableobjects SO;
    public Sprite sprite;
    ClearCounter onthiscounter;
    contaners onthiscontaner;
    cuttingcounter onthiscuttingcouter;
    stoveCounter onthisstove;

    void Awake()
    {
        sprite = SO.sprite;
    }
    public scriptableobjects getSo()
    {
        return SO;
    }
    public ClearCounter getcounter()
    {
        return onthiscounter;
    } 
    public contaners getcontaner(){
        return onthiscontaner;
    }
    public stoveCounter getstove(){
        return onthisstove;
    }
    public void setoncounter(ClearCounter counter)
    {
        onthiscounter = counter;
    }
    public void setoncontaner (contaners contaner)
    {
        onthiscontaner = contaner;
    }
    public void setonCuttingcounter(cuttingcounter CC){
        onthiscuttingcouter = CC;
    }
    public void setonstove(stoveCounter Stove){
        onthisstove = Stove;
    }
    public void selfdestroy(){
        if(onthiscuttingcouter != null){
            onthiscuttingcouter = null;
        }
        Destroy(gameObject);
    }
    public void DIS(Transform player)
    // for removing all the old children (old food)
    {
        foreach (Transform child in player)
        {
            Destroy(child.gameObject);
        }
    }
}
