using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SWIFT.Entities
{
    public class ApplicationHeader
    {
        private string direction;
        private string messageType;
        private string receiverAddress;
        private string senderBIC;
        private string senderLTCode;
        private string senderBranch;
        private string messagePriority;
        private string deliveryMonitoring;
        private string obsolescencePeriod;
        private string inputTime;
        private string inputMessage;
        private string outputDate;
        private string outputTime;
        private string sessionNumber;
        private string sequenceNumber;
        private string mir;

        public ApplicationHeader()
        {

        }

        public ApplicationHeader(string message)
        {
            this.direction = message.Substring(0, 1);
            this.messageType = message.Substring(1, 3);
            if (this.direction == "I")
            {
                this.receiverAddress = message.Substring(4, 12);
                if (message.Length > 16)
                {
                    this.messagePriority = message.Substring(16, 1);
                }

                if (message.Length > 17)
                {
                    this.deliveryMonitoring = message.Substring(17, 1);
                }

                if (message.Length > 18)
                {
                    this.obsolescencePeriod = message.Substring(18, 3);
                }
            }

            else
            {
                this.inputTime = message.Substring(4, 4);
                this.mir = message.Substring(8, 28);

                var regexOutputDate = new Regex(@"^\d{6}");
                var matchOutputDate = regexOutputDate.Match(this.mir);
                this.outputDate = matchOutputDate.ToString();

                var regexLt = new Regex(@"[A-Z]{4}[A-Z]{2}[0-9A-Z]{2}[0-9A-Z][0-9A-Z]{3}");
                var matchLt = regexLt.Match(this.mir);
                this.senderBIC = matchLt.ToString().Substring(0, 8);
                this.senderLTCode = matchLt.ToString().Substring(8, 1);
                this.senderBranch = matchLt.ToString().Substring(9, 3);

                this.sessionNumber = this.mir.Substring(18, 4);
                this.sequenceNumber = this.mir.Substring(this.mir.Length - 6, 6);

                this.outputDate = message.Substring(36, 6);

                this.outputTime = message.Substring(42, 4);

                this.messagePriority = message[message.Length - 1].ToString();


            }




        }
        public string Direction
        {
            get => this.direction;
            private set => this.direction = value;
        }

        public string MessageType
        {
            get => this.messageType;
            private set => this.messageType = value;
        }

        public string ReceiverAddress
        {
            get => this.receiverAddress;
            private set => this.receiverAddress = value;
        }
        public string SenderBIC
        {
            get => this.senderBIC;
            private set => this.senderBIC = value;
        }
        public string SenderLTCode
        {
            get => this.senderLTCode;
            private set => this.senderLTCode = value;
        }
        public string SenderBranch
        {
            get => this.senderBranch;
            private set => this.senderBranch = value;
        }

        public string MessagePriority
        {
            get => this.messagePriority;
            private set => this.messagePriority = value;
        }

        public string DeliveryMonitoring
        {
            get => this.deliveryMonitoring;
            private set => this.deliveryMonitoring = value;
        }

        public string ObsolescencePeriod
        {
            get => this.obsolescencePeriod;
            private set => this.obsolescencePeriod = value;
        }
        public string InputTime
        {
            get => this.inputTime;
            private set => this.inputTime = value;
        }
        public string InputMessage { get; set; }
        public string SessionNumber
        {
            get => this.sessionNumber;
            private set => this.sessionNumber = value;
        }
        public string SequenceNumber
        {
            get => this.sequenceNumber;
            private set => this.sequenceNumber = value;
        }

        public string OutputDate
        {
            get => this.outputDate;
            private set => this.outputDate = value;
        }

        public string OutputTime
        {
            get => this.outputTime;
            private set => this.outputTime = value;
        }
        public string MIR => this.mir;

    }

}
