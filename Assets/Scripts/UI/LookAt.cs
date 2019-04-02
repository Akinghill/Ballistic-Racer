using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

    public MainMenu mainMenu;

    public Camera cameraP1;
    public Camera cameraP2;
    public Camera cameraP3;
    public Camera cameraP4;

    public GameObject garageP1;
    public GameObject garageP2;
    public GameObject garageP3;
    public GameObject garageP4;

    void Update () 
	{
        if (PlayerManager.numOfPlayers == 1)
        {
            OnePlayerSelect();
        }

        if (PlayerManager.numOfPlayers == 2)
        {
            OnePlayerSelect();
            TwoPlayerSelect();
        }

        if (PlayerManager.numOfPlayers == 3)
        {
            OnePlayerSelect();
            TwoPlayerSelect();
            ThreePlayerSelect();
        }

        if (PlayerManager.numOfPlayers == 4)
        {
            OnePlayerSelect();
            TwoPlayerSelect();
            ThreePlayerSelect();
            FourPlayerSelect();
        }
    }

    void OnePlayerSelect()
    {
        if (mainMenu.onePlayerShipSelect.activeInHierarchy)
        {
            garageP1.SetActive(true);
            Quaternion targetRotationP1 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P1").transform.position - cameraP1.transform.position);
            cameraP1.transform.rotation = Quaternion.Slerp(cameraP1.transform.rotation, targetRotationP1, 10 * Time.deltaTime);
        }
        else
        {
            garageP1.SetActive(false);
        }
    }

    void TwoPlayerSelect()
    {
        if (mainMenu.twoPlayerShipSelect.activeInHierarchy)
        {
            garageP1.SetActive(true);
            Quaternion targetRotationP1 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P1").transform.position - cameraP1.transform.position);
            cameraP1.transform.rotation = Quaternion.Slerp(cameraP1.transform.rotation, targetRotationP1, 10 * Time.deltaTime);

            garageP2.SetActive(true);
            Quaternion targetRotationP2 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P2").transform.position - cameraP2.transform.position);
            cameraP2.transform.rotation = Quaternion.Slerp(cameraP2.transform.rotation, targetRotationP2, 10 * Time.deltaTime);
        }
        else
        {
            garageP1.SetActive(false);
            garageP2.SetActive(false);
        }
    }

    void ThreePlayerSelect()
    {
        if (mainMenu.threePlayerShipSelect.activeInHierarchy)
        {
            garageP1.SetActive(true);
            Quaternion targetRotationP1 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P1").transform.position - cameraP1.transform.position);
            cameraP1.transform.rotation = Quaternion.Slerp(cameraP1.transform.rotation, targetRotationP1, 10 * Time.deltaTime);

            garageP2.SetActive(true);
            Quaternion targetRotationP2 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P2").transform.position - cameraP2.transform.position);
            cameraP2.transform.rotation = Quaternion.Slerp(cameraP2.transform.rotation, targetRotationP2, 10 * Time.deltaTime);

            garageP3.SetActive(true);
            Quaternion targetRotationP3 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P3").transform.position - cameraP3.transform.position);
            cameraP3.transform.rotation = Quaternion.Slerp(cameraP3.transform.rotation, targetRotationP3, 10 * Time.deltaTime);
        }
        else
        {
            garageP1.SetActive(false);
            garageP2.SetActive(false);
            garageP3.SetActive(false);
        }
    }

    void FourPlayerSelect()
    {
        if (mainMenu.fourPlayerShipSelect.activeInHierarchy)
        {
            garageP1.SetActive(true);
            Quaternion targetRotationP1 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P1").transform.position - cameraP1.transform.position);
            cameraP1.transform.rotation = Quaternion.Slerp(cameraP1.transform.rotation, targetRotationP1, 10 * Time.deltaTime);

            garageP2.SetActive(true);
            Quaternion targetRotationP2 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P2").transform.position - cameraP2.transform.position);
            cameraP2.transform.rotation = Quaternion.Slerp(cameraP2.transform.rotation, targetRotationP2, 10 * Time.deltaTime);

            garageP3.SetActive(true);
            Quaternion targetRotationP3 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P3").transform.position - cameraP3.transform.position);
            cameraP3.transform.rotation = Quaternion.Slerp(cameraP3.transform.rotation, targetRotationP3, 10 * Time.deltaTime);

            garageP4.SetActive(true);
            Quaternion targetRotationP4 = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Camera Look Point P4").transform.position - cameraP4.transform.position);
            cameraP4.transform.rotation = Quaternion.Slerp(cameraP4.transform.rotation, targetRotationP4, 10 * Time.deltaTime);
        }
        else
        {
            garageP1.SetActive(false);
            garageP2.SetActive(false);
            garageP3.SetActive(false);
            garageP4.SetActive(false);
        }
    }
}
