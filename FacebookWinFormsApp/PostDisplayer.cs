using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class PostDisplayer : ListBoxDataDisplayer<Post>
    {
        public PostDisplayer(ListBox listBox) : base(listBox) { }
        protected override string GetDisplayMember() => "Name";
    }
}
