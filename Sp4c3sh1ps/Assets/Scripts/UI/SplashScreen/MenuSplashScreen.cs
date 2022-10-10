using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuSplashScreen : SplashScreen
{
    public static MenuSplashScreen Self { get; private set; }
    
    [SerializeField] private TextMeshProUGUI _lastScoreTmp, _highScoreTmp;
    private Button _startGameButton;

    #region ANIMATION RELATED FIELDS
    private Animator _animator;
    private const string APPEAR_ANIM = "MenuSplash_appear";
    private const string DISAPPEAR_ANIM = "MenuSplash_disappear";
    private Coroutine _disappearCoroutine;
    private readonly WaitForSeconds _hideDelay = new WaitForSeconds(0.5f);
    #endregion

    public event EventHandler GameStarted;

    private void Awake()
    {
        if (Self != null && Self != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Self = this;

            _animator = GetComponent<Animator>();
            _startGameButton = GetComponentInChildren<Button>();
        }
    }
    
    private void OnEnable() => ScoreCounter.ScoreSubmited += OnScoreSubmitted;

    private void OnDisable() => ScoreCounter.ScoreSubmited -= OnScoreSubmitted;
    
    private void Start()
    {
        SetScoreTmp();
        
        _animator.Play(APPEAR_ANIM);
    }

    private void OnScoreSubmitted(object sender, EventArgs e)
    {
        SetScoreTmp();
        
        OpenSelf();
        _animator.Play(APPEAR_ANIM);
        _startGameButton.gameObject.SetActive(true); // activate "game start" button
    }

    public void StartGame()
    {
        if (_disappearCoroutine != null) StopCoroutine(_disappearCoroutine);
        _disappearCoroutine = StartCoroutine(Disappear());
        
        GameStarted?.Invoke(this, EventArgs.Empty);
        _startGameButton.gameObject.SetActive(false); // deactivate "game start" button
    }

    private IEnumerator Disappear()
    {
        _animator.Play(DISAPPEAR_ANIM);
        yield return _hideDelay;
        CloseSelf();
    }

    private void SetScoreTmp()
    {
        _lastScoreTmp.SetText("Last Score: " + PlayerPrefs.GetInt("lastScore"));
        _highScoreTmp.SetText("Highscore: " + PlayerPrefs.GetInt("highscore"));
    }

}