using System;
using System.Collections.Generic;
using System.Text;

namespace BitServer
{
    public class POP3message
    {
        private const string BR_SUB="[Broadcast subscribers]";
        private const string BR = "broadcast";

        public string Body
        { get; private set; }

        public string UID
        { get; private set; }

        public bool Deleted
        { get; private set; }

        private string retpath = string.Empty;
        private string from = string.Empty;
        private string to = string.Empty;
        private string subject = string.Empty;

        public POP3message(int ID,BitMsg MSG)
        {
            MSG.message.Replace("\r\n", "\n");
            if (!isMail(MSG.message))
            {
                //prepend Mail Headers
                Body += string.Format(@"Return-Path: {0}@bitmessage.ch
To: {1}@bitmessage.ch
From: {0}@bitmessage.ch
Subject: {2}
Received: {3}

", MSG.fromAddress, MSG.toAddress.Replace(BR_SUB, BR), MSG.subject, MSG.receivedTime);
                Body += MSG.message;
            }
            else
            {
                //Valid Mail, use sent headers.
                if (isMailList(MSG.message))
                {
                    MSG.message = MSG.message.Substring(MSG.message.IndexOf('\n') + 1);
                    MSG.message = MSG.message.Substring(MSG.message.IndexOf('\n') + 1);
                    Body += MSG.message;
                }
                else
                {
                    Body += MSG.message;
                }
            }
            UID = MSG.msgid;
            Deleted = false;
        }

        private bool isMailList(string p)
        {
            p=p.Split('\n')[0];
            return p.Contains("UTC   Message ostensibly from ") && p.Trim().EndsWith(":");
        }

        public void MarkDelete()
        {
            Deleted = true;
        }

        public void Reset()
        {
            Deleted = false;
        }

        private bool isMail(string Message)
        {
            var space = new char[] { ' ' };
            var lines = Message.Split('\n');
            if (lines.Length > 0)
            {
                int i = isMailList(lines[0]) ? 2 : 0;
                for (i+=0; i < lines.Length; i++)
                {
                    if (lines[i].Trim() == string.Empty)
                    {
                        break;
                    }
                    else if (lines[i].Trim().ToLower().StartsWith("return-path:"))
                    {
                        retpath = lines[i].Split(space, 2)[1].Trim();
                    }
                    else if (lines[i].Trim().ToLower().StartsWith("from:"))
                    {
                        from = lines[i].Split(space, 2)[1].Trim();
                        if (string.IsNullOrEmpty(retpath))
                        {
                            retpath = from;
                        }
                    }
                    else if (lines[i].Trim().ToLower().StartsWith("to:"))
                    {
                        to = lines[i].Split(space, 2)[1].Replace(BR_SUB, BR).Trim();
                    }
                    else if (lines[i].Trim().ToLower().StartsWith("subject:"))
                    {
                        subject = lines[i].Split(space, 2)[1].TrimEnd();
                    }
                }
            }
            return (retpath.Length > 0);
        }
    }
}
