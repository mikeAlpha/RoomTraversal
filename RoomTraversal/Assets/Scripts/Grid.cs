using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[ExecuteInEditMode]
public class Grid : MonoBehaviour
{

    private int[,] gridArray;

    public List<GameObject> boundaries = new List<GameObject>();
    public List<AssetReference> LevelAssets;

    public List<GameObject> Doors = new List<GameObject>();
    GameObject gridParent , bParents;
    private void Start()
    {
        if(!Application.isPlaying)
          CreateGrid(5, 9, 1);
    }

    public void ChangeLevel(string level)
    {
        foreach (AssetReference l in LevelAssets)
        {
            Debug.Log(l.editorAsset.name);
            if (l.editorAsset.name == level)
            {
                LevelSpawner.instance.DestroyLevelAsset();
                StartCoroutine(LevelSpawner.instance.SpawnLevelInteral(l));
                break;
            }
        }
    }

    void CreateGrid(int width , int height , float Cellsize)
    {
        if (GameObject.Find("GridParent"))
            return;

        gridParent = new GameObject("GridParent");
        bParents = new GameObject("Boundaries");

        gridParent.transform.parent = transform;
        bParents.transform.parent = gridParent.transform;

        gridArray = new int[width, height];

        for(int i = 0; i < gridArray.GetLength(0); i++){
            for(int j = 0; j<gridArray.GetLength(1); j++){
                GameObject g = Resources.Load<GameObject>("Cell");
                Vector3 pos = new Vector3(i * Cellsize, 0.1f, j * Cellsize);
                GameObject c = Instantiate(g, pos, g.transform.rotation, gridParent.transform);

                if (i == gridArray.GetLength(0) - 1 || i == 0) {
                    //boundaries.Add(c);
                    CreateWall(i, j, bParents.transform, false);
                }

                if ((i != 0 && j == 0) || (i != 0 && j == gridArray.GetLength(1) - 1)){
                    //boundaries.Add(c);
                    CreateWall(i, j, bParents.transform, true);
                }

                c.name = i + " " + j;
            }
        }

        boundaries.RemoveAt(boundaries.Count - 1);
        GenerateChest();                   
    }

    void CreateWall(int xIndex , int zIndex, Transform parent, bool xScale)
    {
        GameObject g = Resources.Load<GameObject>("Wall");
        Vector3 pos = new Vector3(xIndex * 1, 0.2f, zIndex * 1);
        GameObject b = Instantiate(g, pos, g.transform.rotation, parent);
        b.name = "Wall" + xIndex + " " + zIndex;
        Vector3 scale = Vector3.zero;

        if (xScale)
            scale = new Vector3(1f, 0.35f, 1f);
        else
            scale = new Vector3(0.35f, 1f, 1f);
        
        b.transform.localScale = scale;
        boundaries.Add(b);
    }

    void GenerateChest()
    {
        GameObject c = Resources.Load<GameObject>("Chest");
        for (int i = 0; i<4; i++)
        {
            Transform rIndx = gridParent.transform.GetChild(Random.Range(1, gridParent.transform.childCount));
            Vector3 pos = new Vector3(rIndx.position.x, 0.12f, rIndx.position.z);
            GameObject b = Instantiate(c, pos, c.transform.rotation, gridParent.transform);
        }
    }
}
