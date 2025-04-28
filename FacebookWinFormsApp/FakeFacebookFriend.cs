using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;
using System.Drawing;

namespace BasicFacebookFeatures
{
    public class FakeFacebookFriend: ISocialNetworkFriend
    {
        public string FullName { get; set; }
        public bool IsOnline { get; set; }
        public object ProfileImage { get; set; }
        public List<Event> Events { get; set; }
        public DateTime Birthday { get; set; }
        public string Hometown { get; set; }

        public FakeFacebookFriend(string i_FullName, bool i_IsOnline, Image i_ProfileImage, DateTime i_Birthday, string i_Hometown)
        {
            FullName = i_FullName;
            IsOnline = i_IsOnline;
            ProfileImage = i_ProfileImage;
            Events = null;
            Birthday = i_Birthday;
            Hometown = i_Hometown;
        }
    }
}
