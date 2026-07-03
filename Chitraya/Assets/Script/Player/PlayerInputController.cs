using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Vector2 mousePosition;


    [Header("Reference")]
    [SerializeField] private PlayerBuildingScript buildingScript;
    [SerializeField] private PlayerMovement playerMovement;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        playerMovement.SetDirection(dir);
        if (ctx.performed)
        {
            playerMovement.SetLastDirection(dir);
        }
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(dir);
    }

    public void OnBuild(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            buildingScript.PlayerBuilding(mousePosition);
        }
    }


    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerMovement.PlayerJump();
        }
    }

}
