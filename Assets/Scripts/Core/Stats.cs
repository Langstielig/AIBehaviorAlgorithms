using UnityEngine;

public class Stats : MonoBehaviour
{
    private int _energy;
    public int energy
    {
        get { return _energy; }
        set
        {
            _energy = Mathf.Clamp(value, 0, 100);
            OnStatValueChanged?.Invoke();
        }
    }

    private int _hunger;
    public int hunger
    {
        get { return _hunger; }
        set
        {
            _hunger = Mathf.Clamp(value, 0, 100);
            OnStatValueChanged?.Invoke();
        }
    }

    private int _money;
    public int money
    {
        get { return _money; }
        set
        {
            _money = value;
            OnStatValueChanged?.Invoke();
        }
    }

    [SerializeField] private float timeToDecreaseHunger = 5f;
    [SerializeField] private float timeToDecreaseEnergy = 5f;
    private float timeLeftEnergy;
    private float timeLeftHunger;

    [SerializeField] private Billboard billboard;

    public delegate void StatValueChangedHandler();
    public event StatValueChangedHandler OnStatValueChanged;

    void Start()
    {
        //Test case: NPC will likely work
        //hunger = 9;
        //energy = 81;
        //money = 70;

        //Test case: NPC will likely eat
        //hunger = 89;
        //energy = 16;
        //money = 500;

        //Test case: NPC will likely sleep
        //hunger = 0;
        //energy = 10;
        //money = 500;

        //Test case: low NPC stats
        //hunger = 50;
        //energy = 10;
        //money = 150;

        //Test case: average NPC stats
        //hunger = 49;
        //energy = 51;
        //money = 10;

        //My case
        hunger = 74;
        energy = 96;
        money = 730;
    }

    private void Update()
    {
        UpdateEnergy();
        UpdateHunger();
    }

    private void OnEnable()
    {
        OnStatValueChanged += UpdateDisplayText;
    }

    private void OnDisable()
    {
        OnStatValueChanged -= UpdateDisplayText;
    }

    public void UpdateHunger()
    {
        if(timeLeftHunger > 0)
        {
            timeLeftHunger -= Time.deltaTime;
            return;
        }

        timeLeftHunger = timeToDecreaseHunger;
        hunger += 1;
    }

    public void UpdateEnergy()
    {
        //if(shouldNotUpdateEnergy)
        //{
        //    return;
        //}

        if(timeLeftEnergy > 0)
        {
            timeLeftEnergy -= Time.deltaTime;
            return;
        }

        timeLeftEnergy = timeToDecreaseEnergy;
        energy -= 1;
    }

    private void UpdateDisplayText()
    {
        billboard.UpdateStatsText(energy, hunger, money);
    }
}
