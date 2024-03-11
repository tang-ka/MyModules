using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    private Dictionary<MainState, string> sceneNameDic = new Dictionary<MainState, string>()
    {
        { MainState.Start, "StartScene" },
        { MainState.A, "Scene_A" },
        { MainState.B, "Scene_B" },
        { MainState.C, "Scene_C" },
        { MainState.D, "Scene_D" },
    };

    private bool isInit = false;

    protected override void Awake()
    {
        if (!isInit)
            Init();
    }

    public void Init()
    {
        Debug.Log("SceneController initialize done");
        isInit = true;
    }

    public async UniTask LoadScene(MainState state, LoadSceneMode mode, Action finishCallback = null)
    {
        if (!isInit)
            Init();

        if (!sceneNameDic.ContainsKey(state) || IsSceneLoaded(sceneNameDic[state]))
        {
            finishCallback?.Invoke();
            return;
        }

        await SceneManager.LoadSceneAsync(sceneNameDic[state], mode);
        finishCallback?.Invoke();
    }

    public async UniTask LoadScene(string sceneName, LoadSceneMode mode, Action finishCallback = null)
    {
        await SceneManager.LoadSceneAsync(sceneName, mode);
        finishCallback?.Invoke();
    }

    public void UnloadScene(MainState state, Action finishCallback = null)
    {
        if (sceneNameDic.ContainsKey(state) && IsSceneLoaded(sceneNameDic[state]))
            SceneManager.UnloadSceneAsync(sceneNameDic[state]).completed += FinishUnload;

        void FinishUnload(AsyncOperation op)
        {
            if (op.isDone)
            {
                finishCallback?.Invoke();
                op.completed -= FinishUnload;
            }
        }
    }

    public async UniTask UnloadScene(string sceneName, Action finishCallback = null)
    {
        if (IsSceneLoaded(sceneName))
            await SceneManager.UnloadSceneAsync(sceneName);

        finishCallback?.Invoke();
    }

    
    private bool IsSceneLoaded(string sceneName)
    {
        return SceneManager.GetSceneByName(sceneName).isLoaded;
    }
}
