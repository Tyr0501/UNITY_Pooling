using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class ParentSpawnController : MonoBehaviour
{
    [SerializeField] public string namePooling;
    [Header("Prefab Object")]
    [SerializeField] public bool testSpawns;
    [SerializeField] public GameObject prefabObj;

    [Header("Camera")]
    [SerializeField] public bool isLimitedCameraArea;

    [Header("Collide")]
    [SerializeField] public bool isCollide;
    [SerializeField] public string nameDestinationTag;

    [Header("Distance")]
    [SerializeField] public bool y;
    [SerializeField] public bool x;
    [SerializeField] public float limitedDistance;


    List<GameObject> _poolObject = new List<GameObject>();
    Vector3 minBounds, maxBounds;
    private GameObject baseObject;

    private void Start()
    {
        if (testSpawns)
        {
            StartCoroutine(spawnDemo());
        }

        baseObject = new GameObject(namePooling);
        baseObject.transform.SetParent(transform);

    }
    IEnumerator spawnDemo()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            SpawnedObj(transform.position);

        }
    }

    public void SpawnedObj(Vector3 pos)
    {
        GameObject obj;
        if (isLimitedCameraArea)
        {
            InitBoundsCamera();
            obj = GetObj();
            obj.GetComponent<ChildObjectSpawner>().SetLimitedCamera(maxBounds.x, minBounds.x, maxBounds.y, minBounds.y);
            obj.transform.position = pos;
        }
        else
        {
            if (isCollide)
            {
                if (nameDestinationTag.Equals("") || nameDestinationTag == null)
                {
                    Debug.Log("Please add tag!");
                    return;
                }
                else
                {
                    obj = GetObj();
                    obj.GetComponent<ChildObjectSpawner>().SetTag(nameDestinationTag);
                    obj.transform.position = pos;
                }
            }
            else
            {
                if (limitedDistance == 0)
                {
                    Debug.Log("Value cannot be zero!");
                    return;
                }
                else
                {
                    obj = GetObj();
                    obj.GetComponent<ChildObjectSpawner>().SetLimitedDistance(limitedDistance, y, x);
                    obj.transform.position = pos;
                }
            }
        }

    }
    private void InitBoundsCamera()
    {
        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector2(0f, 0f));
        maxBounds = cam.ViewportToWorldPoint(new Vector2(1f, 1f));
    }


    private GameObject GetObj()
    {
        foreach (GameObject obj in _poolObject.Where(obj => !obj.activeInHierarchy))
        {
            obj.SetActive(true);
            return obj;
        }
        GameObject newObj;
        newObj = Instantiate(prefabObj, transform.position, Quaternion.identity);

        newObj.transform.SetParent(baseObject.transform);
        _poolObject.Add(newObj);
        return newObj;
    }
}



