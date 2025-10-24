using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    // 체력
    [SerializeField] int enemyMaxHp;
    [SerializeField] int enemyNowHp;

    // 거리
    float distance;
    [SerializeField] float viewingRange;
    [SerializeField] float attackRange;
    [SerializeField] Transform target;

    // 속도
    [SerializeField] float enemySpeed;

    // 공격
    [SerializeField]
    float attackRate;
    float nextAttectTime;


    void EnemyMove()
    {

        distance = Vector2.Distance( target.position, transform.position );

        //ai
        if( distance <= viewingRange && distance >= attackRange )
        {
            transform.position = Vector2.MoveTowards( transform.position, target.position, enemySpeed * Time.deltaTime );
        }

    }

}