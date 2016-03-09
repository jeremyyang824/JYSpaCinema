using System;
using System.Transactions;

namespace JYSpaCinema.Service.Uow
{
    public class UnitOfWorkOptions
    {
        public bool IsTransactional { get; set; }

        public TransactionScopeOption Scope { get; set; }

        public TimeSpan Timeout { get; set; }

        public IsolationLevel IsolationLevel { get; set; }

        public UnitOfWorkOptions()
        {
            this.IsTransactional = true;
            this.Scope = TransactionScopeOption.Required;
            this.Timeout = new TimeSpan(0, 1, 0);
            this.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        }
    }
}
