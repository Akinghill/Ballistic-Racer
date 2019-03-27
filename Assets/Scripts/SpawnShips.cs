using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShips : MonoBehaviour {

    public GameObject[] startPositions;
    public GameObject[] ships;
    public GameObject minimap_1P;
    public GameObject minimap_MP;

    string selectedRacer_P1;
    string selectedRacer_P2;
    string selectedRacer_P3;
    string selectedRacer_P4;

    GameObject[] spawnOrder;
    GameObject[] shipsInstance;

    GameObject ship_P1;
    GameObject ship_P2;
    GameObject ship_P3;
    GameObject ship_P4;

    Camera cam_P1;
    Camera cam_P2;
    Camera cam_P3;
    Camera cam_P4;

    bool keyboard_P1;
    bool keyboard_P2;
    bool keyboard_P3;
    bool keyboard_P4;

    PlayerHealth[] playerHealth;

    int playerOneLayer;
    int playerTwoLayer;
    int playerThreeLayer;
    int playerFourLayer;

    void Awake()
    {
        shipsInstance = new GameObject[ships.Length];
        playerHealth = new PlayerHealth[ships.Length];

        playerOneLayer = LayerMask.NameToLayer("Player1");
        playerTwoLayer = LayerMask.NameToLayer("Player2");
        playerThreeLayer = LayerMask.NameToLayer("Player3");
        playerFourLayer = LayerMask.NameToLayer("Player4");
    }

    void Start()
    {
        selectedRacer_P1 = RacerSelection.selectedShip_P1;
        selectedRacer_P2 = RacerSelection.selectedShip_P2;
        selectedRacer_P3 = RacerSelection.selectedShip_P3;
        selectedRacer_P4 = RacerSelection.selectedShip_P4;
        //Debug.Log(selectedRacer_P1);
        //Debug.Log(selectedRacer_P2);
        //Debug.Log(selectedRacer_P3);
        //Debug.Log(selectedRacer_P4);

        keyboard_P1 = MainMenu.oneKeyboard;
        keyboard_P2 = MainMenu.twoKeyboard;
        keyboard_P3 = MainMenu.threeKeyboard;
        keyboard_P4 = MainMenu.fourKeyboard;

        //spawnOrder = new GameObject[ships.Length];

        if (PlayerManager.numOfPlayers == 1)
        {
            minimap_1P = Instantiate(minimap_1P) as GameObject;
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].name == selectedRacer_P1)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 1;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P1;
                    cam_P1 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    PlayerOneCullingMask(cam_P1);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerOneLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerOneLayer;
                    }
                }
                else
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    //shipsInstance[i].GetComponentInChildren<PlayerHealth>().respawnPoint.transform.position = startPositions[i].transform.position;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 0;

                    //Disables all child cameras
                    Component[] Cameras;
                    Cameras = shipsInstance[i].GetComponentsInChildren<Camera>();
                    foreach (Camera cam in Cameras) { cam.gameObject.SetActive(false); }

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.SetActive(false);
                }
            }
        }
        else if (PlayerManager.numOfPlayers == 2)
        {
            minimap_MP = Instantiate(minimap_MP) as GameObject;
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].name == selectedRacer_P1)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 1;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P1;
                    cam_P1 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P1.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
                    PlayerOneCullingMask(cam_P1);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerOneLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerOneLayer;
                    }
                }
                else if (ships[i].name == selectedRacer_P2)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 2;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P2;
                    cam_P2 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P2.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
                    PlayerTwoCullingMask(cam_P2);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerTwoLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerTwoLayer;
                    }
                }
                else
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 0;

                    //Disables all child cameras
                    Component[] Cameras;
                    Cameras = shipsInstance[i].GetComponentsInChildren<Camera>();
                    foreach(Camera cam in Cameras) { cam.gameObject.SetActive(false); }
                
                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.SetActive(false);
                }
            }
        }
        else if (PlayerManager.numOfPlayers == 3)
        {
            minimap_MP = Instantiate(minimap_MP) as GameObject;
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].name == selectedRacer_P1)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 1;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P1;
                    cam_P1 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P1.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                    PlayerOneCullingMask(cam_P1);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerOneLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerOneLayer;
                    }
                }
                else if (ships[i].name == selectedRacer_P2)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 2;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P2;
                    cam_P2 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    PlayerTwoCullingMask(cam_P2);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerTwoLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerTwoLayer;
                    }
                }
                else if (ships[i].name == selectedRacer_P3)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 3;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P3;
                    cam_P3 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P3.rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
                    PlayerThreeCullingMask(cam_P3);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerThreeLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerThreeLayer;
                    }
                }
                else
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 0;

                    //Disables all child cameras
                    Component[] Cameras;
                    Cameras = shipsInstance[i].GetComponentsInChildren<Camera>();
                    foreach (Camera cam in Cameras) { cam.gameObject.SetActive(false); }

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.SetActive(false);
                }
            }
        }
        else if (PlayerManager.numOfPlayers == 4)
        {
            minimap_MP = Instantiate(minimap_MP) as GameObject;
            for (int i = 0; i < ships.Length; i++)
            {
                if (ships[i].name == selectedRacer_P1)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 1;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P1;
                    cam_P1 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P1.rect = new Rect(0.0f, 0.5f, 0.5f, 0.5f);
                    PlayerOneCullingMask(cam_P1);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerOneLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerOneLayer;
                    }
                }
                else if (ships[i].name == selectedRacer_P2)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 2;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P2;
                    cam_P2 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    PlayerTwoCullingMask(cam_P2);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerTwoLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerTwoLayer;
                    }
                }
                else if (ships[i].name == selectedRacer_P3)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 3;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P3;
                    cam_P3 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P3.rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
                    PlayerThreeCullingMask(cam_P3);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerThreeLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerThreeLayer;
                    }
                }
                else if (ships[i].name == selectedRacer_P4)
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 4;
                    shipsInstance[i].GetComponent<PlayerInput>().usingKeyboard = keyboard_P4;
                    cam_P4 = shipsInstance[i].GetComponentInChildren<Camera>();

                    cam_P4.rect = new Rect(0.5f, 0.0f, 0.5f, 0.5f);
                    PlayerFourCullingMask(cam_P4);

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = playerFourLayer;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = playerFourLayer;
                    }
                }
                else
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 0;

                    //Disables all child cameras
                    Component[] Cameras;
                    Cameras = shipsInstance[i].GetComponentsInChildren<Camera>();
                    foreach (Camera cam in Cameras) { cam.gameObject.SetActive(false); }

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.SetActive(false);
                }
            }
        }
    }


    // Set the culling masks for each camera to everything, and then disable the minimap, ricochet, and the other player's layers
    void PlayerOneCullingMask(Camera cam)
    {
        cam.cullingMask = -1;
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Minimap"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Ricochet"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player2"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player3"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player4"));
    }

    void PlayerTwoCullingMask(Camera cam)
    {
        cam.cullingMask = -1;
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Minimap"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Ricochet"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player1"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player3"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player4"));
    }

    void PlayerThreeCullingMask(Camera cam)
    {
        cam.cullingMask = -1;
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Minimap"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Ricochet"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player1"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player2"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player4"));
    }

    void PlayerFourCullingMask(Camera cam)
    {
        cam.cullingMask = -1;
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Minimap"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Ricochet"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player1"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player2"));
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Player3"));
    }
}
