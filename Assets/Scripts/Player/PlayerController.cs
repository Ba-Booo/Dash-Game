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
    private float attackAngle;
    [SerializeField] private Transform mouseTransform;





    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {

        Debug.DrawRay(transform.position, mouseTransform.position - transform.position, Color.red); //지울거

        if( Input.GetKey( KeyCode.LeftShift) )
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
            //Attack();
            SettingAttackRange();
        }

    }



    #region 움직임

    private void NormalMove()
    {
        
        moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;

        transform.position = new Vector2( transform.position.x + moveX, transform.position.y );
       
    }

    private void DashMove()
    {
        if( Input.GetKey( KeyCode.LeftShift) )
        {

            moveX = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
            moveY = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

            transform.position = new Vector2(transform.position.x + moveX, transform.position.y + moveY);
            
        }
        
       
       
    }

    #endregion



    #region 점프

    private void Jump()
    {

        if( Input.GetKeyDown( KeyCode.Space ) )
        {
            rb.AddForce( Vector2.up * jumpPower, ForceMode2D.Impulse );
        }

    }

    #endregion



    #region 공격

    private void Attack()
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


        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackBoxPosition, attackBoxSize, 0);

        foreach(Collider2D collider in colliders)
        {
            if(collider.name == "Reactionary")
            {
                Debug.Log("호옹이");
            }
        }


    }

    private void SettingAttackRange()
    {

        //방향조절
        Vector2 mouseDistance = mouseTransform.position - transform.position;
        attackAngle = Mathf.Atan2(mouseDistance.y, mouseDistance.x);

        //위치
        attackBoxPosition = new Vector2(transform.position.x + (Mathf.Cos(attackAngle) * 2), transform.position.y + (Mathf.Sin(attackAngle) * 2));        //삼각함수 안쓰면 중앙 기준으로 콜라이더 생성됨

        //크기
        int groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseTransform.position - transform.position, 5f, groundLayerMask);

        if (hit)
        {
            attackBoxSize = new Vector2(Vector3.Distance(transform.position, hit.point), 3);
        }
        else
        {
            attackBoxSize = new Vector2(5, 3);
        }
        
        //콜라이더 생성
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackBoxPosition, attackBoxSize, attackAngle * Mathf.Rad2Deg );
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.name == "Reactionary")
            {
                Debug.Log("으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아으아아아");
            }
        }

    }

    // private void SettingAttackRange(float attackRangePosition, float attackRangeSizeX, float attackRangeSizeY)
    // {
    //     attackBoxSize = new Vector2(attackRangeSizeX, attackRangeSizeY);

    //     attackBoxPosition = (Vector2)transform.position + new Vector2(2f, 0);

    //     Collider2D[] colliders = Physics2D.OverlapBoxAll(attackBoxPosition, attackBoxSize, Mathf.Atan2( mouseTransform.position.x, mouseTransform.position.y ) );

    //     foreach (Collider2D collider in colliders)
    //     {
    //         if (collider.name == "Reactionary")
    //         {
    //             Debug.Log("으아아아");
    //         }
    //     }

    // }

    private void OnDrawGizmos()     //테스트테스트테스트테스트
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackBoxPosition, new Vector3(attackBoxSize.x, attackBoxSize.y) );
    }

    #endregion



    // #region 에니메이션
    // void Animation()
    // {

    // }

    // #endregion



}
