using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir;
    public LayerMask detectLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir = Vector2.right;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir = Vector2.left;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDir = Vector2.up;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDir = Vector2.down;

        if(moveDir != Vector2.zero)
        {
            
            if (CanMoveToDir(moveDir))
            {
                // P G G
                GameManager.Instance.Save();
                Move(moveDir);
            }
            else
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDir, 2.0f, detectLayer);
                Debug.Log(hits.Length);
                if (hits[0].collider.GetComponent<Wall>() != null || (hits.Length > 1 && hits[1].collider.GetComponent<Wall>() != null))
                {
                    // P W * or P * W
                    // play action or sound
                }
                else
                {
                    if (hits.Length == 2)
                    {
                        // P B B
                        GameManager.Instance.Save();
                        Move(moveDir);
                        Box b0 = hits[0].collider.GetComponent<Box>();
                        Box b1 = hits[1].collider.GetComponent<Box>();
                        b0.Move(moveDir);
                        b1.Move(moveDir * -2.0f);
                    }
                    else
                    {
                        // P B G or P G B
                        Box b0 = hits[0].collider.GetComponent<Box>();
                        float dist = Vector3.Distance(transform.position, b0.transform.position);
                        if (dist > 1.5)
                        {
                            // P G B
                            GameManager.Instance.Save();
                            Move(moveDir);
                            b0.Move(moveDir * -2.0f);
                        }
                        else
                        {
                            // P B G
                            GameManager.Instance.Save();
                            Move(moveDir);
                            b0.Move(moveDir);
                        }
                    }
                }
            }
        }

        moveDir = Vector2.zero;
    }

    bool CanMoveToDir(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 2.0f, detectLayer);

        if (!hit)
            return true;
        else
        {
            return false;
        }
    }


    void Move(Vector2 dir)
    {
        transform.Translate(dir);
    }
}
