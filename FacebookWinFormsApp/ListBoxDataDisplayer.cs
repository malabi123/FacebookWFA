using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public abstract class ListBoxDataDisplayer<T>
    {
        protected ListBox m_ListBox = null;

        public ListBoxDataDisplayer(ListBox i_ListBox)
        {
            m_ListBox = i_ListBox;
        }

        protected abstract string GetDisplayMember();

        public void DisplayItems(IEnumerable<T> i_Items)
        {
            m_ListBox.Invoke(new Action(() => m_ListBox.DisplayMember = GetDisplayMember()));

            foreach (T item in i_Items)
            {
                if(Condition(item))
                {
                    m_ListBox.Invoke(new Action(() => m_ListBox.Items.Add(item)));
                } 
            }
        }

        protected virtual bool Condition(T i_Item)
        {
            return true;
        }
    }
}
