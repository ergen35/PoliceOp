using System;
using System.Collections.Generic;
using System.Text;
using Enterwell.Clients.Wpf.Notifications.Controls;
using Enterwell.Clients.Wpf;
using Enterwell.Clients.Wpf.Notifications;

namespace PoliceOp.OpCenter.AppLevel
{
    public static class NotificationManagers
    {
        public static NotificationMessageManager AlertCenterManager = new NotificationMessageManager();   
        public static NotificationMessageManager InAppNotificationsManager = new NotificationMessageManager();   
        public static NotificationMessageManager AuthNotificationsManager = new NotificationMessageManager();   
    }
}
