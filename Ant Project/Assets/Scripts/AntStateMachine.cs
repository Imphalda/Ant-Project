using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    public GameObject home;
    [Range(0.0f, 10.0f)] public float duration = 5.0f;
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
        Collider[] cols = Physics.OverlapSphere(transform.position, 1);
        
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Food"))
            {
                state = AntState.Harvesting;
                break;
            }
            StartCoroutine(MoveToward(duration));
        }
    }
    private void Harvest()
    {
        StartCoroutine(wait());
        state = AntState.Returning;
    }
    private void ReturnHome()
    {
        transform.position = home.transform.position;
        state = AntState.Searching;
    }

    private IEnumerator MoveToward(float duration)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(Random.Range(-9.0f, 9.0f), transform.position.y, Random.Range(-9.0f, 9.0f));
        float timer = 0;

        while (timer < duration)
        {
            float percent = timer / duration;
            transform.position = Vector3.Lerp(startPos, endPos, percent);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
    }
}




