using Game.Core.Zombies;

using System;
using System.Configuration;

public class SettingsService:IService
{
    #region Public methods

    public int GetListeningPort()
    {
        return GetSettingInt("ListeningPort");
    }
    public int GetReceiveInterval()
    {
        return GetSettingInt("ReceiveInterval");
    }
    public int GetFixedDeltaTimeMs()
    {
        return GetSettingInt("FixedDeltaTimeMs");
    }
    public int GetRoomWidth()
    {
        return GetSettingInt("RoomWidth");
    }
    public int GetRoomHeight()
    {
        return GetSettingInt("RoomHeight");
    }

    public float GetZombieSpeed(ZombieType type)
    {
        return GetSettingFloat(string.Format("{0}ZombieSpeed", type));
    }

    public float GetZombieRadius(ZombieType type)
    {
        return GetSettingFloat(string.Format("{0}ZombieRadius", type));
    }

    #endregion

    #region Private methods

    private int GetSettingInt(string name)
    {
        return Convert.ToInt32(ConfigurationManager.AppSettings[name]);
    }

    private float GetSettingFloat(string name)
    {
        return Convert.ToSingle(ConfigurationManager.AppSettings[name]);
    }

    #endregion
}