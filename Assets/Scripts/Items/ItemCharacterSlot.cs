using UnityEngine;

[AddComponentMenu("Item Controller/A Character Slot")]
public class ItemCharacterSlot : MonoBehaviour
{
	[SerializeField]
	[Range(0, 20)]
	private int slotID;

	public bool Taken { get { return taken; } }
	public int ID { get { return slotID; } }

	private bool taken;

	public bool Take() => taken = true;
	public bool Release() => taken = false;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0.1f);	
	}
}
