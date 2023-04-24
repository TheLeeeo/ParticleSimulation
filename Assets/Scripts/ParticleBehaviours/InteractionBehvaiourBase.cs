using UnityEngine;

public abstract class InteractionBehvaiourBase : MonoBehaviour
{
    public ParticleController particleController;

    public abstract void GenerateInteraction(ParticleController otherParticle);
}
