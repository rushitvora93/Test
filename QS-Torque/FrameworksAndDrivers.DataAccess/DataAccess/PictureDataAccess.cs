using System.Linq;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using State;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class PictureDataAccess : DataAccessBase, IPictureDataAccess
    {
        private readonly Mapper _mapper = new Mapper();

        public PictureDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext)
            : base(transactionContext, dbContext)
        {

        }

        public Picture GetQstPicture(int fileType, NodeId nodeId, long nodeseqid)
        {
            var picture = _dbContext.Pictures.SingleOrDefault(p =>
                p.FILETYPE == fileType && p.NODEID == (long)nodeId && p.NODESEQID == nodeseqid);

            return picture == null ? null : _mapper.DirectPropertyMapping(picture);
        }
    }
}
