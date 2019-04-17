using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiUltimate : MonoBehaviour
{
    UltCharge ultCharge;
    PlayerInput playerInput;

    HyperSpeed hyperSpeed;
    Mine mine;

    Invulnerability invulnerability;
    ReflectorShield reflectorShield;

    BulletHell bulletHell;
    EmpController empController;
    PlayerShooting playerShooting;

    void Update ()
    {
        AiUseUltimate();
    }

    void AiUseUltimate ()
    {
        if (ultCharge.ultCharged) // If the ultimate is charged...
        {
            if(hyperSpeed || mine) // ... and is the HyperSpeed or Mine powerup...
            {
                playerInput.powerUpInput = true; // ...Trigger the ultimate
            }
            /* if (invulnerability || reflectorShield) // ... and is the invulnerability or reflectorShield powerup...
            {
                if() // ... and is taking damage...
                {
                    playerInput.powerUpInput = true; // ...Trigger the ultimate
                }
                */
            if(bulletHell || empController) // ... and is BulletHell or EmpController...
                {
                    if (playerShooting.gunLine.enabled) ; // and can shoot...
                    {
                        playerInput.powerUpInput = true; // ...Trigger the ultimate
                    }
                }

                }
                
            }
        }
    }
}
