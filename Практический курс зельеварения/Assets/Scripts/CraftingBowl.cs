using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBowl : MonoBehaviour
{
    [SerializeField] private Transform outputSpawnPoint;
    [SerializeField] private List<IngridientsSO> input_ingridients;
    [SerializeField] private List<IngridientsSO> output_ingridients;

    private void Update()
    {
    }

    // Update is called once per frame
    public void CreateIngridient()
    {
        /*
        if( )
        Instantiate(ingridient[i].prefab, outputSpawnPoint.position, Quaternion.Euler(Vector3.up));*/
    }
}
