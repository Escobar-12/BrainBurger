
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platescountervisual : MonoBehaviour
{
    [SerializeField] platecounter PlatesCounter;
    [SerializeField] Transform spwanPoint;
    [SerializeField] Transform plateVisual;
    // Start is called before the first frame update
    void Start()
    {
        PlatesCounter.Onplatespawned += spawn;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawn(int platesamount){
        Transform visualtransform = Instantiate(plateVisual,spwanPoint);
        float plateoffset = 0.1f;
        visualtransform.localPosition = new Vector3(0 ,plateoffset * platesamount ,0);
    }
}