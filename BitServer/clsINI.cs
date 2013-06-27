using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace BitServer
{
    public static class INI
    {
        public struct INIPart
        {
            public string Section;
            public NameValueCollection Settings;
        }

        public static string[] getSections(string Filename)
        {
            if (File.Exists(Filename))
            {
                string[] Lines = File.ReadAllLines(Filename);
                List<string> sections = new List<string>();
                foreach (string line in Lines)
                {
                    if (line.Length > 0 && !line.StartsWith(";"))
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            sections.Add(line.Substring(1, line.Length - 2));
                        }
                    }
                }
                return sections.ToArray();
            }
            return null;
        }

        public static NameValueCollection getSettings(string Filename, string Section)
        {
            bool inSec = false;
            NameValueCollection nc = new NameValueCollection();
            if (File.Exists(Filename))
            {
                string[] Lines = File.ReadAllLines(Filename);
                foreach (string line in Lines)
                {
                    if (line.Length > 0 && !line.StartsWith(";"))
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            inSec = (line.Substring(1, line.Length - 2) == Section);
                        }
                        else if (inSec && line.Contains("="))
                        {
                            nc.Add(line.Split('=')[0].Trim(), line.Split(new char[] { '=' }, 2)[1]);
                        }
                    }
                }
                return nc;
            }
            return null;

        }

        public static string getSetting(string FileName, string Section, string Setting)
        {
            try
            {
                return getSettings(FileName, Section)[Setting];
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Rewrites a complete INI File. This erases Comments.
        /// </summary>
        /// <param name="FileName">File Name</param>
        /// <param name="Parts">INI Sections (aka. Parts)</param>
        public static void RewriteINI(string FileName, INIPart[] Parts)
        {
            if (Parts != null && Parts.Length>0)
            {
                var Content = string.Empty;
                foreach (INIPart p in Parts)
                {
                    Content += string.Format("[{0}]\r\n",p.Section);
                    foreach (var k in p.Settings.AllKeys)
                    {
                        Content += string.Format("{0}={1}\r\n", k, Program.toEmpty(p.Settings[k]));
                    }
                    Content += "\r\n";
                }
                File.Delete(FileName);
                File.WriteAllText(FileName, Content.Trim());
            }
        }

        /// <summary>
        /// Sets a single Value.
        /// Prevents Comments from being overwritten.
        /// </summary>
        /// <param name="FileName">File Name</param>
        /// <param name="Section">INI Section</param>
        /// <param name="Setting">INI Setting</param>
        /// <param name="Value">Value to set</param>
        public static void setSetting(string FileName, string Section, string Setting,string Value)
        {
            if (!string.IsNullOrEmpty(FileName) && File.Exists(FileName))
            {
                if (!string.IsNullOrEmpty(Section) && !string.IsNullOrEmpty(Setting))
                {
                    var Lines = File.ReadAllLines(FileName);
                    var inSect = false;

                    for (int i = 0; i < Lines.Length; i++)
                    {
                        if (inSect)
                        {
                            if (Lines[i].ToLower().StartsWith(Setting.ToLower() + "="))
                            {
                                Lines[i] = Lines[i].Split('=')[0] + "=" + Program.toEmpty(Value);
                                break;
                            }
                            else if (Lines[i].StartsWith("[") && Lines[i].EndsWith("]"))
                            {
                                inSect = false;
                            }
                        }
                        else
                        {
                            inSect = (Lines[i].ToLower().Trim() == "[" + Section.ToLower() + "]");
                        }
                    }
                    File.WriteAllLines(FileName, Lines);
                }
            }
            else
            {
                File.WriteAllText(FileName,string.Format("[{0}]\r\n{1}={2}",Section,Setting,Value));
            }
        }

        public static INIPart[] completeINI(string FileName)
        {
            List<INIPart> parts = new List<INIPart>();
            if (File.Exists(FileName))
            {
                INIPart p = new INIPart();
                foreach (string Line in File.ReadAllLines(FileName))
                {
                    //Filter invalid Lines
                    if (Line.Length > 0 && !Line.StartsWith(";"))
                    {
                        //New Section
                        if (Line.StartsWith("[") && Line.EndsWith("]"))
                        {
                            if (p.Section != null)
                            {
                                parts.Add(p);
                                p = new INIPart();
                            }
                            p.Section = Line.Substring(1, Line.Length - 2);
                            p.Settings = new NameValueCollection();
                        }
                        else
                        {
                            //New Setting
                            if (p.Section != null && Line.Contains("="))
                            {
                                p.Settings.Add(Line.Split('=')[0].Trim(), Line.Split(new char[] { '=' }, 2)[1]);
                            }
                        }
                    }
                }
                if (p.Section != null)
                {
                    parts.Add(p);
                }
                return parts.ToArray();
            }
            return null;
        }
    }
}
