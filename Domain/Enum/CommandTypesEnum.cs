using System.ComponentModel;

namespace Domain.Enum
{
    public enum CommandTypesEnum
    {
        [Description("/stock=")]
        GetStockValue = 1
    }
}