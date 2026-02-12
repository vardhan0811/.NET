using System;

namespace Day38
{
    public interface INotifier
    {
        public void SendNotification(string message);
    }

    public class EmailNotifier : INotifier
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Email sent: {message}");
        }
    }

    public class SMSNotifier : INotifier
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"SMS sent: {message}");
        }
    }

    public class WhatsAppNotifier : INotifier
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"WhatsApp message sent: {message}");
        }
    }

    class Notifications
    {
        public static void Run()
        {
            string alertMessage = "System maintenance at 10 PM";

            List<INotifier> notifiers = new List<INotifier>
            {
                new EmailNotifier(),
                new SMSNotifier(),
                new WhatsAppNotifier()
            };

            foreach (INotifier notifier in notifiers)
            {
                notifier.SendNotification(alertMessage);
            }
        }
    }
}