using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wave/WaveData")]
public class WaveData : ScriptableObject
{
    public int enemy1Counts;
    public int enemy2Counts;
    public int enemy3Counts;
    public float spawnDelay = 1f;
    public float restTimeBetweenWaves = 3f;
}
