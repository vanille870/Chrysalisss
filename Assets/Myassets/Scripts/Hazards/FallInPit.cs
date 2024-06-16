using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FallInPit : MonoBehaviour
{
    [Header("init refrences")]
    public GameObject playerGO;
    public GameObject playerVisibilityParent;
    public Quaternion originalPlayerRot;
    public MainCharAnimation playerAnim;
    public InputManager inputManagerScript;
    public CharacterController playerController;


    [SerializeField] CinemachineVirtualCamera cinemachineCam;



    [SerializeField] float SuckPower;
    [SerializeField] float RespawnSpeed;

    [PostitionToVector3]
    [SerializeField] Vector3[] fallTowardsTrans;

    [PostitionToVector3]
    [SerializeField] Vector3 RespawnPosition;

    public Transform RespawnGO;
    [SerializeField] Vector3 playerbaseRotation;

    private Vector3 newPos;
    private Vector3 FarthestFallPoint;
    private float Distance;
    private Vector3 PositionPlayerWhenFallen;




    [System.Serializable]
    public struct Timer
    {
        public float Duration;
        private float Clock;

        public Timer(float duration, float time = 0f)
        {
            Duration = duration;
            Clock = time;
        }

        public void SetClock()
        {
            Clock = Time.unscaledTime + Duration;
        }

        public bool IsFinished => Time.unscaledTime >= Clock;
    }

    [SerializeField]
    Timer RespawnTimer = new Timer();

    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") == false)
        {
            if (collider.CompareTag("Player_Dodging") == false)
            {
                print("rturned");
                return;
            }
        }

        if (playerGO == null)
        {
            playerGO = GameObject.Find("MainChar_Parent");
        }

        if (cinemachineCam == null)
        {
            cinemachineCam = GameObject.Find("Top_down_cam").GetComponent<CinemachineVirtualCamera>();
        }


        if (inputManagerScript == null)
        {
            inputManagerScript = GameObject.Find("GameManager").GetComponent<GameMaster>().inputManagerScript;
        }

        PositionPlayerWhenFallen = playerGO.transform.position;

        cinemachineCam.Follow = null;
        cinemachineCam.LookAt = null;

        inputManagerScript.DisableControls();
        RespawnTimer.SetClock();

        playerAnim.ToggleFallInPit();
        playerController.enabled = false;

        FindFarthestFallPoint();

        CustomGameLoop.UpdateLoopFunctionsSubscriber += RespawnUpdate;
        CustomGameLoop.UpdateLoopFunctionsSubscriber += SuckPlayerIn;

    }

    void RespawnUpdate()
    {
        if (RespawnTimer.IsFinished)
        {

            cinemachineCam.Follow = playerGO.transform;
            cinemachineCam.LookAt = playerGO.transform;

            playerAnim.ToggleFallInPit();

            CustomGameLoop.UpdateLoopFunctionsSubscriber -= RespawnUpdate;
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= SuckPlayerIn;
            CustomGameLoop.UpdateLoopFunctionsSubscriber += MovePlayerToRespawn;

            playerGO.transform.rotation = originalPlayerRot;
            playerGO.transform.position = PositionPlayerWhenFallen;
            playerVisibilityParent.SetActive(false);
        }
    }

    void SuckPlayerIn()
    {
        print("sucking");
        newPos = Vector3.MoveTowards(playerGO.transform.position, FarthestFallPoint, SuckPower);
        playerGO.transform.position = newPos;
    }

    void MovePlayerToRespawn()
    {
        newPos = Vector3.Lerp(playerGO.transform.position, RespawnPosition, RespawnSpeed);
        playerGO.transform.position = newPos;

        if ((playerGO.transform.position - RespawnPosition).sqrMagnitude < 0.002)
        {
            playerGO.transform.position = RespawnPosition;
            CustomGameLoop.UpdateLoopFunctionsSubscriber -= MovePlayerToRespawn;

            InputManager.EnableControls();
            playerController.enabled = true;
            playerVisibilityParent.SetActive(true);
        }
    }

    void FindFarthestFallPoint()
    {
        Vector3 PlayerPos = playerGO.transform.position;
        Distance = 0f;

        foreach (Vector3 position in fallTowardsTrans)
        {
            float tempDistance = (position - PlayerPos).sqrMagnitude;

            if (tempDistance > Distance)
            {
                Distance = tempDistance;
                FarthestFallPoint = position;
                print(FarthestFallPoint);
            }
        }
    }
}
