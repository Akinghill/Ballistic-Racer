using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuControl : MonoBehaviour
{
    public Button selectedButton;

    void OnEnable()
    {
        selectedButton.Select();
    }
}
