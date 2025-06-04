using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class PageDisplayer : ListBoxDataDisplayer<Page>
    {
        public PageDisplayer(ListBox i_ListBox) : base(i_ListBox) { }

        protected override string GetDisplayMember() => "Name";
    }
}