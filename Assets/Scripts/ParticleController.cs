using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField]
    new public Rigidbody2D rigidbody;

    public ChargeBehaviourBase chargeController;

    public InteractionBehvaiourBase interactionController { get; private set; }

    public float Mass { get;  private set; }
    [HideInInspector] public float Charge;

    public bool IsStatic { get; private set; }

    private const int ForceMultiplierConstant = 100;

    public void SetMass(float value)
    {
        if(0 == value)
        {
            Debug.LogError("Mass of a particle can not be 0");
        } else
        {
            Mass = value;
            transform.localScale = Vector3.one * Mathf.Sqrt(value / Mathf.PI);
        }
    }

    public void SetStatic(bool value)
    {
        IsStatic = value;
    }

    public void SetInteractionController(InteractionBehvaiourBase interactionBehvaiour)
    {
        interactionController = interactionBehvaiour;
        interactionController.particleController = this;
    }


    public float ChargeAtPosition(Vector3 position)
    {
        return Charge * chargeController.ChargeScaleAtPosition(position);
    }

    public void AddAcceleration(Vector2 forceVector)
    {
        rigidbody.velocity += ForceMultiplierConstant * forceVector * Time.deltaTime;
    }

    public void GenerateInteraction(ParticleController otherParticle)
    {
        if (false == IsStatic)
        {
            interactionController.GenerateInteraction(otherParticle);
        }
    }
}
