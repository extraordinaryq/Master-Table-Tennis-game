using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTennisBall : MonoBehaviour
{
    [SerializeField] private float InitialSpeed = 2; //2.5f for pong
    [SerializeField] private TableTennisEnvControl envController;
    //[SerializeField] private GameObject leftGoal;
    //[SerializeField] private GameObject rightGoal;
    private Vector3 Direction;
    [HideInInspector] private Vector3 StartingPosition;
    [HideInInspector] public PaddleState mPaddleState= PaddleState.NoPaddle;
    [HideInInspector] public AreaState mAreaState= AreaState.NoArea;

    public Rigidbody rb;
    //private Vector3 _vel;
    //[SerializeField]
    //private LineRenderer _lr;
    //[SerializeField]
    //[Range(10, 20)]
    //private int LinePoints = 10;
    //[SerializeField]
    //[Range(0.01f, 1.0f)]
    //private float TimeBetweenPoints = 0.1f;

    //private bool _isGhost;

    private void Awake()
    {
        //force = 120;
        StartingPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        rb = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {       

        Launch();
    }

    //private void Update()
    //{
    //    //Trajectory();
    //}

    //public Vector3 getStartPosisition()
    //{
    //    return StartingPosition;
    //}

    //public void Init(Vector3 velocity, bool isGhost)
    //{
    //    _isGhost = isGhost;
    //    rb.AddForce(velocity, ForceMode.Impulse);
    //}

    //private void Trajectory()
    //{
    //    _lr.enabled = true;
    //    _lr.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
    //    Vector3 startPosition = rb.position;
    //    Vector3 startVelocity = rb.velocity;
    //    int i = 0;
    //    _lr.SetPosition(i, startPosition);
    //    for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
    //    {
    //        i++;
    //        Vector3 point = startPosition + time * startVelocity;
    //        point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

    //        _lr.SetPosition(i, point);

    //    }
    //}


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("RightPaddle"))
        {
            //Debug.Log("Rightpaddle collided");
            if (mAreaState == AreaState.RightArea)
            {
                if (mPaddleState == PaddleState.RightPaddle)
                {
                    mPaddleState = PaddleState.NoPaddle;
                    mAreaState = AreaState.NoArea;
                    envController.Foul(TTFoul.RightPaddleFoul);
                    //envController.ResolveEvent(TTEvent.LeftPaddleGoal);
                    //Debug.Log("Left Goal");
                }
                else
                {

                    mPaddleState = PaddleState.RightPaddle;
                    envController.GetSuccess(TTGet.RightPaddleGet);
                    //mAreaState = AreaState.NoArea;
                }
            }
            else
            {
                mPaddleState = PaddleState.NoPaddle;
                mAreaState = AreaState.NoArea;
                envController.Foul(TTFoul.RightPaddleFoul);
                //envController.ResolveEvent(TTEvent.LeftPaddleGoal);
                //Debug.Log("Left Goal 2");
            }



        }
        if (collision.gameObject.CompareTag("LeftPaddle"))
        {
            //Debug.Log("Leftpaddle collided");
            if (mAreaState == AreaState.LeftArea)
            {
                if (mPaddleState == PaddleState.LeftPaddle)
                {
                    mPaddleState = PaddleState.NoPaddle;
                    mAreaState = AreaState.NoArea;
                    envController.Foul(TTFoul.LeftPaddleFoul);
                    //envController.ResolveEvent(TTEvent.RightPaddleGoal);
                    //Debug.Log("Right Goal");
                }
                else
                {

                    mPaddleState = PaddleState.LeftPaddle;
                    envController.GetSuccess(TTGet.LeftPaddleGet);
                    //mAreaState = AreaState.NoArea;
                }
            }
            else
            {
                mPaddleState = PaddleState.NoPaddle;
                mAreaState = AreaState.NoArea;
                //envController.ResolveEvent(TTEvent.RightPaddleGoal);
                envController.Foul(TTFoul.LeftPaddleFoul);
                //Debug.Log("Right Goal 2");
            }


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TableTennisTable"))
        {
            if (this.transform.localPosition.z > 0)
            {
                if (mPaddleState == PaddleState.LeftPaddle)
                {
                    if (mAreaState == AreaState.LeftArea)
                    {
                        envController.HitSuccess(TTHit.LeftPaddleHit);
                        mAreaState = AreaState.RightArea;
                    }
                    else if(mAreaState == AreaState.RightArea)
                    {
                        mPaddleState = PaddleState.NoPaddle;
                        mAreaState = AreaState.NoArea;
                        envController.ResolveEvent(TTEvent.LeftPaddleGoal);
                        //Debug.Log("Left Goal 3");
                    }
                    else
                    {
                        mAreaState = AreaState.RightArea;
                    }
                }
                else if (mPaddleState == PaddleState.RightPaddle)
                {
                    mPaddleState = PaddleState.NoPaddle;
                    mAreaState = AreaState.NoArea;
                    envController.Foul(TTFoul.RightPaddleFoul);
                    //Debug.Log("Left Goal 4");
                }
                else
                {

                    mAreaState = AreaState.RightArea;
                    //Debug.Log("wrong 1");
                }

            }
            if (this.transform.localPosition.z < 0)
            {
                if (mPaddleState == PaddleState.RightPaddle)
                {
                    if (mAreaState == AreaState.RightArea)
                    {
                        envController.HitSuccess(TTHit.RightPaddleHit);
                        mAreaState = AreaState.LeftArea;
                    }
                    else if (mAreaState == AreaState.LeftArea)
                    {
                        mPaddleState = PaddleState.NoPaddle;
                        mAreaState = AreaState.NoArea;
                        envController.ResolveEvent(TTEvent.RightPaddleGoal);
                        //Debug.Log("Right Goal 3");
                    }
                    else
                    {
                        mAreaState = AreaState.LeftArea;
                    }
                }
                else if (mPaddleState == PaddleState.LeftPaddle)
                {
                    mPaddleState = PaddleState.NoPaddle;
                    mAreaState = AreaState.NoArea;
                    envController.Foul(TTFoul.LeftPaddleFoul);
                    //Debug.Log("Right Goal 4");

                }
                else
                {
                    mAreaState = AreaState.LeftArea;
                    //Debug.Log("wrong 1");

                }

            }
        }

    }

    public void Launch()
    {
        transform.localPosition = StartingPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        float x;
        float z;
        if(Random.value < 0.5f)
        {
            z = 2;
            mPaddleState = PaddleState.LeftPaddle;
            x = Random.Range(-1f, 0f);
        }
        else
        {
            z = -2;
            mPaddleState = PaddleState.RightPaddle;
            x = Random.Range(0f, 1f);
        }
        
        Direction = new Vector3(x , 0, z);
        rb.AddForce(Direction * InitialSpeed);
    }

    
}
