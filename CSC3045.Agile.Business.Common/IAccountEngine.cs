using Core.Common.Contracts;

namespace CSC3045.Agile.Business.Common
{
    public interface IAccountEngine : IBusinessEngine
    {
        bool IsAccountAlreadyCreated(string loginEmail);
    }
}