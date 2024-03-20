using System.Collections;
using System.Collections.Generic;
using StoneBreaker;
using UnityEngine;

public class Stone : MonoBehaviour {

    [SerializeField]
    StoneType.idType stoneType;

    [SerializeField]
    float setGravitySpeed;
    float gravitySpeed;

    Rigidbody2D stoneRigidbody2D;
    bool isInTheAir;
    
    [SerializeField]
    int stoneID;
    public int StoneID
    {
        get
        {
            return stoneID;
        }
        set
        {
            stoneID = value;
        }
    }

	void Start () {
        stoneRigidbody2D = GetComponent<Rigidbody2D>();
        isInTheAir = true;
	}

    void Update()
    {
        if (isInTheAir)
            gravitySpeed = setGravitySpeed;
        else
            gravitySpeed = 0f;

        stoneRigidbody2D.velocity = new Vector2(stoneRigidbody2D.velocity.x, gravitySpeed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Land")
        {
            isInTheAir = false;
        }
        if (col.collider.tag == "Stone")
        {
            if (stoneID > col.gameObject.GetComponent<Stone>().stoneID)
                isInTheAir = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Land")
        {
            isInTheAir = true;
        }
        if (col.collider.tag == "Stone")
        {
            if (stoneID > col.gameObject.GetComponent<Stone>().stoneID)
                isInTheAir = true;
        }
    }
    public StoneType.idType getStoneType()
    {
        return stoneType;
    }
}