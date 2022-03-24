

using System;
using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.ValidadoresTipo
{
    public class CustomRangeAttribute : RangeAttribute
    {

        public CustomRangeAttribute(int min, int max) : base(min, max)
        {
            InitProps();
        }

        public CustomRangeAttribute(double min, double max) : base(min, max)
        {
            InitProps();
        }

        public CustomRangeAttribute(Type type, string min, string max) : base(type, min, max)
        {
            InitProps();
        }

        private void InitProps()
        {
            ErrorMessageResourceName = nameof(DataAnnotation_ES.Range);
            ErrorMessageResourceType = typeof(DataAnnotation_ES);
        }
    }
}
