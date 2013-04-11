using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ExternalFiles
{
    public class ExternalFile
    {
        public int Id { get; set; }
        public byte[] FileContent { get; set; }
        public string MimeType { get; set; }
        public long ArticleId { get; set; }
        public string FileName { get; set; }
    }
}
