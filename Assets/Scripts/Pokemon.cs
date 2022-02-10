using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pokemon
{
    public int id;
    public string name;
    public int height;
    public int weight; 
    public PokemonStat[] stats;
    public PokemonType[] types;
    public PokemonSprite sprites;
    public PokemonAbility[] abilities;
    [NonSerialized]
    public Sprite sprite;

    public static Color GetColorFromStat(int value)
    {
        if (value >= 0 && value < 10)
            return Color.red;
        else if (value >= 10 && value < 50)
            return new Color(1, (float)0.5, 0);
        else if (value >= 50 && value < 100)
            return Color.yellow;
        else if (value >= 100 && value < 150)
            return new Color((float)0.25, 1, 0);
        else if (value >= 150 && value < 200)
            return Color.green;
        else if (value >= 200)
            return new Color(0, (float)0.75, 0);
        else
            return Color.black;
    }

    public static Color GetColorFromType(string type)
    {
        Color color;
        switch (type)
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

[Serializable]
public class PokemonAbility
{
    public Ability ability;
}
[Serializable]
public class Ability
{
    public string name;
}
