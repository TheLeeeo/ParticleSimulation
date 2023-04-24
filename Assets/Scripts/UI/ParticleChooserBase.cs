using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleChooserBase : MonoBehaviour
{
    [SerializeField]
    protected GameObject particleObject;

    public abstract GameObject CreateParticle(Vector2 position);
}
