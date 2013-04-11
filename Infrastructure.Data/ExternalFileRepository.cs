using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Domain.ExternalFiles;
using Dapper;

namespace Infrastructure.Data
{
    public class ExternalFileRepository : dapperBaseRepository, IExternalFileRepository
    {

        public int AddFile(ExternalFile theFile)
        {
            var para = new DynamicParameters();
            para.Add("@fileContent", theFile.FileContent);
            para.Add("@mimeType", theFile.MimeType);
            para.Add("@articleId", theFile.ArticleId);
            para.Add("@fileName", theFile.FileName);
            para.Add("@newId", dbType: DbType.Int32, direction:ParameterDirection.Output);


            string sql = @"INSERT INTO ExternalFiles(FileContent, MimeType, ArticleId, FileName) VALUES(@fileContent, @mimeType, @articleId, @fileName) SET @newId = CAST(scope_identity() as int);";
            using (var conn = base.GetConnection(true))
            {
                conn.Execute(sql, para);

                return para.Get<int>("@newId");
            }
        }

        public ExternalFile GetImage(int imageId)
        {
            using (var conn = base.GetConnection(true))
            {
                var image = conn.Query<ExternalFile>("SELECT * FROM EXTERNALFILES WHERE Id=@id", new {id=imageId }).First();
                return image;
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
