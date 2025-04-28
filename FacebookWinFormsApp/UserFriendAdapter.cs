using System;
using System.Collections.Generic;
using System.Linq;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    class UserFriendAdapter : ISocialNetworkFriend
    {
        private User m_Friend;
        private List<Event> m_Events;

        public UserFriendAdapter(User i_Friend)
        {
            m_Friend = i_Friend;
            m_Events = i_Friend.Events.ToList();
        }
        public string FullName 
        {
            get
            {
                return m_Friend.Name;
            } 
        }
        public DateTime Birthday 
        {
            get
            {
                return DateTime.Parse(m_Friend.Birthday);
            }
        }
        public string Hometown
        {
            get
            {
                return m_Friend.Location.Name;
            }
        }
        public object ProfileImage
        {
            get
            {
                return m_Friend.ImageLarge;
            }
        }
        public bool IsOnline 
        {
            get
            {
                bool isOnline = false;
                if (m_Friend.OnlineStatus != null)
                {
                    isOnline = m_Friend.OnlineStatus == User.eOnlineStatus.active;
                }
                return isOnline;
            }
        }
        public List<Event> Events
        {
            get
            {
                return m_Events;
            }
        }

    }
}
