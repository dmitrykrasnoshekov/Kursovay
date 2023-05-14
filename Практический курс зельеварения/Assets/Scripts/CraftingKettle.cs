using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingKettle : MonoBehaviour
{
    [SerializeField] private Image recipeImage;
    [SerializeField] private List<CraftingRecipeSO> craftingRecipeSOlist;
    [SerializeField] private BoxCollider craftCollider;
    [SerializeField] private LayerMask craftMask;
    [SerializeField] private Transform outputSpawnPoint;
    [SerializeField] private IngridientsSO trash;

    private List<IngridientsSO> inputRecipe;
    private CraftingRecipeSO craftingRecipeSO;

    private void Awake()
    {
        NextRecipe();
    }

    private void Update()
    {
        Craft();
        SpawPosion();
    }
    public void NextRecipe()
    {
        if (craftingRecipeSO == null)
        {
            craftingRecipeSO = craftingRecipeSOlist[0];
        }
        else
        {
            int index = craftingRecipeSOlist.IndexOf(craftingRecipeSO);
            index = (index + 1) % craftingRecipeSOlist.Count;
            craftingRecipeSO = craftingRecipeSOlist[index];
        }

        recipeImage.sprite = craftingRecipeSO.sprite;
        inputRecipe = new List<IngridientsSO>(craftingRecipeSO.InputIngridients);
    }

    public void Craft()
    {
        Collider[] collidersArray = Physics.OverlapSphere(transform.TransformPoint(craftCollider.center), 0.3f, craftMask);

        if (collidersArray.Length >= 1) { 
        Ingridients ingridient = collidersArray[0].gameObject.GetComponentInParent<Ingridients>();
        IngridientsSO ingridientSO = ingridient.GetIngridientsSO();
        Debug.Log(ingridientSO);

        if (ingridientSO.objectName is not null) { 
            if ( ingridientSO.objectName == inputRecipe[0].objectName)
        {
            inputRecipe.RemoveAt(0);
            Destroy(ingridient.gameObject);
        }
                else
                {
                    Destroy(ingridient.gameObject);
                    Instantiate(trash.prefab, outputSpawnPoint.position, Quaternion.Euler(Vector3.up));
                    NextRecipe();
                }
            }

        }

    }

    private bool CheckCraftDone()
    {
        if (inputRecipe.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void SpawPosion()
    {
        if (CheckCraftDone())
        {
            Instantiate(craftingRecipeSO.outputIngridientSO.prefab, outputSpawnPoint.position, Quaternion.Euler(Vector3.up));
            NextRecipe();
        }
    }

}
