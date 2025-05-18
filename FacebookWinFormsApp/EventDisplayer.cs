using FacebookWrapper.ObjectModel;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class EventDisplayer : ListBoxDataDisplayer<Event>
    {
        public EventDisplayer(ListBox listBox) : base(listBox) { }
        protected override string GetDisplayMember() => "Name";
    }
}
