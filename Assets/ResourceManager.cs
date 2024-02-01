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

    //    // 어드레서블로 로드하기
    //    var loadedData = await Addressables.LoadAssetAsync<TextAsset>(assetName);
    //    string jsonData = loadedData.ToString();

    //    // 불러온 데이터 -> ListJson<T>
    //    ListJson<T> listJson = JsonUtility.FromJson<ListJson<T>>(jsonData);

    //    // listJson을 참고해서 데이터 삽입
    //    for (int i = 0; i < listJson.datas.Count; i++)
    //    {
    //        listFromJson.Add(listJson.datas[i]);
    //    }

    //    return listFromJson;
    //}

    public void SaveJsonLocal<T>(string fileName, T data)
    {
        //Debug.Log("함수 SaveJsonLocal 의 directoryPath 존재함: " + directoryPath);

        if (Directory.Exists(directoryPath) == false)
        {
            Directory.CreateDirectory(directoryPath);
            //Debug.Log("함수 SaveJsonLocal 의 directoryPath 존재 안함: " + directoryPath);
        }

        //Debug.Log("함수 SaveJsonLocal 의 fileName : " + fileName);

        //string filePath = customScenesFilePath + fileName;
        string filePath = directoryPath + fileName;

        //Debug.Log("함수 SaveJsonLocal 의 filePath : " + filePath);

        string jsonData = JsonUtility.ToJson(data, true);

        //Debug.Log("함수 SaveJsonLocal 의 jsonData : " + jsonData);

        File.WriteAllText(filePath, jsonData);
    }

    public T LoadJsonLocal<T>(string fileName) where T : new()
    {
        T data = new();
        //string filePath = customScenesFilePath + fileName;
        string filePath = directoryPath + fileName;

        //Debug.Log("함수 LoadJsonLocal 의 filePath 존재 안함: " + filePath);

        if (File.Exists(filePath))
        {
            //Debug.Log("함수 LoadJsonLocal 의 filePath 존재함: " + filePath);

            string jsonData = File.ReadAllText(filePath);

            //Debug.Log("함수 LoadJsonLocal 의 jsonData : " + jsonData);

            data = JsonUtility.FromJson<T>(jsonData);
            return data;
        }

        return default(T);
    }

}
