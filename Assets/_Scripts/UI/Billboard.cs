using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI bestActionText;
    [SerializeField] private TextMeshProUGUI inventoryText;

    private Transform mainCameraTransform;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }

    public void UpdateStatsText(int energy, int hunger, int money)
    {
        statsText.text = $"Energy: {energy}\nHunger: {hunger}\nMoney: {money}";
    }    

    public void UpdateBestAction(string bestAction)
    {
        bestActionText.text = bestAction;
    }

    public void UpdateInventoryText(int wood, int stone, int money)
    {
        inventoryText.text = $"Wood: {wood}\nStone: {stone}\nFood: {money}";
    }
}
