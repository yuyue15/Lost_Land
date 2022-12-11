public interface ISaveavle
{
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);

    }
    GameSaveData GenerateSaveDara();
    void RestorGameData(GameSaveData data);

}
