using UnityEngine;

public static class GameProgress
{
    public static bool fireCompleted = false;
    public static bool earthCompleted = false;
    public static bool waterCompleted = false;

    public static bool fireCrystal = false;
    public static bool earthCrystal = false;
    public static bool waterCrystal = false;

    public static bool swordUnlocked = false;
    public static bool shieldUnlocked = false;
    public static bool spellUnlocked = false;

    public static bool worldMapIntroPlayed = false;

    public static bool BossUnlocked()
    {
        return fireCrystal && earthCrystal && waterCrystal;
    }

    public static void ResetProgress()
    {
        fireCompleted = false;
        earthCompleted = false;
        waterCompleted = false;

        fireCrystal = false;
        earthCrystal = false;
        waterCrystal = false;

        swordUnlocked = false;
        shieldUnlocked = false;
        spellUnlocked = false;

        worldMapIntroPlayed = false;
    }
}