using UnityEngine;
using TMPro;

public class UIMainScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    public int CurrentScores { get; private set; }

    private void OnDisable()
    {
        GameManager.Instance.PlayerShark.PlayerSharkTrigger.HasEatenPeople -= IncrementScore;
    }
    private void Start()
    {
        GameManager.Instance.PlayerShark.PlayerSharkTrigger.HasEatenPeople += IncrementScore;
        CurrentScores = GetScores();
    }


    private void IncrementScore()
    {
        CurrentScores++;
        _scoreText.text = CurrentScores.ToString();  
    }

    private int GetScores() => int.Parse(_scoreText.text);
}
