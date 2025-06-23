using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float mass;
    public float radius;
    public UnityEngine.Vector3 velocity;

    void Start()
    {
        // Initialize celestial body properties

    }

    //Update velocity
    public void UpdateVelocity(CelestialBody otherBody, float timeStep)
    {
        //Calculate the gravitational force between this body and another body
        // F = G * (m1 * m2) / r^2

        //r^2
        Vector3 direction = otherBody.transform.position - transform.position;
        float distanceSqr = direction.sqrMagnitude;
        if (distanceSqr == 0f) return;

        Vector3 forceDir = direction.normalized;
        Vector3 force = forceDir * UniverseConsts.G * (mass * otherBody.mass) / distanceSqr;

        UnityEngine.Vector3 acceleration = force / mass;

        velocity += acceleration * timeStep;
        Debug.Log($"Updated velocity of {gameObject.name}: {velocity}");
    }

    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        //Update the velocity of the celestial body based on acceleration
        velocity += acceleration * timeStep;
    }
    //Update position
    public void UpdatePosition(float timeStep)
    {
        //Update the position of the celestial body based on its velocity
        transform.position += velocity * timeStep;
        //Debug.Log($"Updated position of {gameObject.name}: {transform.position}");
    }
}
