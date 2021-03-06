using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokedexAnim : MonoBehaviour
{

    public GameObject obj_3d;
    private Animator animator;
    private MeshRenderer mesh_3d;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();    
        mesh_3d = obj_3d.GetComponent<MeshRenderer>();
    }
    
    //Animation for opening or closing pokedex
    public void open(){
        bool open = animator.GetBool("open");
        if(open && !animator.GetBool("turn"))
        {
            animator.SetBool("open", false);
            mesh_3d.enabled = false;
        }
        else if(!open && !animator.GetBool("turn"))
        {
            animator.SetBool("open", true);
            mesh_3d.enabled = true;  
        }
    }

    //Animation for turning pokedex when its closed
    public void turn(){
        bool turn = animator.GetBool("turn");
        if(turn && !animator.GetBool("open"))
            animator.SetBool("turn", false);
        else if(!turn && !animator.GetBool("open"))
            animator.SetBool("turn", true);
    }
}
