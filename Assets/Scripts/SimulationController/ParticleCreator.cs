using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleCreator : MonoBehaviour
{
    public static ParticleCreator _instance;

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

    [SerializeField]
    private GameObject particlePrefab;
    private GameObject spawnedParticle;
    private bool ActiveParticleExists = false;

    [SerializeField]
    private GameObject arrowPrefab;
    private GameObject spawnedArrow;

    Vector2 MouseStartPosition;

    public ParticleChooserBase currentParticleChooser;

    [HideInInspector]    
    public bool CanSpawnParticles = false;

    private void Start()
    {
        CanSpawnParticles = false; //Neccesary because unity editor is serializing it as true
    }

    private void Update()
    {
        if (true == ActiveParticleExists)
        {
            Vector2 CurrentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            spawnedArrow.transform.up = MouseStartPosition - CurrentMousePosition;
            spawnedArrow.transform.localScale = new Vector3(2, (MouseStartPosition - CurrentMousePosition).magnitude * 2, 0);
        }
    }

    private void OnDropParticle()
    {
        if (CanSpawnParticles)
        {
            spawnedParticle = currentParticleChooser.CreateParticle(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));

            ParticleController particleController = spawnedParticle.GetComponent<ParticleController>();

            if(0 != particleController.Charge)
            {
                SimulationController._instance.AddToSimulation(particleController);
            }
            else
            {
                SimulationController._instance.AddZeroChargeParticle(particleController);
            }
        }
    }

    private void OnSpawnParticle(InputValue value)
    {
        if (CanSpawnParticles)
        {
            Vector2 CurrentMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (true == value.isPressed)
            {
                spawnedParticle = currentParticleChooser.CreateParticle(CurrentMousePosition);

                MouseStartPosition = CurrentMousePosition;

                spawnedArrow = Instantiate(arrowPrefab, CurrentMousePosition, Quaternion.identity);
                spawnedArrow.transform.localScale = Vector3.zero;

                ActiveParticleExists = true;

            }
            else //Release particle
            {
                if (ActiveParticleExists)
                {
                    spawnedParticle.GetComponent<Rigidbody2D>().velocity = MouseStartPosition - CurrentMousePosition;

                    ParticleController particleController = spawnedParticle.GetComponent<ParticleController>();
                    if (0 != particleController.Charge)
                    {
                        SimulationController._instance.AddToSimulation(particleController);
                    }
                    else
                    {
                        SimulationController._instance.AddZeroChargeParticle(particleController);
                    }

                    Destroy(spawnedArrow);

                    ActiveParticleExists = false;
                }
            }
        }
    }

    private void OnCancelParticle()
    {
        if(true == ActiveParticleExists)
        {
            ActiveParticleExists = false;
            Destroy(spawnedParticle);
            Destroy(spawnedArrow);
        }
    }    
}
