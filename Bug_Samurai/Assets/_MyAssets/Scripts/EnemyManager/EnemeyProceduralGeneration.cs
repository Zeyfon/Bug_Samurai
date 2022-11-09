using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyProceduralGeneration : MonoBehaviour
{

    [SerializeField] Transform spawnLocation;
    [SerializeField] GameObject enemyTemplate;

    EnemyParameters parameters;
    // Start is called before the first frame update
    void Awake()
    {

        SetInitialParameters();
        InstantiateEnemy();
    }

    void SetInitialParameters(){
        parameters = GetComponent<EnemyParameters>();
        parameters.detectionColliderSize = new Vector2(Random.Range(5f,15f),3);
        parameters.movementSpeed = Random.Range(1f,4f);
        parameters.hasDefense = HasDefense();
        parameters.quantityOfAttacks = SetQuantityOfAttacks();
        parameters.attackAnimationSpeed = Random.Range(0.1f,3f);
        parameters.canBeInterruptedByAnything = SetCanBeInterruptedByAnything(parameters.hasDefense);
        print("Can be Interrupted" + "  " + parameters.canBeInterruptedByAnything);
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

    void InstantiateEnemy(){
        GameObject enemy = GameObject.Instantiate(enemyTemplate,spawnLocation.position, Quaternion.identity);
        EnemyParameters thisEnemyParameters = enemy.GetComponent<EnemyParameters>();
        SetParametersToEnemy(thisEnemyParameters);
    }

    void SetParametersToEnemy(EnemyParameters thisEnemyParameters){
        thisEnemyParameters.movementSpeed = parameters.movementSpeed;
        thisEnemyParameters.hasDefense = parameters.hasDefense;
        thisEnemyParameters.detectionColliderSize = parameters.detectionColliderSize;
        thisEnemyParameters.quantityOfAttacks = parameters.quantityOfAttacks;
        thisEnemyParameters.attackAnimationSpeed = parameters.attackAnimationSpeed;
        thisEnemyParameters.canBeInterruptedByAnything = parameters.canBeInterruptedByAnything;
    }
}
