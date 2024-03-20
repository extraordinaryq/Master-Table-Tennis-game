using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public enum Humanmodel
{
    Model0 = 0,
    Model1 = 1,
    Model2 = 2,
    Model3 = 3,
    Model4 = 4,
}

public class PaddleAi : Agent
{
    [SerializeField] private GameObject ball;
    //[SerializeField] private GameObject Opponent;
    [SerializeField] private GameObject spine1;
    [SerializeField] private GameObject spine2;
    [SerializeField] private GameObject spine3;
    [SerializeField] private GameObject upperarm;
    [SerializeField] private GameObject lowerarm;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject leg1_l;
    [SerializeField] private GameObject leg2_l;
    [SerializeField] private GameObject leg3_l;
    [SerializeField] private GameObject leg1_r;
    [SerializeField] private GameObject leg2_r;
    [SerializeField] private GameObject leg3_r;
    [SerializeField] private GameObject human;
    [SerializeField] private Humanmodel Humanmodel;
    private float crouch;
    private float bow;
    private float zedge;

    [SerializeField] private float moveSpeed = 1f;
    
    [HideInInspector] public Vector3 StartingPosition;
    public PaddleState TeamID;
    private float AgentRot;
    private Vector3 Direction;
    //private Vector3 Direction_move;
    private Vector3 Direction_rot;
    public TableTennisBall Tb;
    [HideInInspector] public double actionX, actionY, actionZ;

    [HideInInspector] public Rigidbody Agentrb;
    private Rigidbody Ballrb;
    //private Rigidbody Opponentrb;
    //public GameObject OriginPoint;

    public override void Initialize()
    {
        StartingPosition = new Vector3(human.transform.localPosition.x, human.transform.localPosition.y, human.transform.localPosition.z);
        //OriginStartingPosition = new Vector3(OriginPoint.transform.localPosition.x, OriginPoint.transform.localPosition.y, OriginPoint.transform.localPosition.z);
        Ballrb = ball.GetComponent<Rigidbody>();
        Agentrb = human.GetComponent<Rigidbody>();
        //Opponentrb = Opponent.GetComponent<Rigidbody>();
        //Rotated = this.transform.localRotation;
        if (TeamID == PaddleState.LeftPaddle)
        {
            AgentRot = 1;
        }
        else
        {
            AgentRot = -1;
        }
        crouch = 0.5f;
        bow = 0.5f;
        if (Humanmodel == Humanmodel.Model0)
        {
            zedge = 1.93f;
        }
        else
        {
            zedge = 1.53f;
        }
    }

    public override void OnEpisodeBegin()
    {
        crouch = 0.5f;
        human.transform.localPosition = StartingPosition;
        upperarm.transform.localRotation = Quaternion.Euler(15f, -15f, -30f);
        lowerarm.transform.localRotation = Quaternion.Euler(45f, -3.32f, -4.228f);
        hand.transform.rotation = Quaternion.Euler(0f, 90f * AgentRot + 240f, 300f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((human.transform.localPosition.x - StartingPosition.x) * AgentRot);
        sensor.AddObservation((human.transform.localPosition.z - StartingPosition.z) * AgentRot);
        //sensor.AddObservation(OriginPoint.transform.localPosition);
        //sensor.AddObservation(Agentrb.velocity.x);
        //sensor.AddObservation(Agentrb.velocity.z);
        sensor.AddObservation(crouch);
        //sensor.AddObservation(bow);
        sensor.AddObservation(upperarm.transform.localEulerAngles.x / 90f + 1 / 3);
        sensor.AddObservation(upperarm.transform.localEulerAngles.y / 90f + 2 / 3);
        sensor.AddObservation(upperarm.transform.localEulerAngles.z / 60f + 1f);
        sensor.AddObservation(lowerarm.transform.localEulerAngles.x / 60f + 1f);
        sensor.AddObservation(hand.transform.localEulerAngles.y / 60f + 0.5f);

        Vector3 toBall = new Vector3((Ballrb.transform.position.x - this.transform.position.x) * AgentRot,
        (Ballrb.transform.position.y - this.transform.position.y),
        (Ballrb.transform.position.z - this.transform.position.z) * AgentRot);
        sensor.AddObservation(toBall.normalized);
        // Distance from the ball (1 float)
        sensor.AddObservation(toBall.magnitude);

        sensor.AddObservation(Ballrb.velocity.y);
        sensor.AddObservation(Ballrb.velocity.z * AgentRot);
        sensor.AddObservation(Ballrb.velocity.x * AgentRot);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.Q))
        {
            continuousActionsOut[3] = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            continuousActionsOut[3] = 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            continuousActionsOut[4] = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            continuousActionsOut[4] = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            continuousActionsOut[5] = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            continuousActionsOut[5] = 1f;
        }
        if (Input.GetKey(KeyCode.R))
        {
            continuousActionsOut[6] = -1f;
        }
        if (Input.GetKey(KeyCode.F))
        {
            continuousActionsOut[6] = 1f;
        }
        if (Input.GetKey(KeyCode.T))
        {
            continuousActionsOut[7] = -1f;
        }
        if (Input.GetKey(KeyCode.G))
        {
            continuousActionsOut[7] = 1f;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            continuousActionsOut[8] = -1f;
        }
        if (Input.GetKey(KeyCode.H))
        {
            continuousActionsOut[8] = 1f;
        }
        if (Input.GetKey(KeyCode.U))
        {
            continuousActionsOut[9] = -1f;
        }
        if (Input.GetKey(KeyCode.J))
        {
            continuousActionsOut[9] = 1f;
        }
        //if (Input.GetKey(KeyCode.I))
        //{
        //    continuousActionsOut[10] = -1f;
        //}
        //if (Input.GetKey(KeyCode.K))
        //{
        //    continuousActionsOut[10] = 1f;
        //}
        //if (Input.GetKey(KeyCode.O))
        //{
        //    continuousActionsOut[11] = -1f;
        //}
        //if (Input.GetKey(KeyCode.L))
        //{
        //    continuousActionsOut[11] = 1f;
        //}
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if (Humanmodel == Humanmodel.Model0)
        {
            actionX = 2f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
            actionY = 2.5f + 2f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
            actionZ = 5.5f + 3.5f * Mathf.Clamp(actionBuffers.ContinuousActions[2], -1f, 1f);

            var moveX = moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[3], -1f, 1f);
            crouch = Mathf.Clamp((crouch + 0.04f * actionBuffers.ContinuousActions[4]), 0f, 1f);
            var moveZ = moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[5], -1f, 1f);

            bow = crouch * 0.5f;
            upperarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.x) + actionBuffers.ContinuousActions[6]), -30f, 60f),
                Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.y) + actionBuffers.ContinuousActions[7]), -60f, 30f), Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.z) + actionBuffers.ContinuousActions[8]), -60f, 0f));
            //upperarm.transform.localRotation = Quaternion.Euler(upperarm.transform.localEulerAngles.x, Mathf.Clamp((upperarm.transform.localEulerAngles.y + actionBuffers.ContinuousActions[8]),-60f ,30f), upperarm.transform.localEulerAngles.z);
            //upperarm.transform.localRotation = Quaternion.Euler(upperarm.transform.localEulerAngles.x, upperarm.transform.localEulerAngles.y, Mathf.Clamp((upperarm.transform.localEulerAngles.z + actionBuffers.ContinuousActions[9]),-60f ,0f));
            lowerarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((lowerarm.transform.localEulerAngles.x + actionBuffers.ContinuousActions[9]), 20f, 89.9f), -3.32f, -4.228f);
            //hand.transform.localRotation = Quaternion.Euler(hand.transform.localEulerAngles.x, Mathf.Clamp((CheckAngle(hand.transform.localEulerAngles.y) + actionBuffers.ContinuousActions[10]), -30f, 30f), hand.transform.localEulerAngles.z);
            //Direction_move = new Vector3((float)AgentRot * moveX * Time.deltaTime, 0f, (float)AgentRot * moveZ * Time.deltaTime);
            //Direction_rot = new Vector3(3f+5f*Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f), 90f*(1 - AgentRot) + AgentRot * 20f * (float)actionX ,0);
            //this.transform.localEulerAngles = Direction_rot;
            //Agentrb.velocity = Direction_move;

            spine1.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f + 28f, spine1.transform.localEulerAngles.y, spine1.transform.localEulerAngles.z);
            spine2.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 15f, spine2.transform.localEulerAngles.y, spine2.transform.localEulerAngles.z);
            spine3.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 4f, spine3.transform.localEulerAngles.y, spine3.transform.localEulerAngles.z);

            leg1_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_l.transform.localEulerAngles.y, leg1_l.transform.localEulerAngles.z);
            leg2_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_l.transform.localEulerAngles.y, leg2_l.transform.localEulerAngles.z);
            leg3_l.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);
            leg1_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_r.transform.localEulerAngles.y, leg1_r.transform.localEulerAngles.z);
            leg2_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_r.transform.localEulerAngles.y, leg2_r.transform.localEulerAngles.z);
            leg3_r.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);

            human.transform.localPosition = new Vector3(updatex(moveX), -0.42f - 1.15f * (1 - Mathf.Cos(crouch * (Mathf.PI / 6))), updatez(moveZ));
            hand.transform.rotation = Quaternion.Euler(0f, 90f * AgentRot + 230f, 300f);
        }
        //optimize restrict
        if (Humanmodel == Humanmodel.Model1)
        {
            actionX = 2f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
            actionY = 2.5f + 2f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
            actionZ = 5.5f + 3.5f * Mathf.Clamp(actionBuffers.ContinuousActions[2], -1f, 1f);

            var moveX = moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[3], -1f, 1f);
            crouch = Mathf.Clamp((crouch + 0.04f * actionBuffers.ContinuousActions[4]), 0f, 1f);
            var moveZ = moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[5], -1f, 1f);

            bow = crouch;
            upperarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.x) + actionBuffers.ContinuousActions[6]), -30f, 60f),
                Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.y) + actionBuffers.ContinuousActions[7]), -60f, 30f), Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.z) + actionBuffers.ContinuousActions[8]), -60f, 0f));
            lowerarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((lowerarm.transform.localEulerAngles.x + actionBuffers.ContinuousActions[9]), 20f, 89.9f), -3.32f, -4.228f);

            spine1.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f + 28f, spine1.transform.localEulerAngles.y, spine1.transform.localEulerAngles.z);
            spine2.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 15f, spine2.transform.localEulerAngles.y, spine2.transform.localEulerAngles.z);
            spine3.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 4f, spine3.transform.localEulerAngles.y, spine3.transform.localEulerAngles.z);

            leg1_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_l.transform.localEulerAngles.y, leg1_l.transform.localEulerAngles.z);
            leg2_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_l.transform.localEulerAngles.y, leg2_l.transform.localEulerAngles.z);
            leg3_l.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);
            leg1_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_r.transform.localEulerAngles.y, leg1_r.transform.localEulerAngles.z);
            leg2_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_r.transform.localEulerAngles.y, leg2_r.transform.localEulerAngles.z);
            leg3_r.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);

            human.transform.localPosition = new Vector3(updatex(moveX), -0.42f - 1.15f * (1 - Mathf.Cos(crouch * (Mathf.PI / 6))), updatez(moveZ));
            hand.transform.rotation = Quaternion.Euler(0f, 90f * AgentRot + 230f, 300f);
        }
        //optimize hitting ball
        if (Humanmodel == Humanmodel.Model2)
        {
            actionX = 2f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
            actionY = 1.5f + 1f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
            actionZ = 5.5f + 3f * Mathf.Clamp(actionBuffers.ContinuousActions[2], -1f, 1f);

            var moveX = moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[3], -1f, 1f);
            crouch = Mathf.Clamp((crouch + 0.04f * actionBuffers.ContinuousActions[4]), 0f, 1f);
            var moveZ = moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[5], -1f, 1f);

            bow = crouch;
            upperarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.x) + actionBuffers.ContinuousActions[6]), -30f, 60f),
                Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.y) + actionBuffers.ContinuousActions[7]), -60f, 30f), Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.z) + actionBuffers.ContinuousActions[8]), -60f, 0f));
            lowerarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((lowerarm.transform.localEulerAngles.x + actionBuffers.ContinuousActions[9]), 20f, 89.9f), -3.32f, -4.228f);

            spine1.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f + 28f, spine1.transform.localEulerAngles.y, spine1.transform.localEulerAngles.z);
            spine2.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 15f, spine2.transform.localEulerAngles.y, spine2.transform.localEulerAngles.z);
            spine3.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 4f, spine3.transform.localEulerAngles.y, spine3.transform.localEulerAngles.z);

            leg1_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_l.transform.localEulerAngles.y, leg1_l.transform.localEulerAngles.z);
            leg2_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_l.transform.localEulerAngles.y, leg2_l.transform.localEulerAngles.z);
            leg3_l.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);
            leg1_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_r.transform.localEulerAngles.y, leg1_r.transform.localEulerAngles.z);
            leg2_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_r.transform.localEulerAngles.y, leg2_r.transform.localEulerAngles.z);
            leg3_r.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);

            human.transform.localPosition = new Vector3(updatex(moveX), -0.42f - 1.15f * (1 - Mathf.Cos(crouch * (Mathf.PI / 6))), updatez(moveZ));
            hand.transform.rotation = Quaternion.Euler(0f, 90f * AgentRot + 230f, 300f);
        }
        //reduce speed and improve hitting
        if (Humanmodel == Humanmodel.Model3)
        {
            actionX = 2.5f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
            actionY = 1.5f + 1f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
            actionZ = 5.5f + 4f * Mathf.Clamp(actionBuffers.ContinuousActions[2], -1f, 1f);

            var moveX = 0.6f * moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[3], -1f, 1f);
            crouch = Mathf.Clamp((crouch + 0.04f * actionBuffers.ContinuousActions[4]), 0f, 1f);
            var moveZ = 0.6f * moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[5], -1f, 1f);

            bow = crouch;
            upperarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.x) + actionBuffers.ContinuousActions[6]), -30f, 60f),
                Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.y) + actionBuffers.ContinuousActions[7]), -60f, 30f), Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.z) + actionBuffers.ContinuousActions[8]), -60f, 0f));
            lowerarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((lowerarm.transform.localEulerAngles.x + actionBuffers.ContinuousActions[9]), 20f, 89.9f), -3.32f, -4.228f);

            spine1.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f + 28f, spine1.transform.localEulerAngles.y, spine1.transform.localEulerAngles.z);
            spine2.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 15f, spine2.transform.localEulerAngles.y, spine2.transform.localEulerAngles.z);
            spine3.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 4f, spine3.transform.localEulerAngles.y, spine3.transform.localEulerAngles.z);

            leg1_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_l.transform.localEulerAngles.y, leg1_l.transform.localEulerAngles.z);
            leg2_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_l.transform.localEulerAngles.y, leg2_l.transform.localEulerAngles.z);
            leg3_l.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);
            leg1_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_r.transform.localEulerAngles.y, leg1_r.transform.localEulerAngles.z);
            leg2_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_r.transform.localEulerAngles.y, leg2_r.transform.localEulerAngles.z);
            leg3_r.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);

            human.transform.localPosition = new Vector3(updatex(moveX), -0.42f - 1.15f * (1 - Mathf.Cos(crouch * (Mathf.PI / 6))), updatez(moveZ));
            hand.transform.rotation = Quaternion.Euler(0f, 90f * AgentRot + 230f, 300f);
        }

        //increase speed and weaken hitting
        if (Humanmodel == Humanmodel.Model4)
        {
            actionX = 1.5f * Mathf.Clamp(actionBuffers.ContinuousActions[0], -1f, 1f);
            actionY = 1.5f + 1f * Mathf.Clamp(actionBuffers.ContinuousActions[1], -1f, 1f);
            actionZ = 5f + 2.5f * Mathf.Clamp(actionBuffers.ContinuousActions[2], -1f, 1f);

            var moveX = 1.5f * moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[3], -1f, 1f);
            crouch = Mathf.Clamp((crouch + 0.04f * actionBuffers.ContinuousActions[4]), 0f, 1f);
            var moveZ = 1.5f * moveSpeed * Mathf.Clamp(actionBuffers.ContinuousActions[5], -1f, 1f);

            bow = crouch;
            upperarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.x) + actionBuffers.ContinuousActions[6]), -30f, 60f),
                Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.y) + actionBuffers.ContinuousActions[7]), -60f, 30f), Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.z) + actionBuffers.ContinuousActions[8]), -60f, 0f));
            lowerarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((lowerarm.transform.localEulerAngles.x + actionBuffers.ContinuousActions[9]), 20f, 89.9f), -3.32f, -4.228f);

            spine1.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f + 28f, spine1.transform.localEulerAngles.y, spine1.transform.localEulerAngles.z);
            spine2.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 15f, spine2.transform.localEulerAngles.y, spine2.transform.localEulerAngles.z);
            spine3.transform.localRotation = Quaternion.Euler(0.8f * bow * 20f - 4f, spine3.transform.localEulerAngles.y, spine3.transform.localEulerAngles.z);

            leg1_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_l.transform.localEulerAngles.y, leg1_l.transform.localEulerAngles.z);
            leg2_l.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_l.transform.localEulerAngles.y, leg2_l.transform.localEulerAngles.z);
            leg3_l.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);
            leg1_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 30f + 348f), leg1_r.transform.localEulerAngles.y, leg1_r.transform.localEulerAngles.z);
            leg2_r.transform.localRotation = Quaternion.Euler(convert(0.8f * crouch * 60f + 8f), leg2_r.transform.localEulerAngles.y, leg2_r.transform.localEulerAngles.z);
            leg3_r.transform.localRotation = Quaternion.Euler(convert(-0.8f * crouch * 30f + 286f), 0.01f, 0.01f);

            human.transform.localPosition = new Vector3(updatex(moveX), -0.42f - 1.15f * (1 - Mathf.Cos(crouch * (Mathf.PI / 6))), updatez(moveZ));
            hand.transform.rotation = Quaternion.Euler(0f, 90f * AgentRot + 230f, 300f);
        }

    }

    public float CheckAngle(float value)  // 将大于180度角进行以负数形式输出
    {
        float angle = value - 180;

        if (angle > 0)
        {
            return angle - 180;
        }

        if (value == 0)
        {
            return 0;
        }

        return angle + 180;
    }

    private float convert(float angle)
    {
        if (angle > 360f)
            return (angle - 360f);
        else
            return angle;

    }
    private float updatez(float moveZ)
    {
        if (human.transform.localPosition.x < 0.41f && human.transform.localPosition.x > -1.59f)
        {
            if (AgentRot == 1)
            {
                //if (human.transform.localPosition.z < -1.93f)
                if (human.transform.localPosition.z < -zedge)
                {
                    return (human.transform.localPosition.z + (float)AgentRot * moveZ * Time.deltaTime);
                }
                else
                {
                    if(moveZ < 0)
                    {
                        return (human.transform.localPosition.z + (float)AgentRot * moveZ * Time.deltaTime);
                    }
                    else
                    {
                        return human.transform.localPosition.z;
                    }
                }
            }
            else
            {
                //if (human.transform.localPosition.z > 1.93f)
                if (human.transform.localPosition.z > zedge)
                {
                    return (human.transform.localPosition.z + (float)AgentRot * moveZ * Time.deltaTime);
                }
                else
                {
                    if (moveZ < 0)
                    {
                        return (human.transform.localPosition.z + (float)AgentRot * moveZ * Time.deltaTime);
                    }
                    else
                    {
                        return human.transform.localPosition.z;
                    }
                }
            }
        }
        else
        {
            return (human.transform.localPosition.z + (float)AgentRot * moveZ * Time.deltaTime);
        }
    }

    private float updatex(float moveX)
    {
        //if (human.transform.localPosition.z < 1.93f && human.transform.localPosition.z > -1.93f)
        if (human.transform.localPosition.z < zedge && human.transform.localPosition.z > -zedge)
        {
            if (human.transform.localPosition.x < -1.5f && human.transform.localPosition.x > -1.7f)
            {
                if (moveX < 0)
                {
                    if (AgentRot == 1)
                        return (human.transform.localPosition.x + (float)AgentRot * moveX * Time.deltaTime);
                    else
                    {
                        return human.transform.localPosition.x;
                    }
                }
                else
                {
                    if (AgentRot == -1)
                        return (human.transform.localPosition.x + (float)AgentRot * moveX * Time.deltaTime);
                    else
                    {
                        return human.transform.localPosition.x;
                    }
                }
            }
            else if (human.transform.localPosition.x < 0.5f && human.transform.localPosition.x > 0.3f)
            {
                if (moveX > 0)
                {
                    if (AgentRot == 1)
                        return (human.transform.localPosition.x + (float)AgentRot * moveX * Time.deltaTime);
                    else
                    {
                        return human.transform.localPosition.x;
                    }
                }
                else
                {
                    if (AgentRot == -1)
                        return (human.transform.localPosition.x + (float)AgentRot * moveX * Time.deltaTime);
                    else
                    {
                        return human.transform.localPosition.x;
                    }
                }
            }
            else
                return(human.transform.localPosition.x + (float)AgentRot * moveX * Time.deltaTime);
        }
        else
        {
            return (human.transform.localPosition.x + (float)AgentRot * moveX * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            //AddReward(0.5f);
            Direction = new Vector3((float)actionX * AgentRot, (float)actionY, (float)actionZ * AgentRot);
            Direction_rot = new Vector3(-10f + 5f * (float)actionY, 90f * (1 - AgentRot) + AgentRot * 20f * (float)actionX, 0);
            Ballrb.velocity = Direction;
            
            Vector3 startupperarm = upperarm.transform.localEulerAngles;
            
            StartCoroutine(Hitball(lowerarm, new Vector3(89f, -3.32f, -4.228f), 0.1f));
            StartCoroutine(Hitball(upperarm, new Vector3(59f, upperarm.transform.localEulerAngles.y, upperarm.transform.localEulerAngles.z), 0.2f));
            //StartCoroutine(RotateOverTime(lowerarm, startlowerarm, 0.3f));
            //StartCoroutine(RotateOverTime(upperarm, startupperarm, 0.3f));
            //StartCoroutine(RotateOverTime(lowerarm, lowerarm.transform.localEulerAngles, 0.5f));
            //StartCoroutine(RotateOverTime(upperarm, upperarm.transform.localEulerAngles, 1f));
            //Debug.Log(lowerarm.transform.localEulerAngles);
            //lowerarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((lowerarm.transform.localEulerAngles.x + 30f), 20f, 89.9f), -3.32f, -4.228f);
            //upperarm.transform.localRotation = Quaternion.Euler(Mathf.Clamp((CheckAngle(upperarm.transform.localEulerAngles.x) + 20f), -30f, 60f),
            //        upperarm.transform.localEulerAngles.y, upperarm.transform.localEulerAngles.z);
            //Debug.Log(lowerarm.transform.localEulerAngles);
            //this.transform.localEulerAngles = Direction_rot;
            //Debug.Log("coll ");
            //force = 0;
        }
        //Debug.Log("Direction of ball: " + Direction);
        //Debug.Log("Ball velocity: " + Ballrb.velocity);

    }

    private IEnumerator Hitball(GameObject obj, Vector3 targetEulerAngles, float duration)
    {
        Vector3 origin = obj.transform.localEulerAngles;
        // 首先旋转物体
        yield return RotateOverTime(obj, targetEulerAngles, duration);

        // 然后恢复原始旋转
        yield return RotateOverTime(obj, origin, duration * 1.5f);
    }

    private IEnumerator RotateOverTime(GameObject obj, Vector3 targetEulerAngles, float duration)
    {
        Quaternion startRotation = obj.transform.localRotation;
        Quaternion endRotation = Quaternion.Euler(targetEulerAngles);
        float time = 0f;

        while (time < duration)
        {
            obj.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        obj.transform.localRotation = endRotation; // 确保旋转完全完成
    }

}