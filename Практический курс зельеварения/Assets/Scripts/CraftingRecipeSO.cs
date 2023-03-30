using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CraftingRecipeSO : ScriptableObject
{
    public Sprite sprite;
    public string RecipeName;
    public List<IngridientsSO> InputIngridients;
    public IngridientsSO outputIngridientSO;
    public IngridientsSO Ingridient_liquid_1;
    public IngridientsSO Ingridient_animalPart_2;
    public IngridientsSO Ingridient_mushrum_3;
    public IngridientsSO Ingridient_flower_4;
    public IngridientsSO Ingridient_spyce_5;
}
