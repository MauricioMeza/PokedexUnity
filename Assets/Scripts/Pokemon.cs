using System;
using System.Collections.Generic;

[Serializable]
public class Pokemon
{
    public int id;
    public string name;
    public int height;
    public int weight; 


}

[Serializable]
public class PokedexDB
{
    private List<Pokemon> pokemons;
}
