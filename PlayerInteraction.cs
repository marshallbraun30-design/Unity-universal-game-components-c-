using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Interaction : MonoBehaviour
{
    public float interactdistance = 25f;
    public Camera Pov;
    public GameObject InteractionText;
    RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        Ray beam = new Ray(Pov.transform.position, Pov.transform.forward);

        if (Physics.SphereCast(beam, 0.4f, out hit, interactdistance)) 
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                InteractionText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractionText.SetActive(false);
                    interactable.interact();

                }
            }
            else
            {
                InteractionText.SetActive(false);
            }
       
            
        }
        else
        {
            InteractionText.SetActive(false);
        }
        
        
        
    }
}
 
public interface IInteractable
{
    void interact();
}
