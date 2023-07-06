using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToTargetAgent : Agent
{
    [SerializeField] float speed;
    [SerializeField] Transform target;
    [SerializeField] SpriteRenderer backGround;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3.5f, -1.5f), Random.Range(-3.5f, 3.5f));
        target.localPosition = new Vector3(Random.Range(1.5f, 3.5f), Random.Range(-3.5f, 3.5f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2)transform.localPosition);
        sensor.AddObservation((Vector2)target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float _moveX = actions.ContinuousActions[0];
        float _moveY = actions.ContinuousActions[1];

        float movementSpeed = speed;

        transform.localPosition += new Vector3(_moveX, _moveY)*Time.deltaTime*movementSpeed;
    }

    //Decide what the ai can do
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actionSegment = actionsOut.ContinuousActions;
        actionSegment[0] = Input.GetAxisRaw("Horizontal");
        actionSegment[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Target target))
        {
            AddReward(10);
            backGround.color = Color.green;
            EndEpisode();
        }
        else if(collision.TryGetComponent(out Wall wall))
        {
            AddReward(-2);
            backGround.color = Color.red;
            EndEpisode();
        }
    }
}
