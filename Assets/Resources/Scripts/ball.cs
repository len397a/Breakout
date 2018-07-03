using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball : MonoBehaviour
{

    private bool locked = true;
    private bool gameover = false;
    private Vector3 moveDirection;
    private int blockCount = 0;
    private int hp = 3;

    //private GameObject win, lose;

    public float moveSpeed = 5;

    // Use this for initialization
    void Start()
    {
        //lose = GameObject.Find("You Lose");
        //win = GameObject.Find("You Win");
        //win.SetActive(false);
        //lose.SetActive(false);

        //GameObject go1 = new GameObject();
        //SpriteRenderer renderer1 = go1.AddComponent<SpriteRenderer>();
        //go1.layer = 11;
        //go1.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        //renderer1.sprite = Resources.Load("Sprites/nep", typeof(Sprite)) as Sprite;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentPosition = transform.position;

        if (locked)
        {
            currentPosition = GameObject.Find("player").transform.position;
            currentPosition.y = -4.25F;
            this.transform.position = currentPosition;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && !gameover)
            {
                locked = false;
                moveDirection.x = Random.Range(-1.0F, 1.0F);
                moveDirection.y = Random.Range(0.1F, 1.0F);
                moveDirection.z = 0;
                moveDirection.Normalize();

                Debug.Log("go");
            }
        }
        else
        {
            //Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //moveDirection = moveToward - currentPosition;
            //moveDirection.y = 1;
            //moveDirection.z = 0;
            //moveDirection.Normalize();

            Vector3 target = moveDirection * moveSpeed + currentPosition;
            transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);


            //sicherheit fuer colission bug
            if (currentPosition.x > 8.65F && moveDirection.x > 0)
            {
                moveDirection.x = -moveDirection.x;
            }
            if (currentPosition.x < -8.65F && moveDirection.x < 0)
            {
                moveDirection.x = -moveDirection.x;
            }

            if (currentPosition.y > 4.725 && moveDirection.y > 0)
            {
                moveDirection.y = -moveDirection.y;
            }
            //if (currentPosition.y < -4.725 && moveDirection.y < 0)
            //{
            //    moveDirection.y = -moveDirection.y;
            //}
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(gameover && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector3 contactPoint = coll.contacts[0].point;
        if (coll.gameObject.CompareTag("Block"))
        {
            bool right = contactPoint.x != transform.position.x;
            bool top = contactPoint.y != transform.position.y;
            bool left = contactPoint.x != transform.position.x;
            bool bottom = contactPoint.y != transform.position.y;
            if (top || bottom)
            {
                moveDirection.y = -moveDirection.y;
            }
            if (right || left)
            {
                moveDirection.x = -moveDirection.x;
            }
            coll.gameObject.SetActive(false);
            blockCount++;
            moveSpeed += 0.1F;
            //Debug.Log("ball:" + transform.position + " Contact: " + contactPoint + " block: " + coll.transform.position);
            if(blockCount == 100)
            {
                locked = true;
                gameover = true;
                //win.SetActive(true);
                GameObject go = new GameObject();
                SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                go.layer = 11;
                go.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                renderer.sprite = Resources.Load("Sprites/You Win", typeof(Sprite)) as Sprite;
            }
        }

        if (coll.gameObject.CompareTag("Player"))
        {
            moveDirection.y = transform.position.y - coll.transform.position.y;
            if (moveDirection.y <= 0.4)
            {
                moveDirection.x = transform.position.x - coll.transform.position.x;
            }
            else
            {
                if (moveDirection.x > 0)
                {
                    moveDirection.x = Mathf.Abs(transform.position.x - coll.transform.position.x);
                }
                else
                {
                    moveDirection.x = -Mathf.Abs(transform.position.x - coll.transform.position.x);
                }
            }
            //Debug.Log(moveDirection.y);

            moveDirection.z = 0;
            moveDirection.Normalize();

            //moveDirection.y = -moveDirection.y;
        }

        if (coll.gameObject.CompareTag("Background"))
        {
            //Debug.Log("ball:" + transform.position + " Contact: " + contactPoint + " bg: " + coll.transform.position);

            bool right = contactPoint.x != transform.position.x;
            bool top = contactPoint.y != transform.position.y && contactPoint.y > -5;
            bool left = contactPoint.x != transform.position.x;
            bool bottom = contactPoint.y <= -4.99;
            if (top)
            {
                moveDirection.y = -moveDirection.y;
            }
            if (right || left)
            {
                moveDirection.x = -moveDirection.x;
            }
            if(bottom)
            {
                locked = true;
                moveSpeed = 5;
                hp--;
                Debug.Log("HP:"+hp);
                if(hp == 0)
                {
                    //lose.SetActive(true);
                    gameover = true;
                    GameObject go = new GameObject();
                    SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
                    go.layer = 11;
                    go.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                    renderer.sprite = Resources.Load("Sprites/You Lose", typeof(Sprite)) as Sprite;
                }
            }
        }

    }

    //public int getBlockCount()
    //{
    //    return blockCount;
    //}
    public int getHp()
    {
        return hp;
    }

}
