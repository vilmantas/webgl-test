using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [NonSerialized]
    public static GameManager Instance;

    public FighterScript PlayerFighter;

    public FighterScript AIFighter;

    public TextMeshProUGUI ResultText;
    public TextMeshProUGUI ProgressText;

    [NonSerialized]
    public bool IsGameOver = false;

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

        if (AIFighter.IsDead)
        {
            Wins += 1;
            if (Wins == 3)
            {
                FinalizeGame();
            }
            else
            {
                AIFighter = Instantiate(AIFighter);
            }
        }
    }

    private void HandleProgress()
    {
        if (Wins is 3 or 0) ProgressText.gameObject.SetActive(false);
        
        if (Wins is > 0 and < 3 && !ProgressText.gameObject.activeSelf) ProgressText.gameObject.SetActive(true);
        
        ProgressText.text = $"{Wins}/3";
    }

    public void FinalizeGame()
    {
        ResultText.text = PlayerFighter.IsDead ? "Lost!" : "Won!";
        ResultText.gameObject.SetActive(true);
        IsGameOver = true;
    }

    public FighterScript GetAdversaryFor(FighterScript attacker)
    {
        return attacker == PlayerFighter ? AIFighter : PlayerFighter;
    }
}
