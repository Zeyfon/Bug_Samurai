using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyProceduralGeneration : MonoBehaviour
{

    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject enemyTemplate;

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
        Transform[] spawnLocations = GetAllSpawnLocations();
        foreach(Transform spawnLocation in spawnLocations){
            if(isManagerAway){
            print(spawnLocation.gameObject.name);
            ThisEnemyParameters parameters = SetInitialParameters();
            InstantiateEnemy(parameters, spawnLocation);
            }
            else isManagerAway=true;

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

    void InstantiateEnemy(ThisEnemyParameters parameters, Transform spawnLocation){
        GameObject enemy = GameObject.Instantiate(enemyTemplate,spawnLocation.position, Quaternion.identity);
        EnemyParameters enemyParameters = enemy.GetComponent<EnemyParameters>();
        SetParametersToEnemy(enemyParameters, parameters);
    }

    void SetParametersToEnemy(EnemyParameters thisEnemyParameters, ThisEnemyParameters parameters){
        thisEnemyParameters.movementSpeed = parameters.movementSpeed;
        thisEnemyParameters.hasDefense = parameters.hasDefense;
        thisEnemyParameters.detectionColliderSize = parameters.detectionColliderSize;
        thisEnemyParameters.quantityOfAttacks = parameters.quantityOfAttacks;
        thisEnemyParameters.attackAnimationSpeed = parameters.attackAnimationSpeed;
        thisEnemyParameters.canBeInterruptedByAnything = parameters.canBeInterruptedByAnything;
    }
}
