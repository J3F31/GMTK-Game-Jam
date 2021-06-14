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
            StartCoroutine(Quit());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.name == "Player1" || collision.collider.name == "Player2")
        {
            endGame = true;
        }
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}
