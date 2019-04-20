using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XInputDotNetPure;

public class MenuCursor : MonoBehaviour
{
    GamePadState m_P1State;
    //GamePadState m_P2State;
    //GamePadState m_P3State;
    //GamePadState m_P4State;

    GamePadState m_P1PrevState;
    //GamePadState m_P2PrevState;
    //GamePadState m_P3PrevState;
    //GamePadState m_P4PrevState;

    public BaseRaycaster raycaster;
    public Canvas canvas;

    public float right;
    public float top;

    private void Start()
    {
        right = canvas.transform.position.x * 2;
        top = canvas.transform.position.y * 2;
    }

    private void Update()
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

        if (m_P1State.ThumbSticks.Left.X < 0.0f)
        {
            transform.position -= new Vector3(m_P1State.ThumbSticks.Left.X * Time.unscaledDeltaTime * -500.0f, 0.0f, 0.0f);
        }
        if (m_P1State.ThumbSticks.Left.X > 0.0f)
        {
            transform.position += new Vector3(m_P1State.ThumbSticks.Left.X * Time.unscaledDeltaTime * 500.0f, 0.0f, 0.0f);
        }
        if (m_P1State.ThumbSticks.Left.Y < 0.0f)
        {
            transform.position -= new Vector3(0.0f, m_P1State.ThumbSticks.Left.Y * Time.unscaledDeltaTime * -500.0f, 0.0f);
        }
        if (m_P1State.ThumbSticks.Left.Y > 0.0f)
        {
            transform.position += new Vector3(0.0f, m_P1State.ThumbSticks.Left.Y * Time.unscaledDeltaTime * 500.0f, 0.0f);
        }

        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y);
        }
        if (transform.position.x > right - 25)
        {
            transform.position = new Vector3(right - 25, transform.position.y);
        }
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0);
        }
        if (transform.position.y > top -25)
        {
            transform.position = new Vector3(transform.position.x, top - 25);
        }

        PointerEventData pointerEventData = new PointerEventData(null)
        {
            position = transform.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Button"))
            {
                new WaitForEndOfFrame();

                EventSystem.current.SetSelectedGameObject(result.gameObject);

                if (m_P1PrevState.Buttons.A == ButtonState.Released && m_P1State.Buttons.A == ButtonState.Pressed)
                {
                    result.gameObject.GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    break;
                }
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                break;
            }
            //Debug.Log(result.gameObject.name);
        }
    }
}
