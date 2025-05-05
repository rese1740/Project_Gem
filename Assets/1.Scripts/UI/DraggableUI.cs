using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private	Transform		canvas;				// UI�� �ҼӵǾ� �ִ� �ֻ���� Canvas Transform
	private	Transform		previousParent;		// �ش� ������Ʈ�� ������ �ҼӵǾ� �־��� �θ� Transfron
	private	RectTransform	rect;				// UI ��ġ ��� ���� RectTransform
	private	CanvasGroup		canvasGroup;		// UI�� ���İ��� ��ȣ�ۿ� ��� ���� CanvasGroup

	private void Awake()
	{
		canvas		= FindObjectOfType<Canvas>().transform;
		rect		= GetComponent<RectTransform>();
		canvasGroup	= GetComponent<CanvasGroup>();
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡���ϱ� ������ �� 1ȸ ȣ��
	/// </summary>
	public void OnBeginDrag(PointerEventData eventData)
	{
		// �巡�� ������ �ҼӵǾ� �ִ� �θ� Transform ���� ����
		previousParent = transform.parent;

		// ���� �巡������ UI�� ȭ���� �ֻ�ܿ� ��µǵ��� �ϱ� ����
		transform.SetParent(canvas);		// �θ� ������Ʈ�� Canvas�� ����
		transform.SetAsLastSibling();		// ���� �տ� ���̵��� ������ �ڽ����� ����

		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡�� ���� �� �� ������ ȣ��
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		rect.position = eventData.position;
	}

	/// <summary>
	/// ���� ������Ʈ�� �巡�׸� ������ �� 1ȸ ȣ��
	/// </summary>
	public void OnEndDrag(PointerEventData eventData)
	{
		if ( transform.parent == canvas )
		{
			transform.SetParent(previousParent);
			rect.position = previousParent.GetComponent<RectTransform>().position;
		}

		canvasGroup.alpha = 1.0f;
		canvasGroup.blocksRaycasts = true;
	}
}

