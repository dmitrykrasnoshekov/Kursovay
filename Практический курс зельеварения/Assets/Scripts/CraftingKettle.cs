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

    private static bool successCrafting = true;
    private static bool finishedCrafting = false;

    public uint craftingAttempt = 0;
    public IngridientsSO lastCraftedPotion;

    public bool checkNeeded = false;
    public bool TaskCompleted = false;

    private void Awake()
    {
        NextRecipe();
    }

    private void Update()
    {
        Craft();
        SpawnPotion();
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
            Debug.Log(collidersArray.Length);

            if (ingridientSO.objectName is not null) {
                Debug.Log( ingridientSO);

                if( ingridientSO.objectName == inputRecipe[0].objectName) {
                    inputRecipe.RemoveAt(0);
                }
                else
                    successCrafting = false;

                Destroy(ingridient.gameObject);
            }

        }

    }
    private bool CheckCraftDone()
    {
        if (inputRecipe.Count == 0 || !successCrafting) {
            finishedCrafting = true;
        }
        else
            finishedCrafting = false;
        return finishedCrafting;
    }
    private void SpawnPotion()
    {
        if (CheckCraftDone())
        {
            if (successCrafting)
            {
                Instantiate(craftingRecipeSO.outputIngridientSO.prefab, outputSpawnPoint.position, Quaternion.Euler(Vector3.up));
            }
            else
            {
                Instantiate(trash.prefab, outputSpawnPoint.position, Quaternion.Euler(Vector3.up));
            }

            TaskCompleted = successCrafting;
            checkNeeded = true;

            // ѕереход к созданию следующего продукта
            NextRecipe();   // ќбновл€ем прогресс следовани€ рецепту

            craftingAttempt++;  // ќбновл€ем количество попыток в создании чего-либо
            // —охран€ем название последнего конечного продукта, полученного из котла
            lastCraftedPotion = craftingRecipeSO.outputIngridientSO;

            finishedCrafting = false;   // ќбновл€ем значение, отвечающее за окончание создани€ продукта котла
            successCrafting = true;     // ќбновл€ем значение, отражающее успех создани€ продукта котла
        }
    }
}
