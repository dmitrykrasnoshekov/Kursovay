using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class IngridientsSO : ScriptableObject
{
    public Transform prefab;
    public Ingridients.IngridientType type;
    public string objectName;
}
