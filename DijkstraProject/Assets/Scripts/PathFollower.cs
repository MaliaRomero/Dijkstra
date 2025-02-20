using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Kinematic
{
    PathFollow myMoveType;
    LookWhereGoing myRotateType;
    public GameObject[] pathObjects;

    void Start()
    {
        myMoveType = new PathFollow();
        myMoveType.character = this;
        myMoveType.pathObject = pathObjects;

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
