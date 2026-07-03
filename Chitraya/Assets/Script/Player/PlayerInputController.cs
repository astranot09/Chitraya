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

    //public void OnLook(InputAction.CallbackContext ctx)
    //{
    //    Vector2 dir = ctx.ReadValue<Vector2>();
    //    mousePosition = Camera.main.ScreenToWorldPoint(dir);
    //}

    public void OnBuild(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            // 1. Ambil posisi pixel mouse di layar saat ini secara real-time
            Vector2 currentScreenPos = Mouse.current.position.ReadValue();

            // 2. Konversi ke posisi dunia (World Point) saat tombol ditekan
            if (Camera.main != null)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(currentScreenPos);
            }

            // 3. Spawn bangunan/angin puyuh
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
