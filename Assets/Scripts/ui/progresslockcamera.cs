using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progresslockcamera : MonoBehaviour
{
    enum Modes{
        // LookAt,
        // LookAtInverted,
        OnCameraForword,
        OnCameraReversed
    }
    [SerializeField] Modes mode;
    void LateUpdate()
    {
        switch(mode){
            // case Modes.LookAt:
            //     transform.LookAt(Camera.main.transform);
            //     break;
            // case Modes.LookAtInverted:
            //     Vector3 direction = transform.position - Camera.main.transform.position;
            //     transform.LookAt(transform.position + direction);
            //     break;
            case Modes.OnCameraForword:
                transform.forward = Camera.main.transform.forward;
                break;
            case Modes.OnCameraReversed:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
