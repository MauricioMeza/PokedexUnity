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
    public PokemonType[] types;

}

[Serializable]
public class PokemonStat
{
    public int base_stat;
}

[Serializable]
public class PokemonSprite
{
    public string front_default;
}

[Serializable]
public class PokemonType
{
    public Type type;
}
[Serializable]
public class Type
{
    public string name;
}
