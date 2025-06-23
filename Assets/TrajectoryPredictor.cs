using UnityEngine;
using System.Collections.Generic;

[ExecuteAlways]
public class TrajectoryPredictor : MonoBehaviour
{
    public int steps = 1000;
    public float timeStep = 0.01f;
    public Color trajectoryColor = Color.cyan;

    void OnDrawGizmos()
    {
        CelestialBody[] bodies = FindObjectsByType<CelestialBody>(FindObjectsSortMode.None);
        Vector3[] positions = new Vector3[bodies.Length];
        Vector3[] velocities = new Vector3[bodies.Length];

        // Store initial state
        for (int i = 0; i < bodies.Length; i++)
        {
            positions[i] = bodies[i].transform.position;
            velocities[i] = bodies[i].velocity;
        }

        for (int step = 0; step < steps; step++)
        {
            Vector3[] accelerations = new Vector3[bodies.Length];

            // Calculate accelerations from gravity
            for (int i = 0; i < bodies.Length; i++)
            {
                for (int j = 0; j < bodies.Length; j++)
                {
                    if (i == j) continue;

                    Vector3 direction = positions[j] - positions[i];
                    float distanceSqr = direction.sqrMagnitude;
                    if (distanceSqr == 0f) continue;

                    Vector3 forceDir = direction.normalized;
                    accelerations[i] += UniverseConsts.G * bodies[j].mass * forceDir / distanceSqr;
                }
            }

            // Update velocities and positions
            for (int i = 0; i < bodies.Length; i++)
            {
                velocities[i] += accelerations[i] * timeStep;
                Vector3 newPosition = positions[i] + velocities[i] * timeStep;

                Gizmos.color = trajectoryColor;
                Gizmos.DrawLine(positions[i], newPosition);
                positions[i] = newPosition;
            }
        }
    }
}
