using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;

namespace NCommons.Logging
{
    public class LogEntry
    {
        Guid _activityId;
        bool _activityIdInitialized;

        public LogEntry()
            : this(string.Empty, new List<string>(0), -1, 0, TraceEventType.Information, string.Empty,
                   new Dictionary<string, object>())
        {
        }

        public LogEntry(object message, string category, int priority, int eventId,
                        TraceEventType severity, string title, IDictionary<string, object> properties)
            : this(message, BuildCategoriesCollection(category), priority, eventId, severity, title, properties)
        {
        }

        public LogEntry(object message, ICollection<string> categories, int priority, int eventId,
                        TraceEventType severity, string title, IDictionary<string, object> properties)
        {
            Message = string.Empty;
            Title = string.Empty;
            Categories = new List<string>(0);
            Priority = -1;
            Severity = TraceEventType.Information;
            MachineName = string.Empty;
            TimeStamp = DateTime.Now;

            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            if (categories == null)
            {
                throw new ArgumentNullException("categories");
            }

            Message = message.ToString();
            Priority = priority;
            Categories = categories;
            EventId = eventId;
            Severity = severity;
            Title = title;
            ExtendedProperties = properties;
        }

        public Guid ActivityId
        {
            get
            {
                if (!_activityIdInitialized)
                {
                    InitializeActivityId();
                }
                return _activityId;
            }
            set
            {
                _activityId = value;
                _activityIdInitialized = true;
            }
        }

        public string AppDomainName { get; set; }
        public ICollection<string> Categories { get; set; }
        public int EventId { get; set; }
        public IDictionary<string, object> ExtendedProperties { get; set; }
        public string MachineName { get; set; }
        public string ManagedThreadName { get; set; }
        public string Message { get; set; }
        public int Priority { get; set; }
        public string ProcessId { get; set; }
        public string ProcessName { get; set; }
        public Guid? RelatedActivityId { get; set; }
        public TraceEventType Severity { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Title { get; set; }
        public string Win32ThreadId { get; set; }

        void InitializeActivityId()
        {
            if (IsTracingAvailable())
            {
                try
                {
                    ActivityId = GetActivityId();
                }
                catch (Exception)
                {
                    ActivityId = Guid.Empty;
                }
            }
            else
            {
                ActivityId = Guid.Empty;
            }
        }

        internal static bool IsTracingAvailable()
        {
            bool flag = false;
            try
            {
                flag = SecurityManager.IsGranted(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
            }
            catch (SecurityException)
            {
            }
            return flag;
        }

        static Guid GetActivityId()
        {
            return Trace.CorrelationManager.ActivityId;
        }

        static ICollection<string> BuildCategoriesCollection(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                throw new ArgumentNullException("category");
            }
            return new[] {category};
        }
    }
}