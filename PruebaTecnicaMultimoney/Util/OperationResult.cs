using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaMultimoney.Util
{
    public enum OperationResult
    {
        Success = 0,
        InsufficientBalance = 96,
        UnexpectedError = 97,
        InvalidAmount = 98,
        AccountNotFound = 99,
        DestinationAccountNotFound = 95
    }
}
