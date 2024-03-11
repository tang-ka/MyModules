using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum MainState { Start, A, B, C, D }

public class WorldManager : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    public async void Init()
    {
        await SceneController.Instance.LoadScene(MainState.Start, LoadSceneMode.Additive, FinishCallback);

        void FinishCallback()
        {
            Debug.Log("Loading StartScene has done");
        }
    }
}
