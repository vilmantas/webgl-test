using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [NonSerialized]
    public static GameManager Instance;

    public FighterScript PlayerFighter;

    public FighterScript AIFighter;

    public TextMeshProUGUI Text;

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
        Text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver) return;
        if (PlayerFighter.IsDead || AIFighter.IsDead)
        {
            FinalizeGame();
        }
    }

    public void FinalizeGame()
    {
        Text.text = PlayerFighter.IsDead ? "Lost!" : "Won!";
        Text.gameObject.SetActive(true);
        IsGameOver = true;
    }

    public FighterScript GetAdversaryFor(FighterScript attacker)
    {
        return attacker == PlayerFighter ? AIFighter : PlayerFighter;
    }
}
