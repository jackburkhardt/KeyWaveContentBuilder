using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public struct Email
    {
        public string Sender;
        public string Recipient;
        public string Subject;
        public string BodyText;
        public string[] BodyImagePaths;
        public string[] CompletesAssignments;
        public string[] ActivatesAssignments;
        public bool Read;

        public Email(string sender, string recipient, string subject, string bodyText, string[] bodyImagePaths, string[] completesAssignments, string[] activatesAssignments)
        {
            Sender = sender;
            Recipient = recipient;
            Subject = subject;
            BodyText = bodyText;
            BodyImagePaths = bodyImagePaths;
            Read = false;
            CompletesAssignments = completesAssignments;
            ActivatesAssignments = activatesAssignments;
        }
    }
    
    public struct PhoneContact
    {
        public string ContactName;
        // time is represented on 24hr clock and uses 4 digits
        // ex: 8:00 am -> 0800 , ex: 3:25pm -> 1525
        public int StartAvailableTime; 
        public int EndAvailableTime;

        public PhoneContact(string contactName, int startAvailableTime, int endAvailableTime)
        {
            ContactName = contactName;
            StartAvailableTime = startAvailableTime;
            EndAvailableTime = endAvailableTime;
        }
    }
    
    public struct TextConversation
    {
        public string Recipient;
        public List<TextMessage> Messages;

        public TextConversation(string recipient, List<TextMessage> messages)
        {
            Recipient = recipient;
            Messages = messages;
        }
    }
        
    public struct TextMessage
    {
        public bool FromPlayer;
        public string Content;

        public TextMessage(bool fromPlayer, string content)
        {
            FromPlayer = fromPlayer;
            Content = content;
        }
    }
    
    public struct SimFolder
    {
        public string Name;
        public bool Locked;
        public string Password; // yes we are storing passwords as a string but it's a sim okay
        public List<SimFolder> ContainedFolders;
        public List<SimFile> ContainedFiles;

        public SimFolder(string name, bool locked = false, string password = "")
        {
            Name = name;
            Locked = locked;
            Password = password;
            ContainedFolders = new List<SimFolder>();
            ContainedFiles = new List<SimFile>();
        }
    }
        
    public struct SimFile
    {
        public string Name;
        public string Path;

        public SimFile(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
    
    public struct SearchItem
    {
        public List<string> SearchEntries;
        public string ImageName;

        public SearchItem(List<string> searchEntries, string imageName)
        {
            SearchEntries = searchEntries;
            ImageName = imageName;
        }
    }
    
    public struct Assignment
    {
        public string Name;
        public string Descriptor;
        public AssignmentType Type;
        public AssignmentState State;
        public int TimeToComplete; // in seconds
        public List<Assignment> DependentAssignments;

        public Assignment(string name, string descriptor, AssignmentType type, AssignmentState state, int timeToComplete, List<Assignment> dependentAssignments)
        {
            Name = name;
            Descriptor = descriptor;
            Type = type;
            State = state;
            TimeToComplete = timeToComplete;
            DependentAssignments = dependentAssignments;
        }

        public bool IsTimed => this.Type is AssignmentType.Timed or AssignmentType.PlayerTimed
            or AssignmentType.Emergency or AssignmentType.PlayerEmergency;

        public bool Over => this.State is AssignmentState.Completed or AssignmentState.Inactive or AssignmentState.Failed;

        public bool Completed => this.State == AssignmentState.Completed;
        
        public enum AssignmentType
        {
            General,
            Locked,
            PlayerOnly,
            Timed,
            Emergency,
            PlayerTimed,
            PlayerEmergency
        }

        public enum AssignmentState
        {
            Inactive,
            Active,
            Completed,
            Failed
        }
    }
}