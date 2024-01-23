using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    private void Step()
    {
        AudioManager._instance.StepSound();
    }
}
