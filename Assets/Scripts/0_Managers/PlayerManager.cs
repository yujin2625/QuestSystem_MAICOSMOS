using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }
    private void CheckSingleton()
    {
        if (_instance != null)
        {
            Destroy(_instance);
            return;
        }
        _instance = this;
    }
    #endregion

    #region 1. Property
    public GameObject Player
    {
        get
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("PlayerNotFound\nCheck if player has tag \"Player\"");
            }
            return player;
        }
    }
    public PlayerController PlayerController
    {
        get
        {
            PlayerController playerController;
            if (!Player.TryGetComponent<PlayerController>(out playerController))
            {
                Debug.LogError("PlayerControllerNotFound\nPlayerController Is Not At Root Player!!");
                playerController = Player.GetComponentInChildren<PlayerController>();
            }
            return playerController;
        }
    }


    #endregion




    private void Awake()
    {
        CheckSingleton();

    }



}
