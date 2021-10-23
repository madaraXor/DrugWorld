using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerDeplacement : MonoBehaviour
{
    public NavMeshAgent myNavMeshAgent;
    Animator anim;
    float temps;
    GestionnaireMenu gm;
    // Objet focus
    public GameObject focusObject;

    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GestionnaireMenu").GetComponent<GestionnaireMenu>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            temps = Time.time ;
        }
        if ( Input.GetMouseButtonUp(0) && (Time.time - temps) < 0.2 && gm.menuOpen == false && gm.stateMessage == false)
        {
            SetDestinationToMousePosition();
        }

        if (myNavMeshAgent.remainingDistance > 1)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }
    }

    void SetDestinationToMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            myNavMeshAgent.SetDestination(hit.point);
            focusObject = hit.transform.gameObject;
        }
    }
}
