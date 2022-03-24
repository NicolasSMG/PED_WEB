using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.ValidadoresTipo
{
    public class CustomRequiredAttribute : RequiredAttribute
    {

        public CustomRequiredAttribute() : base()
        {
            InitProps();
        }

        private void InitProps()
        {
            ErrorMessageResourceName = nameof(DataAnnotation_ES.Required);
            ErrorMessageResourceType = typeof(DataAnnotation_ES);
        }
    }
}
