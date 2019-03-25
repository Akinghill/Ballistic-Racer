using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RacerSelection : MonoBehaviour
{
    private GameObject[] CharacterList;
    public int index;

    public static string selectedShip_P1;
    public static string selectedShip_P2;
    public static string selectedShip_P3;
    public static string selectedShip_P4;

    public int playerNumber;

    public Button[] selectionButtons;

    void Start()
    {
        //mainMenu = GetComponentInParent<MainMenu>();
        CharacterList = new GameObject[transform.childCount];

        // Create an array that holds the number of racers.
        // A new place is created for each gameObject that is attached to CharacterList.

        for (int i = 0; i < transform.childCount; i++)
            CharacterList[i] = transform.GetChild(i).gameObject;

        //Deactivate the renders.
        foreach (GameObject go in CharacterList)
        {
            go.SetActive(false);
        }

        // Activate the render for the first index.

        if (CharacterList[0])
            CharacterList[0].SetActive(true);

    }

    void Update()
    {
        if (MainMenu.p1_Confirm)
        {
            if (selectedShip_P1 == CharacterList[index].name && playerNumber != 1)
            {
                //CharacterList[MPManager.mpManager.pOne].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
        if (MainMenu.p2_Confirm)
        {
            if (selectedShip_P2 == CharacterList[index].name && playerNumber != 2)
            {
                //CharacterList[MPManager.mpManager.pTwo].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
        if (MainMenu.p3_Confirm)
        {
            if (selectedShip_P3 == CharacterList[index].name && playerNumber != 3)
            {
                //CharacterList[MPManager.mpManager.pThree].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
        if (MainMenu.p4_Confirm)
        {
            if (selectedShip_P4 == CharacterList[index].name && playerNumber != 4)
            {
                //CharacterList[MPManager.mpManager.pFour].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
    }

    public void ToggleLeft()
    {
        // Toggle off current model
        CharacterList[index].SetActive(false);

        index--;
        if (MainMenu.p1_Confirm)
        {
            if (selectedShip_P1 == CharacterList[index].name)
            {
                index--;
            }
        }
        if (MainMenu.p2_Confirm)
        {
            if (selectedShip_P2 == CharacterList[index].name)
            {
                index--;
            }
        }
        if (MainMenu.p3_Confirm)
        {
            if (selectedShip_P3 == CharacterList[index].name)
            {
                index--;
            }
        }
        if (MainMenu.p4_Confirm)
        {
            if (selectedShip_P4 == CharacterList[index].name)
            {
                index--;
            }
        }
        if (index < 0)
        {
            index = CharacterList.Length - 1;
        }
        //Toggle on current model
        CharacterList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        // Toggle off current model
        CharacterList[index].SetActive(false);

        index++;
        if (MainMenu.p1_Confirm)
        {
            if (selectedShip_P1 == CharacterList[index].name)
            {
                index++;
            }
        }
        if (MainMenu.p2_Confirm)
        {
            if (selectedShip_P2 == CharacterList[index].name)
            {
                index++;
            }
        }
        if (MainMenu.p3_Confirm)
        {
            if (selectedShip_P3 == CharacterList[index].name)
            {
                index++;
            }
        }
        if (MainMenu.p4_Confirm)
        {
            if (selectedShip_P4 == CharacterList[index].name)
            {
                index++;
            }
        }
        if (index == CharacterList.Length)
        {
            index = 0;
        }
        //Toggle on current model
        CharacterList[index].SetActive(true);
    }
    public void Confirm()
    {
        foreach (Button button in selectionButtons)
        {
            button.interactable = false;
        }
        if (playerNumber == 1)
        {
            selectedShip_P1 = CharacterList[index].name;
        }
        if (playerNumber == 2)
        {
            selectedShip_P2 = CharacterList[index].name;
        }
        if (playerNumber == 3)
        {
            selectedShip_P3 = CharacterList[index].name;
        }
        if (playerNumber == 4)
        {
            selectedShip_P4 = CharacterList[index].name;
        }
        //selectedShip_P1 = CharacterList[index].name;
        //SceneManager.LoadScene("Track_1");
    }

    public void Back()
    {
        foreach (Button button in selectionButtons)
        {
            button.interactable = true;
        }
        CharacterList[index].SetActive(false);
        index = 0;
        CharacterList[index].SetActive(true);
    }
}