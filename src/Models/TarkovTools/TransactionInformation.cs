using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools
{
    public class TransactionInformation
    {
        #region Constructors

        public TransactionInformation()
        {
            Requirements = new List<Requirement>();
        }

        #endregion

        #region Properties

        public long Price { get; set; }

        public string Source { get; set; }

        public string Currency { get; set; }

        public List<Requirement> Requirements { get; set; }

        #endregion
    }
}