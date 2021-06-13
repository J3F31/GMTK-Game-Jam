using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCheck : MonoBehaviour
{
    private bool endGame = false;
    public Image image;

    private void Update()
    {
        if (endGame)
        {
            image.GetComponent<Image>().color += new Color(0, 0, 0, .5f * Time.deltaTime);
            if(image.GetComponent<Image>().color == new Color(0, 0, 0, 255))
            {
                Time.timeScale = 0;
                Debug.Log("yes");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Player1" || collision.collider.name == "Player2")
        {
            endGame = true;
        }
    }
}
