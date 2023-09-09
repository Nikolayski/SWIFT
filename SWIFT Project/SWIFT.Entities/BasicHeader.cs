using SWIFT.Main;

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


        public BasicHeader(string message)
        {
            this.message = message;
            this.applicationId = this.message.Substring(0, 1);
            this.serviceId = this.message.Substring(this.applicationId.Length, 2);

            var ltAddressStartIndex = StringHelper.SumLengths(this.applicationId, this.serviceId);
            this.ltAddress = this.message.Substring(ltAddressStartIndex, 12);

            var sessionNumberStartIndex = StringHelper.SumLengths(this.applicationId, this.serviceId, this.ltAddress);
            this.sessionNumber = this.message.Substring(sessionNumberStartIndex, 4);

            var sequenceNumberStartIndex = StringHelper.SumLengths(this.applicationId, this.serviceId, this.ltAddress, this.sessionNumber);
            this.sequenceNumber = this.message.Substring(sequenceNumberStartIndex, 6);
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
