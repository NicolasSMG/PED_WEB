using System;
using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.ValidadoresTipo
{
    public class CustomDateAttribute : RangeAttribute
    {
        public CustomDateAttribute()
          : base(typeof(DateTime), DateTime.Now.ToShortDateString(), DateTime.Now.AddYears(20).ToShortDateString())
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
