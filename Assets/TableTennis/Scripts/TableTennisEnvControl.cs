using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TTEvent
{
    LeftPaddleGoal = 0,
    RightPaddleGoal = 1
}
public enum TTFoul
{
    LeftPaddleFoul = 0,
    RightPaddleFoul = 1
}
public enum TTHit
{
    LeftPaddleHit = 0,
    RightPaddleHit = 1
}
public enum TTGet
{
    LeftPaddleGet = 0,
    RightPaddleGet = 1
}
public enum PaddleState
{
    NoPaddle = 0,
    LeftPaddle = 1,
    RightPaddle = 2
}
public enum AreaState
{
    NoArea = 0,
    RightArea = 1,
    LeftArea = 2

}
public enum Rewardfunction
{
    PPO = 0,
    selfplay = 1,

}


public class TableTennisEnvControl : MonoBehaviour
{
    [SerializeField] private PaddleAi leftPaddleAgent;
    [SerializeField] private PaddleAi rightPaddleAgent;
    [SerializeField] private TableTennisBall ball;
    [SerializeField] private int maxEnvironmentSteps;
    [SerializeField] private Rewardfunction method;
    [SerializeField] private GameObject displayleft;
    [SerializeField] private GameObject displayright;
    public TextMesh leftText;
    public TextMesh rightText;
    private int leftnumber = 0;
    private int rightnumber = 0;
    //[SerializeField] private TrajectoryController _projection;
    private int resetTimer = 0;

    public void ResolveEvent(TTEvent triggerEvent)
    {
        switch (triggerEvent)
        {
            //Rewards for self-competitive play
            case TTEvent.LeftPaddleGoal:
                if (method == Rewardfunction.selfplay)
                {
                    leftPaddleAgent.AddReward(1.0f);
                    rightPaddleAgent.AddReward(-1.0f);
                }
                leftPaddleAgent.EndEpisode();
                rightPaddleAgent.EndEpisode();
                ResetScene();
                ShowAndHideObject(displayleft);
                updatelefttext();
                break;
            case TTEvent.RightPaddleGoal:
                if (method == Rewardfunction.selfplay)
                {
                    leftPaddleAgent.AddReward(-1.0f);
                    rightPaddleAgent.AddReward(1.0f);
                }
                leftPaddleAgent.EndEpisode();
                rightPaddleAgent.EndEpisode();
                ResetScene();
                ShowAndHideObject(displayright);
                updaterighttext();
                break;
        }
    }

    public void Foul(TTFoul triggerEvent)
    {
        switch (triggerEvent)
        {
            //Rewards for self-competitive play
            case TTFoul.LeftPaddleFoul:
                if (method == Rewardfunction.selfplay)
                {
                    leftPaddleAgent.AddReward(-1.0f);
                    rightPaddleAgent.AddReward(1.0f);
                }
                leftPaddleAgent.EndEpisode();
                rightPaddleAgent.EndEpisode();
                ResetScene();
                ShowAndHideObject(displayright);
                updaterighttext();
                break;
            case TTFoul.RightPaddleFoul:
                if (method == Rewardfunction.selfplay)
                {
                    leftPaddleAgent.AddReward(1.0f);
                    rightPaddleAgent.AddReward(-1.0f);
                }
                leftPaddleAgent.EndEpisode();
                rightPaddleAgent.EndEpisode();
                ResetScene();
                ShowAndHideObject(displayleft);
                updatelefttext();
                break;
        }

    }

    public void HitSuccess(TTHit triggerEvent)
    {
        if (method == Rewardfunction.PPO)
        {
            switch (triggerEvent)
            {
                case TTHit.LeftPaddleHit:
                    leftPaddleAgent.AddReward(1f);
                    //Debug.Log("HitSuccess");
                    break;
                case TTHit.RightPaddleHit:
                    rightPaddleAgent.AddReward(1f);
                    //Debug.Log("HitSuccess");
                    break;
            }
        }

    }
    public void GetSuccess(TTGet triggerEvent)
    {
        if (method == Rewardfunction.PPO)
        {
            switch (triggerEvent)
            {
                //Rewards for self-competitive play
                case TTGet.LeftPaddleGet:
                    leftPaddleAgent.AddReward(0.25f);
                    //Debug.Log("GetSuccess");
                    break;
                case TTGet.RightPaddleGet:
                    rightPaddleAgent.AddReward(0.25f);
                    //Debug.Log("GetSuccess");
                    break;
            }
        }

    }

    private void ShowAndHideObject(GameObject display)
    {
        if (display != null)
        {
            // 确保物体是可见的
            display.SetActive(true);

            // 开始协程来隐藏物体
            StartCoroutine(HideAfterDelay(display, 0.75f));
        }
    }

    IEnumerator HideAfterDelay(GameObject display, float delay)
    {
        // 等待指定的秒数
        yield return new WaitForSeconds(delay);

        // 隐藏物体
        display.SetActive(false);
    }

    private void updatelefttext()
    {
        leftnumber++;
        if (leftText != null)
        {
            leftText.text = leftnumber.ToString();
        }
    }

    private void updaterighttext()
    {
        rightnumber++;
        if (rightText != null)
        {
            rightText.text = rightnumber.ToString();
        }
    }

    public void ResetScene()
    {
        resetTimer = 0;
        //resetPaddle();
        ball.Launch();
    }

    private void resetPaddle()
    {
        leftPaddleAgent.transform.localPosition = leftPaddleAgent.StartingPosition;
        //leftPaddleAgent.OriginPoint.transform.localPosition = leftPaddleAgent.OriginStartingPosition;
        leftPaddleAgent.Agentrb.velocity = Vector3.zero;
        leftPaddleAgent.Agentrb.angularVelocity = Vector3.zero;
        //leftPaddleAgent.transform.localRotation = leftPaddleAgent.Rotated;
        //leftPaddleAgent.moving = false;
        rightPaddleAgent.transform.localPosition = rightPaddleAgent.StartingPosition;
        //rightPaddleAgent.OriginPoint.transform.localPosition = rightPaddleAgent.OriginStartingPosition;
        rightPaddleAgent.Agentrb.velocity = Vector3.zero;
        rightPaddleAgent.Agentrb.angularVelocity = Vector3.zero;
        //rightPaddleAgent.transform.localRotation = rightPaddleAgent.Rotated;
        //rightPaddleAgent.moving = false;

    }
    //private void ReorientatePaddle(PaddleAi Paddle)
    //{
    //    if (Paddle.Agentrb.angularVelocity != Vector3.zero)
    //    {
    //        Paddle.Agentrb.angularVelocity = Vector3.zero;
    //    }
    //    if (Paddle.transform.localRotation != Paddle.Rotated)
    //    {
    //        Paddle.transform.localRotation = Paddle.Rotated;
    //    }
    //}

    //private void Update()
    //{
    //    //_projection.SimulateTrajectory(ball, ball.transform.position, ball.rb.velocity);
    //}

    private void FixedUpdate()
    {
        //leftPaddleAgent.AddReward(0.0001f);
        //rightPaddleAgent.AddReward(0.0001f);
        resetTimer += 1;
        if (resetTimer >= 1000)
        {
            leftPaddleAgent.EpisodeInterrupted();
            rightPaddleAgent.EpisodeInterrupted();
            ResetScene();
        }

        //ReorientatePaddle(leftPaddleAgent);
        //ReorientatePaddle(rightPaddleAgent);

        if (ball.transform.localPosition.y < -0.5)
        {
            if (ball.mAreaState == AreaState.NoArea)
            {
                //if (ball.mPaddleState == PaddleState.LeftPaddle)
                //{
                //    ball.mPaddleState = PaddleState.NoPaddle;
                //    ball.mAreaState = AreaState.NoArea;
                //    ResolveEvent(TTEvent.RightPaddleGoal);
                //}
                //else if (ball.mPaddleState == PaddleState.RightPaddle)
                //{
                //    ball.mPaddleState = PaddleState.NoPaddle;
                //    ball.mAreaState = AreaState.NoArea;
                //    ResolveEvent(TTEvent.LeftPaddleGoal);
                //}
                //else
                //{
                //leftPaddleAgent.AddReward(-2f);
                //rightPaddleAgent.AddReward(-2f);
                leftPaddleAgent.EndEpisode();
                rightPaddleAgent.EndEpisode();
                Debug.Log("wrong");
                ResetScene();
                //}
            }
            else if (ball.mAreaState == AreaState.LeftArea)
            {
                if (ball.mPaddleState == PaddleState.LeftPaddle)
                {
                    ball.mPaddleState = PaddleState.NoPaddle;
                    ball.mAreaState = AreaState.NoArea;
                    Foul(TTFoul.LeftPaddleFoul);
                }
                else if (ball.mPaddleState == PaddleState.RightPaddle)
                {
                    ball.mPaddleState = PaddleState.NoPaddle;
                    ball.mAreaState = AreaState.NoArea;
                    ResolveEvent(TTEvent.RightPaddleGoal);
                }

            }
            else if (ball.mAreaState == AreaState.RightArea)
            {
                if (ball.mPaddleState == PaddleState.LeftPaddle)
                {
                    ball.mPaddleState = PaddleState.NoPaddle;
                    ball.mAreaState = AreaState.NoArea;
                    ResolveEvent(TTEvent.LeftPaddleGoal);
                }
                else if (ball.mPaddleState == PaddleState.RightPaddle)
                {
                    ball.mPaddleState = PaddleState.NoPaddle;
                    ball.mAreaState = AreaState.NoArea;
                    Foul(TTFoul.RightPaddleFoul);
                }
            }

        }
    }
}
