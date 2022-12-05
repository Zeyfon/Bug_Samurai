using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PSmash.Core;

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

    float healthScale = 1;

    float currentHealth = 0;

    float timerToStartDecreasingBackground = Mathf.Infinity;

    float scaleIncrease = 0;
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
        scaleIncrease = currentHealth / scaleHealth;
    }

    void OnEnable(){
        playerHealth.OnMaxHealthIncrease += IncreaseMaxHealth;
    }

    void OnDisable(){
        playerHealth.OnMaxHealthIncrease -= IncreaseMaxHealth;
    }

    void Update(){

        if(playerHealth.GetHealth()<currentHealth){
            print("Player got hit");
            PlayerJustGotHit();
        }
        else if(playerHealth.GetHealth()>currentHealth)
        {
        healthBarTransform.localScale = new Vector3(currentHealth / scaleHealth, healthBarTransform.localScale.y, healthBarTransform.localScale.z);
        healthBarBackgroundTransform.localScale = new Vector3(currentHealth / scaleHealth, healthBarBackgroundTransform.localScale.y, healthBarBackgroundTransform.localScale.z);
        healthBarCanvas.transform.localScale = new Vector3(((playerHealth.GetHealth()-currentHealth) / scaleHealth) + healthBarCanvas.transform.localScale.x, healthBarCanvas.transform.localScale.y, healthBarCanvas.transform.localScale.z);
        currentHealth = playerHealth.GetHealth();
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
        timerToStartDecreasingBackground = 0;
        currentHealth = playerHealth.GetHealth();
    }

    void IncreaseMaxHealth(float extraHealth){
        print("UI Ready to VFX Increase Max Health");
        PerformVFXMaxHealthIncrease(extraHealth);
        //Perform the max health increase
    }

    void PerformVFXMaxHealthIncrease(float extraHealth){
        healthBarCanvas.transform.localScale =  new Vector3((scaleHealth+extraHealth)/scaleHealth,healthBarCanvas.transform.localScale.y,healthBarCanvas.transform.localScale.z);
        StartCoroutine(HealthIncreaseVFX());
    }

    IEnumerator HealthIncreaseVFX(){
        CanvasGroup canvasGroup = healthBarCanvas.GetComponent<CanvasGroup>();
        print(canvasGroup);
        Fader uiFader = new Fader();
        print("Starting Fading process");
        yield return uiFader.FadeOut(canvasGroup, fadeInTime);
        print("Middel part");
        yield return new WaitForSeconds(idleTime);
        yield return uiFader.FadeIn(canvasGroup,fadeOutTime);

        print("Ending Fading Process");
    }
}
