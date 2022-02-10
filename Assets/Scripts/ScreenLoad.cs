using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ScreenLoad : MonoBehaviour
{
    public GameObject ui_canRight;
    public GameObject ui_canLeft;
    public GameObject ui_3d;
    public GameObject ui_num;
    public GameObject ui_img;
    public GameObject ui_name;
    public GameObject ui_height;
    public GameObject ui_weight;
    public GameObject ui_type1;
    public GameObject ui_type2;
    public GameObject ui_abl1;
    public GameObject ui_abl2;
    public GameObject ui_searchBar;
    public GameObject ui_searchButton;
    public GameObject[] ui_stats;
    public GameObject[] ui_moves;
    public Mesh[] def_3d;

    Canvas canvas_right;
    Canvas canvas_left;
    MeshFilter mesh_3d;
    Image img;
    Image img_type1;
    Image img_type2;
    TMP_InputField searchBar;
    Button searchButton;
    TextMeshProUGUI text_num;
    TextMeshProUGUI text_type1;
    TextMeshProUGUI text_type2;
    TextMeshProUGUI text_abl1;
    TextMeshProUGUI text_abl2;
    TextMeshProUGUI text_name;
    TextMeshProUGUI text_height;
    TextMeshProUGUI text_weight;
    List<Slider> slider_stats;
    List<TextMeshProUGUI> text_moves;

    List<Pokemon> pokemones;
    int currentPoke = 0;

    // Start is called before the first frame update
    void Start()
    {
        pokemones = new List<Pokemon>();

        //Get all UI elements from component Component
        canvas_right = ui_canRight.GetComponent<Canvas>();
        canvas_right.enabled = false;
        canvas_left = ui_canLeft.GetComponent<Canvas>();
        canvas_left.enabled = false;
        img = ui_img.GetComponent<Image>();
        img.enabled = false;
        mesh_3d = ui_3d.GetComponent<MeshFilter>();
        text_num = ui_num.GetComponent<TextMeshProUGUI>();
        text_name = ui_name.GetComponent<TextMeshProUGUI>();
        text_height = ui_height.GetComponent<TextMeshProUGUI>();
        text_weight = ui_weight.GetComponent<TextMeshProUGUI>();
        searchBar = ui_searchBar.GetComponent<TMP_InputField>();
        searchButton = ui_searchButton.GetComponent<Button>();
        img_type1 = ui_type1.GetComponent<Image>();
        img_type2 = ui_type2.GetComponent<Image>();
        text_type1 = ui_type1.transform.Find("UI_TextType").GetComponent<TextMeshProUGUI>();
        text_type2 = ui_type2.transform.Find("UI_TextType").GetComponent<TextMeshProUGUI>();
        text_abl1 = ui_abl1.GetComponent<TextMeshProUGUI>();
        text_abl2 = ui_abl2.GetComponent<TextMeshProUGUI>();
        
        slider_stats = new List<Slider>();
        foreach(GameObject ui_s in ui_stats)
        {
            slider_stats.Add(ui_s.GetComponent<Slider>());
        }

        text_moves = new List<TextMeshProUGUI>();
        foreach(GameObject ui_t in ui_moves)
        {
            text_moves.Add(ui_t.GetComponent<TextMeshProUGUI>());
        }

        //Load 3 Initial Pokemons
        StartCoroutine(GetPokemon("charizard", true));
        StartCoroutine(GetPokemon("pikachu", false));
        StartCoroutine(GetPokemon("chikorita", false));
    }

    // Update is called once per frame
    void Update()
    {
        //Change of Pokemons working with keyboard arrows
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            pokeRight();
        }  
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            pokeLeft();            
        }  
    } 

    //Loads into the screen the info of a given pokemon
    private void loadPokeToScreen(int num){
        Pokemon currentPoke = pokemones[num];
        
        //Format Text
        float w = (float)currentPoke.weight / 10;
        float h = (float)currentPoke.height / 10;
        
        //Set Img andText in name, weight, height
        img.sprite = currentPoke.sprite;
        img.enabled = true;
        text_name.text = CapitalizeFirst(currentPoke.name) + " #" + currentPoke.id;
        text_height.text = h.ToString() + " m";
        text_weight.text = w.ToString() + " kg";
        text_abl1.text = CapitalizeFirst(currentPoke.abilities[0].ability.name);
        if(currentPoke.abilities.Length == 1)
            text_abl2.text = "/";
        else
            text_abl2.text = currentPoke.abilities[1].ability.name;

        //Set Text and change color depending on types and change postion if onyl 1 type
        if(currentPoke.types.Length == 1){
            RectTransform rect;
            rect = ui_type1.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(0,3,0);
            ui_type2.SetActive(false);
            string type = currentPoke.types[0].type.name;
            text_type1.text = CapitalizeFirst(type);
            img_type1.color = Pokemon.GetColorFromType(type);
        }else{
            RectTransform rect;
            rect = ui_type1.GetComponent<RectTransform>();
            rect.localPosition = new Vector3(-48,3,0);
            ui_type1.GetComponent<RectTransform>();
            ui_type2.SetActive(true);
            string type1 = currentPoke.types[0].type.name;
            string type2 = currentPoke.types[1].type.name;
            text_type1.text = CapitalizeFirst(type1);
            text_type2.text = CapitalizeFirst(type2);
            img_type1.color = Pokemon.GetColorFromType(type1);
            img_type2.color = Pokemon.GetColorFromType(type2);
        }

        //Set Text, change value and change color on stats
        int i = 0;
        foreach(Slider sld in slider_stats)
        {
            int val = currentPoke.stats[i].base_stat;
            sld.value = val;
            sld.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Pokemon.GetColorFromStat(val);
            sld.gameObject.transform.Find("Handle Slide Area").Find("Handle").Find("Value").GetComponent<Text>().text = val.ToString();
            i++;
        }

        //Set Text, change value and change color on stats
        i = 0;
        foreach(TextMeshProUGUI txt in text_moves)
        {
            string mov;
            if(i < (currentPoke.moves.Length))
                mov = currentPoke.moves[i].move.name;
            else
            {
                mov = "/";    
            }
            txt.text = mov;
            i++;
        }

        //Placeholder for 3D Mesh (it looks cool but im not implementing 889 3d models... for now)
        switch(currentPoke.id){
            case 6:
                mesh_3d.mesh = def_3d[0];
                break;
            case 25:
                mesh_3d.mesh = def_3d[1];
                break;
            case 152:
                mesh_3d.mesh = def_3d[2];
                break;
            default:
                mesh_3d.mesh = def_3d[3];
                break;
        }
    }

    //Sends request for pokemon in SearchBar
    private void searchPokemon()
    {
        string pokeString = searchBar.text;
        if(pokeString.Length != 0)
            StartCoroutine(GetPokemon(pokeString, true)); 
    }

    //Capitalizes first letter in a string
    private string CapitalizeFirst(string str){
        str = char.ToUpper(str[0]) + str.Substring(1);
        return str;
    }

    public void pokeRight(){
        if(currentPoke != (pokemones.Count-1))
                currentPoke++;
            else
                currentPoke = 0;
            loadPokeToScreen(currentPoke);      
    }
    public void pokeLeft(){
        if(currentPoke != 0)
                currentPoke--;
            else
                currentPoke = pokemones.Count-1;
            loadPokeToScreen(currentPoke);
    }

    

    //------------------------------------------------------------------------------------
    //-------------------------------HTTP REQUESTS----------------------------------------
    //------------------------------------------------------------------------------------

     //GET HTTP Request to API for Pokemon
    IEnumerator GetPokemon(string pokeId, bool load)
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
            bool alreadyInList = false;
            int i=0;

            //Check if Pokemon is already scanned, else add it to pokedex
            foreach(Pokemon pk in pokemones)
            {
                if(pk.id == poke.id){
                    alreadyInList = true;
                    break;
                }
                i++;
            }
            if(alreadyInList){
                currentPoke = i;
                loadPokeToScreen(currentPoke);
            }else
            {
                pokemones.Add(poke);
                text_num.text = pokemones.Count.ToString() + "/889";
                StartCoroutine(GetPokeSprite(poke, load));
            }
            
        }
    } 

    //GET HTTP Request to API for Sprite PNG
    IEnumerator GetPokeSprite(Pokemon poke, bool load)
    {
        string spriteURL = poke.sprites.front_default;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(spriteURL);
        yield return www.SendWebRequest();

        if ( www.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(www.error);
        }
        else
        {
            //Extract JSON Data from response  
            Texture texture = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create((Texture2D)texture, 
            new Rect(0,0, texture.width, texture.height),      
            Vector2.one/2);
            poke.sprite = sprite;
            if(load)
                currentPoke = pokemones.Count - 1;
                loadPokeToScreen(currentPoke);
                canvas_right.enabled = true;
                canvas_left.enabled = true;
        }
    }
}
