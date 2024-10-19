﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;

namespace RXServer
{
    namespace Web
    {
        namespace Parse
        {
            public class Replace
            {
                static string CLASSNAME = "[Namespace::RXServer::Web::Parse][Class::Replace]";
                public static String Href(String Orginal, String Replacement, String[] CheckString)
                {
                    string FUNCTIONNAME = CLASSNAME + "[Function::Href]";
                    try
                    {
                        RXServer.Web.Parse.ParseHTML parse = new RXServer.Web.Parse.ParseHTML();
                        parse.Source = Orginal;
                        while (!parse.Eof())
                        {
                            char ch = parse.Parse();
                            if (ch == 0)
                            {
                                // denna första kollar om de värden man skickar med finns i strängen
                                // finns de så gör den inget.
                                // om de där emot saknas så byggs urlen i href om.
                                RXServer.Web.Parse.AttributeList tag = parse.GetTag();
                                // Default.aspx?PagId=
                                // /Upload/
                                Boolean UnvalidCheckString = false;
                                if (tag["href"] != null && CheckString != null)
                                {
                                    foreach (String CS in CheckString)
                                        if (tag["href"].Value.ToLower().Contains(CS.ToLower()))
                                            UnvalidCheckString = true;
                                    System.Diagnostics.Debug.WriteLine("UnvalidCheckString Found: " + UnvalidCheckString.ToString());
                                }
                                if (tag["href"] != null && UnvalidCheckString.Equals(false))
                                {
                                    if (tag["href"].Value.Contains("http://"))
                                    {
                                        System.Diagnostics.Debug.WriteLine("Orginal will be replaced");
                                        Orginal = Orginal.Replace("\"" + tag["href"].Value + "\"", "\"" + Replacement + HttpContext.Current.Server.UrlEncode(tag["href"].Value.Replace("http://", "")) + "\"");
                                        tag["href"].Value = Replacement + tag["href"].Value.Replace("http://", "");
                                    }
                                }
                                // Här checkas det om urlen i href börjar med default.aspx
                                // gör den det så händer inget.
                                // om den inte gör det så plockas allt innan bort
                                // och urlen i href skrivs om...
                                Boolean UnvalidBeginning = false;
                                if (tag["href"] != null)
                                {
                                    if (!tag["href"].Value.ToLower().StartsWith("default.aspx") && !tag["href"].Value.ToLower().StartsWith("../default.aspx"))
                                    {
                                        Int32 DIndex = tag["href"].Value.ToLower().IndexOf("default.aspx");
                                        if (DIndex > 0)
                                        {
                                            String Data = tag["href"].Value;
                                            Orginal = Orginal.Replace(Data, Data.Substring(DIndex));
                                        }
                                    }
                                    System.Diagnostics.Debug.WriteLine("UnvalidBeginning Found: " + UnvalidBeginning.ToString());
                                }
                            }
                        }
                        return Orginal;
                    }
                    catch (Exception ex)
                    {
                        //Error.Report(ex, FUNCTIONNAME, String.Empty);
                        return Orginal;
                    }
                }
                public static String HrefBack(String Orginal, String CheckString)
                {
                    string FUNCTIONNAME = CLASSNAME + "[Function::HrefBack]";
                    try
                    {
                        RXServer.Web.Parse.ParseHTML parse = new RXServer.Web.Parse.ParseHTML();
                        parse.Source = Orginal;
                        while (!parse.Eof())
                        {
                            char ch = parse.Parse();
                            if (ch == 0)
                            {
                                RXServer.Web.Parse.AttributeList tag = parse.GetTag();
                                if (tag["href"] != null)
                                {
                                    if (tag["href"].Value.ToLower().Contains(CheckString.ToLower()))
                                    {
                                        System.Console.WriteLine("CheckString Found: " + CheckString);
                                        Orginal = Orginal.Replace(tag["href"].Value, "http://" + tag["href"].Value.Substring(tag["href"].Value.IndexOf(CheckString) + CheckString.Length));
                                    }
                                }
                            }
                        }
                        return Orginal;
                    }
                    catch (Exception ex)
                    {
                        //Error.Report(ex, FUNCTIONNAME, String.Empty);
                        return String.Empty;
                    }
                }
                public static String FixLinksFromDocumentManager(String Orginal)
                {
                    string FUNCTIONNAME = CLASSNAME + "[Function::FixLinksFromDocumentManager]";
                    try
                    {
                        RXServer.Web.Parse.ParseHTML parse = new RXServer.Web.Parse.ParseHTML();
                        parse.Source = Orginal;
                        bool href_found = false;
                        bool target_found = false;
                        Int32 ii = 0;
                        while (!parse.Eof())
                        {
                            char ch = parse.Parse();
                            if (ch == 0)
                            {
                                RXServer.Web.Parse.AttributeList tag = parse.GetTag();
                                if (tag["href"] != null)
                                    href_found = true;
                                if (tag["target"] != null)
                                    target_found = true;
                                if (href_found && !target_found)
                                {
                                    Orginal = Orginal.Replace("<a href=\"" + tag["href"].Value + "\">", "<a href=\"" + tag["href"].Value + "\" target='_blank'>");
                                    href_found = false;
                                    target_found = false;
                                }
                            }
                        }
                        return Orginal;
                    }
                    catch (Exception ex)
                    {
                        //Error.Report(ex, FUNCTIONNAME, String.Empty);
                        return Orginal;
                    }
                }
            }

            /// <summary>
            /// Attribute holds one attribute, as is normally stored in an
            /// HTML or XML file. This includes a name, value and delimiter.
            /// This source code may be used freely under the
            /// Limited GNU Public License(LGPL).
            ///
            /// Written by Jeff Heaton (http://www.jeffheaton.com)
            /// </summary>
            public class Attribute : ICloneable
            {
                /// <summary>
                /// The name of this attribute
                /// </summary>
                private string m_name;

                /// <summary>
                /// The value of this attribute
                /// </summary>
                private string m_value;

                /// <summary>
                /// The delimiter for the value of this attribute(i.e. " or ').
                /// </summary>
                private char m_delim;

                /// <summary>
                /// Construct a new Attribute.  The name, delim, and value
                /// properties can be specified here.
                /// </summary>
                /// <param name="name">The name of this attribute.</param>
                /// <param name="value">The value of this attribute.</param>
                /// <param name="delim">The delimiter character for the value.
                /// </param>
                public Attribute(string name, string value, char delim)
                {
                    m_name = name;
                    m_value = value;
                    m_delim = delim;
                }


                /// <summary>
                /// The default constructor.  Construct a blank attribute.
                /// </summary>
                public Attribute()
                    : this("", "", (char)0)
                {
                }

                /// <summary>
                /// Construct an attribute without a delimiter.
                /// </summary>
                /// <param name="name">The name of this attribute.</param>
                /// <param name="value">The value of this attribute.</param>
                public Attribute(String name, String value)
                    : this(name, value,
                                                               (char)0)
                {
                }

                /// <summary>
                /// The delimiter for this attribute.
                /// </summary>
                public char Delim
                {
                    get
                    {
                        return m_delim;
                    }

                    set
                    {
                        m_delim = value;
                    }
                }

                /// <summary>
                /// The name for this attribute.
                /// </summary>
                public string Name
                {
                    get
                    {
                        return m_name;
                    }

                    set
                    {
                        m_name = value;
                    }
                }

                /// <summary>
                /// The value for this attribute.
                /// </summary>
                public string Value
                {
                    get
                    {
                        return m_value;
                    }

                    set
                    {
                        m_value = value;
                    }
                }

                #region ICloneable Members
                public virtual object Clone()
                {
                    return new Attribute(m_name, m_value, m_delim);
                }
                #endregion
            }

            /// <summary>
            /// The AttributeList class is used to store list of
            /// Attribute classes.
            /// This source code may be used freely under the
            /// Limited GNU Public License(LGPL).
            ///
            /// Written by Jeff Heaton (http://www.jeffheaton.com)
            /// </summary>
            ///
            public class AttributeList : Attribute
            {
                /// <summary>
                /// An internally used Vector.  This vector contains
                /// the entire list of attributes.
                /// </summary>
                protected ArrayList m_list;
                /// <summary>
                /// Make an exact copy of this object using the cloneable
                /// interface.
                /// </summary>
                /// <returns>A new object that is a clone of the specified
                /// object.</returns>
                public override object Clone()
                {
                    AttributeList rtn = new AttributeList();

                    for (int i = 0; i < m_list.Count; i++)
                        rtn.Add((Attribute)this[i].Clone());

                    return rtn;
                }

                /// <summary>
                /// Create a new, empty, attribute list.
                /// </summary>
                public AttributeList()
                    : base("", "")
                {
                    m_list = new ArrayList();
                }


                /// <summary>
                /// Add the specified attribute to the list of attributes.
                /// </summary>
                /// <param name="a">An attribute to add to this
                /// AttributeList.</paramv
                public void Add(Attribute a)
                {
                    m_list.Add(a);
                }


                /// <summary>
                /// Clear all attributes from this AttributeList and return
                /// it to a empty state.
                /// </summary>
                public void Clear()
                {
                    m_list.Clear();
                }

                /// <summary>
                /// Returns true of this AttributeList is empty, with no
                /// attributes.
                /// </summary>
                /// <returns>True if this AttributeList is empty, false
                /// otherwise.</returns>
                public bool IsEmpty()
                {
                    return (m_list.Count <= 0);
                }

                /// <summary>
                /// If there is already an attribute with the specified name,
                /// it will have its value changed to match the specified
                /// value. If there is no Attribute with the specified name,
                /// one will be created. This method is case-insensitive.
                /// </summary>
                /// <param name="name">The name of the Attribute to edit or
                /// create.  Case-insensitive.</param>
                /// <param name="value">The value to be held in this
                /// attribute.</param>
                public void Set(string name, string value)
                {
                    if (name == null)
                        return;
                    if (value == null)
                        value = "";

                    Attribute a = this[name];

                    if (a == null)
                    {
                        a = new Attribute(name, value);
                        Add(a);
                    }

                    else
                        a.Value = value;
                }

                /// <summary>
                /// How many attributes are in this AttributeList?
                /// </summary>
                public int Count
                {
                    get
                    {
                        return m_list.Count;
                    }
                }

                /// <summary>
                /// A list of the attributes in this AttributeList
                /// </summary>
                public ArrayList List
                {
                    get
                    {
                        return m_list;
                    }
                }

                /// <summary>
                /// Access the individual attributes
                /// </summary>
                public Attribute this[int index]
                {
                    get
                    {
                        if (index < m_list.Count)
                            return (Attribute)m_list[index];
                        else
                            return null;
                    }
                }

                /// <summary>
                /// Access the individual attributes by name.
                /// </summary>
                public Attribute this[string index]
                {
                    get
                    {
                        int i = 0;

                        while (this[i] != null)
                        {
                            if (this[i].Name.ToLower().Equals((index.ToLower())))
                                return this[i];
                            i++;
                        }

                        return null;

                    }
                }
            }

            /// <summary>
            /// Base class for parsing tag based files, such as HTML,
            /// HTTP headers, or XML.
            ///
            /// This source code may be used freely under the
            /// Limited GNU Public License(LGPL).
            ///
            /// Written by Jeff Heaton (http://www.jeffheaton.com)
            /// </summary>
            public class Parse : AttributeList
            {
                /// <summary>
                /// The source text that is being parsed.
                /// </summary>
                private string m_source;

                /// <summary>
                /// The current position inside of the text that
                /// is being parsed.
                /// </summary>
                private int m_idx;

                /// <summary>
                /// The most recently parsed attribute delimiter.
                /// </summary>
                private char m_parseDelim;

                /// <summary>
                /// This most recently parsed attribute name.
                /// </summary>
                private string m_parseName;

                /// <summary>
                /// The most recently parsed attribute value.
                /// </summary>
                private string m_parseValue;

                /// <summary>
                /// The most recently parsed tag.
                /// </summary>
                public string m_tag;

                /// <summary>
                /// Determine if the specified character is whitespace or not.
                /// </summary>
                /// <param name="ch">A character to check</param>
                /// <returns>true if the character is whitespace</returns>
                public static bool IsWhiteSpace(char ch)
                {
                    return ("\t\n\r ".IndexOf(ch) != -1);
                }


                /// <summary>
                /// Advance the index until past any whitespace.
                /// </summary>
                public void EatWhiteSpace()
                {
                    while (!Eof())
                    {
                        if (!IsWhiteSpace(GetCurrentChar()))
                            return;
                        m_idx++;
                    }
                }

                /// <summary>
                /// Determine if the end of the source text has been reached.
                /// </summary>
                /// <returns>True if the end of the source text has been
                /// reached.</returns>
                public bool Eof()
                {
                    return (m_idx >= m_source.Length);
                }

                /// <summary>
                /// Parse the attribute name.
                /// </summary>
                public void ParseAttributeName()
                {
                    EatWhiteSpace();
                    // get attribute name
                    while (!Eof())
                    {
                        if (IsWhiteSpace(GetCurrentChar()) ||
                          (GetCurrentChar() == '=') ||
                          (GetCurrentChar() == '>'))
                            break;
                        m_parseName += GetCurrentChar();
                        m_idx++;
                    }

                    EatWhiteSpace();
                }


                /// <summary>
                /// Parse the attribute value
                /// </summary>
                public void ParseAttributeValue()
                {
                    if (m_parseDelim != 0)
                        return;

                    if (GetCurrentChar() == '=')
                    {
                        m_idx++;
                        EatWhiteSpace();
                        if ((GetCurrentChar() == '\'') ||
                          (GetCurrentChar() == '\"'))
                        {
                            m_parseDelim = GetCurrentChar();
                            m_idx++;
                            while (GetCurrentChar() != m_parseDelim)
                            {
                                m_parseValue += GetCurrentChar();
                                m_idx++;
                            }
                            m_idx++;
                        }
                        else
                        {
                            while (!Eof() &&
                              !IsWhiteSpace(GetCurrentChar()) &&
                              (GetCurrentChar() != '>'))
                            {
                                m_parseValue += GetCurrentChar();
                                m_idx++;
                            }
                        }
                        EatWhiteSpace();
                    }
                }

                /// <summary>
                /// Add a parsed attribute to the collection.
                /// </summary>
                public void AddAttribute()
                {
                    Attribute a = new Attribute(m_parseName,
                      m_parseValue, m_parseDelim);
                    Add(a);
                }


                /// <summary>
                /// Get the current character that is being parsed.
                /// </summary>
                /// <returns></returns>
                public char GetCurrentChar()
                {

                    return GetCurrentChar(0);

                }



                /// <summary>
                /// Get a few characters ahead of the current character.
                /// </summary>
                /// <param name="peek">How many characters to peek ahead
                /// for.</param>
                /// <returns>The character that was retrieved.</returns>
                public char GetCurrentChar(int peek)
                {
                    if ((m_idx + peek) < m_source.Length)
                        return m_source[m_idx + peek];
                    else
                        return (char)0;
                }



                /// <summary>
                /// Obtain the next character and advance the index by one.
                /// </summary>
                /// <returns>The next character</returns>
                public char AdvanceCurrentChar()
                {
                    return m_source[m_idx++];
                }



                /// <summary>
                /// Move the index forward by one.
                /// </summary>
                public void Advance()
                {
                    m_idx++;
                }


                /// <summary>
                /// The last attribute name that was encountered.
                /// <summary>
                public string ParseName
                {
                    get
                    {
                        return m_parseName;
                    }

                    set
                    {
                        m_parseName = value;
                    }
                }

                /// <summary>
                /// The last attribute value that was encountered.
                /// <summary>
                public string ParseValue
                {
                    get
                    {
                        return m_parseValue;
                    }

                    set
                    {
                        m_parseValue = value;
                    }
                }

                /// <summary>
                /// The last attribute delimeter that was encountered.
                /// <summary>
                public char ParseDelim
                {
                    get
                    {
                        return m_parseDelim;
                    }

                    set
                    {
                        m_parseDelim = value;
                    }
                }

                /// <summary>
                /// The text that is to be parsed.
                /// <summary>
                public string Source
                {
                    get
                    {
                        return m_source;
                    }

                    set
                    {
                        m_source = value;
                    }
                }
            }
            /// <summary>
            /// Summary description for ParseHTML.
            /// </summary>

            public class ParseHTML : Parse
            {
                public AttributeList GetTag()
                {
                    AttributeList tag = new AttributeList();
                    tag.Name = m_tag;

                    foreach (Attribute x in List)
                    {
                        tag.Add((Attribute)x.Clone());
                    }

                    return tag;
                }

                public String BuildTag()
                {
                    String buffer = "<";
                    buffer += m_tag;
                    int i = 0;
                    while (this[i] != null)
                    {// has attributes
                        buffer += " ";
                        if (this[i].Value == null)
                        {
                            if (this[i].Delim != 0)
                                buffer += this[i].Delim;
                            buffer += this[i].Name;
                            if (this[i].Delim != 0)
                                buffer += this[i].Delim;
                        }
                        else
                        {
                            buffer += this[i].Name;
                            if (this[i].Value != null)
                            {
                                buffer += "=";
                                if (this[i].Delim != 0)
                                    buffer += this[i].Delim;
                                buffer += this[i].Value;
                                if (this[i].Delim != 0)
                                    buffer += this[i].Delim;
                            }
                        }
                        i++;
                    }
                    buffer += ">";
                    return buffer;
                }

                protected void ParseTag()
                {
                    m_tag = "";
                    Clear();

                    // Is it a comment?
                    if ((GetCurrentChar() == '!') &&
                      (GetCurrentChar(1) == '-') &&
                      (GetCurrentChar(2) == '-'))
                    {
                        while (!Eof())
                        {
                            if ((GetCurrentChar() == '-') &&
                              (GetCurrentChar(1) == '-') &&
                              (GetCurrentChar(2) == '>'))
                                break;
                            if (GetCurrentChar() != '\r')
                                m_tag += GetCurrentChar();
                            Advance();
                        }
                        m_tag += "--";
                        Advance();
                        Advance();
                        Advance();
                        ParseDelim = (char)0;
                        return;
                    }

                    // Find the tag name
                    while (!Eof())
                    {
                        if (IsWhiteSpace(GetCurrentChar()) ||
                                         (GetCurrentChar() == '>'))
                            break;
                        m_tag += GetCurrentChar();
                        Advance();
                    }

                    EatWhiteSpace();

                    // Get the attributes
                    while (GetCurrentChar() != '>')
                    {
                        ParseName = "";
                        ParseValue = "";
                        ParseDelim = (char)0;

                        ParseAttributeName();

                        if (GetCurrentChar() == '>')
                        {
                            AddAttribute();
                            break;
                        }

                        // Get the value(if any)
                        ParseAttributeValue();
                        AddAttribute();
                    }
                    Advance();
                }


                public char Parse()
                {
                    if (GetCurrentChar() == '<')
                    {
                        Advance();

                        char ch = char.ToUpper(GetCurrentChar());
                        if ((ch >= 'A') && (ch <= 'Z') || (ch == '!') || (ch == '/'))
                        {
                            ParseTag();
                            return (char)0;
                        }

                        else return (AdvanceCurrentChar());
                    }
                    else return (AdvanceCurrentChar());
                }
            }
        }
    }
}
