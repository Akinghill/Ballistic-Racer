using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateButtonState : Button
{
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
    }

    public void SetNormalState()
    {
        DoStateTransition(SelectionState.Normal, true);
    }

    public void SetHighlightedState()
    {
        DoStateTransition(SelectionState.Highlighted, true);
    }

    public void SetPressedState()
    {
        DoStateTransition(SelectionState.Pressed, true);
    }
}