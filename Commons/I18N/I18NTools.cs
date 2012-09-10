using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace de.ahzf.Illias.Commons
{

    public struct German
    {

        public readonly String Text;

        public German(String myText)
        {
            Text = myText;
        }

        public override String ToString()
        {
            return Text;
        }

    }

    public struct English
    {

        public readonly String Text;

        public English(String myText)
        {
            Text = myText;
        }

        public override String ToString()
        {
            return Text;
        }

    }


    public interface JSONString
    {
        String JSONString { get; }
    }

    public struct I18N : JSONString
    {

        public readonly English English;
        public readonly German  German;

        public I18N(English myEnglish)
        {
            English = myEnglish;
            German  = new German(myEnglish.Text);
        }

        public I18N(English myEnglish, German myGerman)
        {
            English = myEnglish;
            German  = myGerman;
        }

        public override String ToString()
        {
            return String.Concat("en: ", English, ", de:", German);
        }


        public String JSONString
        {
            get
            {
                return String.Concat(@"{ ""en"": """, English, @""", ""de"": """, German, @""" }");
            }
        }

    }


    public static class I18NTools
    {

        public static String ToHTML(this I18N I18NValue)
        {

            return "<span class=\"english\">" + I18NValue.English.Text + "</span>" +
                   "<span class=\"german\">"  + I18NValue.German. Text + "</span>";

        }

        public static String ToLink(this I18N I18NValue, String Link)
        {

            return "<span class=\"english\"><a href = \"" + Link + "?language=en\">" + I18NValue.English.Text + "</a></span>" +
                   "<span class=\"german\" ><a href = \"" + Link + "?language=de\">" + I18NValue.German.Text  + "</a></span>";

        }

        public static String ToHTML(this I18N I18NValue, String Prefix, String Postfix)
        {

            return "<span class=\"english\">" + Prefix + I18NValue.English.Text + Postfix + "</span>" +
                   "<span class=\"german\">"  + Prefix + I18NValue.German. Text + Postfix + "</span>";

        }

        #region AppendI18N(this StringBuilder, I18NValue)

        public static StringBuilder AppendI18N(this StringBuilder StringBuilder,
                                               I18N I18NValue)
        {
            return StringBuilder.Append(I18NValue.ToHTML());
        }

        #endregion

        #region AppendI18N(this StringBuilder, Prefix, I18NValue, Postfix)

        public static StringBuilder AppendI18N(this StringBuilder StringBuilder,
                                               String Prefix,
                                               I18N   I18NValue,
                                               String Postfix)
        {

            return StringBuilder.Append(Prefix).
                                 Append(I18NValue.ToHTML()).
                                 Append(Postfix);

        }

        #endregion

    }

}
