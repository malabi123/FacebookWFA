using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class UserDisplayer : ListBoxDataDisplayer<User>
    {
        public UserDisplayer(ListBox listBox) : base(listBox) { }

        protected override string GetDisplayMember() => "Name";
    }
}
