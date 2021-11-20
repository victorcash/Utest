using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public static class SuperSerializer
{
    private static BinaryFormatter superFormatter;

    public static void SerializeObject(object data, string fileName)
    {
        if (!Directory.Exists("GameData")) Directory.CreateDirectory("GameData");
        FileStream file = new FileStream("GameData/" + fileName, FileMode.Create);
        GetFormatter().Serialize(file, data);
        file.Close();
    }

    public static T DeserializeObject<T>(string fileName) where T : new()
    {
        if (!Directory.Exists("GameData")) Directory.CreateDirectory("GameData");
        FileStream file = new FileStream("GameData/" + fileName, FileMode.OpenOrCreate);
        if (file.Length != 0)
        {
            T data = (T)GetFormatter().Deserialize(file);
            file.Close();
            return data;
        }
        else
        {
            file.Close();
            return new T();
        }
    }

    public static BinaryFormatter GetFormatter()
    {
        if (superFormatter == null)
        {
            superFormatter = new BinaryFormatter();

            BinaryFormatter bf = new BinaryFormatter();
            SurrogateSelector ss = new SurrogateSelector();
            Vector3SerializationSurrogate v3ss = new Vector3SerializationSurrogate();
            QuaternionSerializationSurrogate qss = new QuaternionSerializationSurrogate();

            ss.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3ss);
            ss.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), qss);

            superFormatter.SurrogateSelector = ss;
        }

        return superFormatter;
    }
}