using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShop : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void StartRepairSound()
    {
        if (!_audioSource.isPlaying) {
            _audioSource.Play();
        }
    }
    public void StopRepairSound()
    {
        _audioSource.Stop();
    }
}
