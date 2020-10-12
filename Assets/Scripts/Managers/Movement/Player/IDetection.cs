public interface IDetection
{
	void Select();
	void Deselect();
	void StartInteract(CharacterManager manager, System.Action onFinishInteraction);
}