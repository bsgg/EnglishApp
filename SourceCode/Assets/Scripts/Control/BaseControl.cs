using UnityEngine;

/// <summary>
/// Basic Control
/// </summary>
namespace EnglishApp
{
    public class BaseControl : MonoBehaviour
    {
        protected bool m_IsVisible;
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { m_IsVisible = value; }
        }

        public virtual void Init(){ }
        public virtual void Finish(){ }
        public virtual void Back(){ }
        public virtual void Show(){ m_IsVisible = true; }
        public virtual void Hide(){ m_IsVisible = false; }

        void Awake()
        {
            DoAwake();
        }

        protected virtual void DoAwake() { }

        void Start()
        {
            DoStart();
        }

        protected virtual void DoStart() { }

        void Update()
        {
            DoUpdate();
        }

        protected virtual void DoUpdate() { }
    }
}
