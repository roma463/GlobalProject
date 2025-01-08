using System.Linq;
using UnityEngine;

public class PopupCanvas : MonoBehaviour
{
    [SerializeField] private PopupView[] _allPopups;

    public bool TryGetPopup<T>(out T popup) where T : PopupView
    {
        popup = (T)_allPopups.FirstOrDefault(p => p as T);
        return popup != null;
    }
}
