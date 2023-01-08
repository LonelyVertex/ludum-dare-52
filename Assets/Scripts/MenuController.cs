using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private string _gameSceneKey;

    [Space]
    [SerializeField] private float _fadeOutDuration = 0.5f;
    [SerializeField] private Image _fadeImage;
    
    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(HandlePlay);

        var score = PlayerPrefs.GetInt("score", -1);
        _bestScoreText.gameObject.SetActive(score != -1);
        _bestScoreText.text = $"Best score: {score}";
        
        _fadeImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void HandlePlay()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        _fadeImage.gameObject.SetActive(true);

        var color = _fadeImage.color;
        color.a = 0.0f;
        _fadeImage.color = color;
        
        for (var currentFade = 0.0f; currentFade < _fadeOutDuration; currentFade += Time.deltaTime)
        {
            color.a = currentFade / _fadeOutDuration;
            _fadeImage.color = color;

            yield return null;
        }

        color.a = 1.0f;
        _fadeImage.color = color;

        yield return null;

        SceneManager.LoadScene(_gameSceneKey);
    }
}
