using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : Seek
{
    //Choose a data structure to represent the path that works for you. 
    //For example, an array of invisible GameObjects serves just fine for this exercise.
    public GameObject[] pathObject;
    int currParam; //Current position on the path

    float targetRadius = .5f; //If within this, object has reached the target

    //float predictTime = .1f;

    int closestIndex = 0;

    float closestDistance = float.MaxValue;

    public override SteeringOutput getSteering()
    {
        if (target == null)
        {
            for (int i = 0; i < pathObject.Length; i++)
            {
                float distance = Vector3.Distance(character.transform.position, pathObject[i].transform.position);

                if (distance < closestDistance)
                {
                    closestIndex = i;
                    closestDistance = distance;
                }
            }

            target = pathObject[closestIndex];
        }

        float distanceToTarget = (target.transform.position - character.transform.position).magnitude;

        if (distanceToTarget < targetRadius)
        {
            currParam++;
            if (currParam > pathObject.Length - 1)
            {
                currParam = 0;
            }
            target = pathObject[currParam];
        }
    
    return base.getSteering();

    }
}
