using System.Collections;
using UnityEngine;
using Systems.Fader;

public class UIHealthBar : MonoBehaviour
{
    [Header("UI GameObjects")]
    [SerializeField] Transform _healthBarTransform;
    [SerializeField] Transform _healthBarBackgroundTransform;
    [SerializeField] Transform _healthBarCanvas;
    [Header("Constants")]
    [SerializeField] float ScaleHealth = 100;
    [Range(0,1)]
    [SerializeField] float DecreasingBackgroundSpeed = 0.1f;
    [SerializeField] float TimeToStartDecreasingBackground = 1;
    [SerializeField] float FadeInTime = 0.5f;
    [SerializeField] float IdleTime = 3;
    [SerializeField] float FadeOutTime = 2f;
    [SerializeField] float TimeToIncreaseHealth = 2f;

    PlayerHealth _playerHealth;
    float timer = 1000; // Number larger than 1

    private void Awake()
    {
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
    }
    void Start()
    {
        _healthBarCanvas.GetComponent<CanvasGroup>().alpha=0;
    }

    void OnEnable(){
        _playerHealth.OnMaxHealthIncrease += IncreaseMaxHealth;
        _playerHealth.OnPlayerDamaged += DecreaseHealth;
        _playerHealth.OnHealthRegen += RegenHealth;
    }

    void OnDisable(){
        _playerHealth.OnMaxHealthIncrease -= IncreaseMaxHealth;
        _playerHealth.OnPlayerDamaged -= DecreaseHealth;
        _playerHealth.OnHealthRegen -= RegenHealth;
    }

    void Update(){
        if(IsTimerAfterHitFinished() && IsHealthShorterThanBackground()){
            _healthBarBackgroundTransform.localScale=new Vector3(
                _healthBarBackgroundTransform.localScale.x -
                    DecreasingBackgroundSpeed * Time.deltaTime,
                _healthBarBackgroundTransform.localScale.y,
                _healthBarBackgroundTransform.localScale.z);
        }
        timer += Time.deltaTime;
    }

    void DecreaseHealth(){
        //print("Player got hit");
        timer = 0;
        _healthBarTransform.localScale = new Vector3(
            _playerHealth.GetHealth() / ScaleHealth,
            _healthBarTransform.localScale.y,
            _healthBarTransform.localScale.z);
    }

    bool IsTimerAfterHitFinished()
    {
        return timer > TimeToStartDecreasingBackground;
    }

    bool IsHealthShorterThanBackground()
    {
        return _healthBarTransform.localScale.x
            <_healthBarBackgroundTransform.localScale.x;
    }

    void IncreaseMaxHealth(float newHealth, float initialHealth)
    {
        //print("UI Ready to VFX Increase Max Health");
            StartCoroutine(IncreaseMaxHealthVFX(
                newHealth, 
                initialHealth));
    }   

    void RegenHealth(float regenAmount, float initialHealth)
    {
        float finalHealth = initialHealth + regenAmount;
            StartCoroutine(IncreaseHealthVFX(
                finalHealth,
                initialHealth));
    }

    IEnumerator IncreaseMaxHealthVFX(
        float _finalHealth, 
        float _currentHealth)
    {
        CanvasGroup canvasGroup = _healthBarCanvas.GetComponent<CanvasGroup>();
        SetNewHealthInBar(_healthBarCanvas, (_finalHealth + 5));
        Fader uiFader = new Fader();
        yield return uiFader.FadeOut(canvasGroup, FadeInTime);
        yield return IncreaseHealthVFX(_finalHealth,_currentHealth);
        yield return new WaitForSeconds(IdleTime);
        yield return uiFader.FadeIn(canvasGroup,FadeOutTime);
    }

    IEnumerator IncreaseHealthVFX(
        float _finalHealth, 
        float _currentHealth)
    {
        float _deltaHealth = _finalHealth-_currentHealth;
        while(_currentHealth < _finalHealth)
        {
            _currentHealth += (_deltaHealth / TimeToIncreaseHealth)
                *Time.deltaTime;
            if(_currentHealth >= _finalHealth) 
                _currentHealth = _finalHealth;
            SetNewHealthInBar(_healthBarTransform, _currentHealth);
            SetNewHealthInBar(_healthBarBackgroundTransform, _currentHealth);
            yield return new WaitForEndOfFrame();
        }
    }

    void SetNewHealthInBar(Transform _bar, float _newHealth)
    {
        _bar.localScale = new Vector3(
            _newHealth / ScaleHealth,
            _bar.localScale.y,
            _bar.localScale.z
            );
    }
}
