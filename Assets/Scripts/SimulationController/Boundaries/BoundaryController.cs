using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public static BoundaryController _instance;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Debug.LogError("More than one boundary controller.");
        }
    }

    public int SizeX, SizeY;

    private int DistX, DistY;

    SimulationController simulationController;

    [Range(0, 1)]
    public float plasticity;

    [SerializeField]
    private GameObject[] boundaries = new GameObject[4];

    public void Start()
    {
        DistX = SizeX >> 1;
        DistY = SizeY >> 1;

        simulationController = SimulationController._instance;

        boundaries[0].transform.position = new Vector3(0, DistY, 0);
        boundaries[0].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        boundaries[0].transform.localScale = new Vector3(1, SizeX, 0);

        boundaries[1].transform.position = new Vector3(0, -DistY, 0);
        boundaries[1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        boundaries[1].transform.localScale = new Vector3(1, SizeX, 0);

        boundaries[2].transform.position = new Vector3(DistX, 0, 0);
        boundaries[2].transform.localScale = new Vector3(1, SizeY, 0);

        boundaries[3].transform.position = new Vector3(-DistX, 0, 0);
        boundaries[3].transform.localScale = new Vector3(1, SizeY, 0);
    }

    private void Update()
    {
        ParticleController particle;

        for (int i = 0; i < simulationController.numberOfParticles; i++)
        {
            particle = simulationController.simulatedParticles[i];

            if (Mathf.Abs(particle.transform.position.y) >= DistY)
            {
                float vectorModifyer = -Mathf.Sign((particle.transform.position.y - DistY) * particle.rigidbody.velocity.y);
                vectorModifyer = vectorModifyer < 0 ? vectorModifyer * plasticity : vectorModifyer;

                particle.rigidbody.velocity = new Vector2(particle.rigidbody.velocity.x, particle.rigidbody.velocity.y * vectorModifyer);
            }

            if (Mathf.Abs(particle.transform.position.x) >= DistX)
            {
                float vectorModifyer = -Mathf.Sign((particle.transform.position.x - DistX) * particle.rigidbody.velocity.x);
                vectorModifyer = vectorModifyer < 0 ? vectorModifyer * plasticity : vectorModifyer;

                particle.rigidbody.velocity = new Vector2(particle.rigidbody.velocity.x * vectorModifyer, particle.rigidbody.velocity.y);
            }
        }
    }
}
