using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundarySetter : MonoBehaviour
{
    public float plasticity = 0.75f;

    public void SetBoundaries()
    {
        BoundaryController._instance.plasticity = plasticity;

        BoundaryController._instance.Start();
    }

    public void SetDimensionX(string value)
    {
        BoundaryController._instance.SizeX = int.Parse(value);
    }

    public void SetDimensionY(string value)
    {
        BoundaryController._instance.SizeY = int.Parse(value);
    }

    public void SetNewPlasticity(float value)
    {
        plasticity = value;
    }
}

