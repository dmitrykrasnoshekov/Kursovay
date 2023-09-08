using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatingBarrel : MonoBehaviour
{
    [SerializeField] private Image ingridientImage;
    [SerializeField] private Transform outputSpawnPoint;
    [SerializeField] private IngridientsSO ingridient;

    private void Start()
    {
        ingridientImage.sprite = ingridient.sprite;
    }

    private void Update()
    {
    }

    // Update is called once per frame
    public void CreateIngridient() {
        Instantiate( ingridient.prefab, outputSpawnPoint.position, Quaternion.Euler(Vector3.up));
    }
}
