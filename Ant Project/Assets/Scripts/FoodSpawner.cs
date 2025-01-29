using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FoodSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int numOfFood = 0;

    // Update is called once per frame
    void Update()
    {
        if (numOfFood < 10)
        {
            Instantiate(prefab, new Vector3(Random.Range(-9.0f, 9.0f), prefab.transform.position.y, Random.Range(-9.0f, 9.0f)), Quaternion.identity);
            numOfFood += 1;
        }
    }

    public void DecrementFood(GameObject food)
    {
        Destroy(food);
        numOfFood -= 1;
    }
}
