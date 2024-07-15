using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData
{
    //Universal Save and Load Function
    //Player Stats
    public static void SavePlayerStats(PlayerStats playerstats)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.SaveData";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerstats);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerStats()
    {
        string path = Application.persistentDataPath + "/player.SaveData";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream (path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    //Player Inventory

    //Game State (Scene, Position etc.)

}
