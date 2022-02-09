using System;
using System.Collections.Generic;

[Serializable]
public class Pokemon
{
    public int id;
    public string name;
    public int height;
    public int weight; 
    public PokemonStat[] stats;

}

[Serializable]
public class PokemonStat
{
    public int base_stat;
}
