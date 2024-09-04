using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobilegamepad : MonoBehaviour
{
    [SerializeField] GameObject gamepad;
    // Start is called before the first frame update
    void Start()
    {
        // only show the gamepad if its a mobile
        gamepad.SetActive(Application.isMobilePlatform);
    }

}
