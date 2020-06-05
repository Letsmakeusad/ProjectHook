using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level Stats", menuName ="Level/Stats")]
public class LevelStats : ScriptableObject
{
    public Texture2D levelMap;
    public int maxStars;
    public int starsCompleted;
    public bool isCompleted = false;
    public float bestTime;
 
}
