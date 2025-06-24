using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoicy.Application.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}
