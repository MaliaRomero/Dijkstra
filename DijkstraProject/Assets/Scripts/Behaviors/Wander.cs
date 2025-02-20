using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Note: Millington's pseudocode extends Face. It should extend Seek.
public class Wander : Seek
{
    //*** Paige helped me with this***

    //character: Static
    //maxSpeed: float
    public float maxSpeed;
    public float maxAcceleration;
    public float currOrientation;
    //The maximum rotation speed we'd like, probably should be 
    //smaller # than the maximum possible, for a leisurely change in direction. maxRotation: float
    //public float maxRotation

    //function getSteering()-> KinematicSteeringOutput:
    public override SteeringOutput getSteering()
    {
        // Get velocity from the vector form of the orientation
        //result.velocity = maxSpeed character. orientation.asVector()
        //Change our orientation randomly.
        SteeringOutput result = new SteeringOutput();
        //result.rotation = randomBinomial() maxRotation
        //Note2: feel free to use Random.insideUnitCircle 
        //instead of the portion of Millington's pseudocode that reinvents the same
        currOrientation += Random.insideUnitCircle.x * 10;//not strong enough without *
        Vector3 target = getTargetPosition();

        //result = new KinematicSteeringOutput()
        result.linear = currOrientation * character.transform.position;
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;

        return result;
    }
    /*A kinematic wander behavior always moves in the direction of the character’s current orientation
    with maximum speed. The steering behavior modifies the character’s orientation, which allows the
    character to meander as it moves forward. Figure 3.7 illustrates this. The character is shown at
    successive frames. Note that it moves only forward at each frame (i.e., in the direction it was
    facing at the previous frame). */
}