using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealeur : MonoBehaviour
{
    public Inventaire inv;
    public float price;

    // Gestionnaire Menu
    GestionnaireMenu gm;

    void Start()
    {
        gm = GameObject.Find("GestionnaireMenu").GetComponent<GestionnaireMenu>();
    }

    public void AcheterShit(float value)
    {
        if (value == 25)
        {
            price = 120;
        }
        if (value == 50)
        {
            price = 220;
        }
        if (value == 100)
        {
            price = 400;
        }
        if (inv.listeObjet["argent"] >= price)
        {
            inv.Add("Shit", value);
            inv.Remove("argent", price);
        }
        else
        {
            gm.Close("DealeurForce");
            gm.OpenMessage("Vous avez pas assez d'argent.");
        }
    }
}
