﻿using MediatR;

namespace FinanceApp.Application.Accounts.Queries.GetAllAccounts;

public class GetAllAccountsQuery : IRequest<List<AccountDto>>
{
}