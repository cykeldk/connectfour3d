using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PreGameSettings {
    private static int difficultyLevel;
    public static void setDifficulty(int diffLevel)
    {
        difficultyLevel = diffLevel;
    }

    public static int getDifficulty()
    {
        return difficultyLevel;
    }
}
