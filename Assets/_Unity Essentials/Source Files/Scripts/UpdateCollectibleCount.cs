using UnityEngine;
using TMPro;
using System; // Required for Type handling

public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI collectibleText; // Reference to the TextMeshProUGUI component
    private bool gameComplete = false;
    public GameObject onCompletionEffect;

    public AudioClip completionSoundClip;
    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();
        if (collectibleText == null)
        {
            Debug.LogError("UpdateCollectibleCount script requires a TextMeshProUGUI component on the same GameObject.");
            return;
        }
        UpdateCollectibleDisplay(); // Initial update on start
    }

    void Update()
    {
        if (this.gameComplete == false)
        {
            UpdateCollectibleDisplay();
        }
        else
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player.transform.position.y != 10)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = false;
                if (player.transform.position.y < 10)
                {
                    player.transform.position += new Vector3(0, 10, 0) * Time.deltaTime;
                }
                else
                {
                    player.transform.position = new Vector3(player.transform.position.x, 10, player.transform.position.z);
                }
            }
        }
    }

    private void UpdateCollectibleDisplay()
    {
        int totalCollectibles = 0;

        // Check and count objects of type Collectible
        Type collectibleType = Type.GetType("Collectible");
        if (collectibleType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectibleType, FindObjectsSortMode.None).Length;
        }

        // Optionally, check and count objects of type Collectible2D as well if needed
        Type collectible2DType = Type.GetType("Collectible2D");
        if (collectible2DType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(collectible2DType, FindObjectsSortMode.None).Length;
        }

        if (totalCollectibles == 0)
        {
            GameObject player = GameObject.FindWithTag("Player");
            collectibleText.text = $"You got them all!";
            Instantiate(onCompletionEffect, player.transform.position, player.transform.rotation);
            this.gameComplete = true;
            SoundFXManager.instance.playSoundFXClip(completionSoundClip, transform);
        }
        else
        {
            // Update the collectible count display
            collectibleText.text = $"Collectibles remaining: {totalCollectibles}";
        }
    }

    private void PlayCompletionSound()
    {

    }
}
