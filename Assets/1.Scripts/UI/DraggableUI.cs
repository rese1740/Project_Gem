using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private	Transform		canvas;				// UI가 소속되어 있는 최상단의 Canvas Transform
	private	Transform		previousParent;		// 해당 오브젝트가 직전에 소속되어 있었던 부모 Transfron
	private	RectTransform	rect;				// UI 위치 제어를 위한 RectTransform
	private	CanvasGroup		canvasGroup;		// UI의 알파값과 상호작용 제어를 위한 CanvasGroup

	private void Awake()
	{
		canvas		= FindObjectOfType<Canvas>().transform;
		rect		= GetComponent<RectTransform>();
		canvasGroup	= GetComponent<CanvasGroup>();
	}

	/// <summary>
	/// 현재 오브젝트를 드래그하기 시작할 때 1회 호출
	/// </summary>
	public void OnBeginDrag(PointerEventData eventData)
	{
		// 드래그 직전에 소속되어 있던 부모 Transform 정보 저장
		previousParent = transform.parent;

		// 현재 드래그중인 UI가 화면의 최상단에 출력되도록 하기 위해
		transform.SetParent(canvas);		// 부모 오브젝트를 Canvas로 설정
		transform.SetAsLastSibling();		// 가장 앞에 보이도록 마지막 자식으로 설정

		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;
	}

	/// <summary>
	/// 현재 오브젝트를 드래그 중일 때 매 프레임 호출
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		rect.position = eventData.position;
	}

	/// <summary>
	/// 현재 오브젝트의 드래그를 종료할 때 1회 호출
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

