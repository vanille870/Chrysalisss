using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class Slime_AI : MonoBehaviour
{
    public enum EnemyState
    {
        Idling = 0,
        chasing,
        WaitBeforeReturning,
    }

    public CharacterController playerController;
    public NavMeshAgent slimeAgent;
    public Transform PlayerPoint;
    public Transform visionPoint;
    public Vector3 playerLastSeenLocation;
    public Vector3 playerLastSeenVelocity;
    Vector3 spawnPoint;
    Vector3 idleWaypointFinderRaycastPoint;
    public Animator slimAnimator;
    public float FOVAngle;
    public int EnemyFOV;
    public int SpottingDistance = 50;
    public float DistanceToPlayer = 50;
    public float IdleRadius;

    public bool seenPlayer = false;
    public bool isTracking;
    public bool enemyIsRetunring;

    public LayerMask DetectionLayerMask;
    public RaycastHit enemyLineOfSightRaycast;
    public RaycastHit idleWaypointRaycast;

    [System.Serializable]
    public struct TimedEvent
    {
        [SerializeField]
        private float Duration;
        private float Clock;

        public TimedEvent(float duration, float time = 0f)
        {
            Duration = duration;
            Clock = time;
        }

        public void SetClock()
        {
            Clock = Time.time + Duration;
        }

        public bool IsFinished => Time.time >= Clock;
    }

    [Header("Timers")]
    [SerializeField]
    private TimedEvent ChaseTimer = new TimedEvent();
    [SerializeField]
    private TimedEvent WaitBeforeReturnTimer = new TimedEvent();
    [SerializeField]
    private TimedEvent IdleWayPointTimer = new TimedEvent();

    public EnemyState currentEnemyState = EnemyState.Idling;
    private System.Action[] runCurrentEnemyState = null;



    public Vector3 trackingDestination;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPoint = GameObject.Find("PlayerPoint").GetComponent<Transform>();
        spawnPoint = gameObject.transform.position;

        runCurrentEnemyState = new System.Action[]
        {
            IdleWander,
            ChaseAndTrackPlayer,
            ReturnToSpawn,
        };


    }

    // Update is called once per frame
    void Update()
    {

        DetectPlayer();

        runCurrentEnemyState[(int)currentEnemyState]();

        Color PlayerInConeVision = (FOVAngle < EnemyFOV) ? Color.green : Color.red;
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, (PlayerPoint.position - transform.position).normalized, PlayerInConeVision);

    }

    void DetectPlayer()
    {
        DistanceToPlayer = Vector3.Distance(PlayerPoint.position, transform.position);
        FOVAngle = Vector3.Angle(transform.forward, (PlayerPoint.position - transform.position).normalized);

        if (FOVAngle < EnemyFOV)
        {
            Physics.Raycast(visionPoint.position, PlayerPoint.position - transform.position, out enemyLineOfSightRaycast, 20, DetectionLayerMask);

            if (enemyLineOfSightRaycast.collider != null && DistanceToPlayer < SpottingDistance)
            {
                if (enemyLineOfSightRaycast.collider.tag == "Player")
                {
                    Debug.DrawRay(visionPoint.position, PlayerPoint.position - transform.position, Color.green);
                    currentEnemyState = EnemyState.chasing;
                    ChaseTimer.SetClock();
                    seenPlayer = true;
                }

                if (enemyLineOfSightRaycast.collider.tag == "Solid_Object")
                {

                    Debug.DrawRay(visionPoint.position, PlayerPoint.position - transform.position, Color.red);
                    seenPlayer = false;

                    if (ChaseTimer.IsFinished && currentEnemyState == EnemyState.chasing)
                    {
                        currentEnemyState = EnemyState.WaitBeforeReturning;
                        slimeAgent.autoBraking = true;
                        WaitBeforeReturnTimer.SetClock();
                    }
                }
            }

            else
            {
                seenPlayer = false;

                if (ChaseTimer.IsFinished && currentEnemyState == EnemyState.chasing)
                {
                    currentEnemyState = EnemyState.WaitBeforeReturning;
                    slimeAgent.autoBraking = true;
                    WaitBeforeReturnTimer.SetClock();

                    playerLastSeenLocation += playerLastSeenVelocity;

                    PassDestinationAndCheckIfAiAgenIsActive(playerLastSeenLocation);

                }
            }
        }

        else
        {
            seenPlayer = false;

            if (ChaseTimer.IsFinished && currentEnemyState == EnemyState.chasing)
            {
                currentEnemyState = EnemyState.WaitBeforeReturning;
                slimeAgent.autoBraking = true;
                WaitBeforeReturnTimer.SetClock();

                playerLastSeenLocation += playerLastSeenVelocity;

                PassDestinationAndCheckIfAiAgenIsActive(playerLastSeenLocation);

            }
        }
    }

    void ChaseAndTrackPlayer()
    {
        if (seenPlayer == true)
        {
            //constantly updates position
            PassDestinationAndCheckIfAiAgenIsActive(playerLastSeenLocation);
            playerLastSeenVelocity = playerController.velocity;
            playerLastSeenLocation = PlayerPoint.position;
            isTracking = false;
            slimeAgent.autoBraking = false;
        }

        else
            slimeAgent.autoBraking = true;

        if (slimeAgent)
        {
            if (seenPlayer == false && slimeAgent.remainingDistance < 0.1f)
            {
                isTracking = true;
            }
        }

        if (isTracking)
        {
            playerLastSeenLocation += playerLastSeenVelocity;

            PassDestinationAndCheckIfAiAgenIsActive(playerLastSeenLocation);
            print("we do a little tracking");
        }
    }

    void IdleWander()
    {
        if (IdleWayPointTimer.IsFinished)
        {
            MakeNewWaypoint();
            IdleWayPointTimer.SetClock();
        }

    }

    void ReturnToSpawn()
    {


        if (WaitBeforeReturnTimer.IsFinished)
        {
            PassDestinationAndCheckIfAiAgenIsActive(spawnPoint);
            enemyIsRetunring = true;

            if (slimeAgent.isActiveAndEnabled)
            {
                if (slimeAgent.remainingDistance < 0.2)
                {
                    enemyIsRetunring = false;
                    currentEnemyState = EnemyState.Idling;
                    IdleWayPointTimer.SetClock();
                }
            }
        }
    }

    void MakeNewWaypoint()
    {
        idleWaypointFinderRaycastPoint = new Vector3(spawnPoint.x + (Random.insideUnitCircle.x * IdleRadius), spawnPoint.y, spawnPoint.z + (Random.insideUnitCircle.y * IdleRadius));
        Physics.Raycast(idleWaypointFinderRaycastPoint, Vector3.down, out idleWaypointRaycast);
        slimeAgent.SetDestination(idleWaypointRaycast.point);
    }

    void PassDestinationAndCheckIfAiAgenIsActive(Vector3 destination)
    {
        if (slimeAgent.isActiveAndEnabled)
            slimeAgent.destination = destination;
    }
}
