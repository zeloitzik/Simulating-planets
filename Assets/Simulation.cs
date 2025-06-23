using UnityEngine;

public class Simulation : MonoBehaviour
{
    CelestialBody[] celestialBodies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        celestialBodies = FindObjectsByType<CelestialBody>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < celestialBodies.Length; i++)
        {
            for (int j = 0; j < celestialBodies.Length; j++)
            {
                if (i != j)
                {
                    celestialBodies[i].UpdateVelocity(celestialBodies[j], UniverseConsts.simulationTimeStep);
                }
            }
            celestialBodies[i].UpdatePosition(UniverseConsts.simulationTimeStep);
        }
    }
}
