using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Sprite[] _livesSprites;

    private GameManager _gameManager;

    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        UpdateScoreText(0);
        UpdateLives(3);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("The GameManager is null");
        }
    }

    public void UpdateScoreText(int playerScore) {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int lives) {
        _livesImage.sprite = _livesSprites[lives];
        if (lives <= 0) {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(FlickerText());
    }

    IEnumerator FlickerText()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
