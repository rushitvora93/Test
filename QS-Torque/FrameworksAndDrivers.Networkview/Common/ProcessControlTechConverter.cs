using System;
using FrameworksAndDrivers.NetworkView.T4Mapper;
using Server.Core.Entities;


namespace FrameworksAndDrivers.NetworkView.Common
{
    public class ProcessControlTechConverter
    {
        public static DtoTypes.ProcessControlTech ConvertEntityToDto(ProcessControlTech processControlTech)
        {
            if (processControlTech == null)
            {
                return null;
            }

            var mapper = new Mapper();
            switch (processControlTech)
            {
                case QstProcessControlTech qstProcessControlTech:
                    var processControlTechQstDto = new DtoTypes.ProcessControlTech();
                    processControlTechQstDto.QstProcessControlTech = mapper.DirectPropertyMapping(qstProcessControlTech);
                    return processControlTechQstDto;

                default: throw new NotImplementedException("CondLocTech type ( " + processControlTech.GetType() + " ) not implemented yet!");
            }
        }

        public static ProcessControlTech ConvertDtoToEntity(DtoTypes.ProcessControlTech condLocTech)
        {
            if (condLocTech == null)
            {
                return null;
            }

            var mapper = new Mapper();
            switch (condLocTech.ProcessControlTechOneOfCase)
            {
                case DtoTypes.ProcessControlTech.ProcessControlTechOneOfOneofCase.QstProcessControlTech:
                    var qstProcessControlTech = new QstProcessControlTech();
                    mapper.DirectPropertyMapping(condLocTech.QstProcessControlTech, qstProcessControlTech);
                    return qstProcessControlTech;

                default: throw new NotImplementedException("CondLocTech type ( " + DtoTypes.ProcessControlTech.ProcessControlTechOneOfOneofCase.QstProcessControlTech + " ) not implemented yet!");
            }
        }
    }
}
