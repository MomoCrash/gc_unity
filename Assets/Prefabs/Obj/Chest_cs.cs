using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // V�rifie si le joueur entre dans le collider et si le coffre n'est pas d�j� ouvert
        if (other.CompareTag("Player") && !isOpen)
        {
            print("cc");
            animator.SetTrigger("Open"); // Remplacez "Open" par le nom de votre trigger d'animation
            isOpen = true;
        }
    }
}
