namespace SpinsNew.Models
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public string AttachmentName { get; set; }
        public int MasterListId { get; set; }

        public MasterListModel MasterListModel { get; set; }
    }
}
