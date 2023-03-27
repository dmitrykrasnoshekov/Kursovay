using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float upLimit = -50;
    [SerializeField] private float downLimit = 50;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float mousSens = 0.25f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactive; //ƒŒ¡¿¬»“‹   œ–≈‘¿¡” Ã¿— ”
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject interactText;

    private Ray interactRay;
    private RaycastHit interactHit;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

    public static Player Instance { get; private set; }
    private BaseInteractiveElement selected;
    private Vector3 lastInteractDir;

    private void Awake()
    {
        Instance = this;
    }
    

    private void Start()
    {
        Cursor.visible = false;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selected != null)
        {
            selected.Interact();
        }
    }
    private void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        InteractRay();
        HandleInteractions();

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

        interactText.SetActive(false);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 1f;

        if (Physics.Raycast(interactRay, out interactHit, interactDistance, interactive))
        {
            Debug.DrawRay(interactRay.origin, interactRay.direction * interactDistance, Color.red);
            if (interactHit.transform.TryGetComponent(out BaseInteractiveElement baseInteractiveElement))
            {
                interactText.SetActive(true);
                if (baseInteractiveElement != selected)
                {
                    selected = baseInteractiveElement;
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

}
