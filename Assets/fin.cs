using UnityEngine;
using UnityEngine.SceneManagement;

public class fin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    

    // Update is called once per frame
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "fin")
        {
            print("fin");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
    
