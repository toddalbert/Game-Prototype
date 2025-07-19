using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private TMP_Text _laserShotsText;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Sprite[] _livesSprites;
    [SerializeField] private Image _thrusterChargeBar;

    private GameManager _gameManager;
    private RectTransform _thrusterChargeBarRect;

    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("The GameManager is null");
        }
        
        _thrusterChargeBarRect = _thrusterChargeBar.GetComponent<RectTransform>();
        if (_thrusterChargeBarRect == null)
        {
            Debug.LogError("The Thruster Charge Bar RectTransform is null");
        }
    }

    public void UpdateScoreText(int playerScore) {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLaserShots(int laserShots) {
        _laserShotsText.text = "Shots: " + laserShots;
    }

    public void UpdateThrusterCharge(float chargePercentage) {
        if (_thrusterChargeBarRect != null) {
            float targetWidth = 100f * chargePercentage; // 100 when full, 0 when empty
            _thrusterChargeBarRect.sizeDelta = new Vector2(targetWidth, _thrusterChargeBarRect.sizeDelta.y);
        }
    }

    public void UpdateLives(int lives) {
        if (lives < 0 || lives > _livesSprites.Length) return;
        _livesImage.sprite = _livesSprites[lives];
        if (lives == 0) {
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
