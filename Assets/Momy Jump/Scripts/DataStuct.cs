using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MommyJump
{
    public enum GameState
    {
        Starting,
        Playing,
        Gameover
    }

    public enum GameTag
    {
        Platform,
        Player,
        LeftCorner,
        RightCorner,
        Collectable
    }

    public enum PrefKey
    {
        BestScore
    }

    [System.Serializable]
    public class CollectableItem
    {
        public Collectable collectablePrefab;
        [Range(0, 1f)]
        public float spawnRate;
    }
}
