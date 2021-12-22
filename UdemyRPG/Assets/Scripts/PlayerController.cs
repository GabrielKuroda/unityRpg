using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public float moveSpeed;

    public Animator myAnim;

    public static PlayerController instance;

    public string areaTransitionName;

    public Vector3 bottomLeftLimit;
    public Vector3 topRightLimit;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start() 
    {
        //Verifica se existe um player
        if(instance == null)
        {
            //Instancia um Player
            instance = this;
        } else
        {
            if(instance != this)
            {
                //Destroi o objeto
                Destroy(gameObject);
            }
        }
        
        //Bloquei a destruição ao carregar
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Verifica se o Player pode se mover
        if (canMove)
        {
            //Movimento do Player
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

            
        }
        else
        {
            //Desabilida o movimento
            theRB.velocity = Vector2.zero;
        }

        //Animações Walk
        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        //Verifica se a posição é idle
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || 
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                //Animação Idle
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        //Mantem o Player dentro dos limites do mapa
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                         Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
                                         transform.position.z);
    }

    //Função para pegar o valor do limite do mapa
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f,1f,0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
    }
}
