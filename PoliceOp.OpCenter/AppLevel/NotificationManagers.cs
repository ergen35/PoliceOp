using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Enterwell.Clients.Wpf.Notifications.Controls;
using Enterwell.Clients.Wpf;
using Enterwell.Clients.Wpf.Notifications;
using Tiny.RestClient;

namespace PoliceOp.OpCenter.AppLevel
{
    public enum NotificationLevel
    {
        Warning, Error, Info
    }
    public static class NotificationManagers
    {
        public static NotificationMessageManager AlertCenterManager = new NotificationMessageManager();
        public static NotificationMessageManager InAppNotificationsManager = new NotificationMessageManager();
        public static NotificationMessageManager AuthNotificationsManager = new NotificationMessageManager();

        public static void ShowNotification(string Message, string BadgeInfo, NotificationLevel Level = NotificationLevel.Info)
        {
            if (Level == NotificationLevel.Info)
            {
                InAppNotificationsManager.CreateMessage()
                                       .Accent("#1751C3")
                                       .Animates(true)
                                       .AnimationInDuration(0.75)
                                       .AnimationOutDuration(2)
                                       .Background("#333")
                                       .HasBadge(BadgeInfo)
                                       .HasMessage(Message)
                                       .Dismiss().WithButton("Ok", button => { })
                                       .Dismiss().WithDelay(TimeSpan.FromSeconds(25))
                                       .Queue();

                InAppNotificationsManager = new NotificationMessageManager();
            }

            if (Level == NotificationLevel.Error)
            {
                InAppNotificationsManager.CreateMessage()
                                       .Accent("#F15B19")
                                       .Animates(true)
                                       .AnimationInDuration(0.75)
                                       .AnimationOutDuration(2)
                                       .Background("#F15B19")
                                       .HasBadge(BadgeInfo)
                                       .HasMessage(Message)
                                       .Dismiss().WithButton("Ok", button => { })
                                       .Dismiss().WithDelay(TimeSpan.FromSeconds(25))
                                       .Queue();

                InAppNotificationsManager = new NotificationMessageManager();
            }

            if (Level == NotificationLevel.Warning)
            {
                InAppNotificationsManager.CreateMessage()
                                       .Accent("#E0A030")
                                       .Animates(true)
                                       .AnimationInDuration(0.75)
                                       .AnimationOutDuration(2)
                                       .Background("#333")
                                       .HasBadge(BadgeInfo)
                                       .HasMessage(Message)
                                       .Dismiss().WithButton("Ok", button => { })
                                       .Dismiss().WithDelay(TimeSpan.FromSeconds(25))
                                       .Queue();

                InAppNotificationsManager = new NotificationMessageManager();
            }

        }
    }
}
