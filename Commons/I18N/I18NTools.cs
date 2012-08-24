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

    }

    public struct English
    {

        public readonly String Text;

        public English(String myText)
        {
            Text = myText;
        }

    }

    public struct I18N
    {

        public readonly English English;
        public readonly German German;

        public I18N(English myEnglish)
        {
            English = myEnglish;
            German = new German(myEnglish.Text);
        }

        public I18N(English myEnglish, German myGerman)
        {
            English = myEnglish;
            German = myGerman;
        }

    }


    public static class I18NTools
    {

        public static String ToHTML(this I18N I18NValue)
        {

            return "<span class=\"english\">" + I18NValue.English.Text + "</span>" +
                   "<span class=\"german\">"  + I18NValue.German. Text + "</span>";

        }

        public static String ToHTML(this I18N I18NValue, String Prefix, String Postfix)
        {

            return "<span class=\"english\">" + Prefix + I18NValue.English.Text + Postfix + "</span>" +
                   "<span class=\"german\">"  + Prefix + I18NValue.German. Text + Postfix + "</span>";

        }

    }

}
