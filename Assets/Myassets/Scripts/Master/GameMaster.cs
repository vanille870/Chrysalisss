using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gameMasterSingleton { get; private set; }

    public CharacterController playerController;
    public Transform playerTrans;



    private void Awake()
    {
        //ensures there's only 1 instance
        if (gameMasterSingleton != null && gameMasterSingleton != this)
        {
            Destroy(this);
        }

        else
        {
            gameMasterSingleton = this;
        }

        gameMasterSingleton.playerController = playerController;
        gameMasterSingleton.playerTrans = playerTrans;
    }

    public void MovePlayer(Vector3 TargetPosition)
    {
        gameMasterSingleton.playerTrans.position = TargetPosition;
        print("teleport");
    }
}
