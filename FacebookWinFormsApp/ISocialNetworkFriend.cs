using System;
using System.Collections.Generic;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public interface ISocialNetworkFriend
    {
        string FullName { get; }
        DateTime Birthday { get; }
        string Hometown { get; }
        object ProfileImage { get; }
        bool IsOnline { get; }
        List<Event> Events { get; }
    }
}