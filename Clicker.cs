using UnityEngine;
using TMPro;

public class Clicker : MonoBehaviour
{
    public int currency = 0;
    public TextMeshProUGUI currencyText;
    public int clickValue = 1;
    public GameObject secondButton;
    public bool hasPurchasedFirstUpgrade = false;
    public AudioSource clickSound;
    public bool isVideoPlaying = false;
    public GameObject originalModel;
    public GameObject newModel;

    void Start()
    {
        if (originalModel != null && newModel != null)
        {
            originalModel.SetActive(true);
            newModel.SetActive(false);
            Debug.Log("Initial setup: Original model active, New model inactive.");
        }
        else
        {
            Debug.LogError("OriginalModel or NewModel not assigned!");
        }

        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
            Debug.Log("Added BoxCollider to Coin GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name + " at position: " + hit.point);
            }
            else
            {
                Debug.Log("Raycast missed everything. Mouse position: " + Input.mousePosition + ", World position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    void OnMouseDown()
    {
        if (!isVideoPlaying)
        {
            Debug.Log("Click detected on Coin! Position: " + transform.position);
            currency += clickValue;
            Debug.Log("Currency increased to: " + currency);
            UpdateCurrencyText();

            if (clickSound != null)
            {
                clickSound.Play();
                Debug.Log("Sound played successfully.");
            }
            else
            {
                Debug.Log("AudioSource not assigned!");
            }
        }
    }

    public void UpdateCurrencyText()
    {
        if (currencyText != null)
        {
            currencyText.text = "Currency: " + currency;
            Debug.Log("Text updated: " + currencyText.text);
        }
        else
        {
            Debug.Log("currencyText is not assigned!");
        }
    }

    public void SetFirstUpgradePurchase()
    {
        hasPurchasedFirstUpgrade = true;
    }

    public void ChangeCoinModel()
    {
        if (originalModel != null && newModel != null)
        {
            originalModel.SetActive(false);
            newModel.SetActive(true);
            Debug.Log("Switched to NewCoin at position: " + newModel.transform.position);

            Collider col = GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true;
                Debug.Log("Collider active: " + col.enabled + ", Position: " + transform.position + ", Size: " + ((BoxCollider)col).size);
            }
            else
            {
                Debug.LogError("No Collider found on Coin!");
            }
        }
        else
        {
            Debug.LogError("Error: Coin models not assigned in Clicker script!");
        }
    }
}