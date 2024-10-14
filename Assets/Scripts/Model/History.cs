[System.Serializable]
public class History
{
    public int killedEnemies;
    public string survived;
    public int levelReached;
    public string timestamp;

    public History(int killedEnemies, string survived, int levelReached, string timestamp)
    {
        this.killedEnemies = killedEnemies;
        this.survived = survived;
        this.levelReached = levelReached;
        this.timestamp = timestamp;
    }
}