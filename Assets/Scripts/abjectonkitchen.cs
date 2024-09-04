using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abjectonkitchen : MonoBehaviour
{
    public static abjectonkitchen Instance { get; set; }
    public List<SOobjects> thingsInKitchen { get; set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            thingsInKitchen = new List<SOobjects>(); // Initialize the list
        } else {
            Destroy(gameObject);
        }
    }

    public SOobjects GetRandomFromKitchen() {
        if (thingsInKitchen.Count > 0) {
            return thingsInKitchen[UnityEngine.Random.Range(0, thingsInKitchen.Count)];
        }
        return null;
    }
}
