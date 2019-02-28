using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShips : MonoBehaviour {

    public GameObject[] startPositions;
    public GameObject[] ships;
    public GameObject minimap_1P;
    public GameObject minimap_MP;

    private string selectedRacer_P1;
    private string selectedRacer_P2;
    private string selectedRacer_P3;
    private string selectedRacer_P4;

    private GameObject[] spawnOrder;
    private GameObject[] shipsInstance;

    private GameObject ship_P1;
    private GameObject ship_P2;
    private GameObject ship_P3;
    private GameObject ship_P4;

    private Camera cam_P1;
    private Camera cam_P2;
    private Camera cam_P3;
    private Camera cam_P4;

    private bool keyboard_P1;
    private bool keyboard_P2;
    private bool keyboard_P3;
    private bool keyboard_P4;

    private PlayerHealth[] playerHealth;

    void Awake()
    {
        shipsInstance = new GameObject[ships.Length];
        playerHealth = new PlayerHealth[ships.Length];
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 14;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 14;
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

                    shipsInstance[i].GetComponentInChildren<Camera>().gameObject.SetActive(false);
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 14;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 14;
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 15;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 15;
                    }
                }
                else
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 0;

                    shipsInstance[i].GetComponentInChildren<Camera>().gameObject.SetActive(false);
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 14;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 14;
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 15;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 15;
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 16;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 16;
                    }
                }
                else
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 0;

                    shipsInstance[i].GetComponentInChildren<Camera>().gameObject.SetActive(false);
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 14;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 14;
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 15;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 15;
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 16;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 16;
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

                    shipsInstance[i].GetComponentInChildren(typeof(Cinemachine.CinemachineMixingCamera)).gameObject.layer = 17;
                    Component[] vCams = shipsInstance[i].GetComponentsInChildren(typeof(Cinemachine.CinemachineVirtualCamera));

                    foreach (Cinemachine.CinemachineVirtualCamera vCam in vCams)
                    {
                        vCam.gameObject.layer = 17;
                    }
                }
                else
                {
                    shipsInstance[i] = Instantiate(ships[i]) as GameObject;
                    shipsInstance[i].GetComponent<Transform>().position = startPositions[i].GetComponent<Transform>().position;

                    playerHealth[i] = shipsInstance[i].GetComponentInChildren<PlayerHealth>();
                    playerHealth[i].respawnPoint = startPositions[i].transform;

                    shipsInstance[i].GetComponent<PlayerInput>().controllerNumber = 0;

                    shipsInstance[i].GetComponentInChildren<Camera>().gameObject.SetActive(false);
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
