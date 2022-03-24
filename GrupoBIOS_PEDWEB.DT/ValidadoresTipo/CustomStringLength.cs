using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.ValidadoresTipo
{
    public class CustomStringLength : StringLengthAttribute
    {
        public CustomStringLength(int maximumLength) : base(maximumLength)
        {
            InitProps();
        }

        private void InitProps()
        {
            ErrorMessageResourceName = nameof(DataAnnotation_ES.StringLength);
            ErrorMessageResourceType = typeof(DataAnnotation_ES);
        }
    }
}
