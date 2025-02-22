using System.Linq;
using UnityEngine;

public class PopupCanvas : MonoBehaviour
{
    public static PopupCanvas Instance { get; private set; }

    [SerializeField] private PopupView<PopupParameters>[] _allPopups;

    private void Awake()
    {
        Instance = this;
    }

    public bool TryGetPopup<T>(out T popup) where T : PopupView<PopupParameters>
    {
        popup = (T)_allPopups.FirstOrDefault(p => p as T);
        return popup != null;
    }
}
