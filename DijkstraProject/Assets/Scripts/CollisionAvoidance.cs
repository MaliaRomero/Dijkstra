using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : Kinematic
{
    public Kinematic[] myTargets = new Kinematic[4];
    public float radius = 1.0f; // Collision radius
    public float coneAngle = 45.0f; // Cone checks 90 degrees
    public float avoidanceStrength = 10.0f; // How heavily to avoid

    protected override void Update()
    {
        SteeringOutput steering = GetSteering();

        // Apply steering to your object's movement (velocity or position)
        this.transform.position += steering.linear * Time.deltaTime; // Adjust this according to your movement logic
        
        base.Update(); // Update kinematic's update
    }

    private SteeringOutput GetSteering()
    {

        SteeringOutput steering = new SteeringOutput(); 

        foreach (var target in myTargets) //going through all of the targets
        {
            // Calculate the direction to the target
            Vector3 directionToTarget = target.transform.position - this.transform.position; //direction, same idea as seek/flee
            float distanceToTarget = directionToTarget.magnitude; //gets distance
            directionToTarget.Normalize(); // Unit vectors to get direction

            float dot = Vector3.Dot(this.transform.forward, directionToTarget); // how close is the character to facing the target?
            float coneThreshold = Mathf.Cos(coneAngle * Mathf.Deg2Rad); // determines if target is within the cone

            if (dot > coneThreshold) //checks if the character is facing within the cone of vision
            {
                if (distanceToTarget < radius) //is target in collision distance?
                {
                    //calculate direction to move away
                    Vector3 avoidanceDirection = this.transform.position - target.transform.position;
                    avoidanceDirection.Normalize();
                    
                    steering.linear = avoidanceDirection * avoidanceStrength; //how strong to avoid
                }
            }
        }

        return steering; //keep this

    }
}
