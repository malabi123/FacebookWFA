using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class PageDisplayer : ListBoxDataDisplayer<Page>
    {
        public PageDisplayer(ListBox listBox) : base(listBox) { }
        protected override string GetDisplayMember() => "Name";
    }
}
