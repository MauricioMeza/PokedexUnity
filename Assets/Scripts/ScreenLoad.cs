using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScreenLoad : MonoBehaviour
{
    public List<Pokemon> Pokemones;

    // Start is called before the first frame update
    void Start()
    {
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
            Pokemones.add(poke);
        }

    } 


}
