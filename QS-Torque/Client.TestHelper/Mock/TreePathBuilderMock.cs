using System.Collections.Generic;
using Core.Entities;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class TreePathBuilderMock : ITreePathBuilder
    {
        public readonly List<Location> GetTreePathLocationParameters = new List<Location>();
        public List<string> GetTreePathLocationReturnValues;
        public string GetTreePathSeperatorParameter;
        private int _getTreePathCallCount;
        private int _getMaskedTreePathWithBase64CallCount;
        public readonly List<Location> GetMaskedTreePathWithBase64Parameters = new List<Location>();
        public List<string> GetMaskedTreePathWithBase64ReturnValue;
        public bool GetDeMaskedTreePathFromBase64Called;

        public string GetTreePath(Location location, string seperator)
        {
            GetTreePathLocationParameters.Add(location);
            GetTreePathSeperatorParameter = seperator;
            if (GetTreePathLocationReturnValues == null)
            {
                return "";
            }
            return GetTreePathLocationReturnValues[_getTreePathCallCount++];
        }

        public string GetMaskedTreePathWithBase64(Location location)
        {
            GetMaskedTreePathWithBase64Parameters.Add(location);
            if (GetMaskedTreePathWithBase64ReturnValue == null)
            {
                return "";
            }
            return GetMaskedTreePathWithBase64ReturnValue[_getMaskedTreePathWithBase64CallCount++]; 
        }

        public string GetDeMaskedTreePathFromBase64(string path, string seperator)
        {
            GetDeMaskedTreePathFromBase64Called = true;
            return "";
        }
    }
}
