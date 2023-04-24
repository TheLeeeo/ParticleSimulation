using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of " + name + ". Do better!");
        }
    }

    public GameObject ActiveParticleChooser;
    [SerializeField]
    private GameObject OptionsBar;

    private bool IsActive = false;

    public void ToggleUI()
    {
        ParticleCreator._instance.CanSpawnParticles = IsActive;

        IsActive = !IsActive;

        ActiveParticleChooser.SetActive(IsActive);
        OptionsBar.SetActive(IsActive);
    }
}
