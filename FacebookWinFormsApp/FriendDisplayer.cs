using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class FriendDisplayer : ListBoxDataDisplayer<ISocialNetworkFriend>
    {
        public FriendDisplayer(ListBox listBox) : base(listBox) { }
        protected override string GetDisplayMember() => "FullName";
    }
}
