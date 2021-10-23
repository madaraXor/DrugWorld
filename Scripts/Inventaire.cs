using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventaire : MonoBehaviour
{
    public Dictionary<string, float> listeObjet = new Dictionary<string, float>();
    public List<Slot> listSlot = new List<Slot>();
    public TextMeshProUGUI affArgent;

    // AddScore Variables
    public GameObject affAddArgent;
    public GameObject canvas;

    void Start()
    {
        Add("argent", 360);
        Add("10Shit(2,5g)", 5);
        Add("20Shit(2,2g)", 5);
        Add("10Beuh(1,3g)", 5);
        Add("20Beuh(2,0g)", 5);
        Add("30Shit(2,2g)", 5);
        Add("30Beuh(1,3g)", 5);

    }

    public void Add(string name, float value)
    {
        // Test Si l'objet existe dans l'inventaire
        bool isExist = false;
        foreach (KeyValuePair<string, float> item in listeObjet)
        {
            if (item.Key == name)
            {
                isExist = true;
            }
        }
        // Si existe deja dans l'inventaire
        if (isExist)
        {
            float currentValue;
            if (name == "argent")
            {
                currentValue = listeObjet[name];
                listeObjet[name] =  currentValue + value;
                affArgent.text = "Argent : " + listeObjet[name] + "€";
                SpawnAnimation(value);
                return;
            }
            currentValue = listeObjet[name];
            listeObjet[name] =  currentValue + value;
            foreach (Slot slot in listSlot)
            {
                if (slot.name == name)
                {
                    slot.value = listeObjet[name];
                    slot.SetAff();
                    return;
                }
            }
        }
        else
        {
            if (name == "argent")
            {
                listeObjet.Add(name, value);
                affArgent.text = "Argent : " + listeObjet[name] + "€";
                SpawnAnimation(value);
                return;
            }
            foreach (Slot slot in listSlot)
            {
                if (slot.name == "none")
                {
                    listeObjet.Add(name, value);
                    slot.name = name;
                    slot.value = listeObjet[name];
                    slot.SetAff();
                    return;
                }
            }
            
        }
    }

    public void Remove(string name, float value)
    {
        if (name == "argent")
        {
            float currentValue = listeObjet[name];
            listeObjet[name] =  currentValue - value;
            affArgent.text = "Argent : " + listeObjet[name];
            return;
        }
        // Test Si l'objet existe dans l'inventaire
        bool isExist = false;
        foreach (KeyValuePair<string, float> item in listeObjet)
        {
            if (item.Key == name)
            {
                isExist = true;
            }
        }
        // Si existe deja dans l'inventaire
        if (isExist)
        {
            float currentValue = listeObjet[name];
            listeObjet[name] =  currentValue - value;
            foreach (Slot slot in listSlot)
            {
                if (slot.name == name)
                {
                    slot.value = listeObjet[name];
                    slot.SetAff();
                    return;
                }
            }
        }
        else
        {
            Debug.Log(name + " n'existe pas dans l'inventaire");
        }
    }

    void SpawnAnimation(float add)
    {
        GameObject instAffAddArgent = Instantiate(affAddArgent, canvas.transform);
        instAffAddArgent.GetComponent<TextMeshProUGUI>().text = "+" + add + "€";
        Destroy(instAffAddArgent, 2f);
    }
}
