using System.Collections.Generic;
using System.Linq;

namespace SWIFT.Entities
{
    public class UserHeader
    {
        private bool isMT103triggered = false;
        private string bankingPriorityCode;
        private string mur;
        private List<string> userHeadersTags = new List<string> {"103", "106", "108", "111", "113", "115", "119", "121", "165" };

        public UserHeader(string message)
        {
            if(message.Contains(" ")) //means that there are 2 blocks
            {
                string firstBlock = message.Split(" ").First();
                isMT103triggered = CheckIs103Triggered(firstBlock);
                CheckBankingPriorityCode(firstBlock);
                string secondBlock = message.Split(" ").Last();
                CheckMur(secondBlock);
            }
            else
            {
                var userTag = message.Split(":").First();
                isMT103triggered = CheckIs103Triggered(userTag);
                CheckBankingPriorityCode(userTag);
                CheckMur(userTag);
            }
        }

        private void CheckMur(string block)
        {
            if(this.userHeadersTags.Contains(block.Split(":")[0]) && block.Split(":")[0] == "108")
            {
                this.mur = block.Split(":")[1];
            }
               
        }

        private void CheckBankingPriorityCode(string block)
        {
            if(this.userHeadersTags.Contains(block.Split(":")[0]) && block.Split(":")[0] == "103")
            {
                this.bankingPriorityCode = block.Split(":").Last();
            }
        }

        public string BankingPriorityCode => this.bankingPriorityCode;
        public string Mur => this.mur;
        public bool IsM103Triggered => this.isMT103triggered;

        private bool CheckIs103Triggered(string block)
        {
            var IsExist = this.userHeadersTags.Contains(block.Split(":")[0]) && block.Split(":")[0] == "119" && block.Split(":")[1] == "STP";
            if(IsExist)
            {
                this.bankingPriorityCode = block.Split(":")[1];
            }

            return IsExist;
        }


    }
}
