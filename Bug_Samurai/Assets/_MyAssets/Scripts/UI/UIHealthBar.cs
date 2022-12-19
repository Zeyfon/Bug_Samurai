using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.Fader;

public class UIHealthBar : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] Transform healthBarTransform;
    [SerializeField] Transform healthBarBackgroundTransform;
    [SerializeField] GameObject healthBarCanvas;

    [SerializeField] float scaleHealth = 100;
    [SerializeField] float decreasingBarSpeed = 2;
    [SerializeField] float timeToStartDecreasingBackground = 1;

    [SerializeField] float fadeInTime = 0.5f;
    [SerializeField] float idleTime = 3;
    [SerializeField] float fadeOutTime = 0.5f;

    [SerializeField] float timeToIncreaseHealth = 2f;

    float healthScale = 1;

    float currentHealth = 0;

    float timerToStartDecreasingBackground = Mathf.Infinity;

    float scaleIncrease = 0;

   // float currentMaxHealth;
    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();     
    }

    // Update is called once per frame
    void Start()
    {
        healthBarCanvas.GetComponent<CanvasGroup>().alpha=0;
        currentHealth = playerHealth.GetHealth();
  //      currentMaxHealth = currentHealth;
    }

    void OnEnable(){
        playerHealth.OnHealthIncrease += IncreaseHealth;
    }

    void OnDisable(){
        playerHealth.OnHealthIncrease -= IncreaseHealth;
    }

    void Update(){

        if(playerHealth.GetHealth()<currentHealth){
            PlayerJustGotHit();
        }
        if(timerToStartDecreasingBackground > timeToStartDecreasingBackground && healthBarTransform.localScale.x < healthBarBackgroundTransform.localScale.x){
            healthBarBackgroundTransform.localScale=new Vector3(healthBarBackgroundTransform.localScale.x-(decreasingBarSpeed/100)*Time.deltaTime,healthBarBackgroundTransform.localScale.y,healthBarBackgroundTransform.localScale.z);
        }
        else if(healthBarBackgroundTransform.localScale.x < healthBarTransform.localScale.x){
            healthBarBackgroundTransform.localScale = healthBarTransform.localScale;
        }
        timerToStartDecreasingBackground += Time.deltaTime;
    }

    void PlayerJustGotHit(){
        print("Player got hit");
        timerToStartDecreasingBackground = 0;
        currentHealth = playerHealth.GetHealth();
        healthBarTransform.localScale = new Vector3(currentHealth / scaleHealth, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
    }

    void IncreaseHealth(float healthValueToReach, float initialHealth, bool isMaxHealthIncrease){
        print("UI Ready to VFX Increase Max Health");
        if(isMaxHealthIncrease) StartCoroutine(IncreaseMaxHealthVFX(healthValueToReach, initialHealth));
        else StartCoroutine(IncreaseHealthVFX(healthValueToReach, initialHealth));
    }   

    IEnumerator IncreaseMaxHealthVFX(float healthValueToReach, float initialHealth){
        CanvasGroup canvasGroup = healthBarCanvas.GetComponent<CanvasGroup>();
        healthBarCanvas.transform.localScale = new Vector3((healthValueToReach + 5) / scaleHealth,healthBarCanvas.transform.localScale.y,healthBarCanvas.transform.localScale.z);
        Fader uiFader = new Fader();

        yield return uiFader.FadeOut(canvasGroup, fadeInTime);

        float deltaHealth = healthValueToReach-initialHealth;
        while(initialHealth < healthValueToReach){
            initialHealth += (( deltaHealth * Time.deltaTime) / timeToIncreaseHealth);

            if(initialHealth >= healthValueToReach) initialHealth = healthValueToReach;
            print(initialHealth);

            healthBarTransform.localScale = new Vector3(initialHealth/scaleHealth, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(idleTime);

        yield return uiFader.FadeIn(canvasGroup,fadeOutTime);
    }

    IEnumerator IncreaseHealthVFX(float healthValueToReach, float initialHealth){
        float deltaHealth = healthValueToReach-initialHealth;
        while(initialHealth < healthValueToReach){
            initialHealth += (( deltaHealth * Time.deltaTime) / timeToIncreaseHealth);

            if(initialHealth >= healthValueToReach) initialHealth = healthValueToReach;
            print(initialHealth);

            healthBarTransform.localScale = new Vector3(initialHealth/scaleHealth, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
            yield return new WaitForEndOfFrame();
        }
    }
}
