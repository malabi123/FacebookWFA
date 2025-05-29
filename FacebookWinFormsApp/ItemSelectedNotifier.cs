using System;

namespace BasicFacebookFeatures
{
    public class ItemSelectedNotifier<T>
    {
        private Action<T> m_ItemSelected;

        public void Subscribe(Action<T> i_Action)
        {
            m_ItemSelected += i_Action;
        }

        public void UnSubscribe(Action<T> i_Action)
        {
            m_ItemSelected -= i_Action;
        }

        public void SelectItem(T i_Item)
        {
            m_ItemSelected?.Invoke(i_Item);
        }
    }
}
