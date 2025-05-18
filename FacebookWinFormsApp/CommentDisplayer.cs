using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class CommentDisplayer : ListBoxDataDisplayer<Comment>
    {
        public CommentDisplayer(ListBox listBox) : base(listBox) { }

        protected override string GetDisplayMember() => "Message";
    }
}
