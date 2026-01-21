using UnityEngine;
using UnityEngine.UIElements;

namespace DaanBanaan.UI.Behaviour
{
    [RequireComponent(typeof(UIDocument))]
    public class UIBehaviour : MonoBehaviour
    {
        private UIDocument _document;
        protected UIDocument Document
        {
            get 
            {
                if ( _document == null )
                    _document = GetComponent<UIDocument>();

                return _document;
            }
        }

        private VisualElement _root;
        protected VisualElement RootVisualElement
        { 
            get
            {
                if ( _root == null )
                    _root = Document.rootVisualElement;
                return _root;
            }
        }


    }
}
