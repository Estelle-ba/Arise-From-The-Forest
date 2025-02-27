using UnityEngine;
using UnityEngine.SceneManagement;

public class fin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Camera camera;
    void Start()
    {
        camera = GameObject.Find("/Player/Main Camera").GetComponent<Camera>();
        
        transform.position = new Vector3(-315.8f, -22.6f, 0);
        camera.transform.position = new Vector3(-315.8f, -22.6f, -10);
    }

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
    
