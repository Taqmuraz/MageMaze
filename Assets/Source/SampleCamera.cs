using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleCamera : MonoBehaviour 
{
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(1f, -1f, 1f).normalized);
    }
}
