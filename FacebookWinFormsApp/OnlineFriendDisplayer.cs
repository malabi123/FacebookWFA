using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class OnlineFriendDisplayer : ListBoxDataDisplayer<ISocialNetworkFriend>
    {
        public OnlineFriendDisplayer(ListBox i_ListBox) : base(i_ListBox) { }

        protected override string GetDisplayMember() => "FullName";

        protected override bool Condition(ISocialNetworkFriend i_Friend)
        {
            return i_Friend.IsOnline;
        }
    }
}
