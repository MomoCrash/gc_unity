using System.Collections;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;

    public ItemStack[] stacks;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifie si le joueur entre dans le collider et si le coffre n'est pas ouvert
        if (other.CompareTag("Player") && !isOpen)
        {
            foreach (var item in stacks)
            {
                DropItem.DropItemInWorld(GameObject.Find("Items"), gameObject.transform.position, GameObject.Find("itemexemple"), item);
            }
            animator.SetTrigger("Open"); // Remplacez "Open" par le nom de votre trigger d'animation
            isOpen = true;
            StartCoroutine(DestoyDelay());
        }
    }

    IEnumerator DestoyDelay()
    {

        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
}
