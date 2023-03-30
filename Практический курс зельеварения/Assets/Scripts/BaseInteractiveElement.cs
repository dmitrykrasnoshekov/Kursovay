using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractiveElement : MonoBehaviour
{
    [SerializeField] private IngridientsSO ingridientsSO;
    [SerializeField] private Transform[] spawnPoints = new Transform[0];

    private Transform spawnPoint;

    private void Update()
    {
        spawnPoint = SpawnPoint();
    }
    public void Interact()
    {
        Debug.Log("Interact");
        Transform IngridientTransform =  Instantiate(ingridientsSO.prefab, spawnPoint);
        IngridientTransform.localPosition = Vector3.zero;

        Debug.Log(IngridientTransform.GetComponent<Ingridients>().GetIngridientsSO().objectName);
    }

    private Transform SpawnPoint()
    {
        float r;
        float minR = (spawnPoints[0].position.x - Player.Instance.transform.position.x) * (spawnPoints[0].position.x - Player.Instance.transform.position.x) + (spawnPoints[0].position.z - Player.Instance.transform.position.z) * (spawnPoints[0].position.z - Player.Instance.transform.position.z);
        Transform spawnPoint = spawnPoints[0];
        foreach (Transform t in spawnPoints)
        {
            r = (t.position.x - Player.Instance.transform.position.x)* (t.position.x - Player.Instance.transform.position.x) + (t.position.z - Player.Instance.transform.position.z)* (t.position.z - Player.Instance.transform.position.z);
            if (r < minR)
            {
                minR = r;
                spawnPoint = t;
            }
        }
        return spawnPoint;
    }
}
