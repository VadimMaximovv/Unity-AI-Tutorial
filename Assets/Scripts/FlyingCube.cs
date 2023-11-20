using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCube : MonoBehaviour
{
    public float rX;
    void Update()
    {
        transform.Rotate(rX, rX, rX);
    }
}
