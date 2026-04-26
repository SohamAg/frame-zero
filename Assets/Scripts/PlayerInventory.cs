using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasSword = false;
    public bool hasShield = false;
    public bool hasSpell = false;

    public void GetSword()
    {
        hasSword = true;
        Debug.Log("Player got Sword");
    }

    public void GetShield()
    {
        hasShield = true;
        Debug.Log("Player got Shield");
    }

    public void GetSpell()
    {
        hasSpell = true;
        Debug.Log("Player got Spell");
    }
}