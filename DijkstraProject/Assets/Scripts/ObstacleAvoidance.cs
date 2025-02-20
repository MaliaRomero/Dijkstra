using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoider : Kinematic
{
    // Adjustable parameters for obstacle avoidance
    public float detectionDistance = 5.0f;
    public float avoidanceStrength = 1.5f;
    public float safeDistance = 1.0f;
    public float sidewaysStrength = 2.0f;  // Strength of sideways movement when close to an obstacle
    public GameObject[] obstacles;
    protected override void Update()
    {
        steeringUpdate = GetObstacleAvoidanceSteering();
        base.Update();
    }

    private SteeringOutput GetObstacleAvoidanceSteering()
    {
        SteeringOutput steeringOutput = new SteeringOutput();

        foreach (var obstacle in obstacles)
        {
            RaycastHit hit;

            // Cast a ray from the character's position forward
            if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
            {
                //if the ray hits the obstacle
                if (hit.collider.gameObject == obstacle)
                {
                    // calculate the avoidance target
                    Vector3 hitPoint = hit.point;    // The point of collision
                    Vector3 hitNormal = hit.normal;  // "The surface normal at the collision point"

                    // add the safe distance
                    Vector3 avoidanceTarget = hitPoint + hitNormal * safeDistance;

                    // Calculate the steering force to avoid the obstacle
                    Vector3 steering = avoidanceTarget - transform.position;
                    steering.Normalize();
                    steering *= avoidanceStrength;

                    // If the character is very close to the obstacle, apply sideways velocity
                    if (hit.distance < safeDistance)
                    {
                        // calculate a sideways direction perpenducular
                        Vector3 sideways = Vector3.Cross(hitNormal, Vector3.up);
                        sideways.Normalize();
                        steering += sideways * sidewaysStrength;  // Apply sideways velocity to the steering
                    }

                    steeringOutput.linear = steering;
                }
            }
        }

        return steeringOutput;
    }
}