using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularChargeBehaviour : ChargeBehaviourBase
{
    public override float ChargeScaleAtPosition(Vector3 otherPosition)
    {
        return 1 / (transform.position - otherPosition).sqrMagnitude;
    }
}
