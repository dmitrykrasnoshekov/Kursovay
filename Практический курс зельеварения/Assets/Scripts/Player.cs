using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float upLimit = -50;
    [SerializeField] private float downLimit = 50;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float mousSens = 0.25f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactive; //ƒŒ¡¿¬»“‹   œ–≈‘¿¡” Ã¿— ”
    [SerializeField] private GameObject Camera;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform objectGrabPointTransform;

    private ObjectGrab objectGrab;

    private Ray interactRay;
    private RaycastHit interactHit;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    public static Player Instance { get; private set; }
    private BaseInteractiveElement selected;
    private Vector3 lastInteractDir;
    [SerializeField] private CraftingKettle kettle;

    [SerializeField] private GameObject interactKettleText;
    [SerializeField] private GameObject interactBowlText;
    [SerializeField] private GameObject interactSieveText;
    [SerializeField] private GameObject interactBarrelText;
    [SerializeField] private TMP_Text EXPText;
    [SerializeField] private Image interactImage;

    private int EXP = 0;
    [SerializeField] private int EXP_TASK;
    [SerializeField] private int EXP_ERROR;
    [SerializeField] public List<IngridientsSO> taskCraftPotion;
    [SerializeField] public List<bool> taskCompletion;
    private int EXP_MAX;

    public static bool pauseRequired;

    private void Awake()
    {
        Instance = this;
        pauseRequired = false;
    }

    private void Start()
    {
        Cursor.visible = false;
        gameInput.OnInteractAction += GameInput_OnInteractAction;

        EXP_MAX = EXP_TASK*taskCraftPotion.Count;
        for (int i = 0; i < taskCraftPotion.Count; i++)
        {
            if(taskCompletion[i])
            {
                EXP += EXP_TASK;
            }
        }

        float tmp = Mathf.Max(EXP, 0);
        int mark = Mathf.RoundToInt(tmp.Remap(0, EXP_MAX, 0, 5));
        switch (SceneManager.GetActiveScene().buildIndex - LevelManager.levelStartCount + 1)
        {
            case 1:
                PlayerPrefs.SetInt("levelMark1", mark);
                break;

            case 2:
                PlayerPrefs.SetInt("levelMark2", mark);
                break;

            case 3:
                PlayerPrefs.SetInt("levelMark3", mark);
                break;
        }
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!PauseChecker.gameIsPaused && selected != null)
        {
            selected.Interact();
        }
    }
    private void Update() {
        if ( !PauseChecker.gameIsPaused) { 
            HandleMovement();
            HandleCameraRotation();
            InteractRay();
            HandleInteractions();
            HandlePickUpDrop();
        }

        HandlePause();
    }

    private void HandleMovement() //¬˚Á˚‚‡ÂÚÒˇ ‚ Update
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);


    }

    private void InteractRay()
    {
        interactRay = Camera.GetComponent<Camera>().ScreenPointToRay(screenCenter);
    }

    private void HandleInteractions() //¬˚Á˚‚‡ÂÚÒˇ ‚ Update
    {
        Vector3 moveDir = transform.forward;

        interactKettleText.SetActive(false);
        interactBarrelText.SetActive(false);
        interactImage.gameObject.SetActive(false);

        EXPText.text = EXP.ToString() + "/" + EXP_MAX.ToString();
        if (kettle.checkNeeded) TaskCompleted();

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if( Physics.Raycast(interactRay, out interactHit, interactDistance, interactive) )
        {
            Debug.Log(selected);    
            if (interactHit.transform.TryGetComponent(out BaseInteractiveElement baseInteractiveElement))
            {
                if (baseInteractiveElement != selected)
                {
                    selected = baseInteractiveElement;
                }
            }
            else if (interactHit.transform.TryGetComponent(out CraftingKettle craftingKettle))
            {
                interactKettleText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    craftingKettle.NextRecipe();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    craftingKettle.Craft();
                }
            }
            else if (interactHit.transform.TryGetComponent(out CraftingBowl craftingBowl))
            {
                /*
                interactBowlText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    craftingBowl.Craft();
                }*/
            }
            else if (interactHit.transform.TryGetComponent(out CraftingSieve craftingSieve))
            {
                /*
                interactSieveText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    craftingSieve.Craft();
                }*/
            }
            else if (interactHit.transform.TryGetComponent(out CreatingBarrel barrel))
            {
                interactBarrelText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    barrel.CreateIngridient();
                }
            }

            else
            {
                selected = null;
            }
        }

        else
        {
            selected = null;
        }
    }
    private void HandleCameraRotation()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");
        transform.Rotate(0, horizontalRotation * mousSens, 0);
        Camera.transform.Rotate(-verticalRotation * mousSens, 0, 0);
        Vector3 currentRotation = Camera.transform.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        Camera.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    private void HandlePickUpDrop()
    {
        float pickUpDistance = 2.5f;
        RaycastHit hit;

        if (objectGrab != null)
            interactImage.gameObject.SetActive(true);
        else
            interactImage.gameObject.SetActive(false);

        if (Input.GetMouseButtonDown(0))
        {
            if (objectGrab == null)
            {
                if( Physics.Raycast(interactRay, out hit, pickUpDistance, pickUpLayerMask))
                {
                    if( hit.transform.TryGetComponent(out objectGrab))
                    {
                        objectGrab.Grab(objectGrabPointTransform);
                        IngridientsSO tmpInfoGrabbedObject = hit.transform.GetComponent<Ingridients>().GetIngridientsSO();
                        interactImage.sprite = tmpInfoGrabbedObject.sprite;
                    }
                }
            } else
            {
                objectGrab.Drop();
                objectGrab = null;
            }
        }
    }

    private void HandlePause()
    {
        if (PauseChecker.gameIsPaused) {
            interactKettleText.SetActive(false);
            interactBarrelText.SetActive(false);
            interactImage.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log( "Player : Input change state of Puase");
            ChangeStatePauseRequired();
        }
    }

    public void ChangeStatePauseRequired()
    {
        pauseRequired = !pauseRequired;

        if( pauseRequired) Debug.Log( "Player : Required Pause");
        else Debug.Log( "Player : Required Continue");
    }

    public void TaskCompleted()
    {
        if ( kettle.TaskCompleted)
        {
            for( int i = 0; i < taskCraftPotion.Count; i++)
            {
                if (taskCraftPotion[i] == kettle.lastCraftedPotion && !taskCompletion[i])
                {
                    EXP += EXP_TASK;
                    taskCompletion[i] = true;
                }
            }
        }
        else EXP -= EXP_ERROR;

        float tmp = Mathf.Max(EXP, 0);
        int mark = Mathf.RoundToInt(tmp.Remap( 0, EXP_MAX, 0, 5));
        switch( SceneManager.GetActiveScene().buildIndex - LevelManager.levelStartCount + 1)
        {
            case 1:
                PlayerPrefs.SetInt("levelMark1", mark);
            break;

            case 2:
                PlayerPrefs.SetInt("levelMark2", mark);
            break;

            case 3:
                PlayerPrefs.SetInt("levelMark3", mark);
            break;
        }

        kettle.TaskCompleted = false;
        kettle.checkNeeded = false;
    }
}
