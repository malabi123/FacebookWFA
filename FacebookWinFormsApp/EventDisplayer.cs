using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class EventDisplayer : ListBoxDataDisplayer<Event>
    {
        public EventDisplayer(ListBox i_ListBox) : base(i_ListBox) { }

        protected override string GetDisplayMember() => "Name";
    }
}