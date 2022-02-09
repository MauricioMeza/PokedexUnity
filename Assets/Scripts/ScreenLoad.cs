using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ScreenLoad : MonoBehaviour
{

    public GameObject ui_name;
    public GameObject ui_height;
    public GameObject ui_weight;
    public GameObject[] ui_stats;

    List<Slider> slider_stats;
    TextMeshProUGUI text_name;
    TextMeshProUGUI text_height;
    TextMeshProUGUI text_weight;

    List<Pokemon> pokemones;
    int curentPoke = 0;

    // Start is called before the first frame update
    void Start()
    {
        pokemones = new List<Pokemon>();

        //Get all UI elements Text Component
        text_name = ui_name.GetComponent<TextMeshProUGUI>();
        text_height = ui_height.GetComponent<TextMeshProUGUI>();
        text_weight = ui_weight.GetComponent<TextMeshProUGUI>();

        slider_stats = new List<Slider>();
        foreach(GameObject ui_s in ui_stats)
        {
            slider_stats.Add(ui_s.GetComponent<Slider>());
        }

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

        int i = 0;
        foreach(Slider sld in slider_stats)
        {
            int val = currentPoke.stats[i].base_stat;
            sld.value = val;
            sld.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = GetColorFromValue(val);
            sld.gameObject.transform.Find("Handle Slide Area").Find("Handle").Find("Value").GetComponent<Text>().text = val.ToString();
            i++;
        }
    }


    private Color GetColorFromValue(int value){
        if(value >= 0 && value < 10){
            return Color.red;
        }
        else if(value >= 10 && value < 50){
            return new Color(1, (float)0.5, 0);
        }
        else if(value >= 50 && value < 100){
            return Color.yellow;
        }
        else if(value >= 100 && value < 150){
            return new Color((float)0.25, 1, 0);
        }
        else if(value >= 150 && value < 200){
            return Color.green;
        }
        else if(value >= 200)
        {
            return new Color(0, (float)0.75, 0);
        }
        else
        {
            return Color.black;
        }
    }
}
