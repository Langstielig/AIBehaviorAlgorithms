public class Food : DestinationPoint
{
    protected override void Awake()
    {
        destinationType = DestinationType.food;
        base.Awake();
    }
}
