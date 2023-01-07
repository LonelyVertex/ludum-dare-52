using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject bloodPrefab;
    [SerializeField] private FlowManager flowManager;
    [SerializeField] private AudioSource dieAudioSource;
    public void KillEnemy(Ennemy enemy)
    {
        dieAudioSource.PlayOneShot(dieAudioSource.clip);
        enemy.Kill();
        var blood = Instantiate(bloodPrefab, enemy.gameObject.transform.position, Quaternion.identity);
        flowManager.AddTime(2);
        StartCoroutine(ResetEnemy(enemy, blood));
    }

    // Update is called once per frame
    IEnumerator ResetEnemy(Ennemy enemy, GameObject blood)
    {
        yield return new WaitForSecondsRealtime(3);
        enemy.ResetEnemy();
        DestroyImmediate(blood);
    }
}
