using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using System.Windows.Forms;
using System.Drawing;

namespace BasicFacebookFeatures
{
    public class FakeFacebookFriend
    {
        public FakeFacebookFriend(string i_FullName, bool i_IsOnline, Image i_ProfileImage, DateTime i_Birthday, string i_Hometown)
        {
            FullName = i_FullName;
            IsOnline = i_IsOnline;
            ProfileImage = i_ProfileImage;
            AttendingEvents = null;
            Birthday = i_Birthday;
            Hometown = i_Hometown;
        }

        public string FullName { get; set; }
        public bool IsOnline { get; set; }
        public Image ProfileImage { get; set; }
        public List<Event> AttendingEvents { get; set; }
        public DateTime Birthday { get; set; }
        public string Hometown { get; set; }
        
    }
}
