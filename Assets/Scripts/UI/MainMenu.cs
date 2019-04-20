using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public enum MenuStates { MainMenu, Credits, PlayerSelect, TrackSelect, ShipSelect, Options };
    public MenuStates currentState;
    public GameObject mainUI;
    public GameObject creditUI;
    public GameObject optionsUI;
    public GameObject playerSelectUI;
    public GameObject trackSelectUI;

    public GameObject onePlayerShipSelect;
    public GameObject twoPlayerShipSelect;
    public GameObject threePlayerShipSelect;
    public GameObject fourPlayerShipSelect;
    //AudioSource audioData;
    public static string level;

    public PlayerManager playerManager;

    public Text[] pOneText;
    public Text[] pTwoText;
    public Text[] pThreeText;
    public Text pFourText;

    public static bool p1_Confirm, p2_Confirm, p3_Confirm, p4_Confirm;

    public static bool oneKeyboard, twoKeyboard, threeKeyboard, fourKeyboard;

    public static int pOne, pTwo, pThree, pFour;

    public bool isShipSelected;

    GamePadState m_P1State;
    //GamePadState m_P2State;
    //GamePadState m_P3State;
    //GamePadState m_P4State;

    GamePadState m_P1PrevState;
    //GamePadState m_P2PrevState;
    //GamePadState m_P3PrevState;
    //GamePadState m_P4PrevState;

    public GameObject gamepadCursor;

    void Start()
    {
        Reset();
       // audioData = GetComponent<AudioSource>();
        //audioData.Play(0);
        //Debug.Log("started");
        StartCoroutine(LoadScene());
    }

    void Update()
    {
        // Set the previous state for 1-4 player controllers to the state from the last frame
        m_P1PrevState = m_P1State;
        //m_P2PrevState = m_P2State;
        //m_P3PrevState = m_P3State;
        //m_P4PrevState = m_P4State;

        // Get gamepad states for 1-4 player controllers for this frame
        m_P1State = GamePad.GetState(PlayerIndex.One);
        //m_P2State = GamePad.GetState(PlayerIndex.Two);
        //m_P3State = GamePad.GetState(PlayerIndex.Three);
        //m_P4State = GamePad.GetState(PlayerIndex.Four);

        if (m_P1PrevState.IsConnected)
        {
            gamepadCursor.SetActive(true);
        }
        else
        {
            gamepadCursor.SetActive(false);
        }

        switch (currentState)
        {
            case MenuStates.MainMenu:
                mainUI.SetActive(true);
                creditUI.SetActive(false);
                playerSelectUI.SetActive(false);
                optionsUI.SetActive(false);
                break;
            case MenuStates.Credits:
                mainUI.SetActive(false);
                creditUI.SetActive(true);
                break;
            case MenuStates.PlayerSelect:
                mainUI.SetActive(false);
                //shipSelectUI.SetActive(false);
                playerSelectUI.SetActive(true);
                trackSelectUI.SetActive(false);
                break;
            case MenuStates.TrackSelect:
                trackSelectUI.SetActive(true);
                playerSelectUI.SetActive(false);
                break;
            case MenuStates.ShipSelect:
                playerSelectUI.SetActive(false);
                trackSelectUI.SetActive(false);
                break;
            case MenuStates.Options:
                optionsUI.SetActive(true);
                playerSelectUI.SetActive(false);
                trackSelectUI.SetActive(false);
                creditUI.SetActive(false);
                break;
        }

        if (playerManager.onePlayer)
        {
            if (p1_Confirm)
            {
                isShipSelected = true;
                DontDestroy.menuMusic.GetComponent<AudioSource>().Stop();
            }
        }
        if (playerManager.twoPlayer)
        {
            if (p1_Confirm && p2_Confirm)
            {
                isShipSelected = true;
                DontDestroy.menuMusic.GetComponent<AudioSource>().Stop();
            }
        }
        if (playerManager.threePlayer)
        {
            if (p1_Confirm && p2_Confirm && p3_Confirm)
            {
                isShipSelected = true;
                DontDestroy.menuMusic.GetComponent<AudioSource>().Stop();
            }
        }
        if (playerManager.fourPlayer)
        {
            if (p1_Confirm && p2_Confirm && p3_Confirm && p4_Confirm)
            {
                isShipSelected = true;
                DontDestroy.menuMusic.GetComponent<AudioSource>().Stop();
            }
        }

        foreach (Text text in pOneText)
        {
            if (oneKeyboard)
            {
                text.text = "Keyboard";
            }
            else
            {
                text.text = "Controller";
            }
        }
        foreach (Text text in pTwoText)
        {
            if (twoKeyboard)
            {
                text.text = "Keyboard";
            }
            else
            {
                text.text = "Controller";
            }
        }
        foreach (Text text in pThreeText)
        {
            if (threeKeyboard)
            {
                text.text = "Keyboard";
            }
            else
            {
                text.text = "Controller";
            }
        }
        if (fourKeyboard)
        {
            pFourText.text = "Keyboard";
        }
        else
        {
            pFourText.text = "Controller";
        }
    }

    public void SetKeybaord(float controller)
    {
        if (controller == 1)
        {
            oneKeyboard = !oneKeyboard;
            twoKeyboard = false;
            threeKeyboard = false;
            fourKeyboard = false;
        }

        if (controller == 2)
        {
            twoKeyboard = !twoKeyboard;
            oneKeyboard = false;
            threeKeyboard = false;
            fourKeyboard = false;
        }

        if (controller == 3)
        {
            threeKeyboard = !threeKeyboard;
            twoKeyboard = false;
            oneKeyboard = false;
            fourKeyboard = false;
        }

        if (controller == 4)
        {
            fourKeyboard = !fourKeyboard;
            twoKeyboard = false;
            threeKeyboard = false;
            oneKeyboard = false;
        }
    }

    public void Confirm (float playerNumber)
    {
        if (playerNumber == 1)
        {
            p1_Confirm = true;
        }
        if (playerNumber == 2)
        {
            p2_Confirm = true;
        }
        if (playerNumber == 3)
        {
            p3_Confirm = true;
        }
        if (playerNumber == 4)
        {
            p4_Confirm = true;
        }
    }

    public void Play()
    {
        currentState = MenuStates.PlayerSelect;
    }

    public void PlayersSelected()
    {
        currentState = MenuStates.ShipSelect;
        if (playerManager.onePlayer)
        {
            onePlayerShipSelect.SetActive(true);
        }
        else if (playerManager.twoPlayer)
        {
            twoPlayerShipSelect.SetActive(true);
        }
        else if (playerManager.threePlayer)
        {
            threePlayerShipSelect.SetActive(true);
        }
        else if (playerManager.fourPlayer)
        {
            fourPlayerShipSelect.SetActive(true);
        }
    }

    public void TrackSelect()
    {
        currentState = MenuStates.TrackSelect;
    }

    public void OnePlayer()
    {
        playerManager.onePlayer = true;
    }

    public void TwoPlayer()
    {
        playerManager.twoPlayer = true;
    }

    public void ThreePlayer()
    {
        playerManager.threePlayer = true;
    }

    public void FourPlayer()
    {
        playerManager.fourPlayer = true;
    }

    public void Credits()
    {
        currentState = MenuStates.Credits;
    }

    public void Options()
    {
        currentState = MenuStates.Options;
    }

    public void Back()
    {
        if (currentState == MenuStates.Credits)
        {
            currentState = MenuStates.MainMenu;
        }
        if (currentState == MenuStates.PlayerSelect)
        {
            currentState = MenuStates.MainMenu;
        }
        if (currentState == MenuStates.Options)
        {
            currentState = MenuStates.MainMenu;
        }
        if (currentState == MenuStates.TrackSelect)
        {
            currentState = MenuStates.PlayerSelect;

            playerManager.onePlayer = false;
            playerManager.twoPlayer = false;
            playerManager.threePlayer = false;
            playerManager.fourPlayer = false;
        }
        if (currentState == MenuStates.ShipSelect)
        {
            currentState = MenuStates.TrackSelect;
            onePlayerShipSelect.SetActive(false);
            twoPlayerShipSelect.SetActive(false);
            threePlayerShipSelect.SetActive(false);
            fourPlayerShipSelect.SetActive(false);

            p1_Confirm = false;
            p2_Confirm = false;
            p3_Confirm = false;
            p4_Confirm = false;

            isShipSelected = false;
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("LoadingScene");
        asyncOperation.allowSceneActivation = false;

        yield return new WaitUntil(() => isShipSelected);

        asyncOperation.allowSceneActivation = true;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void Reset()
    {
        p1_Confirm = false;
        p2_Confirm = false;
        p3_Confirm = false;
        p4_Confirm = false;
    }
}
