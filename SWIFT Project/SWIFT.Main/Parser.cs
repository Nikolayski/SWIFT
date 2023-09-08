using System.Collections.Generic;

namespace SWIFT.Main
{
    public class Parser
    {
        public Dictionary<string, string> SeparateTxtFile(string message)
        {
            Dictionary<string, string> swiftMessage = new Dictionary<string, string>();
            if (message.Contains("{1:"))
            {
                string firstBlock = StringHelper.BetweenStrings(message, "{1:", "}");
                swiftMessage.Add("BasicHeader", firstBlock);
            }

            if (message.Contains("{2:"))
            {
                string secondBlock = StringHelper.BetweenStrings(message, "{2:", "}");
                swiftMessage.Add("ApplicationHeader", secondBlock);
            }

            if (message.Contains("{3:"))
            {
                string thirdBlock = StringHelper.BetweenStrings(message, ":{", "}");
                swiftMessage.Add("UserHeader", thirdBlock);
            }

            if (message.Contains("{4:"))
            {
                string fourthBlock = StringHelper.BetweenStrings(message, "{4:", "}");
                swiftMessage.Add("TextBlock", fourthBlock);
            }

            if (message.Contains("{5:"))
            {
                string fifthBlock = StringHelper.BetweenStrings(message, "{5:", "}");
                swiftMessage.Add("TrailerBlock", fifthBlock);
            }

            return swiftMessage;

        }
    }

}
