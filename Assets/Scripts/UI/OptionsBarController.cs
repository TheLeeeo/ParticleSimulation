using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsBarController : MonoBehaviour
{
    public void ActivateDotTrail(bool value)
    {
        SimulationController._instance.ShowDotTrail = value;
    }
}
