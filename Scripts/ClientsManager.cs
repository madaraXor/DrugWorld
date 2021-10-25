using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsManager : MonoBehaviour
{
    public GameObject[] spawns;
    public GameObject[] clients;
    public Dictionary<GameObject, GameObject> listClientsActif = new Dictionary<GameObject, GameObject>();
    public int maxClients;
    public GameObject spawnEnCour;
    public GameObject clientEnCour;

    // PrixTransac
    public float prixTransac;
    public string drogueTypeTransac;

    // Inventaire
    public Inventaire inv;

    // Apreciation client
    public float satisfactionClient = 0;

    // Gestionnaire Menu
    GestionnaireMenu gm;

    // Numéro des clients
    public int numClient = 0;

    // Nom du client en transaction
    public string nameClientEnCour;



    // Start is called before the first frame update
    void Start()
    {
        inv = GameObject.Find("Inventaire").GetComponent<Inventaire>();
        gm = GameObject.Find("GestionnaireMenu").GetComponent<GestionnaireMenu>();
        Debut();
    }


    // Update is called once per frame
    void Update()
    {
        if (listClientsActif.Count < maxClients)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        //Debug.Log("nb spawn : " + spawns.Length);
        int rdms = Random.Range(0, spawns.Length);
        //Debug.Log("rdm spawn : " + rdms);
        int rdmc = Random.Range(0, clients.Length);
        //Debug.Log("rdm client : " + rdmc);
        if (spawns[rdms]==null || clients[rdmc] == null)
        {
            return;
        }
        GameObject spawn = spawns[rdms];
        GameObject client = clients[rdmc];
        int i = 0;
        foreach (GameObject item in spawns)
        {
            if (spawn == item)
            {
                //Debug.Log("Corespond, index : " + i);
                spawns[i] = null;
            }
            else
            {
                //Debug.Log("Corespond pas");
            }
            i++;
        }
        GameObject instClient = Instantiate(client);
        instClient.transform.position = spawn.transform.position;
        numClient++;
        string nameClient = "Client" + numClient;
        instClient.name = nameClient;
        instClient.GetComponent<Client>().name = nameClient;

        listClientsActif.Add(spawn, instClient);
        
        //instAffAddArgent.GetComponent<TextMeshProUGUI>().text = "+" + add + "€";
        //Destroy(instAffAddArgent, 2f);
        return;
    }

    void Debut()
    {
        int i = 0;
        while (listClientsActif.Count < maxClients)
        {
            Spawn();
            if (i > 50)
            {
                break;
            }
            i++;
        }
    }


    public void Vente()
    {
        foreach (KeyValuePair<string, float> item in inv.listeObjet)
        {
            if (item.Key.StartsWith(prixTransac + drogueTypeTransac))
            {
                //Debug.Log(item.Key);
                if (item.Value >= 1)
                {
                    string name = item.Key;
                    int startPos = name.IndexOf("(") + 1;
                    int endPos = name.IndexOf("g)") - startPos;
                    string dosage = name.Substring(startPos, endPos);
                    float poidSachet = float.Parse(dosage);
                    //Debug.Log("poid sachet : " + poidSachet);
                    if (drogueTypeTransac == "Shit")
                    {
                        //Debug.Log(poidSachet + " / " + prixTransac/10);
                        float poidDu10 = poidSachet / (prixTransac/10);

                        //Debug.Log("poid du 10 : " + poidDu10);
                        if (poidDu10 <= 1.6f)
                        {
                            //Debug.Log("radin");
                        }
                        else if (poidDu10 <= 2f)
                        {
                            //Debug.Log("ok");
                        }
                        else if (poidDu10 > 2f)
                        {
                            //Debug.Log("gg mec");
                        }
                    }
                    if (drogueTypeTransac == "Beuh")
                    {
                        //Debug.Log(poidSachet + " / " + prixTransac/10);
                        float poidDu10 = poidSachet / (prixTransac/10);

                        //Debug.Log("poid du 10 : " + poidDu10);
                        if (poidDu10 <= 0.8f)
                        {
                            //Debug.Log("radin");
                        }
                        else if (poidDu10 <= 1.2f)
                        {
                            //Debug.Log("ok");
                        }
                        else if (poidDu10 > 1.2f)
                        {
                            //Debug.Log("gg mec");
                        }
                    }
                    // Effectuer Transaction
                    inv.Remove(name, 1);
                    inv.Add("argent", prixTransac);
                    // Fermer menue de vente
                    gm.Close("Client");
                    // Retrouver et supprimer le clien
                    clientEnCour = GameObject.Find(nameClientEnCour);
                    Destroy(clientEnCour, 2f);
                    clientEnCour.GetComponent<Client>().end = true;
                    // Recuperer le spawn asocier au client
                    foreach (KeyValuePair<GameObject, GameObject> item1 in listClientsActif)
                    {
                        if (item1.Value.transform.name == clientEnCour.transform.name)
                        {
                            //Debug.Log("spawn trouver");
                            spawnEnCour = item1.Key;
                            break;
                        }
                    }
                    // Rajouter le spawn a son array
                    int i = 0;
                    foreach (GameObject item2 in spawns)
                    {
                        if (item2 == null)
                        {
                            break;
                        }
                        i++;
                    }
                    spawns[i] = spawnEnCour;
                    // Retirer le client des Actif
                    foreach (KeyValuePair<GameObject, GameObject> item3 in listClientsActif)
                    {
                        if (item3.Value == clientEnCour)
                        {
                            listClientsActif.Remove(item3.Key);
                            break;
                        }
                    }
                    return;
                }
            }
        }
        Debug.Log("Pas le bon item");
    }
}
