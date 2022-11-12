using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyProceduralGeneration : MonoBehaviour
{

    [Tooltip("The Enemy Object template used for the spawning. Always use latest version")]
    [SerializeField] GameObject enemyTemplate;

     [Tooltip("The minimum quantity of enemies for a Random Range to spawn at each spawn position")]
    [Range(0,4)]
    [SerializeField] int minEnemyQuantity;

    [Tooltip("The maximum quantity of enemies for a Random Range to spawn at each spawn position")]
    [Range(0,4)]
    [SerializeField] int maxEnemyQuantity;

    [Tooltip("The distance the enemis might spawn away from the current spawn location")]   
    [Range(0,4)]
    [SerializeField]float spawnFromOriginDistanceRange;

    public struct ThisEnemyParameters{
        public Vector2 detectionColliderSize;
        public float movementSpeed;
        public bool hasDefense;
        public int quantityOfAttacks;
        public float attackAnimationSpeed;
        public bool canBeInterruptedByAnything;
    }


    bool isManagerAway=false;
    // Start is called before the first frame update
    void Start()
    {


        int counter = 0;
        Transform[] spawnLocations = GetAllSpawnLocations();
        foreach(Transform spawnLocation in spawnLocations){
            int quantityOfEnemies = Random.Range(minEnemyQuantity,maxEnemyQuantity);
            if(quantityOfEnemies <=0) quantityOfEnemies = 1;
            int innerCounter=0;
            print("Quantit of Enemies " + quantityOfEnemies);
            print(spawnLocation.gameObject.name);
            for(int i=0; i<quantityOfEnemies;i++){

                Vector3 thisEnemySpawnPosition = new Vector3(Random.Range(-spawnFromOriginDistanceRange,spawnFromOriginDistanceRange)+spawnLocation.localPosition.x,spawnLocation.localPosition.y,spawnLocation.localPosition.z);
                if(isManagerAway){

                ThisEnemyParameters parameters = SetInitialParameters();
                InstantiateEnemy(parameters, thisEnemySpawnPosition);
                print("Inner Counter" + ++innerCounter);
                }
                else isManagerAway=true;
            }
            print("Outer Counter" + ++counter);

        }
    }

    Transform[] GetAllSpawnLocations(){
        return GetComponentsInChildren<Transform>();
    }

    ThisEnemyParameters SetInitialParameters(){
        ThisEnemyParameters parameters = new ThisEnemyParameters();
        parameters.detectionColliderSize = new Vector2(Random.Range(5f,15f),3);
        parameters.movementSpeed = Random.Range(1f,4f);
        parameters.hasDefense = HasDefense();
        parameters.quantityOfAttacks = SetQuantityOfAttacks();
        parameters.attackAnimationSpeed = Random.Range(0.1f,3f);
        parameters.canBeInterruptedByAnything = SetCanBeInterruptedByAnything(parameters.hasDefense);
        return parameters;
    }

    bool SetCanBeInterruptedByAnything(bool hasDefense){
        if(hasDefense)
            return false;

        else
            return Random.Range(0f,1f)>0.3f;
    }
    bool HasDefense(){
        return Random.Range(0f,1f)>0.7f;
    }

    int SetQuantityOfAttacks(){
        int quantity = 0;
        if(Random.Range(0f,1f)>0.5f){
            quantity=1;
        }
        else{
            quantity=3;
        }

        return quantity;
    }

    void InstantiateEnemy(ThisEnemyParameters parameters, Vector3 spawnLocation){
        GameObject enemy = GameObject.Instantiate(enemyTemplate,spawnLocation, Quaternion.identity);
        EnemyParameters enemyParameters = enemy.GetComponent<EnemyParameters>();
        SetParametersToEnemy(enemyParameters, parameters);
        SetEnemyColor(enemy,parameters);
    }

    void SetParametersToEnemy(EnemyParameters thisEnemyParameters, ThisEnemyParameters parameters){
        thisEnemyParameters.movementSpeed = parameters.movementSpeed;
        thisEnemyParameters.hasDefense = parameters.hasDefense;
        thisEnemyParameters.detectionColliderSize = parameters.detectionColliderSize;
        thisEnemyParameters.quantityOfAttacks = parameters.quantityOfAttacks;
        thisEnemyParameters.attackAnimationSpeed = parameters.attackAnimationSpeed;
        thisEnemyParameters.canBeInterruptedByAnything = parameters.canBeInterruptedByAnything;
    }

    void SetEnemyColor(GameObject enemy, ThisEnemyParameters parameters){
        SpriteRenderer m_Renderer = enemy.GetComponentInChildren<SpriteRenderer>();
        print(m_Renderer.gameObject.name);

        //Defense guy
        if(parameters.hasDefense){
            print("Blue one");
            m_Renderer.color = Color.blue;
        }
        //Three Attacks Combo Guy
        else if (parameters.quantityOfAttacks>1){
            print("Red one");
            m_Renderer.color = Color.red;
        }
        //Is Easily Interrupted guy
        else if(parameters.canBeInterruptedByAnything){
            print("Green one");
            m_Renderer.color = Color.green;
        }
        //No interrupted easily with 1 attack
        else{
            print("Body Color did not change");
        }
    }
}
