using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MenuSplashScreen : SplashScreen
{
    public static MenuSplashScreen Self { get; private set; }
    
    [SerializeField] private TextMeshProUGUI lastScoreTmp, highScoreTmp;
    private Animator animator;
    private const string AppearAnim = "MenuSplash_appear";
    private const string DisappearAnim = "MenuSplash_disappear";
    private Coroutine disappearCoroutine;
    private WaitForSeconds hideDelay = new WaitForSeconds(0.5f);
    
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

            animator = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        SetScoreTmp();
        
        animator.Play(AppearAnim);
    }

    private void OnEnable() => ScoreCounter.ScoreSubmited += OnScoreSubmitted;

    private void OnDisable() => ScoreCounter.ScoreSubmited -= OnScoreSubmitted;

    private void OnScoreSubmitted(object sender, EventArgs e)
    {
        SetScoreTmp();
        
        OpenSelf();
        animator.Play(AppearAnim);
    }

    public void StartGame()
    {
        if (disappearCoroutine != null) StopCoroutine(disappearCoroutine);
        disappearCoroutine = StartCoroutine(Disappear());
        
        GameStarted?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator Disappear()
    {
        animator.Play(DisappearAnim);
        yield return hideDelay;
        CloseSelf();
    }

    private void SetScoreTmp()
    {
        lastScoreTmp.SetText("Last Score: " + PlayerPrefs.GetInt("lastScore"));
        highScoreTmp.SetText("Highscore: " + PlayerPrefs.GetInt("highscore"));
    }

}