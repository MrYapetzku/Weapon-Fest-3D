using System;

[Serializable]
public class GameData
{
    public int Scores;
    public int CurrentLevel;

    public GameData()
    {
        Scores = 0;
        CurrentLevel = 1;
    }
}