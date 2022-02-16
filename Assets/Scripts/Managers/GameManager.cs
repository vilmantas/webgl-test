using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int WinsForVictory = 3;

    public Transform AISpawn;
    
    [NonSerialized]
    public static GameManager Instance;

    public FighterScript PlayerFighter;

    public FighterScript AIFighter;

    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI ProgressText;

    public List<FighterScriptable> Enemies = new();

    private Queue<FighterScriptable> _enemies;
    
    [NonSerialized]
    public static bool IsGameOver = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResultText.gameObject.SetActive(false);

        _enemies = new Queue<FighterScriptable>(Enemies);
        
        PrepareEnemyFighter();
    }

    private int Wins = 0;
    
    // Update is called once per frame
    void Update()
    {
        HandleProgress();
        if (IsGameOver) return;
        if (PlayerFighter.IsDead)
        {
            FinalizeGame();
        }

        if (!AIFighter.IsDead) return;
        
        Wins += 1;
        if (Wins == WinsForVictory)
        {
            FinalizeGame();
        }
        else
        {
            PrepareEnemyFighter();
        }
    }

    private void PrepareEnemyFighter()
    {
        AIFighter.fighterScriptable = _enemies.Dequeue();
        AIFighter = Instantiate(AIFighter, AISpawn);
    }

    private void HandleProgress()
    {
        if (Wins == WinsForVictory || Wins == 0) ProgressText.gameObject.SetActive(false);
        
        if (Wins > 0 && Wins < WinsForVictory && !ProgressText.gameObject.activeSelf) ProgressText.gameObject.SetActive(true);
        
        ProgressText.text = $"{Wins}/{WinsForVictory}";
    }

    public void FinalizeGame()
    {
        ResultText.text = PlayerFighter.IsDead ? "Lost!" : "Won!";
        ProgressText.gameObject.SetActive(false);
        ResultText.gameObject.SetActive(true);
        IsGameOver = true;
    }

    public FighterScript GetAdversaryFor(FighterScript attacker)
    {
        return attacker == PlayerFighter ? AIFighter : PlayerFighter;
    }
}
