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

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.O))
        {
            if(animator.GetBool("open") && !animator.GetBool("turn"))
            {
                animator.SetBool("open", false);
                mesh_3d.enabled = false;
            }
            else if(!animator.GetBool("open") && !animator.GetBool("turn"))
            {
                animator.SetBool("open", true);
                mesh_3d.enabled = true;  
            }               
        } 
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(animator.GetBool("turn") && !animator.GetBool("open"))
                animator.SetBool("turn", false);
            else if(!animator.GetBool("turn") && !animator.GetBool("open"))
                animator.SetBool("turn", true);     
        }
    }
}
