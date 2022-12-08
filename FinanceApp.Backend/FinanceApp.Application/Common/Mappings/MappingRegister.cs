using FinanceApp.Application.Accounts.Queries.GetAllAccounts;
using FinanceApp.Application.Categories.Queries.GetAllCategories;
using FinanceApp.Domain.Entities;
using Mapster;

namespace FinanceApp.Application.Common.Mappings;

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Account, AccountDto>()
            .Map(dest => dest.TransactionsCount,
                src => src.Transactions.Count);
        
        config
            .NewConfig<Category, CategoryDto>()
            .Map(dest => dest.TransactionsCount,
                src => src.Transactions.Count);
    }
}