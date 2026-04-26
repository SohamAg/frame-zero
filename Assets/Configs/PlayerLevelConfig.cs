using UnityEngine;

public enum EquipmentPlacement
{
    None,
    Hand,
    Back
}

[CreateAssetMenu(menuName = "Game/Player Level Config")]
public class PlayerLevelConfig : ScriptableObject
{
    [Header("Actions")]
    public bool canMove = true;
    public bool canJump = true;
    public bool canPickup = false;
    public bool canCast = false;
    public bool canAttack = false;
    public bool canBlock = false;

    [Header("Default Equipment")]
    public EquipmentPlacement defaultSword = EquipmentPlacement.None;
    public EquipmentPlacement defaultShield = EquipmentPlacement.None;

    [Header("After Pickup")]
    public EquipmentPlacement swordAfterPickup = EquipmentPlacement.Back;
    public EquipmentPlacement shieldAfterPickup = EquipmentPlacement.Back;

    [Header("Respawn")]
    public Vector3 respawnPosition = new Vector3(95.1f, 2.5f, 164.9f);
}