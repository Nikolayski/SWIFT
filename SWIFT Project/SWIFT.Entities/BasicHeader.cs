namespace SWIFT.Entities
{
    public class BasicHeader
    {
        private string applicationId;
        private string serviceId;
        private string ltAddress;
        private string sessionNumber;
        private string sequenceNumber;
        private string message;

        public BasicHeader()
        {

        }
        public BasicHeader(string message)
        {
            this.message = message;
            this.applicationId = this.message.Substring(0, 1);
            this.serviceId = this.message.Substring(1, 2);
            this.ltAddress = this.message.Substring(3, 12);
            this.sessionNumber = this.message.Substring(15, 4);
            this.sequenceNumber = this.message.Substring(19, 6);
        }
        public string ApplicationId
        {
            get => this.applicationId;
            private set => this.applicationId = value;
        }

        public string ServiceId
        {
            get => this.serviceId;
            private set => this.serviceId = value;
        }

        public string LtAddress
        {
            get => this.ltAddress;
            private set => this.ltAddress = value;
        }

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
    }

}
