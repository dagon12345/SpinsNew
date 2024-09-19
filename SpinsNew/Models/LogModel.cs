using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Models
{
    public class LogModel
    {
        public int Id { get; set; }
        public int MasterListId { get; set; }
        public string Log { get; set; }
        public string User { get; set; }
        public DateTime DateTimeEntry { get; set; }

        //Has only one masterlist
        public MasterListModel masterListModel { get; set; }
    }
}
