using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RacerSelection : MonoBehaviour
{
    public GameObject[] CharacterList;
    public bool easterEggShipActivated;
    public int index;

    public static string selectedShip_P1;
    public static string selectedShip_P2;
    public static string selectedShip_P3;
    public static string selectedShip_P4;

    public int playerNumber;

    public Button[] selectionButtons;

    void Start()
    {
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
        if (playerNumber == 1 && !MainMenu.p1_Confirm)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                easterEggShipActivated = true;
            }
        }

        if (MainMenu.p1_Confirm)
        {
            if (MainMenu.pOne == index && playerNumber != 1)
            {
                CharacterList[MainMenu.pOne].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }

        if (MainMenu.p2_Confirm)
        {
            if (MainMenu.pTwo == index && playerNumber != 2)
            {
                CharacterList[MainMenu.pTwo].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }

        if (MainMenu.p3_Confirm)
        {
            if (MainMenu.pThree == index && playerNumber != 3)
            {
                CharacterList[MainMenu.pThree].SetActive(false);
                index++;
                CharacterList[index].SetActive(true);
            }
        }

        if (MainMenu.p4_Confirm)
        {
            if (MainMenu.pFour == index && playerNumber != 4)
            {
                CharacterList[MainMenu.pFour].SetActive(false);
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
            if (MainMenu.pOne == index)
            {
                ToggleLeft();
            }
        }

        if (MainMenu.p2_Confirm)
        {
            if (MainMenu.pTwo == index)
            {
                ToggleLeft();
            }
        }

        if (MainMenu.p3_Confirm)
        {
            if (MainMenu.pThree == index)
            {
                ToggleLeft();
            }
        }

        if (MainMenu.p4_Confirm)
        {
            if (MainMenu.pFour == index)
            {
                ToggleLeft();
            }
        }

        if (index < 0)
        {
            index = CharacterList.Length - 1;
        }

        if (CharacterList[index].name == "Lego_Ship")
        {
            if (!easterEggShipActivated)
            {
                ToggleLeft();
            }
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
            if (MainMenu.pOne == index)
            {
                ToggleRight();
            }
        }

        if (MainMenu.p2_Confirm)
        {
            if (MainMenu.pTwo == index)
            {
                ToggleRight();
            }
        }

        if (MainMenu.p3_Confirm)
        {
            if (MainMenu.pThree == index)
            {
                ToggleRight();
            }
        }

        if (MainMenu.p4_Confirm)
        {
            if (MainMenu.pFour == index)
            {
                ToggleRight();
            }
        }

        if (index == CharacterList.Length)
        {
            index = 0;
        }

        if (CharacterList[index].name == "Lego_Ship")
        {
            if (!easterEggShipActivated)
            {
                ToggleRight();
            }
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
            MainMenu.pOne = index;
        }

        if (playerNumber == 2)
        {
            selectedShip_P2 = CharacterList[index].name;
            MainMenu.pTwo = index;
        }

        if (playerNumber == 3)
        {
            selectedShip_P3 = CharacterList[index].name;
            MainMenu.pThree = index;
        }

        if (playerNumber == 4)
        {
            selectedShip_P4 = CharacterList[index].name;
            MainMenu.pFour = index;
        }
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

        easterEggShipActivated = false;

        MainMenu.pOne = 0;
        MainMenu.pTwo = 0;
        MainMenu.pThree = 0;
        MainMenu.pFour = 0;
    }
}