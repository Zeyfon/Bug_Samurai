using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] Transform healthBarTransform;
    [SerializeField] Transform healthBarBackgroundTransform;

    [SerializeField] float scaleHealth = 100;
    [SerializeField] float decreasingBarSpeed = 2;
    [SerializeField] float timeToStartDecreasingBackground = 1;

    float healthScale = 1;

    float currentHealth = 0;

    float timerToStartDecreasingBackground = Mathf.Infinity;
    // Start is called before the first frame update
    void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();     
    }

    // Update is called once per frame
    void Start()
    {
        currentHealth = playerHealth.GetHealth();
    }

    void Update(){

        if(playerHealth.GetHealth()<currentHealth){
            print("Player got hit");
            PlayerJustGotHit();
        }

        healthBarTransform.localScale = new Vector3(currentHealth/scaleHealth,healthBarTransform.localScale.y,healthBarTransform.localScale.z);

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
}
