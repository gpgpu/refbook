using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ExternalFiles
{
    public interface IExternalFileRepository : IRepository
    {
        int AddFile(ExternalFile theFile);
        ExternalFile GetImage(int imageId);
    }
}
