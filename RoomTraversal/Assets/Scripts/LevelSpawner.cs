using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelSpawner : MonoBehaviour
{
    public AssetReference LevelPrefab;

    public static LevelSpawner instance;

    private AsyncOperationHandle<GameObject> CurrentGameObjectHandle;

    public AssetReference CurrentLevelPrefab;

    void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        StartCoroutine(SpawnLevelInteral(LevelPrefab));
    }

    public IEnumerator SpawnLevelInteral(AssetReference Level)
    {
        //if (CurrentLevelPrefab != null && CurrentLevelPrefab.Asset.name == Level.Asset.name)
        //    yield return null;
        Debug.Log("Here");
        CurrentLevelPrefab = Level;
        AsyncOperationHandle<GameObject> handle = Level.InstantiateAsync();
        CurrentGameObjectHandle = handle;
        yield return new WaitForSeconds(0.5f);

        //Addressables.Release(handle);
    }

    public void DestroyLevelAsset()
    {
        Addressables.Release(CurrentGameObjectHandle);
    }
}
