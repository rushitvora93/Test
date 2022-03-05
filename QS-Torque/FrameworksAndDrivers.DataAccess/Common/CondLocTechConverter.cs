using System;
using Common.Types.Enums;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Server.Core.Entities;

namespace FrameworksAndDrivers.DataAccess.Common
{
    public class CondLocTechConverter
    {
        public static CondLocTech ConvertEntityToCondLocTech(ProcessControlTech processControlTech)
        {
            if (processControlTech == null)
            {
                return null;
            }

            var mapper = new Mapper();
            switch (processControlTech)
            {
                case QstProcessControlTech qstProcessControlTech:
                    return mapper.DirectPropertyMapping(qstProcessControlTech);

                default: throw new NotImplementedException("CondLocTech type ( " + processControlTech.GetType() + " ) not implemented yet!");
            }
        }

        public static ProcessControlTech ConvertCondLocTechToEntity(CondLocTech condLocTech)
        {
            if (condLocTech == null)
            {
                return null;
            }

            var mapper = new Mapper();
            switch (condLocTech.HERSTELLERID)
            {
                case ManufacturerIds.ID_QST:
                    var qstProcessControlTech = new QstProcessControlTech();
                    mapper.DirectPropertyMapping(condLocTech, qstProcessControlTech);
                    return qstProcessControlTech;

                default: throw new NotImplementedException("CondLocTech type ( " + condLocTech.HERSTELLERID + " ) not implemented yet!");
            }
        }

    }
}
