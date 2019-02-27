using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
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
        public string camView;
    }
    public InputNames inputNames;

    string accelerateInput;
    string brakeInput;
    string steeringInput;
    string boostInput;
    string powerUpInput;
    string shootInput;
    string airBrakeInput;
    string camViewInput;

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

    bool clicked;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AbleToShoot());

        if (controllerNumber != 0)
        {
            SetController();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerNumber != 0)
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
            if (!usingKeyboard)
            {
                if (Input.GetAxis(camViewInput) < 0)
                {
                    if (!clicked)
                    {
                        cameraView = true;
                        clicked = true;
                    }
                    else
                    {
                        cameraView = false;
                    }
                }
                else
                {
                    clicked = false;
                    cameraView = false;
                }
            }
            else
            {
                cameraView = Input.GetButtonDown(camViewInput);
            }
        }
    }

    IEnumerator AbleToShoot()
    {
        yield return new WaitForSeconds(10);
        canShoot = true;
    }

    void SetController()
    {
        if (!usingKeyboard)
        {
            accelerateInput = inputNames.joystickInitial + controllerNumber + inputNames.accelerate;
            brakeInput = inputNames.joystickInitial + controllerNumber + inputNames.brake;
            steeringInput = inputNames.joystickInitial + controllerNumber + inputNames.steering;
            boostInput = inputNames.joystickInitial + controllerNumber + inputNames.boost;
            powerUpInput = inputNames.joystickInitial + controllerNumber + inputNames.powerUp;
            shootInput = inputNames.joystickInitial + controllerNumber + inputNames.shoot;
            airBrakeInput = inputNames.joystickInitial + controllerNumber + inputNames.airBrake;
            camViewInput = inputNames.joystickInitial + controllerNumber + inputNames.camView;
        }
        else
        {
            accelerateInput = "K" + inputNames.accelerate;
            brakeInput = "K" + inputNames.brake;
            steeringInput = "K" + inputNames.steering;
            boostInput = "K" + inputNames.boost;
            powerUpInput = "K" + inputNames.powerUp;
            shootInput = "K" + inputNames.shoot;
            airBrakeInput = "K" + inputNames.airBrake;
            camViewInput = "K" + inputNames.camView;
        }
    }
}
