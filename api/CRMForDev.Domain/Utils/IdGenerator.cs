using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMForDev.Domain.Utils;

public static class IdGenerator
{
    public static string GenerateNewId() => Guid.NewGuid().ToString();

    public static string NewObjectId() => Guid.Empty.ToString();
}
