using System.Collections;
using System.Collections.Generic;

namespace SpinsNew.Models
{
    public class SpbufModel
    {
        public int Id { get; set; }
        public int ReferenceCode { get; set; }
        public int MasterListId { get; set; }

        public MasterListModel MasterListModel { get; set; }
    }
}
