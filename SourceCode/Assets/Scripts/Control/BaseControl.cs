using UnityEngine;

/// <summary>
/// Basic Control
/// </summary>
namespace EnglishApp
{
    public abstract class BaseControl : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Finish();
        public abstract void Back();
    }
}
