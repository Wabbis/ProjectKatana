using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBugFix : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        btn.Select();
    }

}
