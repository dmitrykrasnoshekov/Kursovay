using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingridients : MonoBehaviour
{
    [SerializeField] private IngridientsSO ingridientsSO;

    public enum IngridientType
    {
        None,
        Flowers,
        Mushrums,
        AnimalParts,
        Liquids,
        Spyces,
        Spoiled,
        CrushedFlowers,
        CrushedMushrums,
        FriedAnimalParts,
        ProcessedSpyces,
        Potion
    }
    public IngridientsSO GetIngridientsSO() { return ingridientsSO; }
}
