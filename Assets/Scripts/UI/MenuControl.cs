using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UpdateButtonState updateButtonState;

    void Start()
    {
        AddButtonEvents();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (EventSystem.current.currentSelectedGameObject.CompareTag("Button"))
        {
            updateButtonState.SetHighlightedState();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        updateButtonState.SetNormalState();
    }

    void AddButtonEvents()
    {
        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((data) => { OnButtonDown((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((data) => { OnButtonUp((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    public void OnButtonDown(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject && GetComponentInParent<MainMenu>().gamepadCursor.activeInHierarchy)
        {
            updateButtonState.SetPressedState();
        }
    }

    public void OnButtonUp(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject && GetComponentInParent<MainMenu>().gamepadCursor.activeInHierarchy)
        {
            updateButtonState.SetHighlightedState();
            ExecuteEvents.Execute(gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }
}
