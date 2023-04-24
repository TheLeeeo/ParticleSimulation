using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularInteractionBehaviour : InteractionBehvaiourBase
{
    public override void GenerateInteraction(ParticleController otherParticle)
    {
        Vector3 normalizedDirection = (otherParticle.transform.position - transform.position).normalized; //Vector from 1 to 2

        Vector3 accelerationOnSelf = Mathf.Sign(particleController.Charge) * otherParticle.ChargeAtPosition(transform.position) * -normalizedDirection;

        particleController.AddAcceleration(accelerationOnSelf);
    }
}
