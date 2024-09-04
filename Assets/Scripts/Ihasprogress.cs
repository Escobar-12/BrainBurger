using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface Ihasprogress
{
    public event EventHandler<progressONui> ProgressHundler;
    public class progressONui : EventArgs{
        public float CurrentProgressNormalized;
    }
}
