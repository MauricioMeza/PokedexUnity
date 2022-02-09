using UnityEngine;

public class ClickLeft : MonoBehaviour
{
    public GameObject screen;

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        if(Input.GetMouseButtonDown(0))
            screen.GetComponent<ScreenLoad>().pokeLeft();
    }
}
