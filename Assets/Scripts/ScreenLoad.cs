using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ScreenLoad : MonoBehaviour
{
    public GameObject ui_img;
    public GameObject ui_name;
    public GameObject ui_height;
    public GameObject ui_weight;
    public GameObject ui_type1;
    public GameObject ui_type2;
    public GameObject[] ui_stats;

    Image img;
    Image img_type1;
    Image img_type2;
    TextMeshProUGUI text_type1;
    TextMeshProUGUI text_type2;
    TextMeshProUGUI text_name;
    TextMeshProUGUI text_height;
    TextMeshProUGUI text_weight;
    List<Slider> slider_stats;

    List<Pokemon> pokemones;
    int curentPoke = 0;

    // Start is called before the first frame update
    void Start()
    {
        pokemones = new List<Pokemon>();

        //Get all UI elements Text Component
        img = ui_img.GetComponent<Image>();
        text_name = ui_name.GetComponent<TextMeshProUGUI>();
        text_height = ui_height.GetComponent<TextMeshProUGUI>();
        text_weight = ui_weight.GetComponent<TextMeshProUGUI>();
        img_type1 = ui_type1.GetComponent<Image>();
        img_type2 = ui_type2.GetComponent<Image>();
        text_type1 = ui_type1.transform.Find("UI_TextType").GetComponent<TextMeshProUGUI>();
        text_type2 = ui_type2.transform.Find("UI_TextType").GetComponent<TextMeshProUGUI>();
        slider_stats = new List<Slider>();
        foreach(GameObject ui_s in ui_stats)
        {
            slider_stats.Add(ui_s.GetComponent<Slider>());
        }

        StartCoroutine(GetPokemon("shuckle"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //GET HTTP Request to API
    IEnumerator GetPokemon(string pokeId)
    {
        Pokemon poke;

        UnityWebRequest www = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + pokeId);
        yield return www.SendWebRequest();

        if ( www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //Extract JSON Data from response  
            poke = JsonUtility.FromJson<Pokemon>(www.downloadHandler.text);
            pokemones.Add(poke);
            loadPokeToScreen(curentPoke);
        }
    } 


    public void loadPokeToScreen(int num){
        Pokemon currentPoke = pokemones[num];
        
        //Format Text
        float w = (float)currentPoke.weight / 10;
        float h = (float)currentPoke.height / 10;
        
        //Set Text in name, weight, height
        text_name.text = CapitalizeFirst(currentPoke.name) + " #" + currentPoke.id;
        text_height.text = h.ToString() + " m";
        text_weight.text = w.ToString() + " kg";

        //Set Text and change color depending on types and how many types
        if(currentPoke.types.Length == 1){
            RectTransform rect;
            rect = ui_type1.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(0,20,0);
            ui_type2.SetActive(false);
            string type = currentPoke.types[0].type.name;
            text_type1.text = CapitalizeFirst(type);
            img_type1.color = GetColorFromType(type);
        }else{
            RectTransform rect;
            rect = ui_type1.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(-48,20,0);
            ui_type1.GetComponent<RectTransform>();
            ui_type2.SetActive(true);
            string type1 = currentPoke.types[0].type.name;
            string type2 = currentPoke.types[1].type.name;
            Debug.Log(type1);
            Debug.Log(type2);
            text_type1.text = CapitalizeFirst(type1);
            text_type2.text = CapitalizeFirst(type2);
            img_type1.color = GetColorFromType(type1);
            img_type2.color = GetColorFromType(type2);
            
        }


        //Set Text, change value and change color on stats
        int i = 0;
        foreach(Slider sld in slider_stats)
        {
            int val = currentPoke.stats[i].base_stat;
            sld.value = val;
            sld.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = GetColorFromStat(val);
            sld.gameObject.transform.Find("Handle Slide Area").Find("Handle").Find("Value").GetComponent<Text>().text = val.ToString();
            i++;
        }
    }


    private string CapitalizeFirst(string str){
        str = char.ToUpper(str[0]) + str.Substring(1);
        return str;
    }


    private Color GetColorFromStat(int value){
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

    private Color GetColorFromType(string type)
    {
        Color color;
        switch(type)
        {
            case "grass":
                color = new Color((float)0.48, (float)0.78, (float)0.3);
                break;
            case "normal":
                color = new Color((float)0.66, (float)0.65, (float)0.48);
                break;
            case "fire":
                color = new Color((float)0.93, (float)0.51, (float)0.19);
                break;
            case "water":
                color = new Color((float)0.39, (float)0.56, (float)0.94);
                break;
            case "electric":
                color = new Color((float)0.97, (float)0.82, (float)0.17);
                break;
            case "ice":
                color = new Color((float)0.39, (float)0.56, (float)0.60);
                break;
            case "fighting":
                color = new Color((float)0.66, (float)0.65, (float)0.48);
                break;
            case "poison":
                color = new Color((float)0.64, (float)0.24, (float)0.63);
                break;
            case "ground":
                color = new Color((float)0.89, (float)0.75, (float)0.40);
                break;
            case "psychic":
                color = Color.magenta;
                break;
            case "bug":
                color = new Color((float)0.48, (float)0.5, (float)0.3);
                break;
            case "dragon":
                color = new Color((float)0.44, (float)0.21, (float)0.99);
                break;
            case "dark":
                color = Color.black;
                break;
            case "steel":
                color = Color.gray;
                break;
            case "fairy":
                color = new Color((float)0.84, (float)0.51, (float)0.68);
                break;
            default:
                color = Color.white;
                break;
        }

        return color;   
    }
}
