using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState State;
    public Player player;
    public int startingPlatform;
    public float xSpawnOffset;
    public float minYSpawnPos;
    public float maxYSpawnPos;
    public Platform[] platformPrefabs;
    public CollectableItem[] collectableItems;

    
    private Platform lastPlatformSpa;
    private List<int> platformLandedId;
    private float haltCamSizeX;
    private int score;

    public Platform LastPlatformSpa { get => lastPlatformSpa; set => lastPlatformSpa = value; }
    public List<int> PlatformLandedId { get => platformLandedId; set => platformLandedId = value; }
    public int Score { get => score; }

    public override void Awake()
    {
        MakeSingleton(false);
        platformLandedId = new List<int>();
        haltCamSizeX = Helper.Get2DCamSize().x / 2;
    }

    public override void Start()
    {
        base.Start();
        State = GameState.Starting;
        Invoke("PlatformInit", 0.5f);

        if (AudioController.Ins)
            AudioController.Ins.PlayBackgroundMusic();
    }

    public void PlayGame()
    {
        if (GUIManager.Ins) GUIManager.Ins.ShowGamePlay(true);
        Invoke("PlayGameIvk", 1f);
    }

    private void PlayGameIvk()
    {
        State = GameState.Playing;
        if (player)
        {
            player.jump();
        }
    }

    private void PlatformInit()
    {
        lastPlatformSpa = player.PlatformLanded;
        for (int i = 0; i < startingPlatform; i++)
            SpawnPlatform(); 
    }

    public bool IsPlatformLanded(int id)
    {
        if(platformLandedId == null || platformLandedId.Count <= 0) return false;

        return platformLandedId.Contains(id);
    }

    public void SpawnPlatform()
    {
        if (!player || platformPrefabs == null || platformPrefabs.Length <= 0) return;

        float spawnPosX = Random.Range(-(haltCamSizeX - xSpawnOffset), (haltCamSizeX - xSpawnOffset));
        float distBetweenPlatf = Random.Range(minYSpawnPos, maxYSpawnPos);
        float spawnPosY = lastPlatformSpa.transform.position.y + distBetweenPlatf;        

        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0f);

        int randIdx = Random.Range(0, platformPrefabs.Length);

        var platfPrefab = platformPrefabs[randIdx];

        if (!platfPrefab) return;

        var platfClone = Instantiate(platfPrefab, spawnPos, Quaternion.identity);
        platfClone.Id = lastPlatformSpa.Id + 1;
        lastPlatformSpa = platfClone;
    }

    public void SpawnCollectable(Transform SpwanPoint)
    {
        if (collectableItems == null || collectableItems.Length <= 0 || State != GameState.Playing) return;

        int randIdx = Random.Range(0, collectableItems.Length);

        var collectItem = collectableItems[randIdx];

        if(collectItem == null) return;

        float randCheck = Random.Range(0, 1f);

        if (randCheck <= collectItem.spawnRate && collectItem.collectablePrefab)
        {
            var cClone = Instantiate(collectItem.collectablePrefab, SpwanPoint.position, Quaternion.identity);
            cClone.transform.SetParent(SpwanPoint);
        }
    }

    public void AddScore(int scoreAdd)
    {
        if(State != GameState.Playing) return;

        score += scoreAdd;

        Pref.bestScore = score;

        if (GUIManager.Ins) GUIManager.Ins.UpdateScore(score);
    }
}
