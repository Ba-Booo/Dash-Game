using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;

    //normal move
    [SerializeField] private float playerSpeed;
    private float moveX;
    private float moveY;

    //jump
    [SerializeField] private float jumpPower;
    

    //attack
    private Vector2 attackBoxSize;
    private Vector3 attackBoxPosition;
    [SerializeField] private Transform mouseTransform;





    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        if( Input.GetKey( KeyCode.LeftShift))
        {
            DashMove(); //보류
        }
        else
        {
            NormalMove();
            Jump();
        }

        if( Input.GetMouseButtonDown(0) )
        {
            Attack();
        }

    }



    #region 움직임

    void NormalMove()
    {
        
        moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.position = new Vector2( transform.position.x + moveX, transform.position.y );
       
    }

    void DashMove()
    {
        
        moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        transform.position = new Vector2( transform.position.x + moveX, transform.position.y + moveY );
       
    }

    #endregion



    #region 점프

    void Jump()
    {

        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            rb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
        }

    }

    #endregion



    #region 공격

    void Attack()
    {

        //마우스 위치에따라 공격 방향 바뀜
        if (transform.position.x <= mouseTransform.position.x)
        {
            attackBoxPosition = (Vector2)transform.position + new Vector2(2f, 0);
        }
        else
        {
            attackBoxPosition = (Vector2)transform.position + new Vector2(-2f, 0);
        }
        
        //공격 범위
        attackBoxSize = new Vector2(5, 3);


        Collider2D[] colliders = Physics2D.OverlapBoxAll( attackBoxPosition, attackBoxSize, 0 );

        foreach (Collider2D collider in colliders)
        {
            if(collider.name == "Reactionary")
            {
                Debug.Log("각별 시발쉑");
            }
        }
        

    }

    private void OnDrawGizmos()     //테스트테스트테스트테스트
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackBoxPosition, new Vector3(attackBoxSize.x, attackBoxSize.y, 0) );
    }

    #endregion



    // #region 에니메이션
    // void Animation()
    // {

    // }

    // #endregion



}
