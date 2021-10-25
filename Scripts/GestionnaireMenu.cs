using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GestionnaireMenu : MonoBehaviour
{
    // Inventaire Variables
    public GameObject panelInventaire;
    public bool stateInventaire = false;

    // Dealeur Variables
    public GameObject panelDealeur;
    public bool stateDealeur = false;

    // Message Variables
    public GameObject messagePanel;
    public bool stateMessage = false;

    // Client Variables
    public GameObject panelClient;
    public bool stateClient = false;

    // Home Variables
    public GameObject panelHome;
    public bool stateHome = false;
    public Home home;

    // Coroutine Variables
    private IEnumerator coroutine;

    // True si un des menu est ouvert
    public bool menuOpen = false;

    // Liste des affichage des Slots
    public List<TextMeshProUGUI> listAffSlot = new List<TextMeshProUGUI>();

    // Liste des affichage des Bouton
    public List<Button> listButton = new List<Button>();

    // Liste des affichage des Images
    public List<Image> listImage = new List<Image>();

    // ClientsManager
    ClientsManager cm;

    // Affichage Demande Clien
    public TextMeshProUGUI affDemandeClient;

    void Awake()
    {
        cm = GameObject.Find("ClientsManager").GetComponent<ClientsManager>();
        panelInventaire.SetActive(true);
        Close("InventaireForce");
        Close("DealeurForce");
        Close("ClientForce");
        Close("HomeForce");
        CloseMessage();
    }

    // Fonction Ouvrir un menu
    public void Open(string menuName)
    {
        // Inventaire
        if(menuName == "Inventaire" && stateInventaire == false && menuOpen == false)
        {
            //panelInventaire.SetActive(true);
            stateInventaire = true;
            menuOpen = true;
            foreach (TextMeshProUGUI affSlot in listAffSlot)
            {
                affSlot.enabled = true;
            }
            foreach (Button button in listButton)
            {
                button.enabled = true;
            }
            foreach (Image image in listImage)
            {
                image.enabled = true;
            }
        }
        // Dealer
        if(menuName == "Dealeur" && stateDealeur == false && menuOpen == false)
        {
            panelDealeur.SetActive(true);
            stateDealeur = true;
            menuOpen = true;
            
        }
        // Client
        if(menuName == "Client" && stateClient == false && menuOpen == false)
        {
            panelClient.SetActive(true);
            affDemandeClient.text = "Bonjour, j'ai besoin d'un " + cm.prixTransac + "€ de " + cm.drogueTypeTransac + ".";
            stateClient = true;
            menuOpen = true;
            
        }
        // Home
        if(menuName == "Home" && stateHome == false && menuOpen == false)
        {
            panelHome.SetActive(true);
            stateHome = true;
            menuOpen = true;
            
        }
    }

    // Fonction Fermer un menu
    public void Close(string menuName)
    {
        // Inventaire
        if(menuName == "Inventaire" && stateInventaire == true)
        {
            //panelInventaire.SetActive(false);
            coroutine = StateFalse("Inventaire");
            StartCoroutine(coroutine);
            foreach (TextMeshProUGUI affSlot in listAffSlot)
            {
                affSlot.enabled = false;
            }
            foreach (Button button in listButton)
            {
                button.enabled = false;
            }
            foreach (Image image in listImage)
            {
                image.enabled = false;
            }
        }
        else if (menuName == "InventaireForce")
        {
            //panelInventaire.SetActive(false);
            stateInventaire = false;
            menuOpen = false;
            foreach (TextMeshProUGUI affSlot in listAffSlot)
            {
                affSlot.enabled = false;
            }
            foreach (Button button in listButton)
            {
                button.enabled = false;
            }
            foreach (Image image in listImage)
            {
                image.enabled = false;
            }
        }
        // Dealer
        if(menuName == "Dealeur" && stateDealeur == true)
        {
            panelDealeur.SetActive(false);
            coroutine = StateFalse("Dealeur");
            StartCoroutine(coroutine);
        }
        else if (menuName == "DealeurForce")
        {
            panelDealeur.SetActive(false);
            stateDealeur = false;
            menuOpen = false;
        }
        // Client
        if(menuName == "Client" && stateClient == true)
        {
            panelClient.SetActive(false);
            coroutine = StateFalse("Client");
            StartCoroutine(coroutine);
        }
        else if (menuName == "ClientForce")
        {
            panelClient.SetActive(false);
            stateClient = false;
            menuOpen = false;
        }
        // Home
        if(menuName == "Home" && stateHome == true)
        {
            home.ButtonDetaillerFermer();
            panelHome.SetActive(false);
            coroutine = StateFalse("Home");
            StartCoroutine(coroutine);
        }
        else if (menuName == "HomeForce")
        {
            home.ButtonDetaillerFermer();
            panelHome.SetActive(false);
            stateHome = false;
            menuOpen = false;
        }
    }

    IEnumerator StateFalse(string cible) 
    {
        yield return new WaitForSeconds(0.1f);
        if (cible == "Inventaire")
        {
            stateInventaire = false;
            menuOpen = false;
        }
        if (cible == "Dealeur")
        {
            stateDealeur = false;
            menuOpen = false;
        }
        if (cible == "Message")
        {
            stateMessage = false;
            menuOpen = false;
        }
        if (cible == "Client")
        {
            stateClient = false;
            menuOpen = false;
        }
        if (cible == "Home")
        {
            stateHome = false;
            menuOpen = false;
        }
    }

    public void OpenMessage(string text)
    {
        messagePanel.SetActive(true);
        messagePanel.transform.FindChild("AffMessage").gameObject.GetComponent<TextMeshProUGUI>().text = text;
        stateMessage = true;
        menuOpen = true;
    }

    public void CloseMessage()
    {
        //messagePanel.GetComponent<TextMeshProUGUI>().text = "";
        messagePanel.SetActive(false);
        coroutine = StateFalse("Message");
        StartCoroutine(coroutine);
    }
}
