using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerState 
{
    public IState curentState;


    public void InstallState(IState state)
    {
        if (state == null) return;
        curentState = state;
        curentState.Enter();
    }

    public void ChangeState(IState state)
    {
        if(curentState == state || state == null) return;
        curentState.Exit();
        curentState = state;
        curentState.Enter();
    }
}
