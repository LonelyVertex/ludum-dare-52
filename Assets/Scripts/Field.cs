#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] List<HarvestableCrop> _cropPrefabsToGenerate;
    [SerializeField] Collider _colliderToGenerate;
    [SerializeField] float _roomForCrop;
    [SerializeField] List<HarvestableCrop> _currentlyGeneratedCrops;

    [ContextMenu("RegenerateField")]
    private void GenerateCrops()
    {
        //Clean
        foreach(var crop in _currentlyGeneratedCrops)
        {
            DestroyImmediate(crop.gameObject);
        }

        _currentlyGeneratedCrops.Clear();

        var bounds = _colliderToGenerate.bounds;
        var sizeX = bounds.size.x;
        var sizeZ = bounds.size.z;
        int numberOfRows = (int)(sizeX / _roomForCrop);
        int numberOfColumns = (int)(sizeZ / _roomForCrop);
        for(int i = 0; i < numberOfRows; i++)
        {
            for(int j = 0; j < numberOfColumns; j++)
            {
                float posX = transform.position.x - bounds.size.x / 2 + i * _roomForCrop + Random.Range(0.0f, 1.0f) * _roomForCrop;
                float posZ = transform.position.y - bounds.size.z / 2 + j * _roomForCrop + Random.Range(0.0f, 1.0f) * _roomForCrop;
                var cropToGenerate = _cropPrefabsToGenerate[Random.Range(0, _cropPrefabsToGenerate.Count)];
                var currentInstance = PrefabUtility.InstantiatePrefab(cropToGenerate, transform) as HarvestableCrop;
                var position = currentInstance.transform.position;
                position.x = posX;
                position.z = posZ;
                currentInstance.transform.position = position;
                _currentlyGeneratedCrops.Add(currentInstance);
            }
        }
        EditorUtility.SetDirty(this);
    }
}
#endif