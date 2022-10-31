﻿using System.Text;
using UserManager.Server.EntityFramework;
using UserManager.Server.Model;

namespace UserManager.Server.Service;

public class OperationLogService
{
    private UserDbContext UserDbContext { get; }

    public OperationLogService(UserDbContext userDbContext)
    {
        UserDbContext = userDbContext;
    }

    public async Task<bool> Save(params OperationLog[] operationLogs)
    {
        try
        {
            await UserDbContext.OperationLogs.AddRangeAsync(operationLogs);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
}