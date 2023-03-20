using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float upLimit = -50;
    [SerializeField] private float downLimit = 50;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float mousSens = 0.25f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactive; //ÄÎÁÀÂÈÒÜ Ê ÏÐÅÔÀÁÓ ÌÀÑÊÓ
    [SerializeField] private GameObject Camera;
    private Vector3 lastInteractDir;

    private void Start()
    {
        Cursor.visible = false;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        // Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        // Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        Vector3 moveDir = transform.forward;
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 1f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, interactive))
        {
            if (raycastHit.transform.TryGetComponent(out BaseInteractiveElement baseInteractiveElement))
            {
                baseInteractiveElement.Interact();
            }
        }
    }
    private void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleInteractions();

    }

    private void HandleMovement() //Âûçûâàåòñÿ â Update
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);


    }

    private void HandleInteractions() //Âûçûâàåòñÿ â Update
    {
        Vector3 moveDir = transform.forward;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, interactive))
        {
            if (raycastHit.transform.TryGetComponent(out BaseInteractiveElement baseInteractiveElement))
            {
                //ìîæíî âçàèìîäåéñòâîâàòü
            }
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
