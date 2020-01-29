using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public DataLoader dataStorage;

	private void Awake()
	{
		dataStorage = LoadData();
	}

	private void OnDisable()
	{
		SaveData();
	}

	void SaveData()
	{
		string json = JsonUtility.ToJson(dataStorage);
		File.WriteAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "StorageData.txt", json);
	}

	DataLoader LoadData()
	{
		DataLoader data = null;
        data = ScriptableObject.CreateInstance<DataLoader>();
        data.LoadAllData();
		return data;
	}
}