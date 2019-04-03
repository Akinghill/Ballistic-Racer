using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RacerSelection : MonoBehaviour
{
    public GameObject[] CharacterList;
    public GameObject easterEggShip;
    public int index;

    public static string selectedShip_P1;
    public static string selectedShip_P2;
    public static string selectedShip_P3;
    public static string selectedShip_P4;

    public int playerNumber;

    public Button[] selectionButtons;

    int pOne, pTwo, pThree, pFour;

    void Start()
    {
        //Deactivate the renders.
        foreach (GameObject go in CharacterList)
        {
            go.SetActive(false);
        }

        easterEggShip.SetActive(false);

        // Activate the render for the first index.

        if (CharacterList[0])
            CharacterList[0].SetActive(true);

    }

    void Update()
    {
        if (MainMenu.p1_Confirm)
        {
            if (pOne == index && playerNumber != 1)
            {
                CharacterList[pOne].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
        else if (playerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                CharacterList[index].SetActive(false);
                easterEggShip.SetActive(true);
            }
        }
        if (MainMenu.p2_Confirm)
        {
            if (pTwo == index && playerNumber != 2)
            {
                CharacterList[pTwo].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
        if (MainMenu.p3_Confirm)
        {
            if (pThree == index && playerNumber != 3)
            {
                CharacterList[pThree].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
        if (MainMenu.p4_Confirm)
        {
            if (pFour == index && playerNumber != 4)
            {
                CharacterList[pFour].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }
    }

    public void ToggleLeft()
    {
        if (easterEggShip.activeInHierarchy)
        {
            easterEggShip.SetActive(false);
        }
        // Toggle off current model
        CharacterList[index].SetActive(false);

        index--;
        if (MainMenu.p1_Confirm)
        {
            if (pOne == index)
            {
                index--;
            }
        }
        if (MainMenu.p2_Confirm)
        {
            if (pTwo == index)
            {
                index--;
            }
        }
        if (MainMenu.p3_Confirm)
        {
            if (pThree == index)
            {
                index--;
            }
        }
        if (MainMenu.p4_Confirm)
        {
            if (pFour == index)
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
        if (easterEggShip.activeInHierarchy)
        {
            easterEggShip.SetActive(false);
        }
        // Toggle off current model
        CharacterList[index].SetActive(false);

        index++;
        if (MainMenu.p1_Confirm)
        {
            if (pOne == index)
            {
                index++;
            }
        }
        if (MainMenu.p2_Confirm)
        {
            if (pTwo == index)
            {
                index++;
            }
        }
        if (MainMenu.p3_Confirm)
        {
            if (pThree == index)
            {
                index++;
            }
        }
        if (MainMenu.p4_Confirm)
        {
            if (pFour == index)
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
            if (easterEggShip.activeInHierarchy)
            {
                selectedShip_P1 = easterEggShip.name;
            }
            else
            {
                selectedShip_P1 = CharacterList[index].name;
                pOne = index;
            }
        }
        if (playerNumber == 2)
        {
            selectedShip_P2 = CharacterList[index].name;
            pTwo = index;
        }
        if (playerNumber == 3)
        {
            selectedShip_P3 = CharacterList[index].name;
            pThree = index;
        }
        if (playerNumber == 4)
        {
            selectedShip_P4 = CharacterList[index].name;
            pFour = index;
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