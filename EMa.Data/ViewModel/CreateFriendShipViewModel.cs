using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMa.Data.ViewModel
{
    public class CreateFriendShipViewModel
    {
        public Guid SenderId { get; set; }
        public Guid ReceipId { get; set; }
        public bool Confirmed { get; set; }
    }
}
