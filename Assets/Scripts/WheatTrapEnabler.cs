using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WheatTrapEnabler: MonoBehaviour
{
    [SerializeField] private GameObject harvestableWheat;
    [SerializeField] private GameObject trapWheat;
    [SerializeField] private bool isTrapEnabled = true;

    private void Start()
    {
        var rng = Random.Range(0f, 1f);
        if (rng-0.001f < 0.0f && isTrapEnabled)
        {
            harvestableWheat.SetActive(false);
            trapWheat.SetActive(true);
        }
    }
}
