using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;
//using UnityEngine.ResourceManagement.ResourceLocations;
using System;
using System.IO;
using Cysharp.Threading.Tasks;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] string assetLabel;

    //AsyncOperationHandle downloadHandle;

    string customScenesFilePath;
    string directoryPath;

    public override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        customScenesFilePath = Application.dataPath + "/CustomScenes/";
#elif PLATFORM_ANDROID
        customScenesFilePath = Application.persistentDataPath + "/CustomScenes/";
#endif
        directoryPath = Path.GetDirectoryName(customScenesFilePath);
        //Debug.Log(directoryPath);
    }

    //public async UniTask<List<T>> LoadJsonDataAsync<T>(string assetName)
    //{
    //    List<T> listFromJson = new List<T>();

    //    // ��巹����� �ε��ϱ�
    //    var loadedData = await Addressables.LoadAssetAsync<TextAsset>(assetName);
    //    string jsonData = loadedData.ToString();

    //    // �ҷ��� ������ -> ListJson<T>
    //    ListJson<T> listJson = JsonUtility.FromJson<ListJson<T>>(jsonData);

    //    // listJson�� �����ؼ� ������ ����
    //    for (int i = 0; i < listJson.datas.Count; i++)
    //    {
    //        listFromJson.Add(listJson.datas[i]);
    //    }

    //    return listFromJson;
    //}

    public void SaveJsonLocal<T>(string fileName, T data)
    {
        //Debug.Log("�Լ� SaveJsonLocal �� directoryPath ������: " + directoryPath);

        if (Directory.Exists(directoryPath) == false)
        {
            Directory.CreateDirectory(directoryPath);
            //Debug.Log("�Լ� SaveJsonLocal �� directoryPath ���� ����: " + directoryPath);
        }

        //Debug.Log("�Լ� SaveJsonLocal �� fileName : " + fileName);

        //string filePath = customScenesFilePath + fileName;
        string filePath = directoryPath + fileName;

        //Debug.Log("�Լ� SaveJsonLocal �� filePath : " + filePath);

        string jsonData = JsonUtility.ToJson(data, true);

        //Debug.Log("�Լ� SaveJsonLocal �� jsonData : " + jsonData);

        File.WriteAllText(filePath, jsonData);
    }

    public T LoadJsonLocal<T>(string fileName) where T : new()
    {
        T data = new();
        //string filePath = customScenesFilePath + fileName;
        string filePath = directoryPath + fileName;

        //Debug.Log("�Լ� LoadJsonLocal �� filePath ���� ����: " + filePath);

        if (File.Exists(filePath))
        {
            //Debug.Log("�Լ� LoadJsonLocal �� filePath ������: " + filePath);

            string jsonData = File.ReadAllText(filePath);

            //Debug.Log("�Լ� LoadJsonLocal �� jsonData : " + jsonData);

            data = JsonUtility.FromJson<T>(jsonData);
            return data;
        }

        return default(T);
    }

}
