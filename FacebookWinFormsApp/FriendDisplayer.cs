using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class FriendDisplayer : ListBoxDataDisplayer<ISocialNetworkFriend>
    {
        public FriendDisplayer(ListBox i_ListBox) : base(i_ListBox) { }
       
        protected override string GetDisplayMember() => "FullName";
    }
}
