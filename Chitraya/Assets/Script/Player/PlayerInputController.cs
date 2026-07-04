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
    [SerializeField] private PlayerAttack playerAttack;

    [Header("Klik kiri")]
    [SerializeField] private int index = 1;
    public int Index => index;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        playerMovement.SetDirection(dir);
        if (ctx.performed)
        {
            playerMovement.SetLastDirection(dir);
        }
    }

    public void ChangeToMeleeAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            index = 1;
        }
    }



    public void ChangeToRangedAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            index = 2;
        }
    }



    public void ChangeToBuildPlatform(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            index = 3;
        }
    }


    public void ChangeToBuildWindVent(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            index = 4;
        }
    }

    public void OnBuild(InputAction.CallbackContext ctx)
    {
        if(index == 1 ||  index == 2)
        {
            if (ctx.started)
            {
                playerAttack.PlayerAttacking();
            }
        }
        else if(index == 3 || index == 4)
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
    }


    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            playerMovement.PlayerJump();
        }
    }

}
