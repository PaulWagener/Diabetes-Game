public class ExampleEvent : Event {

	public override void Trigger (Player player, object arg)
	{
		player.glucoseLevel += 3;
	}
}
