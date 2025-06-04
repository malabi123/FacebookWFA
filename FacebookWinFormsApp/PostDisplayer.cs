using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class PostDisplayer : ListBoxDataDisplayer<Post>
    {
        public PostDisplayer(ListBox i_ListBox) : base(i_ListBox) { }

        protected override string GetDisplayMember() => "Name";
    }
}