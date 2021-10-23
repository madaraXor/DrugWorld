using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public float prix;
    public string name;
    public string drogueType;
    BatimentCollider collider;
    public Material Material1;
    public Material Material2;

    // PlayerDeplacement
    PlayerDeplacement playerDeplacement;

    // Gestionnaire Menu
    GestionnaireMenu gm;

    // MeshRenderer
    public SkinnedMeshRenderer smr;

    // ClientsManager
    ClientsManager cm;

    // true si la vente a eu lieux
    public bool end = false;

    void Start()
    {
        playerDeplacement = GameObject.Find("Player").GetComponent<PlayerDeplacement>();
        collider = transform.FindChild("Collider").gameObject.GetComponent<BatimentCollider>();
        gm = GameObject.Find("GestionnaireMenu").GetComponent<GestionnaireMenu>();
        cm = GameObject.Find("ClientsManager").GetComponent<ClientsManager>();
        // Definir prix
        prix = Random.Range(1, 3)*10;
        int rdm = Random.Range(1,3);
        if (rdm == 1)
        {
            drogueType = "Shit";
        }
        if (rdm == 2)
        {
            drogueType = "Beuh";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDeplacement.focusObject!=null && !end)
        {
            if (playerDeplacement.focusObject.name == name && collider.playerIsInside == true)
            {
                cm.prixTransac = prix;
                cm.drogueTypeTransac = drogueType;
                cm.nameClientEnCour = transform.name;
                gm.Open("Client");
                playerDeplacement.focusObject = null;
            }
        }
    }

    void OnMouseOver()
    {
        if (!gm.menuOpen && !end)
        {
            smr.material = Material2;
        }
    }

    void OnMouseExit()
    {   
        if (!end)
        {
            smr.material = Material1;
        }
    }

}
