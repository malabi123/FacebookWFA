using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class CommentDisplayer : ListBoxDataDisplayer<Comment>
    {
        public CommentDisplayer(ListBox i_ListBox) : base(i_ListBox) { }

        protected override string GetDisplayMember() => "Message";
    }
}