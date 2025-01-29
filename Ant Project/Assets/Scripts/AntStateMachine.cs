using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private Coroutine _myCo;

    Vector3 _randomPoint = Vector3.zero;

    bool _isHarvesting = false;

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
                    if (_isHarvesting == false)
                    {
                        StartCoroutine(Harvest());
                    }
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
        if(_randomPoint == Vector3.zero || Vector3.Distance(transform.position, _randomPoint) <= 0.5f)
        {
            _randomPoint = new Vector3(Random.Range(-9.0f, 9.0f), transform.position.y, Random.Range(-9.0f, 9.0f));
        }

        Vector3 direction = _randomPoint - transform.position;
        direction.Normalize();

        transform.position += direction * 3 * Time.deltaTime;

        Collider[] cols = Physics.OverlapSphere(transform.position, 1);
        
        foreach (Collider col in cols)
        {
            if (col.CompareTag("Food"))
            {
                _randomPoint = Vector3.zero;
                state = AntState.Harvesting;
                col.gameObject.tag = "Eaten";
                food = col.gameObject;
                break;
            }
        }
    }
    IEnumerator Harvest()
    {
        _isHarvesting = true;
        yield return new WaitForSeconds(3);
        _isHarvesting = false;
        state = AntState.Returning;
        GameObject.Find("Food Spawner").GetComponent<FoodSpawner>().DecrementFood(food);
    }
    private void ReturnHome()
    {
        Vector3 homePos = home.transform.position;
        Vector3 currPos = transform.position;
        Vector3 direction = homePos - transform.position;
        direction.Normalize();

        transform.position += direction * 3 * Time.deltaTime;
        if (currPos == homePos)
        {
            state = AntState.Searching;
        }
    }

    /*
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
    */
}




