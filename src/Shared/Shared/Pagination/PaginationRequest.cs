using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Pagination
{
    public record PaginationRequest(int PageNumber = 0, int PageSize = 10)
    {
    }
}
