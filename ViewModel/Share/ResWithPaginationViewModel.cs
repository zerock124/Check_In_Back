using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Share
{
    public class ResWithPaginationViewModel : ResponseViewModel
    {
        public PaginationViewModel Pagination { get; set; }

        public DateTime MinDateTime { get; set; }

        public DateTime MaxDateTime { get; set; }

    }
}
