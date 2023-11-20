using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    public float upForce = 40f;

    private Animator anim;
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private int count;
    public Text countText;
    public Text winText;

    const int NUMOFITEMS = 12;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        count = 0;
        setCountText();
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
            if (Input.GetMouseButton(0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Moving");
            }
    }

    void OnCollisionEnter2D()
    {
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.zombieDied();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }
    }

    void setCountText()
    {
        countText.text = "SCORE: " + count.ToString();
        if (count >= NUMOFITEMS)
        {
            winText.text = "YOU WON!!!";
        }
    }

}
