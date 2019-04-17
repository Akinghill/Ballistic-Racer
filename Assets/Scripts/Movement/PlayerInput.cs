using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerInput : MonoBehaviour
{
    GamePadState m_P1State;
    GamePadState m_P2State;
    GamePadState m_P3State;
    GamePadState m_P4State;

    [System.Serializable]
    public class InputNames
    {
        public string joystickInitial;
        public string accelerate;
        public string brake;
        public string steering;
        public string boost;
        public string powerUp;
        public string shoot;
        public string airBrake;
    }
    public InputNames inputNames;

    string accelerateInput;
    string brakeInput;
    string steeringInput;
    string boostInput;
    string powerUpInput;
    string shootInput;
    string airBrakeInput;

    public int controllerNumber;

    public bool usingKeyboard;

    [HideInInspector] public float rudder;
    [HideInInspector] public float accelerate;
    [HideInInspector] public float brake;

    [HideInInspector] public bool boost;
    [HideInInspector] public bool cameraView;
    [HideInInspector] public bool powerUp;
    [HideInInspector] public bool shoot;
    [HideInInspector] public bool airBrake;
    [HideInInspector] public bool canShoot;

    void Start()
    {
        StartCoroutine(AbleToShoot());

        if (controllerNumber != 0)
        {
            SetController();
        }
    }

    void Update()
    {
        // Get gamepad states for 1-4 player controllers
        m_P1State = GamePad.GetState(PlayerIndex.One);
        m_P2State = GamePad.GetState(PlayerIndex.Two);
        m_P3State = GamePad.GetState(PlayerIndex.Three);
        m_P4State = GamePad.GetState(PlayerIndex.Four);

        // Set controller inputs for all ships other than AI
        if (controllerNumber != 0)
        {
            // Set the gamepad inputs for player 1 (if not using keyboard)
            if (controllerNumber == 1 && !usingKeyboard)
            {
                rudder = m_P1State.ThumbSticks.Left.X;
                accelerate = m_P1State.Triggers.Right;
                brake = m_P1State.Triggers.Left;

                if (m_P1State.Buttons.A == ButtonState.Pressed)
                {
                    boost = true;
                }
                else
                {
                    boost = false;
                }

                if (m_P1State.Buttons.Y == ButtonState.Pressed)
                {
                    powerUp = true;
                }
                else
                {
                    powerUp = false;
                }

                if (canShoot)
                {
                    if (m_P1State.Buttons.B == ButtonState.Pressed)
                    {
                        shoot = true;
                        GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
                    }
                    else
                    {
                        shoot = false;
                        GamePad.SetVibration(PlayerIndex.One, 0.0f, 0.0f);
                    }
                }

                if (m_P1State.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    airBrake = true;
                }
                else
                {
                    airBrake = false;
                }
            }
            // Set the gamepad inputs for player 2 (if not using keyboard)
            else if (controllerNumber == 2 && !usingKeyboard)
            {
                rudder = m_P2State.ThumbSticks.Left.X;
                accelerate = m_P2State.Triggers.Right;
                brake = m_P2State.Triggers.Left;

                if (m_P2State.Buttons.A == ButtonState.Pressed)
                {
                    boost = true;
                }
                else
                {
                    boost = false;
                }

                if (m_P2State.Buttons.Y == ButtonState.Pressed)
                {
                    powerUp = true;
                }
                else
                {
                    powerUp = false;
                }

                if (canShoot)
                {
                    if (m_P2State.Buttons.B == ButtonState.Pressed)
                    {
                        shoot = true;
                        GamePad.SetVibration(PlayerIndex.Two, 1.0f, 1.0f);
                    }
                    else
                    {
                        shoot = false;
                        GamePad.SetVibration(PlayerIndex.Two, 0.0f, 0.0f);
                    }
                }

                if (m_P2State.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    airBrake = true;
                }
                else
                {
                    airBrake = false;
                }
            }
            // Set the gamepad inputs for player 3 (if not using keyboard)
            else if (controllerNumber == 3 && !usingKeyboard)
            {
                rudder = m_P3State.ThumbSticks.Left.X;
                accelerate = m_P3State.Triggers.Right;
                brake = m_P3State.Triggers.Left;

                if (m_P3State.Buttons.A == ButtonState.Pressed)
                {
                    boost = true;
                }
                else
                {
                    boost = false;
                }

                if (m_P3State.Buttons.Y == ButtonState.Pressed)
                {
                    powerUp = true;
                }
                else
                {
                    powerUp = false;
                }

                if (canShoot)
                {
                    if (m_P3State.Buttons.B == ButtonState.Pressed)
                    {
                        shoot = true;
                        GamePad.SetVibration(PlayerIndex.Three, 1.0f, 1.0f);
                    }
                    else
                    {
                        shoot = false;
                        GamePad.SetVibration(PlayerIndex.Three, 0.0f, 0.0f);
                    }
                }

                if (m_P3State.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    airBrake = true;
                }
                else
                {
                    airBrake = false;
                }
            }
            // Set the gamepad inputs for player 4 (if not using keyboard)
            else if (controllerNumber == 4 && !usingKeyboard)
            {
                rudder = m_P4State.ThumbSticks.Left.X;
                accelerate = m_P4State.Triggers.Right;
                brake = m_P4State.Triggers.Left;

                if (m_P4State.Buttons.A == ButtonState.Pressed)
                {
                    boost = true;
                }
                else
                {
                    boost = false;
                }

                if (m_P4State.Buttons.Y == ButtonState.Pressed)
                {
                    powerUp = true;
                }
                else
                {
                    powerUp = false;
                }

                if (canShoot)
                {
                    if (m_P4State.Buttons.B == ButtonState.Pressed)
                    {
                        shoot = true;
                        GamePad.SetVibration(PlayerIndex.Four, 1.0f, 1.0f);
                    }
                    else
                    {
                        shoot = false;
                        GamePad.SetVibration(PlayerIndex.Four, 0.0f, 0.0f);
                    }
                }

                if (m_P4State.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    airBrake = true;
                }
                else
                {
                    airBrake = false;
                }
            }
        }

        // Set the keyboard inputs for player using keyboard
        if (usingKeyboard)
        {
            rudder = Input.GetAxis(steeringInput);
            accelerate = Input.GetAxis(accelerateInput);
            brake = Input.GetAxis(brakeInput);

            boost = Input.GetButton(boostInput);
            powerUp = Input.GetButtonDown(powerUpInput);

            if (canShoot)
            {
                shoot = Input.GetButton(shootInput);
            }

            airBrake = Input.GetButton(airBrakeInput);
        }
    }

    IEnumerator AbleToShoot()
    {
        yield return new WaitForSeconds(10);
        canShoot = true;
    }

    void SetController()
    {
        if (usingKeyboard)
        {
            accelerateInput = "K" + inputNames.accelerate;
            brakeInput = "K" + inputNames.brake;
            steeringInput = "K" + inputNames.steering;
            boostInput = "K" + inputNames.boost;
            powerUpInput = "K" + inputNames.powerUp;
            shootInput = "K" + inputNames.shoot;
            airBrakeInput = "K" + inputNames.airBrake;
        }
    }
}
