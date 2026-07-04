using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private PlayerBuildingScript buildingScript;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    
    public void PlayerMeleeAttack()
    {
        playerAttack.MeleeAttack();
    }
    public void PlayerDoneAttack()
    {
        playerAttack.MeleeDoneAttack();
    }
    public void PlayerShootProjectile()
    {
        playerAttack.ShootProjectile();
    }
}
