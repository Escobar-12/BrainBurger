using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rot : MonoBehaviour
{
    public Transform fps;

    void FixedUpdate()
    {
        float y = fps.rotation.eulerAngles.y;
        Quaternion targetrotation = Quaternion.Euler(0,y,0);
        transform.rotation = targetrotation;
    }
}
