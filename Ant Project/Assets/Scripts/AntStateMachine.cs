using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public enum AntState
{
    Searching,
    Harvesting,
    Returning
}
public class AntStateMachine : MonoBehaviour
{
    public AntState state = AntState.Searching;
    public GameObject food;

    private void Update()
    {
        switch (state)
        {
            case AntState.Searching:
                {
                    Search();
                    break;
                }
            case AntState.Harvesting:
                {
                    Harvest();
                    break;
                }
            case AntState.Returning:
                {
                    ReturnHome();
                    break;
                }
        }
    }

    private void Search()
    {
        Collider[] cols = Physics.OverlapSphere()
    }
    private void Harvest()
    {

    }
    private void ReturnHome()
    {
        
    }
}




