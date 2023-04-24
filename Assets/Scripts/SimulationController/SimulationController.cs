using UnityEngine;
using System.Collections.Generic;

public class SimulationController : MonoBehaviour
{
    public static SimulationController _instance;

    public ParticleController[] simulatedParticles;
    public List<ParticleController> zeroChargeParticles = new List<ParticleController>();
    public int numberOfParticles;

    [SerializeField]
    private GameObject trailDot;

    private int maxSimulatedParticles = 32;

    public bool simulationIsActive;

    public bool ShowDotTrail;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            Debug.LogError("More than one instance of " + name + ". Do better!");
        }
    }

    public void AddToSimulation(ParticleController newParticle)
    {
        if(numberOfParticles < maxSimulatedParticles)
        {
            simulatedParticles[numberOfParticles] = newParticle;
            numberOfParticles++;
        }
        else
        {
            Debug.LogError("Maximum number of particles had been reached");
            Destroy(newParticle.gameObject);
        }
    }

    public void AddZeroChargeParticle(ParticleController particle)
    {
        zeroChargeParticles.Add(particle);
    }

    private void Start()
    {
        simulatedParticles = new ParticleController[maxSimulatedParticles];
    }

    private void Update()
    {
        if(true == simulationIsActive)
        {
            for (int i = 0; i < numberOfParticles - 1; i++)
            {
                for (int j = i + 1; j < numberOfParticles; j++)
                {
                    simulatedParticles[i].GenerateInteraction(simulatedParticles[j]);
                    simulatedParticles[j].GenerateInteraction(simulatedParticles[i]);
                }
            }

            ShowParticleEffects();
        }
    }

    private void ShowParticleEffects()
    {
        if (ShowDotTrail)
        {
            for (int i = 0; i < numberOfParticles; i++)
            {
                if (false == simulatedParticles[i].IsStatic)
                {
                    Instantiate(trailDot, simulatedParticles[i].rigidbody.position, Quaternion.identity);
                }
            }
        }
    }

    /*private void GenerateInteraction(int index_1, int index_2)
    {
        ParticleController particle_1 = simulatedParticles[index_1];
        ParticleController particle_2 = simulatedParticles[index_2];

        Vector3 position_1 = particle_1.transform.position;
        Vector3 position_2 = particle_2.transform.position;

        float chargeOn_1 = particle_2.ChargeAtPosition(position_1); //particle 2:s charge on particle 1
        float chargeOn_2 = particle_1.ChargeAtPosition(position_2); //particle 1:s charge on particle 2

        Vector2 normalizedDirection = position_2 - position_1; //Vector from 1 to 2

        Vector2 forceOn_1 = chargeOn_1 * particle_1.Charge * -normalizedDirection;
        Vector2 forceOn_2 = chargeOn_2 * particle_2.Charge * normalizedDirection;

        particle_1.AddAcceleration(forceOn_1);
        particle_2.AddAcceleration(forceOn_2);
    }*/

    private void OnToggleSimulation()
    {
        Time.timeScale = 1 - Time.timeScale;
        simulationIsActive = !simulationIsActive;
    }

    private void OnResetSimulation()
    {
        for (int i = 0; i < numberOfParticles; i++)
        {
            Destroy(simulatedParticles[i].gameObject);
        }

        foreach (var particle in zeroChargeParticles)
        {
            Destroy(particle.gameObject);
        }

        numberOfParticles = 0;
    }

    private void OnToggleUI()
    {
        UIController._instance.ToggleUI();
    }   
}
