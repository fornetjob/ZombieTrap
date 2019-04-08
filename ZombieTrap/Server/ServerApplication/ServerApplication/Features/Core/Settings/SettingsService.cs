using Game.Core;

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
    public float GetFixedDeltaTime()
    {
        return GetSettingFloat("FixedDeltaTime");
    }
    public int GetRoomWidth()
    {
        return GetSettingInt("RoomWidth");
    }
    public int GetRoomHeight()
    {
        return GetSettingInt("RoomHeight");
    }

    public int GetRoomsPerThread()
    {
        return GetSettingInt("RoomsPerThread");
    }

    public int GetItemHealth(ItemType type)
    {
        return GetSettingInt(string.Format("{0}Health", type));
    }

    public float GetItemSpeed(ItemType type)
    {
        return GetSettingFloat(string.Format("{0}Speed", type));
    }

    public float GetItemRadius(ItemType type)
    {
        return GetSettingFloat(string.Format("{0}Radius", type));
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