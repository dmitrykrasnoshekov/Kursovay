using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingridients : MonoBehaviour
{
    [SerializeField] private IngridientsSO ingridientsSO;

    public IngridientsSO GetIngridientsSO() { return ingridientsSO; }
}
