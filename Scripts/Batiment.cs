using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Batiment : MonoBehaviour
{
    public GameObject affName;
    public GameObject canvas;
    public Camera cam;
    GameObject instAffName;
    GameObject aff;
    public string name;
    public Vector3 offset;
    public Material Material1;
    public Material Material2;

    // Gestionnaire Menu
    GestionnaireMenu gm;

    // PlayerDeplacement
    PlayerDeplacement playerDeplacement;

    // True si le joueur est pres du batiment
    bool playerIsInside = false;

    BatimentCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        GameObject instAffName = Instantiate(affName, canvas.transform);
        instAffName.GetComponent<TextMeshProUGUI>().text = name;
        aff = instAffName;
        gm = GameObject.Find("GestionnaireMenu").GetComponent<GestionnaireMenu>();
        playerDeplacement = GameObject.Find("Player").GetComponent<PlayerDeplacement>();
        collider = transform.FindChild("Collider").gameObject.GetComponent<BatimentCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerDeplacement.myNavMeshAgent.remainingDistance);
        if (playerDeplacement.focusObject!=null)
        {
            if (playerDeplacement.focusObject.name == name && collider.playerIsInside == true)
            {
                aff.SetActive(false);
                gm.Open(name);
                playerDeplacement.focusObject = null;
            }
        }
        if (name == "Dealeur" && gm.stateDealeur == false)
        {
            aff.SetActive(true);
        }
        if (gm.menuOpen)
        {
            aff.SetActive(false);
        }
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position) + offset;
        aff.transform.position = screenPos;
    }

    void OnMouseOver()
    {
        if (!gm.menuOpen)
        {
            GetComponent<MeshRenderer> ().material = Material2;
        }
    }

    void OnMouseExit()
    {
        GetComponent<MeshRenderer> ().material = Material1;
    }

    
}
