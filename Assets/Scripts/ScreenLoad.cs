using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ScreenLoad : MonoBehaviour
{

    public GameObject ui_name;
    public GameObject ui_height;
    public GameObject ui_weight;
    TextMeshProUGUI text_name;
    TextMeshProUGUI text_height;
    TextMeshProUGUI text_weight;

    public List<Pokemon> pokemones;
    public int curentPoke = 0;

    // Start is called before the first frame update
    void Start()
    {
        pokemones = new List<Pokemon>();

        //Get all UI elements Text Component
        text_name = ui_name.GetComponent<TextMeshProUGUI>();
        text_height = ui_height.GetComponent<TextMeshProUGUI>();
        text_weight = ui_weight.GetComponent<TextMeshProUGUI>();

        StartCoroutine(GetPokemon("chikorita"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GET HTTP Request to API
    IEnumerator GetPokemon(string pokeId)
    {
        Pokemon poke;

        UnityWebRequest getPoke = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + pokeId);
        yield return getPoke.SendWebRequest();

        bool protError = getPoke.result == UnityWebRequest.Result.ProtocolError;
        bool connError = getPoke.result == UnityWebRequest.Result.ConnectionError;
        if ( protError || connError)
        {
            Debug.Log(getPoke.error);
        }
        else
        {
            //Extract JSON Data from response  
            poke = JsonUtility.FromJson<Pokemon>(getPoke.downloadHandler.text);
            pokemones.Add(poke);
            loadPokeToScreen(curentPoke);
        }
    } 

    public void loadPokeToScreen(int num){
        Pokemon currentPoke = pokemones[num];
        
        //Format Text
        string n = currentPoke.name;
        n = char.ToUpper(n[0]) + n.Substring(1);
        float w = (float)currentPoke.weight / 10;
        float h = (float)currentPoke.height / 10;
        
        //Set Text
        text_name.text = n + " #" + currentPoke.id;
        text_height.text = h.ToString() + " m";
        text_weight.text = w.ToString() + " kg";
    }
}
