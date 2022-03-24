using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.ValidadoresTipo
{
    internal class SelectRequiredAttribute : RangeAttribute
    {

        public SelectRequiredAttribute() : base(1, int.MaxValue)
        {
            InitProps();
        }

        private void InitProps()
        {
            ErrorMessageResourceName = nameof(DataAnnotation_ES.SelectRequired);
            ErrorMessageResourceType = typeof(DataAnnotation_ES);
        }
    }
}
