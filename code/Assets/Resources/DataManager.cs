using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public DataLoader dataStorage;

	private void Awake()
	{
		dataStorage = LoadData();
	}

	//private void Start()
	//{
	//	for (int i = 0; i < avatarData.avatars.Count; i++)
	//	{
	//		Debug.Log("Avatar Name: " + avatarData.avatars[i].AvatarName);
	//		Debug.Log("Avatar Id: " + avatarData.avatars[i].AvatarId);
	//		Debug.Log("Avatar description: " + avatarData.avatars[i].Description);
	//	}
	//}

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
		if (File.Exists(Application.persistentDataPath + Path.DirectorySeparatorChar + "StorageData.txt"))
		{
			data = ScriptableObject.CreateInstance<DataLoader>();
			string json = File.ReadAllText(Application.persistentDataPath + Path.DirectorySeparatorChar + "StorageData.txt");
			JsonUtility.FromJsonOverwrite(json, data);
		}
		else
		{
			data = Resources.Load<DataLoader>("Avatar Data");
		}

		return data;
	}
}