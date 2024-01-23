using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotate : MonoBehaviour
{
    public float speedRotate = 40;

    // Update is called once per frame
    void Update()
    {
        // quay theo trục y
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * speedRotate));
    }
}
