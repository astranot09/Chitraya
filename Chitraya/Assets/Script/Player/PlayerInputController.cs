using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    [Header("UI")]
    public List<Image> allKeyAbility = new List<Image>();

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
            UpdateAbilityUI();
        }
    }



    public void ChangeToRangedAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            index = 2;
            UpdateAbilityUI();
        }
    }



    public void ChangeToBuildPlatform(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            index = 3;
            UpdateAbilityUI();
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

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            playerMovement.PlayerDash();
        }
    }

    private void UpdateAbilityUI()
    {
        // 1. Reset semua slot ke warna putih, tapi pertahankan alpha aslinya
        foreach (Image x in allKeyAbility)
        {
            if (x == null) continue;

            float originalAlpha = x.color.a;
            x.color = new Color(1f, 1f, 1f, originalAlpha);
        }

        int targetIndex = Index - 1;
        if (targetIndex >= 0 && targetIndex < allKeyAbility.Count)
        {
            Image activeImage = allKeyAbility[targetIndex];
            if (activeImage != null)
            {
                float originalAlpha = activeImage.color.a;
                activeImage.color = new Color(0f, 0f, 1f, originalAlpha);
            }
        }
    }

}
